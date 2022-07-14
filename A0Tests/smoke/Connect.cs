// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты проверки соединений с A0Service

namespace A0Tests.Smoke
{
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using A0Service;
    using Config;
    using NUnit.Framework;
    using static Config.UserConfigParser;

    /// <summary>
    /// Содержит тесты проверки установки соединения.
    /// </summary>
    [TestFixture(
        Category = "Smoke",
        Description = "Проверка установки соединения",
        Author = "agalkin")]
    public class Test_Connect : A0Base
    {
        /// <summary>
        /// Признак установки соединения с БД А0.
        /// </summary>
        private bool connectionSuccess;

        /// <summary>
        /// Получает или устанавливает время ожидания установки соединения.
        /// </summary>
        protected int TimeOutMS { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            IA0ConnectionConfig config = null;
            string error = null;
            try
            {
                config = GetA0ConnectionConfig(this.XmlText);
            }
            catch (System.Runtime.Serialization.SerializationException ex)
            {
                error = "Ошибка наименования элементов в xml-файле пользовательских настроек" + ex.Message;
            }
            catch (System.Exception ex)
            {
                error = "Ошибка чтения xml-файла пользовательских настроек" + ex.Message;
            }

            Assert.NotNull(config, error);
            this.TimeOutMS = config.ConnectTimeoutMS;
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            // Интерфейс A0 после теста должен остаться.
            Assert.NotNull(this.A0, "Ожидается A0 после теста");

            if (connectionSuccess)
            {
                this.A0.Disconnect();
            }

            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность метода подключения с тремя параметрами.
        /// </summary>
        [Test(Description = "Проверка установки соединения 3"), Timeout(15000)]
        public void Connect3()
        {
            // Установка соединения с БД А0.
            EConnectReturnCode result = this.A0.Connect3(this.Config.ConnectionString, this.Config.UserName, this.Config.Password);
            Assert.AreEqual(EConnectReturnCode.crcSuccess, result, $"Не могу установить соединение с БД А0. Код возврата {result}");
            this.connectionSuccess = true;

            // Пауза для ожидания установки соединения.
            Thread.Sleep(this.TimeOutMS);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода подключения с двумя параметрами.
        /// </summary>
        [Test(Description = "Проверка установки соединения 4"), Timeout(15000)]
        public void Connect4()
        {
            // Установка соединения с БД А0.
            this.A0.Connect4(this.Config.UserName, this.Config.Password);

            this.connectionSuccess = true;

            // Пауза для ожидания установки соединения.
            Thread.Sleep(this.TimeOutMS);
        }
    }

    /// <summary>
    /// Содержит тесты проверки установки параллельных соединений.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Проверка установки соединений",
        Author = "agalkin")]
    public class Test_MultiConnect : BaseConfig
    {
        /// <summary>
        /// Получает или устанавливает количество одновременно устанавливаемых соединений.
        /// </summary>
        protected int Count { get; private set; }

        /// <summary>
        /// Получает или устанавливает время ожидания установки соединения.
        /// </summary>
        protected int TimeOutMS { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            IA0MultiConnectionConfig config = null;
            string error = null;
            try
            {
                config = GetA0MultiConnectionConfig(this.XmlText);
            }
            catch (System.Runtime.Serialization.SerializationException ex)
            {
                error = "Ошибка наименования элементов в xml-файле пользовательских настроек" + ex.Message;
            }
            catch (System.Exception ex)
            {
                error = "Ошибка чтения xml-файла пользовательских настроек" + ex.Message;
            }

            Assert.NotNull(config, error);
            this.Count = config.MultiConnectCount;
            this.TimeOutMS = config.MultiConnectTimeoutMS;
        }

        /// <summary>
        /// Проверяет работоспособность установки нескольких соединений.
        /// </summary>
        [Test(Description = "Проверка установки нескольких соединений 3"), Timeout(20000)]
        public void MultiConnect3()
        {
            Parallel.For(0, this.Count, (int i) =>
            {
                // Создание A0 API.
                API a0 = new API();
                try
                {
                    // Установка соединения с БД А0.
                    EConnectReturnCode result = a0.Connect3(this.Config.ConnectionString, this.Config.UserName, this.Config.Password);
                    Assert.AreEqual(EConnectReturnCode.crcSuccess, result, $"Не могу установить соединение с БД А0. Код возврата {result}");

                    // Пауза для ожидания установки соединения.
                    Thread.Sleep(this.TimeOutMS);

                    // Разъединение с БД А0.
                    a0.Disconnect();
                }
                finally
                {
                    Marshal.ReleaseComObject(a0);
                    a0 = null;
                }
            });
        }

        /// <summary>
        /// Проверяет работоспособность установки нескольких соединений.
        /// </summary>
        [Test(Description = "Проверка установки нескольких соединений 4"), Timeout(20000)]
        public void MultiConnect4()
        {
            Parallel.For(0, this.Count, (int i) =>
            {
                // Создание A0 API
                API a0 = new API();
                try
                {
                    // Установка соединения с БД А0
                    a0.Connect4(this.Config.UserName, this.Config.Password);

                    // Пауза для ожидания установки соединения.
                    Thread.Sleep(this.TimeOutMS);

                    // Разъединение с БД А0.
                    a0.Disconnect();
                }
                finally
                {
                    Marshal.ReleaseComObject(a0);
                    a0 = null;
                }
            });
        }
    }
}