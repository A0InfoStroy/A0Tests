// $Date: 2020-12-01 15:30:34 +0300 (Вт, 01 дек 2020) $
// $Revision: 446 $
// $Author: agalkin $
// Тесты разъединения

namespace A0Tests.LateTests.Smoke
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки разъединения с БД А0.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Проверка разъеднения",
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
            dynamic returnCode = this.A0.Connect3(this.Config.ConnectionString, this.Config.UserName, this.Config.Password);
            Assert.Zero(returnCode, $"Не могу установить соединение с БД А0. Код возврата {returnCode}");
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
                dynamic estimate = this.A0.Estimate;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Отсутствует соединение с БД", ex.Message);
            }

            try
            {
                dynamic estimate = this.A0.Implement;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Отсутствует соединение с БД", ex.Message);
            }
        }
    }
}