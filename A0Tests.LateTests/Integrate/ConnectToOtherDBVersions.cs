// $Date: 2020-12-14 13:47:13 +0300 (Пн, 14 дек 2020) $
// $Revision: 456 $
// $Author: agalkin $
// Тесты проверки соединений с A0Service

namespace A0Tests.LateTests.Integrate
{
    using System.Data.OleDb;
    using System.Runtime.InteropServices;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки установки соединения с версией базы отличной от рабочей.
    /// </summary>
    [TestFixture(
        Category = "Itegrate",
        Description = "Проверка установки соединения с устаревшей версией БД",
        Author = "agalkin")]
    public class Test_ConnectToOtherDBVersions : A0Base
    {
        /// <summary>
        /// Получает номер текущей версии БД.
        /// </summary>
        protected int CurrentMinorVersion { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            // Соединение с БД для получения данных в обход A0Service.
            using (OleDbConnection connection = new OleDbConnection(this.Config.ConnectionString))
            {
                connection.Open();

                // Запрос на получение текущей версии БД.
                using (OleDbCommand selectMinorVersion = new OleDbCommand("SELECT MinorVersion FROM A0VersionInfo", connection))
                using (OleDbDataReader reader = selectMinorVersion.ExecuteReader())
                {
                    Assert.True(reader.HasRows);

                    while (reader.Read())
                    {
                        this.CurrentMinorVersion = reader.GetInt32(0);
                    }
                }
            }
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            // Возвращаем текущую версию БД.
            this.UpdateDBVersionToCurrent();

            // Интерфейс A0 после теста должен остаться.
            Assert.NotNull(this.A0, "Ожидается A0 после теста");

            this.A0.Disconnect();
            base.TearDown();
        }

        /// <summary>
        /// Проверяет результат подключения при попытке подключиться к версии БД отличной от рабочей.
        /// </summary>
        [Test(Description = "Проверка установки соединения 3")]
        public void Connect3()
        {
            this.UpdateDBVersionToInitial();
            dynamic result = this.A0.Connect3(this.Config.ConnectionString, this.Config.UserName, this.Config.Password);

            // Значение 6 соответствует EConnectReturnCode.crcCheckDBError.
            int errorCode = 6;
            Assert.AreEqual(errorCode, result);
        }

        /// <summary>
        /// Проверяет обработку исключения при попытке подключиться к версии БД отличной от рабочей.
        /// </summary>
        [Test(Description = "Проверка установки соединения 4")]
        public void Connect4()
        {
            this.UpdateDBVersionToInitial();
            try
            {
                this.A0.Connect4(this.Config.ConnectionString, this.Config.UserName);
            }
            catch (COMException ex)
            {
                Assert.AreEqual(-2147418113, ex.HResult);
            }
        }

        /// <summary>
        /// Изменяет версию БД на первоначальную.
        /// </summary>
        private void UpdateDBVersionToInitial()
        {
            // Соединение с БД для получения данных в обход A0Service.
            using (OleDbConnection connection = new OleDbConnection(this.Config.ConnectionString))
            {
                connection.Open();

                // Изменение версии БД на первоначальную.
                using (OleDbCommand updateDB = new OleDbCommand("UPDATE A0VersionInfo SET MinorVersion = 1", connection))
                {
                    updateDB.ExecuteScalar();
                }
            }

        }

        /// <summary>
        /// Изменяет версию БД на прежнее значение.
        /// </summary>
        private void UpdateDBVersionToCurrent()
        {
            // Соединение с БД для получения данных в обход A0Service.
            using (OleDbConnection connection = new OleDbConnection(this.Config.ConnectionString))
            {
                connection.Open();

                // Изменение версии БД на текущую.
                using (OleDbCommand updateDB = new OleDbCommand($"UPDATE A0VersionInfo SET MinorVersion = {this.CurrentMinorVersion}", connection))
                {
                    updateDB.ExecuteScalar();
                }
            }

        }
    }
}