// $Date: 2020-08-17 15:31:37 +0300 (Пн, 17 авг 2020) $
// $Revision: 373 $
// $Author: agalkin $
// Базовые тесты IConnection

namespace A0Tests.Smoke.App
{
    using A0Service;
    using ADODB;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит базовые тесты проверки работоспособности соединения с БД.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IConnection",
        Author = "agalkin")]
    public class Test_IConnection : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает значение IConnection.
        /// </summary>
        protected IConnection Connection { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Connection = this.A0.App.Connection;
            Assert.NotNull(this.Connection);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Connection);
            this.Connection = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет установлено ли соединение с БД.
        /// </summary>
        [Test]
        public void Test_Connected()
        {
            Assert.IsTrue(this.Connection.Connected);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращение к основному соединению с БД.
        /// </summary>
        [Test]
        public void Test_DBVersion()
        {
            Assert.NotNull(this.Connection.MainConnection);
            _Connection conn = this.Connection.MainConnection as _Connection;
            Assert.NotNull(conn);
        }
    }
}