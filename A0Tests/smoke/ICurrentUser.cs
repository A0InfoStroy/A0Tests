// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты ICurrentUser

namespace A0Tests.Smoke
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовые тесты проверки работоспособности пользователя системы разделения доступа.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности ICurrentUser",
        Author = "agalkin")]
    public class Test_ICurrentUser : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает пользователя системы разделения доступа.
        /// </summary>
        protected ICurrentUser CurrentUser { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.CurrentUser = this.A0.CurrentUser;
            Assert.NotNull(this.CurrentUser);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.CurrentUser);
            this.CurrentUser = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения Id сессии.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_ACExitNotify()
        {
            string sessionID = this.CurrentUser.SessionID;
            Assert.NotNull(sessionID);
            Assert.IsTrue(sessionID.Length > 0);
        }
    }
}