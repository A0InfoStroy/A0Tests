// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты IA0Estimate

namespace A0Tests.Smoke
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовые тесты проверки работоспособности домена сметных данных в системе.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0Estimate",
        Author = "agalkin")]
    public class Test_IA0EstimateDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен сметных данных в системе.
        /// </summary>
        protected IA0EstimateDomain Estimate { get; private set; }

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
        [Test, Timeout(15000)]
        public void Test_Repo()
        {
            IA0EstimateRepo repo = this.Estimate.Repo;
            Assert.NotNull(repo);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к операциям для домена сметных данных.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_Services()
        {
            IA0EstimateServices services = this.Estimate.Services;
            Assert.NotNull(services);
        }
    }
}