using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Models;

namespace Domain.Tests.Adapters
{
	using System.Collections.Generic;
	using System.Windows.Forms;

	using Domain.Tests.Helper;

	[TestClass]
	public class CrucibleTests
	{
		readonly Security _security = new Security();

		[TestMethod]
		[TestCategory("Integrated")]
		public void GetOpenReviews_NullDownload_ReturnsNull()
		{
			var logger = Rhino.Mocks.MockRepository.GenerateStub<ILog>();
			var downloader = Rhino.Mocks.MockRepository.GenerateStub<ICanDownload>();
			var cru = new Domain.Adapters.Crucible(logger, downloader);
			var reviews = cru.GetOpenReviews(Environment.UserName, Properties.Settings.Default.crucibleAuthority);
			Assert.IsNull(reviews);
		}

		[TestMethod]
		[TestCategory("Integrated")]
		public void GetOpenReviews_validCredentials_IsNotNull()
		{
			var logger = Rhino.Mocks.MockRepository.GenerateStub<ILog>();
			var password = AtlassianHelper.InitCredentials(_security);
			var downloader = new Domain.Adapters.BasicAuthWebClient(Environment.UserName, password);
			
			var cru = new Domain.Adapters.Crucible(logger, downloader);
			var reviews = cru.GetOpenReviews(Environment.UserName, Properties.Settings.Default.crucibleAuthority);
			Assert.IsNotNull(reviews);
		}

		[TestMethod]
		[TestCategory("UnitTest")]
		public void TestMethod2()
		{

		}
	}
}
