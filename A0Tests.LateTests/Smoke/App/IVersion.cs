// $Date: 2020-12-07 16:50:47 +0300 (Пн, 07 дек 2020) $
// $Revision: 449 $
// $Author: agalkin $
// Базовые тесты IVersion

namespace A0Tests.LateTests.Smoke.App
{
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности информации о версии приложения.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IVersion",
        Author = "agalkin")]
    public class Test_IVersion : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает информацию о версии приложения.
        /// </summary>
        protected dynamic Version { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Version = this.A0.App.Version;
            Assert.NotNull(this.Version);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Version);
            this.Version = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения версии продукта.
        /// </summary>
        [Test]
        public void Test_ProductVersion()
        {
            string productVersion = this.Version.ProductVersion;
            Assert.NotNull(productVersion);
            Assert.IsTrue(productVersion.Length > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения версии БД.
        /// </summary>
        [Test]
        public void Test_DBVersion()
        {
            string dbVersion = this.Version.DBVersion;
            Assert.NotNull(dbVersion);
            Assert.IsTrue(dbVersion.Length > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения даты издания системы.
        /// </summary>
        [Test]
        public void Test_EditionDate()
        {
            string editionDate = this.Version.EditionDate;
            Assert.NotNull(editionDate);
            Assert.IsTrue(editionDate.Length > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения идентификатора лицензии.
        /// </summary>
        [Test]
        public void Test_LicenseID()
        {
            string licenseID = this.Version.LicenseID;
            Assert.NotNull(licenseID);
            Assert.IsTrue(licenseID.Length > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения версии API.
        /// </summary>
        [Test]
        public void Test_Version()
        {
            string version = this.Version.Version;
            Assert.NotNull(version);
            Assert.IsTrue(version.Length > 0);
        }
    }
}