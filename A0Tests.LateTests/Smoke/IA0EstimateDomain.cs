// $Date: 2020-12-07 13:40:15 +0300 (Пн, 07 дек 2020) $
// $Revision: 448 $
// $Author: agalkin $
// Базовые тесты IA0EstimateDomain

namespace A0Tests.LateTests.Smoke
{
    using NUnit.Framework;

    /// <summary>
    ///  Базовые тесты проверки работоспособности домена сметных данных в системе.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0EstimateDomain",
        Author = "agalkin")]
    public class Test_IA0EstimateDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен сметных данных в системе.
        /// </summary>
        protected dynamic Estimate { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Estimate = this.A0.Estimate;
            Assert.NotNull(this.Estimate);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Estimate);
            this.Estimate = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу сметных данных.
        /// </summary>
        [Test]
        public void Test_Repo()
        {
            dynamic repo = this.Estimate.Repo;
            Assert.NotNull(repo);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к операциям для домена сметных данных.
        /// </summary>
        [Test]
        public void Test_Services()
        {
            dynamic services = this.Estimate.Services;
            Assert.NotNull(services);
        }
    }
}