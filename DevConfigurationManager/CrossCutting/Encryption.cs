namespace DeveloperConfigurationManager.CrossCutting
{
	class Encryption
	{
		readonly string _sharedSecret;

		public Encryption(string sharedSecret)
		{
			_sharedSecret = sharedSecret;
		}

		public string Decrypt(string s)
		{
			if (s.IsNullOrEmpty()) return s;
			return Domain.Crypto.DecryptStringAES(s, _sharedSecret);
		}

		public string Encrypt(string s)
		{
			if (s.IsNullOrEmpty()) return s;
			return Domain.Crypto.EncryptStringAES(s, _sharedSecret);

		}
	}
}
