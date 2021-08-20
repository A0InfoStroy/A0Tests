// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты ИД ЛС

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности IA0LSID.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSID",
        Author = "agalkin")]
    public class Test_IA0LSID : Test_NewLS
    {
        /// <summary>
        /// Получает или устанавливает значение IA0LSID.
        /// </summary>
        protected IA0LSID LSID { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LSID = this.LS.ID;
            Assert.NotNull(this.LSID);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LSID);
            this.LSID = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет остутствие ошибок при обращении к Guid ЛС.
        /// </summary>
        [Test]
        public void Test_GUID()
        {
            Guid guid = this.LSID.GUID;
        }

        /// <summary>
        /// Проверяет остутствие ошибок при обращении к Id ЛС.
        /// </summary>
        [Test]
        public void Test_ID()
        {
            int guid = this.LSID.ID;
        }

        /// <summary>
        /// Проверяет присвоенный тип сметного объекта.
        /// </summary>
        [Test]
        public void Test_Kind()
        {
            EA0ObjectKind kind = this.LSID.Kind;
            Assert.AreEqual(EA0ObjectKind.okLS, kind);
        }

        /// <summary>
        /// Проверяет Guid родительской объектной сметы.
        /// </summary>
        [Test]
        public void Test_OSGUID()
        {
            Guid osGuid = this.LSID.OSGUID;
            Assert.AreEqual(this.OS.ID.GUID, osGuid);
        }

        /// <summary>
        /// Проверяет Id родительской объектной сметы.
        /// </summary>
        [Test]
        public void Test_OSID()
        {
            int osId = this.LSID.OSID;
            Assert.AreEqual(this.OS.ID.ID, osId);
        }

        /// <summary>
        /// Проверяет родительский объект ЛС.
        /// </summary>
        [Test]
        public void Test_Parent()
        {
            IA0ObjectID parent = this.LSID.Parent;
            Assert.NotNull(parent);

            // Правильный тип
            Assert.AreEqual(parent.Kind, EA0ObjectKind.okOS);

            // Приводится
            IA0OSID osID = parent as IA0OSID;
            Assert.NotNull(osID);
        }

        /// <summary>
        /// Проверяет Id родительского узла ЛС.
        /// </summary>
        [Test]
        public void Test_ParentNodeID()
        {
            int parentNode = this.LSID.ParentNodeID;
            Assert.NotNull(parentNode);

            // В правильном узле
            Assert.AreEqual(parentNode, this.OS.Tree.Head.ID);
        }

        /// <summary>
        /// Проверяет Guid родительского проекта.
        /// </summary>
        [Test]
        public void Test_ProjGUID()
        {
            Guid projGuid = this.LSID.ProjGUID;
            Assert.AreEqual(this.Proj.ID.GUID, projGuid);
        }

        /// <summary>
        /// Проверяет Id родительского проекта.
        /// </summary>
        [Test]
        public void Test_ProjID()
        {
            int projID = this.LSID.ProjID;
            Assert.AreEqual(this.Proj.ID.ID, projID);
        }
    }
}