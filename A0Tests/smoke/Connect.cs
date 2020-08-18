// $Date: 2020-08-17 15:35:09 +0300 (Пн, 17 авг 2020) $
// $Revision: 374 $
// $Author: agalkin $
// Тесты проверки соединений с A0Service

namespace A0Tests.Smoke
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки установки соединения.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Проверка установки соединения",
        Author = "agalkin")]
    public class Test_Connect : A0Base
    {
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

            // Получение значения из файла пользовательских настроек.
            string timeOut = this.Configuration?.Descendants("testConnectTimeOutMS")?.SingleOrDefault()?.Value;
            Assert.True(int.TryParse(timeOut, out int result));
            this.TimeOutMS = result;
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            // Интерейс A0 после теста должен остаться.
            Assert.NotNull(this.A0, "Ожидается A0 после теста");

            this.A0.Disconnect();

            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность метода подключения с тремя параметрами.
        /// </summary>
        [Test(Description = "Проверка установки соединения 3")]
        public void Connect3()
        {
            // Установка соединения с БД А0.
            EConnectReturnCode result = this.A0.Connect3(this.ConnStr, this.UserName, this.Password);
            if (result != EConnectReturnCode.crcSuccess)
            {
                throw new Exception(string.Format("Не могу установить соединение с БД А0. Код возврата {0}", result));
            }

            // Пауза для ожидания установки соединения.
            Thread.Sleep(this.TimeOutMS);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода подключения с двумя параметрами.
        /// </summary>
        [Test(Description = "Проверка установки соединения 4")]
        public void Connect4()
        {
            // Установка соединения с БД А0.
            this.A0.Connect4(this.UserName, this.Password);

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
    public class Test_MultiConnect : A0Config
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

            // Получение значений из файла пользовательских настроек.
            string count = this.Configuration?.Descendants("testMultiConnectCount")?.SingleOrDefault()?.Value;
            Assert.True(int.TryParse(count, out int result));
            this.Count = result;
            string timeOut = this.Configuration?.Descendants("testMultiConnectTimeOutMS")?.SingleOrDefault()?.Value;
            Assert.True(int.TryParse(timeOut, out result));
            this.TimeOutMS = result;
        }

        /// <summary>
        /// Проверяет работоспособность установки нескольких соединений.
        /// </summary>
        [Test(Description = "Проверка установки нескольких соединений 3")]
        public void MultiConnect3()
        {
            Parallel.For(0, this.Count, (int i) =>
            {
                // Создание A0 API.
                API a0 = new API();
                try
                {
                    // Установка соединения с БД А0.
                    EConnectReturnCode result = a0.Connect3(this.ConnStr, this.UserName, this.Password);

                    if (result != EConnectReturnCode.crcSuccess)
                    {
                        throw new Exception(string.Format("Не могу установить соединение с БД А0. Код возврата {0}", result));
                    }

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
        [Test(Description = "Проверка установки нескольких соединений 4")]
        public void MultiConnect4()
        {
            Parallel.For(0, this.Count, (int i) =>
            {
                // Создание A0 API
                API a0 = new API();
                try
                {
                    // Установка соединения с БД А0
                    a0.Connect4(this.UserName, this.Password);

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