using Domain.Extensions;

namespace Domain
{
	using System.Reactive.Linq;

	public class StreamOuts
	{
		public string Errors { get; set; }

		public string Output { get; set; }

		public int ExitCode { get; set; }

		public override string ToString()
		{
			if (Errors.HasValue() == Output.HasValue() || ExitCode != 0)
			{
				return Newtonsoft.Json.JsonConvert.SerializeObject(this);
			}

			if (Output.HasValue())
				return Output;
			return Errors;
		}
	}
}
