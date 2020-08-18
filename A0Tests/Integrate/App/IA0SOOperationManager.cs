// $Date: 2020-07-21 11:15:34 +0300 (Вт, 21 июл 2020) $
// $Revision: 311 $
// $Author: agalkin $
// Тесты IA0SOOperationManager

namespace A0Tests.Integrate.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности IA0SOOperationManager.
    /// </summary>
    [TestFixture(Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0SOOperationManager",
        Author = "agalkin")]
    public class Test_IA0SOOperationManager : NewAct
    {
        /// <summary>
        /// Получает или устанавливает значение IA0SOOperationManager.
        /// </summary>
        protected IA0SOOperationManager Operation { get; private set; }

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
            this.Test(this.Proj.ID.GUID, EA0ObjectKind.okProject, this.Proj.ID.ID);
        }

        /// <summary>
        /// Тестирует операцию предоставления доступа к объектной смете.
        /// </summary>
        [Test(Description = "Предоставление доступа к ОС")]
        public void Test_AllowAccessOS()
        {
            this.Test(this.OS.ID.GUID, EA0ObjectKind.okOS, this.OS.ID.ID);
        }

        /// <summary>
        /// Тестирует операцию предоставления доступа к локальной смете.
        /// </summary>
        [Test(Description = "Предоставление доступа к ЛС")]
        public void Test_AllowAccessLS()
        {
            this.Test(this.LS.ID.GUID, EA0ObjectKind.okLS, this.LS.ID.ID);
        }

        /// <summary>
        /// Тестирует операцию предоставления доступа к акту.
        /// </summary>
        [Test(Description = "Предоставление доступа к акту")]
        public void Test_AllowAccessAct()
        {
            this.Test(this.Act.ID.GUID, EA0ObjectKind.okAct, this.Act.ID.ID);
        }

        /// <summary>
        /// Тестируюет доступ группы GID к объекту GUID типа Kind.
        /// </summary>
        private bool CheckAccess(Guid guid, EA0ObjectKind kind, int groupId)
        {
            IA0Groups groups = this.Operation.GetGroups(guid, kind);

            Dictionary<int, IISGroup> a0Groups = new Dictionary<int, IISGroup>();
            for (int i = 0; i < groups.Count; i++)
            {
                a0Groups.Add(groups.Item[i].ID, groups.Item[i]);
            }

            return a0Groups.ContainsKey(groupId);
        }

        /// <summary>
        /// Получает новую группу, которой ещё не предоставлен доступ.
        /// </summary>
        private IISGroup GetNewGroup(Guid guid, EA0ObjectKind kind, int id)
        {
            // Группы в системе. Из них возмем новую группу для предоставления доступа.
            Dictionary<int, IISGroup> a0Groups = new Dictionary<int, IISGroup>();
            for (int i = 0; i < this.A0.App.Admin.Groups.Count; i++)
            {
                a0Groups.Add(this.A0.App.Admin.Groups.Item[i].ID, this.A0.App.Admin.Groups.Item[i]);
            }

            // Группы к которым уже предоставлен доступ.
            IA0Groups groups = this.Operation.GetGroups(guid, kind);
            IA0Groups sameGroups = this.Operation.GetGroups2(id.ToString());
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
            IISGroup group = a0Groups.Values.FirstOrDefault();
            Assert.NotNull(group, "Не могу найти новую группу для назначения прав");

            return group;
        }

        /// <summary>
        /// Проверяет предоставление доступа к объекту GUID для группы GID.
        /// </summary>
        private void TestAllowAccess(Guid guid, EA0ObjectKind kind, int groupId)
        {
            // Тестируемая операция.
            this.Operation.AllowAccess(guid, kind, groupId, true);

            // Проверка доступа.
            Assert.IsTrue(this.CheckAccess(guid, kind, groupId), "Новой группе не предоставлен доступ");
        }

        /// <summary>
        /// Проверяет предоставление доступа к объекту GUID для группы GID.
        /// </summary>
        private void TestAllowAccess2(Guid guid, EA0ObjectKind kind, int id, int groupId)
        {
            // Тестируемая операция.
            this.Operation.AllowAccess2(id.ToString(), groupId, true);

            // Проверка доступа.
            Assert.IsTrue(this.CheckAccess(guid, kind, groupId), "Новой группе не предоставлен доступ");
        }

        /// <summary>
        /// Проверяет запрет доступа к объекту GUID для группы GID.
        /// </summary>
        private void TestDenyAccess(Guid guid, EA0ObjectKind kind, int groupId)
        {
            // Тестируемая операция
            this.Operation.DenyAccess(guid, kind, groupId, true);

            // Проверка доступа
            Assert.IsFalse(this.CheckAccess(guid, kind, groupId), "Доступ группе должен быть запрещен");
        }

        /// <summary>
        /// Проверяет запрет доступа к объекту GUID для группы GID.
        /// </summary>
        private void TestDenyAccess2(Guid guid, EA0ObjectKind kind, int id, int groupId)
        {
            // Тестируемая операция
            this.Operation.DenyAccess2(id.ToString(), groupId, true);

            // Проверка доступа
            Assert.IsFalse(this.CheckAccess(guid, kind, groupId), "Доступ группе должен быть запрещен");
        }

        /// <summary>
        /// Проверяет передачу объекта GUID в собственность группе GID.
        /// </summary>
        private void TestPassOwner(Guid guid, EA0ObjectKind kind, int id, int groupId)
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
        private void Test(Guid guid, EA0ObjectKind kind, int id)
        {
            // Этой группе будет предоставлен доступ.
            IISGroup group = this.GetNewGroup(guid, kind, id);

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