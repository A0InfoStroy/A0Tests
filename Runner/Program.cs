// $Date: 2020-03-25 11:16:15 +0300 (Ср, 25 мар 2020) $
// $Revision: 63 $
// $Author: agalkin $
// Запуск тестов в отдельном приложении

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NUnit.Framework;

namespace Runner
{
	class Program
	{
		
		static void Main(string[] args)
		{
			string path = typeof(A0Tests.A0Tests).Assembly.Location;
			//string path = Assembly.GetExecutingAssembly().Location; // требуется "using System.Reflection;"
			// На выходе почему различаются версии используемых библиотек
			NUnit.ConsoleRunner.Program.Main(new[] { path });
		}
	}
}
