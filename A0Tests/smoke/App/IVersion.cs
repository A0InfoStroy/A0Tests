// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты IVersion

namespace A0Tests.Smoke.App
{
    using A0Service;
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
        protected IVersion Version { get; private set; }

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
        [Test, Timeout(15000)]
        public void Test_ProductVersion()
        {
            string productVersion = this.Version.ProductVersion;
            Assert.NotNull(productVersion);
            Assert.IsTrue(productVersion.Length > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения версии БД.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_DBVersion()
        {
            string dbVersion = this.Version.DBVersion;
            Assert.NotNull(dbVersion);
            Assert.IsTrue(dbVersion.Length > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения даты издания системы.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_EditionDate()
        {
            string editionDate = this.Version.EditionDate;
            Assert.NotNull(editionDate);
            Assert.IsTrue(editionDate.Length > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения идентификатора лицензии.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_LicenseID()
        {
            string licenseID = this.Version.LicenseID;
            Assert.NotNull(licenseID);
            Assert.IsTrue(licenseID.Length > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения версии API.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_Version()
        {
            string version = this.Version.Version;
            Assert.NotNull(version);
            Assert.IsTrue(version.Length > 0);
        }
    }
}