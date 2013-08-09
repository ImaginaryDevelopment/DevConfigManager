using System;
using System.Security.Cryptography;
namespace Domain.Tests.Helper
{
	public class Security
	{
		readonly byte[] _entropy;

		public Security(byte[] entropy = null)
		{
			_entropy = entropy;
		}

		public string Encrypt(string text)
		{
			return Convert.ToBase64String(ProtectedData.Protect(Convert.FromBase64String(text), _entropy, DataProtectionScope.CurrentUser));
		}

		public string Decrypt(string text)
		{
			return
				Convert.ToBase64String(
					ProtectedData.Unprotect(Convert.FromBase64String(text), _entropy, DataProtectionScope.CurrentUser));
		}
		

	}
}
