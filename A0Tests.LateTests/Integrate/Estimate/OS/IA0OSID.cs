// $Date: 2020-12-18 16:57:27 +0300 (Пт, 18 дек 2020) $
// $Revision: 462 $
// $Author: agalkin $
// Тесты ИД ОС

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности IA0OSID.
    /// </summary>
    public class Test_IA0OSID : Test_NewOS
    {
        /// <summary>
        /// Получает или устанавливает значение IA0OSID.
        /// </summary>
        protected dynamic OSID { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.OSID = this.OS.ID;
            Assert.NotNull(this.OSID);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.OSID);
            this.OSID = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет остутствие ошибок при обращении к Guid ОС.
        /// </summary>
        [Test]
        public void Test_GUID()
        {
            Guid guid = this.OSID.GUID;
        }

        /// <summary>
        /// Проверяет остутствие ошибок при обращении к Id ОС.
        /// </summary>
        [Test]
        public void Test_ID()
        {
            int id = this.OSID.ID;
        }

        /// <summary>
        /// Проверяет присвоенный тип сметного объекта.
        /// </summary>
        [Test]
        public void Test_Kind()
        {
            dynamic kind = this.OSID.Kind;

            // Значение 3 соответствует EA0ObjectKind.okOS.
            Assert.AreEqual(3, kind);
        }

        /// <summary>
        /// Проверяет родительский объект ОС.
        /// </summary>
        [Test]
        public void Test_Parent()
        {
            dynamic parent = this.OSID.Parent;
            Assert.NotNull(parent);

            // Проверка типа.
            // Значение 2 соответствует EA0ObjectKind.okProject.
            Assert.AreEqual(parent.Kind, 2);
        }

        /// <summary>
        /// Проверяет Id родительского узла ОС.
        /// </summary>
        [Test]
        public void Test_ParentNodeID()
        {
            int parentNode = this.OSID.ParentNodeID;
            Assert.NotNull(parentNode);

            // Проверка нахождения в правильном узле.
            Assert.AreEqual(parentNode, this.Proj.Tree.Head.ID);
        }

        /// <summary>
        /// Проверяет Guid родительского проекта.
        /// </summary>
        [Test]
        public void Test_ProjGUID()
        {
            Guid projGuid = this.OSID.ProjGUID;
            Assert.AreEqual(this.Proj.ID.GUID, projGuid);
        }

        /// <summary>
        /// Проверяет Id родительского проекта.
        /// </summary>
        [Test]
        public void Test_ProjID()
        {
            int projID = this.OSID.ProjID;
            Assert.AreEqual(this.Proj.ID.ID, projID);
        }
    }
}