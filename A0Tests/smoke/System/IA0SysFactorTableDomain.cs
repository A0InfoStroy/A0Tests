// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты IA0SysFactorTableDomain

namespace A0Tests.Smoke.Sys
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит Базовые тесты проверки работоспособности домена таблиц коэффициентов.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0SysFactorTableDomain",
        Author = "agalkin")]
    public class Test_IA0SysFactorTableDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен таблиц коэффициентов.
        /// </summary>
        protected IA0SysFactorTableDomain Domain { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Domain = this.A0.Sys.FactorTable;
            Assert.NotNull(this.Domain);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Domain);
            this.Domain = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу таблиц коэффициентов.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Repo()
        {
            IA0SysFactorTableRepo repo = this.Domain.Repo;
            Assert.NotNull(repo);
        }
    }
}