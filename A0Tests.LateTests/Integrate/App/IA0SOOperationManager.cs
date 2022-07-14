// $Date: 2020-12-30 17:37:30 +0300 (Ср, 30 дек 2020) $
// $Revision: 483 $
// $Author: agalkin $
// Тесты IA0SOOperationManager

namespace A0Tests.LateTests.Integrate.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности IA0SOOperationManager.
    /// </summary>
    [TestFixture(Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0SOOperationManager",
        Author = "agalkin")]
    public class Test_IA0SOOperationManager : Test_NewAct
    {
        /// <summary>
        /// Получает или устанавливает значение IA0SOOperationManager.
        /// </summary>
        protected dynamic Operation { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Operation = this.A0.App.Admin.OperationManager;
            Assert.NotNull(this.Operation);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Operation);
            this.Operation = null;
            base.TearDown();
        }

        /// <summary>
        /// Тестирует операцию предоставления доступа к проекту.
        /// </summary>
        [Test(Description = "Предоставление доступа к проекту")]
        public void Test_AllowAccessProject()
        {
            // Значение 2 соответствует EA0ObjectKind.okProject.
            this.Test(this.Proj.ID.GUID, 2, this.Proj.ID.ID);
        }

        /// <summary>
        /// Тестирует операцию предоставления доступа к объектной смете.
        /// </summary>
        [Test(Description = "Предоставление доступа к ОС")]
        public void Test_AllowAccessOS()
        {
            // Значение 3 соответствует EA0ObjectKind.okOS.
            this.Test(this.OS.ID.GUID, 3, this.OS.ID.ID);
        }

        /// <summary>
        /// Тестирует операцию предоставления доступа к локальной смете.
        /// </summary>
        [Test(Description = "Предоставление доступа к ЛС")]
        public void Test_AllowAccessLS()
        {
            // Значение 4 соответствует EA0ObjectKind.okLS.
            this.Test(this.LS.ID.GUID, 4, this.LS.ID.ID);
        }

        /// <summary>
        /// Тестирует операцию предоставления доступа к акту.
        /// </summary>
        [Test(Description = "Предоставление доступа к акту")]
        public void Test_AllowAccessAct()
        {
            // Значение 5 соответствует EA0ObjectKind.okAct.
            this.Test(this.Act.ID.GUID, 5, this.Act.ID.ID);
        }

        /// <summary>
        /// Тестируюет доступ группы GID к объекту GUID типа Kind.
        /// </summary>
        private bool CheckAccess(Guid guid, dynamic kind, int groupId)
        {
            dynamic groups = this.Operation.GetGroups(guid, kind);

            Dictionary<int, dynamic> a0Groups = new Dictionary<int, dynamic>();
            for (int i = 0; i < groups.Count; i++)
            {
                a0Groups.Add(groups.Item[i].ID, groups.Item[i]);
            }

            return a0Groups.ContainsKey(groupId);
        }

        /// <summary>
        /// Получает новую группу, которой ещё не предоставлен доступ.
        /// </summary>
        private dynamic GetNewGroup(Guid guid, dynamic kind, int id)
        {
            // Группы в системе. Из них возмем новую группу для предоставления доступа.
            Dictionary<int, dynamic> a0Groups = new Dictionary<int, dynamic>();
            for (int i = 0; i < this.A0.App.Admin.Groups.Count; i++)
            {
                a0Groups.Add(this.A0.App.Admin.Groups.Item[i].ID, this.A0.App.Admin.Groups.Item[i]);
            }

            // Группы к которым уже предоставлен доступ.
            dynamic groups = this.Operation.GetGroups(guid, kind);
            dynamic sameGroups = this.Operation.GetGroups2(id.ToString());
            Assert.AreEqual(groups, sameGroups);

            // Удаляем из общего списка групп те которым уже предоставлен доступ.
            for (int i = 0; i < groups.Count; i++)
            {
                if (a0Groups.ContainsKey(groups.Item[i].ID))
                {
                    a0Groups.Remove(groups.Item[i].ID);
                }
            }

            // Этой группе будет предоставлен доступ.
            dynamic group = a0Groups.Values.FirstOrDefault();
            Assert.NotNull(group, "Не могу найти новую группу для назначения прав");

            return group;
        }

        /// <summary>
        /// Проверяет предоставление доступа к объекту GUID для группы GID.
        /// </summary>
        private void TestAllowAccess(Guid guid, dynamic kind, int groupId)
        {
            // Тестируемая операция.
            this.Operation.AllowAccess(guid, kind, groupId, true);

            // Проверка доступа.
            Assert.IsTrue(this.CheckAccess(guid, kind, groupId), "Новой группе не предоставлен доступ");
        }

        /// <summary>
        /// Проверяет предоставление доступа к объекту GUID для группы GID.
        /// </summary>
        private void TestAllowAccess2(Guid guid, dynamic kind, int id, int groupId)
        {
            // Тестируемая операция.
            this.Operation.AllowAccess2(id.ToString(), groupId, true);

            // Проверка доступа.
            Assert.IsTrue(this.CheckAccess(guid, kind, groupId), "Новой группе не предоставлен доступ");
        }

        /// <summary>
        /// Проверяет запрет доступа к объекту GUID для группы GID.
        /// </summary>
        private void TestDenyAccess(Guid guid, dynamic kind, int groupId)
        {
            // Тестируемая операция
            this.Operation.DenyAccess(guid, kind, groupId, true);

            // Проверка доступа
            Assert.IsFalse(this.CheckAccess(guid, kind, groupId), "Доступ группе должен быть запрещен");
        }

        /// <summary>
        /// Проверяет запрет доступа к объекту GUID для группы GID.
        /// </summary>
        private void TestDenyAccess2(Guid guid, dynamic kind, int id, int groupId)
        {
            // Тестируемая операция
            this.Operation.DenyAccess2(id.ToString(), groupId, true);

            // Проверка доступа
            Assert.IsFalse(this.CheckAccess(guid, kind, groupId), "Доступ группе должен быть запрещен");
        }

        /// <summary>
        /// Проверяет передачу объекта GUID в собственность группе GID.
        /// </summary>
        private void TestPassOwner(Guid guid, dynamic kind, int id, int groupId)
        {
            // Текущий собственник
            int groupOwner = this.Operation.GetGroupOwner(guid, kind);
            int sameGroupOwner = this.Operation.GetGroupOwner2(id.ToString());
            Assert.AreEqual(groupOwner, sameGroupOwner);

            Assert.AreNotEqual(groupOwner, groupId, "Необходимо, чтобы новая группа не была собственником");

            // Тестируемая операция
            this.Operation.PassOwner(guid, kind, true, groupId);
            this.Operation.PassOwner2(id.ToString(), true, groupId);

            // Проверка
            int newGroupOwner = this.Operation.GetGroupOwner(guid, kind);
            Assert.AreEqual(newGroupOwner, groupId, "Собственность не передана группе");
        }

        /// <summary>
        /// Проверяет операции предоставления, запрета доступа и передачи собственности.
        /// </summary>
        private void Test(Guid guid, dynamic kind, int id)
        {
            // Этой группе будет предоставлен доступ.
            dynamic group = this.GetNewGroup(guid, kind, id);

            // Тестирование предоставления доступа.
            this.TestAllowAccess(guid, kind, group.ID);

            // Тестирование запрета доступа.
            this.TestDenyAccess(guid, kind, group.ID);

            // Тестирование предоставления доступа.
            this.TestAllowAccess2(guid, kind, id, group.ID);

            // Тестирование запрета доступа.
            this.TestDenyAccess2(guid, kind, id, group.ID);

            // Ещё раз предоставим доступ для передачи собственности
            this.TestAllowAccess(guid, kind, group.ID);

            // Тестирование передачи владения другой группе
            this.TestPassOwner(guid, kind, id, group.ID);
        }
    }
}