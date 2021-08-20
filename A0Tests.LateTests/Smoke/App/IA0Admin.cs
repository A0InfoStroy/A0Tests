﻿// $Date: 2020-12-07 16:50:47 +0300 (Пн, 07 дек 2020) $
// $Revision: 449 $
// $Author: agalkin $
// Базовые тесты IA0Admin

namespace A0Tests.LateTests.Smoke.App
{
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
        protected dynamic Admin { get; private set; }

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
        [Test]
        public void Test_Groups()
        {
            Assert.NotNull(this.Admin.Groups);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к менеджеру операций (видимость/собственность) с объектами A0 для РД.
        /// </summary>
        [Test]
        public void Test_OperationManager()
        {
            Assert.NotNull(this.Admin.OperationManager);
        }

        /// <summary>
        /// Проверяет тестовое исключение при подтверждении сессии.
        /// </summary>
        [Test]
        public void Test_ConfirmSession()
        {
            this.Admin.TestConfirmSession();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода освобождающего сессию.
        /// </summary>
        [Test]
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
        protected dynamic Groups { get; private set; }

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
        [Test]
        public void Test_Count()
        {
            Assert.Greater(this.Groups.Count, 0);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям группы пользователей.
        /// </summary>
        [Test]
        public void Test_Items()
        {
            int count = this.Groups.Count;
            Assert.Greater(count, 0);

            for (int i = 0; i < count; i++)
            {
                dynamic groupItem = this.Groups.Item[i];
                Assert.NotNull(groupItem);
                string comment = groupItem.Comment;
                string groupName = groupItem.GroupName;
                Assert.NotNull(groupName, "GroupName");
                int id = groupItem.ID;
            }
        }
    }
}