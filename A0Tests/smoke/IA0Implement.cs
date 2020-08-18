// $Date: 2020-08-17 15:31:37 +0300 (Пн, 17 авг 2020) $
// $Revision: 373 $
// $Author: agalkin $
// Базовые тесты IA0ImplementDomain

namespace A0Tests.Smoke
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности домена сметных данных выполнения.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ImplementDomain",
        Author = "agalkin")]
    public class Test_IA0ImplementDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен сметных данных выполнения.
        /// </summary>
        protected IA0ImplementDomain Implement { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Implement = this.A0.Implement;
            Assert.NotNull(this.Implement);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Implement);
            this.Implement = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу сметных данных выполнения.
        /// </summary>
        [Test(Description = "Repo")]
        public void Test_Repo()
        {
            IA0ImplementRepo repo = this.Implement.Repo;
            Assert.NotNull(repo);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к операциям для домена сметных данных выполнения.
        /// </summary>
        [Test(Description = "Services")]
        public void Test_Services()
        {
            IA0ImplementServices services = this.Implement.Services;
            Assert.NotNull(services);
        }
    }
}