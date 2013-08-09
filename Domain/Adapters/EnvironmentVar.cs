using System;

namespace Domain.Adapters
{
	public  class EnvironmentVar
	{
		
		public static void SetEnvironmentVariable(string key, string value, EnvironmentVariableTarget target = EnvironmentVariableTarget.Machine) { Environment.SetEnvironmentVariable(key, value, target); }
		
	}

	
}
