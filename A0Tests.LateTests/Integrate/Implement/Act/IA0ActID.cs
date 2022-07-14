﻿// $Date: 2020-12-29 14:24:46 +0300 (Вт, 29 дек 2020) $
// $Revision: 481 $
// $Author: agalkin $
// Тесты ИД акта

namespace A0Tests.LateTests.Integrate.Implement
{
    using System;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности IA0ActID.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActID",
        Author = "agalkin")]
    public class Test_IA0ActID : Test_NewAct
    {
        /// <summary>
        /// Получает или устанавливает значение IA0ActID.
        /// </summary>
        protected dynamic ActID { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ActID = this.Act.ID;
            Assert.IsNotNull(this.ActID);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.IsNotNull(this.ActID);
            this.ActID = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid акта.
        /// </summary>
        [Test]
        public void Test_GUID()
        {
            Guid guid = this.ActID.GUID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id акта.
        /// </summary>
        [Test]
        public void Test_ID()
        {
            int id = this.ActID.ID;
        }

        /// <summary>
        /// Проверяет присвоенный тип сметного объекта.
        /// </summary>
        [Test]
        public void Test_Kind()
        {
            dynamic kind = this.ActID.Kind;

            // Значение 5 соответствует EA0ObjectKind.okAct.
            Assert.AreEqual(5, kind);
        }

        /// <summary>
        /// Проверяет Guid родительской локальной сметы.
        /// </summary>
        [Test]
        public void Test_LSGUID()
        {
            Guid lsGuid = this.ActID.LSGUID;
            Assert.AreEqual(this.LS.ID.GUID, lsGuid);
        }

        /// <summary>
        /// Проверяет Id родительской локальной сметы.
        /// </summary>
        [Test]
        public void Test_LSID()
        {
            int lsId = this.ActID.LSID;
            dynamic ls = this.Repo.LS.Load2(this.LS.ID.GUID);
            Assert.AreEqual(ls.ID.ID, lsId);
        }

        /// <summary>
        /// Проверяет Guid родительской объектной сметы.
        /// </summary>
        [Test]
        public void Test_OSGUID()
        {
            Guid osGuid = this.ActID.OSGUID;
            Assert.AreEqual(this.OS.ID.GUID, osGuid);
        }

        /// <summary>
        /// Проверяет Id родительской объектной сметы.
        /// </summary>
        [Test]
        public void Test_OSID()
        {
            int osId = this.ActID.OSID;
            dynamic os = this.Repo.OS.Load(this.OS.ID.GUID, false);
            Assert.AreEqual(os.ID.ID, osId);
        }

        /// <summary>
        /// Проверяет Guid родительского проекта.
        /// </summary>
        [Test]
        public void Test_ProjGUID()
        {
            Guid projGuid = this.ActID.ProjGUID;
            Assert.AreEqual(this.Proj.ID.GUID, projGuid);
        }

        /// <summary>
        /// Проверяет Id родительского проекта.
        /// </summary>
        [Test]
        public void Test_ProjID()
        {
            int projId = this.ActID.ProjID;
            var proj = this.Repo.Proj.Load(this.Proj.ID.GUID, false);
            Assert.AreEqual(proj.ID.ID, projId);
        }

        /// <summary>
        /// Проверяет родительский объект ЛС.
        /// </summary>
        [Test]
        public void Test_Parent()
        {
            dynamic parent = this.ActID.Parent;
            Assert.NotNull(parent);

            // Значение 5 соответствует EA0ObjectKind.okAct.
            Assert.AreEqual(5, parent.Kind);
            Assert.AreEqual(this.LS.ID.GUID, parent.GUID);
        }
    }
}