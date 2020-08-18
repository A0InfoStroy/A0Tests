// $Date: 2020-07-20 08:57:31 +0300 (Пн, 20 июл 2020) $
// $Revision: 306 $
// $Author: agalkin $
// Базовые классы

namespace A0Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Xml.Linq;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Осуществляет чтение данных из файла пользовательских настроек.
    /// </summary>
    public abstract class Config
    {
        /// <summary>
        /// Получает или устанавливает значение ссылки на xml-файл пользовательских настроек.
        /// </summary>
        protected XDocument Configuration { get; private set; }

        /// <summary>
        ///  Осуществляет подготовку к тестированию.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            // Получение пути файла пользовательских настроек.
            string xmlFilePath = AppDomain.CurrentDomain.BaseDirectory + @"userConfig.xml";

            Assert.True(File.Exists(xmlFilePath), "Файл пользовательских настроек не найден.");

            // Загрузка файла.
            this.Configuration = XDocument.Load(xmlFilePath);
            Assert.NotNull(this.Configuration, "Ошибка загрузки файла пользовательских настроек");
        }
    }

    /// <summary>
    /// Предоставляет настройки соединения A0Service.
    /// </summary>
    public abstract class A0Config : Config
    {
        /// <summary>
        /// Получает или устанавливает строку соединения OLEDB.
        /// </summary>
        protected string ConnStr { get; private set; }

        /// <summary>
        /// Получает или устанавливает имя пользователя в системе А0.
        /// </summary>
        protected string UserName { get; private set; }

        /// <summary>
        /// Получает или устанавливает пароль пользователя в системе А0.
        /// </summary>
        protected string Password { get; private set; }

        /// <summary>
        /// Осуществляет подготовку к тестированию.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ConnStr = this.Configuration.Descendants("connectionString").SingleOrDefault()?.Value;
            this.UserName = this.Configuration.Descendants("userName").SingleOrDefault()?.Value;
            this.Password = this.Configuration.Descendants("passwword").SingleOrDefault()?.Value;
        }
    }

    /// <summary>
    /// Осуществляет создание IAPI.
    /// </summary>
    public abstract class A0Base : A0Config
    {
        /// <summary>
        /// Получает или устанавливает значение IAPI.
        /// </summary>
        protected IAPI A0 { get; private set; }

        /// <summary>
        /// Осуществляет подготовку к тестированию.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            // Создание A0 API.
            this.A0 = new API();

            Assert.NotNull(this.A0, "Не могу получить интерфейс доступа к API A0");
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            Assert.NotNull(this.A0);
            Marshal.ReleaseComObject(this.A0);
            this.A0 = null;
        }
    }

    /// <summary>
    /// Базовый класс тестирования API А0.
    /// </summary>
    public abstract class Test_Base : A0Base
    {
        /// <summary>
        /// Получает значение Guid головного комплекса.
        /// </summary>
        public Guid HeadComplexGuid => this.A0.Estimate.Repo.ComplexID.HeadComplexGUID;

        /// <summary>
        /// Осуществляет подготовку к тестированию.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            // Установка соединения с БД А0
            EConnectReturnCode result = this.A0.Connect3(this.ConnStr, this.UserName, this.Password);
            if (result != EConnectReturnCode.crcSuccess)
            {
                throw new Exception(string.Format("Не могу установить соединение с БД А0. Код возврата {0}", result));
            }
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            // Интерейс A0 после теста должен остаться
            Assert.NotNull(this.A0, "Ожидается A0 после теста");

            this.A0.Disconnect();
            base.TearDown();
        }
    }
}