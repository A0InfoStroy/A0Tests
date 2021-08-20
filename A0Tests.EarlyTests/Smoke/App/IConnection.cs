// $Date: 2021-01-19 15:20:11 +0300 (Вт, 19 янв 2021) $
// $Revision: 500 $
// $Author: agalkin $
// Базовые тесты IConnection

namespace A0Tests.EarlyTests.Smoke.App
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