// $Date: 2020-11-30 17:15:16 +0300 (Пн, 30 ноя 2020) $
// $Revision: 441 $
// $Author: agalkin $
// Базовые классы

namespace A0Tests.LateTests
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using A0Tests.Config;
    using NUnit.Framework;
    using static A0Tests.Config.UserConfigParser;

    /// <summary>
    /// Осуществляет получение основных настроек соединения с БД А0.
    /// </summary>
    public abstract class BaseConfig
    {
        /// <summary>
        /// Получает или устанавливает xml-текст файла пользовательских настроек.
        /// </summary>
        protected string XmlText { get; private set; }

        /// <summary>
        /// Получает и устанавливает основные настройки соединения с БД А0.
        /// </summary>
        protected IA0BaseConfig Config { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            // Получение пути файла пользовательских настроек.
            string xmlFilePath = AppDomain.CurrentDomain.BaseDirectory + @"userConfig.xml";

            Assert.True(File.Exists(xmlFilePath), "Не удалось найти файл пользовательских настроек");
            this.XmlText = File.ReadAllText(xmlFilePath);
            Assert.NotNull(this.XmlText, "Не удалось прочитать файл пользовательских настроек");
            Assert.IsNotEmpty(this.XmlText, "Ошибка чтения файла");
            string error = null;
            try
            {
                this.Config = GetA0BaseConfig(this.XmlText);
            }
            catch (System.Runtime.Serialization.SerializationException ex)
            {
                error = "Ошибка наименования элементов в xml-файле пользовательских настроек" + ex.Message;
            }
            catch (Exception ex)
            {
                error = "Ошибка чтения xml-файла пользовательских настроек" + ex.Message;
            }

            Assert.NotNull(this.Config, error);
        }
    }

    /// <summary>
    /// Осуществляет создание IAPI.
    /// </summary>
    public abstract class A0Base : BaseConfig
    {
        /// <summary>
        /// Получает или устанавливает значение IAPI.
        /// </summary>
        protected dynamic A0 { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>      
        public override void SetUp()
        {
            base.SetUp();

            // Получаем тип объекта API.
            Type a0Type = Type.GetTypeFromProgID("A0Service.API");

            // Создаем A0 API.
            this.A0 = Activator.CreateInstance(a0Type);

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
        /// Признак установки соединения с БД А0.
        /// </summary>
        private bool connectionSuccess;

        /// <summary>
        /// Осуществляет подготовку к тестированию.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            // Установка соединения с БД А0
            dynamic result = this.A0.Connect3(this.Config.ConnectionString, this.Config.UserName, this.Config.Password);

            Assert.Zero(result, $"Не могу установить соединение с БД А0. Код возврата {result}");
            this.connectionSuccess = true;
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            // Интерейс A0 после теста должен остаться
            Assert.NotNull(this.A0, "Ожидается A0 после теста");

            if (this.connectionSuccess)
            {
                this.A0.Disconnect(); 
            }

            base.TearDown();
        }
    }
}