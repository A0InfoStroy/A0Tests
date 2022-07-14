// $Date: 2019-10-15 14:18:33 +0300 (Вт, 15 окт 2019) $
// $Revision: 7 $
// $Author: vbutov $
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
