// $Date: 2020-08-07 11:25:11 +0300 (Пт, 07 авг 2020) $
// $Revision: 355 $
// $Author: agalkin $
// Тесты оповещения об ошибке обновления сессии

namespace A0Tests.Integrate
{
    using A0Service;
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
            EConnectReturnCode result = this.A0.Connect3(this.ConnStr, this.UserName, "wrongPassword");
            if (result != EConnectReturnCode.crcSuccess)
            {
                this.A0.ACExitNotify.OnNotify(EACExitEnum.ConnectionError, "неверный пароль");
            }
        }
    }

    /// <summary>
    /// Содержит метод оповещения об ошибке обновления сессии.
    /// </summary>
    public class ExitNotify : IACExitNotify
    {
        /// <summary>
        /// Проверяет наличие ошибки соединения.
        /// </summary>
        /// <param name="kind">Код возврата при соединении.</param>
        /// <param name="str">Сообщение.</param>
        public void OnNotify(EACExitEnum kind, string str)
        {
            if (kind == EACExitEnum.ConnectionError)
            {
                Assert.AreEqual("неверный пароль", str);
                return;
            }

            Assert.True(false);
        }
    }
}