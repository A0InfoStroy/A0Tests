// $Date: 2021-01-19 15:20:11 +0300 (Вт, 19 янв 2021) $
// $Revision: 500 $
// $Author: agalkin $
// Тесты разъединения

namespace A0Tests.EarlyTests.Smoke
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки разъединения с БД А0.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Проверка установки соединения",
        Author = "agalkin")]
    public class Test_Disconnect : A0Base
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            // Установка соединения с БД А0.
            EConnectReturnCode returnCode = this.A0.Connect3(this.Config.ConnectionString, this.Config.UserName, this.Config.Password);
            Assert.AreEqual(EConnectReturnCode.crcSuccess, returnCode, $"Не могу установить соединение с БД А0. Код возврата {returnCode}");
        }

        /// <summary>
        /// Проверяет работоспособность метода разъединения с БД А0.
        /// </summary>
        [Test(Description = "Проверка разъединения")]
        public void TestDisconnect()
        {
            this.A0.Disconnect();
            Assert.IsNull(this.A0.CurrentUser);
            Assert.IsNull(this.A0.App);
            Assert.IsNotNull(this.A0.Links);
            Assert.IsNotNull(this.A0.Sys);
            Assert.IsNotNull(this.A0.Entities);
            try
            {
                IA0EstimateDomain estimate = this.A0.Estimate;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Отсутствует соединение с БД", ex.Message);
            }

            try
            {
                IA0ImplementDomain estimate = this.A0.Implement;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Отсутствует соединение с БД", ex.Message);
            }
        }
    }
}