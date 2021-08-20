// $Date: 2020-12-07 13:33:29 +0300 (Пн, 07 дек 2020) $
// $Revision: 447 $
// $Author: agalkin $
// Базовые тесты IA0System

namespace A0Tests.LateTests.Smoke
{
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности системных данных в системе А0.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0System",
        Author = "agalkin")]
    public class Test_IA0System : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает системные данные в системе А0.
        /// </summary>
        protected dynamic Sys { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Sys = this.A0.Sys;
            Assert.NotNull(this.Sys);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Sys);
            this.Sys = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогам системных данных.
        /// </summary>
        [Test]
        public void Test_Repo()
        {
            dynamic repo = this.Sys.Repo;
            Assert.NotNull(repo);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену таблицы коэффициентов.
        /// </summary>
        [Test]
        public void Test_FactorTable()
        {
            dynamic factorTable = this.Sys.FactorTable;
            Assert.NotNull(factorTable);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к НСИ.
        /// </summary>
        [Test]
        public void Test_NSI()
        {
            dynamic nsi = this.Sys.NSI;
            Assert.NotNull(nsi);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену организаций.
        /// </summary>
        [Test]
        public void Test_Organizations()
        {
            dynamic organizations = this.Sys.Organizations;
            Assert.NotNull(organizations);
        }
    }
}