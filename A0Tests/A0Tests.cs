// $Date: 2019-10-15 14:18:33 +0300 (Вт, 15 окт 2019) $
// $Revision: 7 $
// $Author: vbutov $

// Библиотека тестов A0service API A0

// https://software-testing.org/automation-testing/osnovnye-atributy-nunit-dlya-napisaniya-avtotestov-na-c.html
// Все классы в проекте, помеченные атрибутом [TestFixture] означают что данный класс содержит автотесты и фактически это тест сьют.

// Атрибуты NUnit для выполнения перед и после Тест сьюта
//[TestFixture]
//[TestFixtureSetUp]
//[TestFixtureTearDown]

// Атрибуты NUnit для выполнения перед и после каждого Тест кейса
//[TestFixture]
//[SetUp]
//[TearDown]

// базовые тесты
//   smoke - Отсутвие ошибок при обращение к полям струтуры API. 
// Тусты функциональности.
//   Каталоги
//     Repo - Каталоги объектов (CRUD)
//     Search - Каталоги поиска (R)
//   Services
//     

namespace A0Tests
{
	using NUnit.Framework;

	[TestFixture]
    public class A0Tests
    {
		[Test(Description = "Тест для проверки ошибки")]
		public void Test_Error()
		{
			Assert.IsFalse(true);
		}

		[Test(Description = "Тест для проверки отсутствия ошибки")]
		public void Test_Successed()
		{
			Assert.IsFalse(false);
		}
	}
}
