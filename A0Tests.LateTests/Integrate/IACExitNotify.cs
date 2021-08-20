// $Date: 2020-12-31 13:30:35 +0300 (Чт, 31 дек 2020) $
// $Revision: 484 $
// $Author: agalkin $
// Тесты оповещения об ошибке обновления сессии

namespace A0Tests.LateTests.Integrate
{
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности оповещения об ошибке обновления сессии.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IACExitNotify",
        Author = "agalkin")]
    public class Test_IACExitNotify : Test_Base
    {
        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        [TearDown]
        public override void TearDown()
        {
            Assert.NotNull(this.A0.ACExitNotify);
            this.A0.ACExitNotify = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность метода оповещения.
        /// </summary>
        [Test]
        public void Test_OnNotify()
        {
            this.A0.Disconnect();
            this.A0.ACExitNotify = new ExitNotify();
            dynamic result = this.A0.Connect3(this.Config.ConnectionString, this.Config.UserName, "wrongPassword");
            if (result != 0) // 0 - EConnectReturnCode.crcSuccess.
            {
                // 3 - EACExitEnum.ConnectionError.
                this.A0.ACExitNotify.OnNotify(3, "неверный пароль");
            }
        }
    }

    /// <summary>
    /// Содержит метод оповещения об ошибке обновления сессии.
    /// </summary>
    public class ExitNotify
    {
        /// <summary>
        /// Проверяет наличие ошибки соединения.
        /// </summary>
        /// <param name="kind">Код возврата при соединении.</param>
        /// <param name="str">Сообщение.</param>
        public void OnNotify(int kind, string str)
        {
            // 3 - EACExitEnum.ConnectionError.
            if (kind == 3)
            {
                Assert.AreEqual("неверный пароль", str);
                return;
            }

            Assert.True(false);
        }
    }
}