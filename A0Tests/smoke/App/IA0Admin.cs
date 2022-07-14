// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты IA0Admin

namespace A0Tests.Smoke.App
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности административной части.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0Admin",
        Author = "agalkin")]
    public class Test_IA0Admin : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает административную часть.
        /// </summary>
        protected IA0Admin Admin { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Admin = this.A0.App.Admin;
            Assert.NotNull(this.Admin);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Admin);
            this.Admin = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к группам в системе.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_Groups()
        {
            Assert.NotNull(this.Admin.Groups);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к менеджеру операций (видимость/собственность) с объектами A0 для РД.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_OperationManager()
        {
            Assert.NotNull(this.Admin.OperationManager);
        }

        /// <summary>
        /// Проверяет тестовое исключение при подтверждении сессии.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_ConfirmSession()
        {
            this.Admin.TestConfirmSession();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода освобождающего сессию.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_ReleaseSession()
        {
            this.Admin.ReleaseSession(this.A0.CurrentUser.SessionID);
        }
    }

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности списка групп пользователей А0.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0Groups",
        Author = "agalkin")]
    public class Test_IA0Groups : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает список групп пользователей А0.
        /// </summary>
        protected IA0Groups Groups { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Groups = this.A0.App.Admin.Groups;
            Assert.NotNull(this.Groups);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Groups);
            this.Groups = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет наличие групп в списке.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_Count()
        {
            Assert.Greater(this.Groups.Count, 0);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям группы пользователей.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_Items()
        {
            int count = this.Groups.Count;
            Assert.Greater(count, 0);

            for (int i = 0; i < count; i++)
            {
                IISGroup groupItem = this.Groups.Item[i];
                Assert.NotNull(groupItem);
                string comment = groupItem.Comment;
                string groupName = groupItem.GroupName;
                Assert.NotNull(groupName, "GroupName");
                int id = groupItem.ID;
            }
        }
    }
}