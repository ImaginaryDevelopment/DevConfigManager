

// ReSharper disable CheckNamespace
namespace com.bodurov
// ReSharper restore CheckNamespace
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Reflection.Emit;
	using System.Text;
	using System.Text.RegularExpressions;

	//http://blog.bodurov.com/How-to-bind-Silverlight-DataGrid-from-IEnumerable-of-IDictionary
	public static class DataSourceCreator
	{
		static readonly Regex PropertNameRegex =
				  new Regex(@"^[A-Za-z]+[A-Za-z0-9_]*$", RegexOptions.Singleline);

		static readonly Dictionary<string, Type> _typeBySignature = new Dictionary<string, Type>();

		public static IEnumerable ToDataSource<TKey, TValue>(this IEnumerable<IDictionary<TKey, TValue>> list)
		{
			IDictionary<TKey,TValue> firstDict = null;
			bool hasData = false;
			foreach (var currentDict in list)
			{
				hasData = true;
				firstDict = currentDict;
				break;
			}
			if (!hasData)
			{
				return new object[] { };
			}
			if (firstDict == null)
			{
				throw new ArgumentException("IDictionary entry cannot be null");
			}

			string typeSignature = GetTypeSignature(firstDict, list);

			Type objectType = GetTypeByTypeSignature(typeSignature);

			if (objectType == null)
			{
				TypeBuilder tb = GetTypeBuilder(typeSignature);

				ConstructorBuilder constructor =
								tb.DefineDefaultConstructor(
												MethodAttributes.Public |
												MethodAttributes.SpecialName |
												MethodAttributes.RTSpecialName);


				foreach (var pair in firstDict)
				{
					if (PropertNameRegex.IsMatch(Convert.ToString(pair.Key), 0))
					{
						CreateProperty(tb,
											 Convert.ToString(pair.Key),
											 GetValueType(pair.Value, list, pair.Key));
					}
					else
					{
						throw new ArgumentException(
										@"Each key of IDictionary must be 
                                alphanumeric and start with character.");
					}
				}
				objectType = tb.CreateType();

				_typeBySignature.Add(typeSignature, objectType);
			}

			return GenerateEnumerable(objectType, list, firstDict);
		}

		public static IEnumerable ToDataSource(this IEnumerable<IDictionary> list)
		{
			IDictionary firstDict = null;
			bool hasData = false;
			foreach (var currentDict in list)
			{
				hasData = true;
				firstDict = currentDict;
				break;
			}
			if (!hasData)
			{
				return new object[] { };
			}
			if (firstDict == null)
			{
				throw new ArgumentException("IDictionary entry cannot be null");
			}

			string typeSignature = GetTypeSignature(firstDict, list);

			Type objectType = GetTypeByTypeSignature(typeSignature);

			if (objectType == null)
			{
				TypeBuilder tb = GetTypeBuilder(typeSignature);

				ConstructorBuilder constructor =
								tb.DefineDefaultConstructor(
												MethodAttributes.Public |
												MethodAttributes.SpecialName |
												MethodAttributes.RTSpecialName);


				foreach (DictionaryEntry pair in firstDict)
				{
					if (PropertNameRegex.IsMatch(Convert.ToString(pair.Key), 0))
					{
						CreateProperty(tb,
											 Convert.ToString(pair.Key),
											 GetValueType(pair.Value, list, pair.Key));
					}
					else
					{
						throw new ArgumentException(
										@"Each key of IDictionary must be 
                                alphanumeric and start with character.");
					}
				}
				objectType = tb.CreateType();

				_typeBySignature.Add(typeSignature, objectType);
			}

			return GenerateEnumerable(objectType, list, firstDict);
		}



		static Type GetTypeByTypeSignature(string typeSignature)
		{
			Type type;
			return _typeBySignature.TryGetValue(typeSignature, out type) ? type : null;
		}

		static Type GetValueType<TKey, TValue>(object value, IEnumerable<IDictionary<TKey, TValue>> list, TKey key)
		{
			if (value == null)
			{
				foreach (var dictionary in list)
				{
					if (dictionary.ContainsKey(key))
					{
						value = dictionary[key];
						if (value != null) break;
					}
				}
			}
			return (value == null) ? typeof(object) : value.GetType();
		}

		static Type GetValueType(object value, IEnumerable<IDictionary> list, object key)
		{
			if (value == null)
			{
				foreach (var dictionary in list)
				{
					if (dictionary.Contains(key))
					{
						value = dictionary[key];
						if (value != null) break;
					}
				}
			}
			return (value == null) ? typeof(object) : value.GetType();
		}

		static string GetTypeSignature<TKey, TValue>(IDictionary<TKey, TValue> firstDict, IEnumerable<IDictionary<TKey,TValue>> list)
		{
			var sb = new StringBuilder();
			foreach (var pair in firstDict)
			{
				sb.AppendFormat("_{0}_{1}", pair.Key, GetValueType(pair.Value, list, pair.Key));
			}
			return sb.ToString().GetHashCode().ToString().Replace("-", "Minus");
		}

		static string GetTypeSignature(IDictionary firstDict, IEnumerable<IDictionary> list)
		{
			var sb = new StringBuilder();
			foreach (DictionaryEntry pair in firstDict)
			{
				sb.AppendFormat("_{0}_{1}", pair.Key, GetValueType(pair.Value, list, pair.Key));
			}
			return sb.ToString().GetHashCode().ToString().Replace("-", "Minus");
		}

		static IEnumerable GenerateEnumerable<TKey, TValue>(
					Type objectType, IEnumerable<IDictionary<TKey, TValue>> list, IDictionary<TKey,TValue> firstDict)
		{
			var listType = typeof(List<>).MakeGenericType(new[] { objectType });
			var listOfCustom = Activator.CreateInstance(listType);

			foreach (var currentDict in list)
			{
				if (currentDict == null)
				{
					throw new ArgumentException("IDictionary entry cannot be null");
				}
				var row = Activator.CreateInstance(objectType);
				foreach (var pair in firstDict)
				{
					if (currentDict.ContainsKey(pair.Key))
					{
						PropertyInfo property =
							 objectType.GetProperty(Convert.ToString(pair.Key));

						var value = currentDict[pair.Key];
						if (value != null &&
							 value.GetType() != property.PropertyType &&
							 !property.PropertyType.IsGenericType)
						{
							try
							{
								value = (TValue)Convert.ChangeType(
									currentDict[pair.Key],
									property.PropertyType,
												null);
							}
							catch { }
						}

						property.SetValue(row, value, null);
					}
				}
				listType.GetMethod("Add").Invoke(listOfCustom, new[] { row });
			}
			return listOfCustom as IEnumerable;
		}

		static IEnumerable GenerateEnumerable(
					Type objectType, IEnumerable<IDictionary> list, IDictionary firstDict)
		{
			var listType = typeof(List<>).MakeGenericType(new[] { objectType });
			var listOfCustom = Activator.CreateInstance(listType);

			foreach (var currentDict in list)
			{
				if (currentDict == null)
				{
					throw new ArgumentException("IDictionary entry cannot be null");
				}
				var row = Activator.CreateInstance(objectType);
				foreach (DictionaryEntry pair in firstDict)
				{
					if (currentDict.Contains(pair.Key))
					{
						PropertyInfo property =
							 objectType.GetProperty(Convert.ToString(pair.Key));

						var value = currentDict[pair.Key];
						if (value != null &&
							 value.GetType() != property.PropertyType &&
							 !property.PropertyType.IsGenericType)
						{
							try
							{
								value = Convert.ChangeType(
												currentDict[pair.Key],
												property.PropertyType,
												null);
							}
							catch { }
						}

						property.SetValue(row, value, null);
					}
				}
				listType.GetMethod("Add").Invoke(listOfCustom, new[] { row });
			}
			return listOfCustom as IEnumerable;
		}

		static TypeBuilder GetTypeBuilder(string typeSignature)
		{
			var an = new AssemblyName("TempAssembly" + typeSignature);
			AssemblyBuilder assemblyBuilder =
				 AppDomain.CurrentDomain.DefineDynamicAssembly(
					  an, AssemblyBuilderAccess.Run);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");

			TypeBuilder tb = moduleBuilder.DefineType("TempType" + typeSignature
									  , TypeAttributes.Public |
									  TypeAttributes.Class |
									  TypeAttributes.AutoClass |
									  TypeAttributes.AnsiClass |
									  TypeAttributes.BeforeFieldInit |
									  TypeAttributes.AutoLayout
									  , typeof(object));
			return tb;
		}

		static void CreateProperty(
							 TypeBuilder tb, string propertyName, Type propertyType)
		{
			if (propertyType.IsValueType && !propertyType.IsGenericType)
			{
				propertyType = typeof(Nullable<>).MakeGenericType(new[] { propertyType });
			}

			FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName,
																	  propertyType,
																	  FieldAttributes.Private);


			PropertyBuilder propertyBuilder =
				 tb.DefineProperty(
					  propertyName, PropertyAttributes.HasDefault, propertyType, null);
			MethodBuilder getPropMthdBldr =
				 tb.DefineMethod("get_" + propertyName,
					  MethodAttributes.Public |
					  MethodAttributes.SpecialName |
					  MethodAttributes.HideBySig,
					  propertyType, Type.EmptyTypes);

			ILGenerator getIL = getPropMthdBldr.GetILGenerator();

			getIL.Emit(OpCodes.Ldarg_0);
			getIL.Emit(OpCodes.Ldfld, fieldBuilder);
			getIL.Emit(OpCodes.Ret);

			MethodBuilder setPropMthdBldr =
				 tb.DefineMethod("set_" + propertyName,
					MethodAttributes.Public |
					MethodAttributes.SpecialName |
					MethodAttributes.HideBySig,
					null, new Type[] { propertyType });

			ILGenerator setIL = setPropMthdBldr.GetILGenerator();

			setIL.Emit(OpCodes.Ldarg_0);
			setIL.Emit(OpCodes.Ldarg_1);
			setIL.Emit(OpCodes.Stfld, fieldBuilder);
			setIL.Emit(OpCodes.Ret);

			propertyBuilder.SetGetMethod(getPropMthdBldr);
			propertyBuilder.SetSetMethod(setPropMthdBldr);
		}
	}
}