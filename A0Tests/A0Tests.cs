// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $

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
		[Test(Description = "Тест для проверки ошибки"), Timeout(1000)]

		public void Test_Error()
		{
			Assert.IsFalse(true);
		}

		[Test(Description = "Тест для проверки отсутствия ошибки"), Timeout(1000)]
		public void Test_Successed()
		{
			Assert.IsFalse(false);
		}
	}
}
