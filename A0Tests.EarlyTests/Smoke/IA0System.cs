// $Date: 2021-01-19 15:20:11 +0300 (Вт, 19 янв 2021) $
// $Revision: 500 $
// $Author: agalkin $
// Базовые тесты IA0System

namespace A0Tests.EarlyTests.Smoke
{
    using A0Service;
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
        protected IA0System Sys { get; private set; }

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
            IA0SystemRepo repo = this.Sys.Repo;
            Assert.NotNull(repo);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену таблицы коэффициентов.
        /// </summary>
        [Test]
        public void Test_FactorTable()
        {
            IA0SysFactorTableDomain factorTable = this.Sys.FactorTable;
            Assert.NotNull(factorTable);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к НСИ.
        /// </summary>
        [Test]
        public void Test_NSI()
        {
            INSI nsi = this.Sys.NSI;
            Assert.NotNull(nsi);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену организаций.
        /// </summary>
        [Test]
        public void Test_Organizations()
        {
            IA0SysOrganizationsDomain organizations = this.Sys.Organizations;
            Assert.NotNull(organizations);
        }
    }
}