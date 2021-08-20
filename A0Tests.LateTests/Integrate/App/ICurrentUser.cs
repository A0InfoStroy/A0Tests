// $Date: 2020-12-30 17:32:45 +0300 (Ср, 30 дек 2020) $
// $Revision: 482 $
// $Author: agalkin $
// Тесты CurrentUser.
// Планируется, что ICurrentUser переедет в IApp

namespace A0Tests.LateTests.Integrate.App
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности ICurrentUser.
    /// </summary>
    [TestFixture(Category = "Integrate",
        Description = "Тесты проверки работоспособности ICurrentUser",
        Author = "agalkin")]
    public class Test_ICurrentUser : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает текущего пользователя А0.
        /// </summary>
        protected dynamic CurrentUser { get; private set; }

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
        /// Тестирует права доступа для создания комплекса.
        /// </summary>
        [Test(Description = "Проверка прав доступа Create")]
        public void Test_CanDoItComplexCreate()
        {
            dynamic it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                dynamic item = it.Item;

                // 0 - EA0OperKind.opCreate, 0 - EA0EntityKind.ekComplex.
                bool result = this.CurrentUser.CanDoIt(0, item.ID.GUID, 0);
            }
        }

        /// <summary>
        /// Тестирует права доступа для удаления комплекса.
        /// </summary>
        [Test(Description = "Проверка прав доступа Delete")]
        public void Test_CanDoItComplexDelete()
        {
            dynamic it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                dynamic item = it.Item;

                // 1 - EA0OperKind.opDelete, 0 - EA0EntityKind.ekComplex.
                bool result = this.CurrentUser.CanDoIt(1, item.ID.GUID, 0);
            }
        }

        /// <summary>
        /// Тестирует права доступа для редактирования комплекса.
        /// </summary>
        [Test(Description = "Проверка прав доступа Edit")]
        public void Test_CanDoItComplexEdit()
        {
            dynamic it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                dynamic item = it.Item;

                // 2 - EA0OperKind.opDelete, 0 - EA0EntityKind.ekComplex.
                bool result = this.CurrentUser.CanDoIt(2, item.ID.GUID, 0);
            }
        }

        /// <summary>
        /// Тестирует права доступа View комплекса.
        /// </summary>
        [Test(Description = "Проверка прав доступа View")]
        public void Test_CanDoItComplexView()
        {
            dynamic it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                dynamic item = it.Item;

                // 3 - EA0OperKind.opView, 0 - EA0EntityKind.ekComplex.
                bool result = this.CurrentUser.CanDoIt(3, item.ID.GUID, 0);
            }
        }

        /// <summary>
        /// Тестирует блокировку/разблокировку редактирования.
        /// </summary>
        [Test(Description = "Проверка Блокировки/Разблокировки сущности Edit")]
        public void Test_LockUnLockEdit()
        {
            dynamic it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                dynamic item = it.Item;

                // 0 - EA0EntityKind.ekComplex, 1 - EAccessKind.akEdit.
                int result = this.CurrentUser.Lock(0, 1, item.ID.GUID);
                Assert.Greater(result, 0);
                this.CurrentUser.UnLock(result);
            }
        }

        /// <summary>
        /// Тестирует блокировку/разблокировку чтения.
        /// </summary>
        [Test(Description = "Проверка Блокировки/Разблокировки сущности Read")]
        public void Test_LockUnLockRead()
        {
            dynamic it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                var item = it.Item;

                // 0 - EA0EntityKind.ekComplex, 0 - EAccessKind.akRead.
                int result = this.CurrentUser.Lock(0, 0, item.ID.GUID);
                Assert.Greater(result, 0);
                this.CurrentUser.UnLock(result);
            }
        }

        /// <summary>
        /// Тестирует блокировку/разблокировку удаления.
        /// </summary>
        [Test(Description = "Проверка Блокировки/Разблокировки сущности Delete")]
        public void Test_LockUnLockDelete()
        {
            dynamic it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                dynamic item = it.Item;

                // 0 - EA0EntityKind.ekComplex, 2 - EAccessKind.akDelete.
                int result = this.CurrentUser.Lock(0, 2, item.ID.GUID);
                Assert.Greater(result, 0);
                this.CurrentUser.UnLock(result);
            }
        }

        /// <summary>
        /// Тестирует блокировку/разблокировку акта.
        /// </summary>
        [Test(Description = "Проверка Блокировки/Разблокировки акта")]
        public void Test_CanDoItAct()
        {
            // 3 - EA0OperKind.opView, 4 - EA0EntityKind.ekAct.
            bool result = this.CurrentUser.CanDoIt(3, Guid.Parse("{4D83FCDF-E9A4-45C9-B9A8-ADA0A0553CA8}"), 4);
            Assert.False(result);
        }
    }
}