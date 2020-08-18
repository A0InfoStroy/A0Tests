// $Date: 2020-08-17 15:35:09 +0300 (Пн, 17 авг 2020) $
// $Revision: 374 $
// $Author: agalkin $
// Базовые тесты IA0App

namespace A0Tests.Smoke
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности приложения А0.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0App",
        Author = "agalkin")]
    public class Test_IA0App : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает приложение А0.
        /// </summary>
        protected IA0App App { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.App = this.A0.App;
            Assert.NotNull(this.App);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.App);
            this.App = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к информации о версии приложения.
        /// </summary>
        [Test]
        public void Test_Version()
        {
            IVersion version = this.App.Version;
            Assert.NotNull(version);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id процесса.
        /// </summary>
        [Test]
        public void Test_ProcessID()
        {
            uint processID = this.App.ProcessID;
            Assert.IsTrue(processID > 0);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к текущей директории.
        /// </summary>
        [Test]
        public void Test_CurrentDir()
        {
            string currentDir = this.App.CurrentDir;
            Assert.NotNull(currentDir);
            Assert.IsTrue(currentDir != string.Empty);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к протоколу А0.
        /// </summary>
        [Test]
        public void Test_Log()
        {
            IA0Log log = this.App.Log;
            Assert.NotNull(log);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку видимости отладочной формы.
        /// </summary>
        [Test]
        public void Test_Visible()
        {
            bool visible = this.App.Visible;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к соединению с БД.
        /// </summary>
        [Test]
        public void Test_Connection()
        {
            IConnection connection = this.App.Connection;
            Assert.NotNull(connection);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к административной части.
        /// </summary>
        [Test]
        public void Test_Admin()
        {
            IA0Admin admin = this.App.Admin;
            Assert.NotNull(admin);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену объектов с атрибутами приложения.
        /// </summary>
        [Test]
        public void Test_Attributes()
        {
            IAppAttributesDomain attributes = this.App.Attributes;
            Assert.NotNull(attributes);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену с расширениями атрибутов объектов.
        /// </summary>
        [Test]
        public void Test_AttrExtension()
        {
            IAppAttrExtensionDomain attrExtension = this.App.AttrExtension;
            Assert.NotNull(attrExtension);
        }
    }
}