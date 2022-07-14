// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты ИД ОС

namespace A0Tests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности IA0OSID.
    /// </summary>
    public class Test_IA0OSID : NewOS
    {
        /// <summary>
        /// Получает или устанавливает значение IA0OSID.
        /// </summary>
        protected IA0OSID OSID { get; set; }

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
        [Test, Timeout(10000)]
        public void Test_GUID()
        {
            Guid guid = this.OSID.GUID;
        }

        /// <summary>
        /// Проверяет остутствие ошибок при обращении к Id ОС.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_ID()
        {
            int id = this.OSID.ID;
        }

        /// <summary>
        /// Проверяет присвоенный тип сметного объекта.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Kind()
        {
            EA0ObjectKind kind = this.OSID.Kind;
            Assert.AreEqual(EA0ObjectKind.okOS, kind);
        }

        /// <summary>
        /// Проверяет родительский объект ОС.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Parent()
        {
            IA0ObjectID parent = this.OSID.Parent;
            Assert.NotNull(parent);

            // Проверка типа.
            Assert.AreEqual(parent.Kind, EA0ObjectKind.okProject);

            // Проверка приведения.
            IA0ProjectID projID = parent as IA0ProjectID;
            Assert.NotNull(projID);
        }

        /// <summary>
        /// Проверяет Id родительского узла ОС.
        /// </summary>
        [Test, Timeout(10000)]
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
        [Test, Timeout(10000)]
        public void Test_ProjGUID()
        {
            Guid projGuid = this.OSID.ProjGUID;
            Assert.AreEqual(this.Proj.ID.GUID, projGuid);
        }

        /// <summary>
        /// Проверяет Id родительского проекта.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_ProjID()
        {
            int projID = this.OSID.ProjID;
            Assert.AreEqual(this.Proj.ID.ID, projID);
        }
    }
}