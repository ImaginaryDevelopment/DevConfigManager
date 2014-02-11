using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Windows.Forms;

using DeveloperConfigurationManager.Controls;
using DeveloperConfigurationManager.CrossCutting;
using DeveloperConfigurationManager.Properties;

using Domain.Adapters;
using Domain.Extensions;
using Domain.Helpers;
using Domain.Models;

using Ninject;

namespace DeveloperConfigurationManager
{
    public static class NinjectConfig
	{
		public static void Run(bool isAdmin, Action<Form> runAction)
		{
			Start();
			var frm = Kernel.Get<Form1>();
			if (isAdmin) frm.Text = frm.Text + " - Admin";
			runAction(frm);
		}

		public static void Start()
		{
			if (Kernel != null) return ;
			Kernel = new StandardKernel();
			RegisterServices();
		}

		static IKernel Kernel { get;  set; }

		static void RegisterSettingsServices()
		{
			const string sharedSecret = "atlassian";

			var encryption = new Encryption(sharedSecret);
			
			BindSetting(s => s.AtlassianUserName);
			BindSetting(s => s.CrucibleAuthority);
			BindSetting(s => s.GitPath);
			BindSetting(s => s.JunctionTarget);
			BindSetting(s => s.StashAuthority);
            BindSetting(s=>s.JunctionRelativeGitPath);

			Kernel.BindSettingByConvention(Settings.Default, s => s.AtlassianPassword, encryption.Decrypt, encryption.Encrypt);
			Kernel.BindSettingByConvention(Settings.Default, s => s.ConfigXmlUris);
			Kernel.BindSettingByConvention(Settings.Default, c => c.Junctions);
			Kernel.BindSettingByConvention(Settings.Default, s => s.Servers);
			Kernel.BindSettingByConvention(Settings.Default, s => s.Links);
            
		}

		static void BindSetting(Expression<Func<Settings, string>> selector)
		{
			Kernel.BindSettingByConvention(Settings.Default, selector);
		}

		static void RegisterServices()
		{
			RegisterDynamicAssemblies();
			RegisterCrossCuttings();
			RegisterSettingsServices();
			
			Kernel.Bind<string>().ToMethod(context =>
				Kernel.Get<string>("AtlassianUserName".Camelize()))
				.When(
				req => req.Target != null && req.Target.Name.ToLowerInvariant().Contains("username"));

			Kernel.Bind<string>().ToMethod(context =>
				Kernel.Get<string>("AtlassianPassword".Camelize())).When(
				req => req.Target != null && req.Target.Name.ToLowerInvariant().Contains("password"));

			Kernel.Bind<Func<ICanDownload>>().ToMethod(context => () => context.Kernel.Get<BasicAuthWebClient>());
			Kernel.Bind<Func<bool>>().ToMethod(context => () => context.Kernel.Get<string>("AtlassianPassword".Camelize()).Length > 0)
				.When(req => req.Target != null && req.Target.Name.ToLowerInvariant().Contains("hasStoredCredentials".ToLowerInvariant()));

			Kernel.Bind<UcMain>().ToSelf()
				.WithConstructorArgument("children", new Control[]
				{
					Kernel.Get<UcIIS>(),
					Kernel.Get<UcJunction>(),
					Kernel.Get<UcMsBuild>(),
					Kernel.Get<UcEnvironment>(),
					Kernel.Get<UcGit>(),
					Kernel.Get<UcService>(),
					Kernel.Get<UcDatabase>(),
					Kernel.Get<UcConfigXml>(),
					Kernel.Get<UcStash>(),
					Kernel.Get<UcCrucible>()
					
				})
				.WithConstructorArgument("links", Settings.Default.GetLinks())
                .WithConstructorArgument("dynamicChildren", new Dictionary<Type,Lazy<Control>>
                {
                    {(typeof(UcEnvDte)),new Lazy<Control>(()=> Kernel.Get<UcEnvDte>())}
                });
		}

        /// <summary>
        /// Setup for defining how a control could get a reference to calling a dynamic download assembly without eagerly triggering it.
        /// </summary>
		static void RegisterDynamicAssemblies()
		{
			Kernel.Bind<DynamicDownload>().ToSelf().InSingletonScope(); //this should never run twice
			var dd=Kernel.Get<DynamicDownload>(); 

			Kernel.Bind<Func<string, IAdministerIIS>>().ToMethod(context => s => new IISAdministration(s));
		    Kernel.Bind<Func<Domain.EnvDte.Dte>>().ToMethod(context => () => new Domain.EnvDte.Dte());
		}

		static void RegisterCrossCuttings()
		{
			Kernel.Bind<Profiler>().ToSelf().InSingletonScope().WithConstructorArgument("name", string.Empty);

		}

        /// <summary>
        /// Defines conventional bindings for user settings allowing a constructor to require:
        /// just the value, the getter and setter, and/or a notifier about value changes.
        /// </summary>
		static void BindSettingByConvention<T>(
			this IKernel kernel,
			T settingsStore,
			Expression<Func<T, string>> selector,
			Func<string, string> getterTransformer = null,
			Func<string, string> setterTransformer = null) where T : ApplicationSettingsBase
		{
			var name = LinqOp.PropertyOf(selector).Name.Camelize();
			
			var setterRaw = selector.GetterToSetter();
			
			Func<string, string> setter = setterTransformer ?? (s => s);
			
			var compiled = selector.Compile();
			var getter = getterTransformer ?? (s => s);
			
			kernel.Bind<IObservable<string>>().ToMethod(context => settingsStore.ToObservable(selector).Select(_ =>
				getter(compiled(settingsStore))))
				.When(req =>
					req.Target.Name == name + "Notifier").InSingletonScope();
			kernel.Bind<string>().ToMethod(context => getter(compiled(settingsStore))).Named(name);

			kernel.Bind<string>().ToMethod(context => getter(compiled(settingsStore))).When(req =>
				req.Target != null && req.Target.Name == name).InTransientScope();
			kernel.Bind<AccessorHelper<string>>().ToMethod(
				context => new AccessorHelper<string>(
					           () => getter(compiled(settingsStore)),
					           v =>
						           {
							           setterRaw(settingsStore, setter(v));
						settingsStore.Save();
				})).When(
				req => req != null && req.Target != null && req.Target.Name == name + "Accessor").InSingletonScope();
			kernel.Bind<Func<string>>().ToMethod(context => () => getter(compiled(settingsStore))).When(
				req => req.Target != null && req.Target.Name == name + "Getter");
		}
		
		static void BindSettingByConvention<T>(this IKernel kernel, T settingsStore, Expression<Func<T, StringCollection>> selector)
			where T : ApplicationSettingsBase
		{
			var name = LinqOp.PropertyOf(selector).Name.Camelize().Pluralize();
			var setter = selector.GetterToSetter();
			var value = selector.Compile()(settingsStore);

			if (value == null)
				setter(settingsStore, new StringCollection());

			kernel.Bind<ObservableCollection<string>>().ToMethod(context =>
			settingsStore.ToObservableCollectionWrapper(selector)).When(req => req.Target.Name == name);
			
		}

	}
}
