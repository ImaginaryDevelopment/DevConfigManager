using System;
using System.Drawing;
using Domain.Models;

namespace DeveloperConfigurationManager.CrossCutting
{
    class Logging : ILog
	{
		readonly Action<string> _logger;

		public Logging(Action<string> logger)
		{
			_logger = logger;
		}

		public T Dump<T>(T src, string message = null)
			where T : class 
		{
			_logger(src != null ? src.ToString() : "null");
			return src;
		}

		public string Log(string text)
		{
			_logger(text);
			return text;
		}

		public string LogJson(string json)
		{
			_logger(json);
			return json;
		}

		public Tuple<string, Color> Log(string text, Color color)
		{
			Log(text);
			return Tuple.Create(text, color);
		}

		public Tuple<string, Color> Log(Tuple<string, Color> data)
		{
			Log(data.Item1);
			return data;
		}
	}
}
