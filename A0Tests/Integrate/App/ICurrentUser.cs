// $Date: 2020-07-20 12:38:56 +0300 (Пн, 20 июл 2020) $
// $Revision: 309 $
// $Author: agalkin $
// Тесты CurrentUser.
// Планируется, что ICurrentUser переедет в IApp

namespace A0Tests.Integrate.App
{
    using System;
    using A0Service;
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
        /// Тестирует права доступа для создания комплекса.
        /// </summary>
        [Test(Description = "Проверка прав доступа Create")]
        public void Test_CanDoItComplexCreate()
        {
            IA0ObjectIterator it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                IA0Object item = it.Item;
                bool result = this.CurrentUser.CanDoIt(EA0OperKind.opCreate, item.ID.GUID, EA0EntityKind.ekComplex);
            }
        }

        /// <summary>
        /// Тестирует права доступа для удаления комплекса.
        /// </summary>
        [Test(Description = "Проверка прав доступа Delete")]
        public void Test_CanDoItComplexDelete()
        {
            IA0ObjectIterator it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                IA0Object item = it.Item;
                bool result = this.CurrentUser.CanDoIt(EA0OperKind.opDelete, item.ID.GUID, EA0EntityKind.ekComplex);
            }
        }

        /// <summary>
        /// Тестирует права доступа для редактирования комплекса.
        /// </summary>
        [Test(Description = "Проверка прав доступа Edit")]
        public void Test_CanDoItComplexEdit()
        {
            IA0ObjectIterator it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                IA0Object item = it.Item;
                bool result = this.CurrentUser.CanDoIt(EA0OperKind.opEdit, item.ID.GUID, EA0EntityKind.ekComplex);
            }
        }

        /// <summary>
        /// Тестирует права доступа View комплекса.
        /// </summary>
        [Test(Description = "Проверка прав доступа View")]
        public void Test_CanDoItComplexView()
        {
            IA0ObjectIterator it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                IA0Object item = it.Item;
                bool result = this.CurrentUser.CanDoIt(EA0OperKind.opView, item.ID.GUID, EA0EntityKind.ekComplex);
            }
        }

        /// <summary>
        /// Тестирует блокировку/разблокировку редактирования.
        /// </summary>
        [Test(Description = "Проверка Блокировки/Разблокировки сущности Edit")]
        public void Test_LockUnLockEdit()
        {
            IA0ObjectIterator it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                IA0Object item = it.Item;
                int result = this.CurrentUser.Lock(EA0EntityKind.ekComplex, EAccessKind.akEdit, item.ID.GUID);
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
            IA0ObjectIterator it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                var item = it.Item;
                int result = this.CurrentUser.Lock(EA0EntityKind.ekComplex, EAccessKind.akRead, item.ID.GUID);
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
            IA0ObjectIterator it = this.A0.Estimate.Repo.ComplexID.Read(null);
            while (it.Next())
            {
                IA0Object item = it.Item;
                int result = this.CurrentUser.Lock(EA0EntityKind.ekComplex, EAccessKind.akDelete, item.ID.GUID);
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
            bool result = this.CurrentUser.CanDoIt(EA0OperKind.opView, Guid.Parse("{4D83FCDF-E9A4-45C9-B9A8-ADA0A0553CA8}"), EA0EntityKind.ekAct);
            Assert.False(result);
        }
    }
}