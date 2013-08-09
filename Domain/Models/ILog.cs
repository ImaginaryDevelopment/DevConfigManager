using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	using System.Drawing;

	public interface ILog
	{
		string Log(string text);

		string LogJson(string json);

		Tuple<string,Color> Log(string text, Color color);

		Tuple<string, Color> Log(Tuple<string, Color> data);
	}
}
