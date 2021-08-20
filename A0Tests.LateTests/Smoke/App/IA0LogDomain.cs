// $Date: 2020-12-11 14:53:06 +0300 (Пт, 11 дек 2020) $
// $Revision: 454 $
// $Author: agalkin $
// Базовые тесты IA0LogDomain

namespace A0Tests.LateTests.Smoke.App
{
    using NUnit.Framework;

    /// <summary>
    ///  Базовые тесты проверки работоспособности домена протокола А0.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0LogDomain",
        Author = "agalkin")]
    public class Test_IA0LogDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен протокола А0.
        /// </summary>
        protected dynamic LogDomain { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LogDomain = this.A0.App.Log;
            Assert.NotNull(this.LogDomain);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LogDomain);
            this.LogDomain = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку ведения записи лога в файл.
        /// </summary>
        [Test]
        public void Test_Active()
        {
            bool active = this.LogDomain.Active;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к событию протокола.
        /// </summary>
        [Test]
        public void Test_Event()
        {
            dynamic logEvent = this.LogDomain.Event;
        }

        /// <summary>
        /// Проверяет работоспособность чтения имени файла протокола.
        /// </summary>
        [Test]
        public void Test_FileName()
        {
            string fileName = this.LogDomain.FileName;
            Assert.NotNull(fileName);
            Assert.IsTrue(fileName.Length > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения протокола.
        /// </summary>
        [Test]
        public void Test_Log()
        {
            dynamic log = this.LogDomain.Log;
            Assert.NotNull(log);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи включения/выключения трассировки.
        /// </summary>
        [Test]
        public void Test_TraceMessages()
        {
            bool traceMessages = this.LogDomain.TraceMessages;
            Assert.True(this.LogDomain.TraceMessages);
            this.LogDomain.TraceMessages = false;
            Assert.False(this.LogDomain.TraceMessages);
            this.LogDomain.TraceMessages = traceMessages;
        }
    }

    /// <summary>
    ///  Базовые тесты проверки работоспособности протокола А0.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0Log",
        Author = "agalkin")]
    public class Test_IA0Log : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает протокол А0.
        /// </summary>
        protected dynamic Log { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Log = this.A0.App.Log.Log;
            Assert.NotNull(this.Log);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Log);
            this.Log = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода добавления в протокол записи об ошибке.
        /// </summary>
        [Test]
        public void Test_Error()
        {
            this.Log.Error(null);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода добавления в протокол записи об исключении.
        /// </summary>
        [Test]
        public void Test_ExceptLog()
        {
            this.Log.ExceptLog(null);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода добавления в протокол записи.
        /// </summary>
        [Test]
        public void Test_Message()
        {
            this.Log.Message(null);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода добавления в протокол записи о трассировке.
        /// </summary>
        [Test]
        public void Test_Trace()
        {
            this.Log.Trace(null);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода добавления в протокол записи о предупреждении.
        /// </summary>
        [Test]
        public void Test_Warning()
        {
            this.Log.Warning(null);
        }
    }
}