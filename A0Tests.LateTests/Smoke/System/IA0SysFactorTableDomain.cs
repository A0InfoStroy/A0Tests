// $Date: 2020-12-11 14:38:49 +0300 (Пт, 11 дек 2020) $
// $Revision: 453 $
// $Author: agalkin $
// Базовые тесты IA0SysFactorTableDomain

namespace A0Tests.LateTests.Smoke.Sys
{
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
        protected dynamic Domain { get; private set; }

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
        [Test]
        public void Test_Repo()
        {
            dynamic repo = this.Domain.Repo;
            Assert.NotNull(repo);
        }
    }
}