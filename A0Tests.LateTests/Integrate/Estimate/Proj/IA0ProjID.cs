// $Date: 2020-12-18 13:18:31 +0300 (Пт, 18 дек 2020) $
// $Revision: 460 $
// $Author: agalkin $
// Тесты ИД проекта

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности IA0ProjectID.
    /// </summary>
    public class Test_IA0ProjID : Test_NewProj
    {
        /// <summary>
        /// Получает или устанавливает значение IA0OSID.
        /// </summary>
        protected dynamic ProjID { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ProjID = this.Proj.ID;
            Assert.NotNull(this.ProjID);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ProjID);
            this.ProjID = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid проекта.
        /// </summary>
        [Test]
        public void Test_GUID()
        {
            Guid guid = this.ProjID.GUID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id проекта.
        /// </summary>
        [Test]
        public void Test_ID()
        {
            int id = this.ProjID.ID;
        }

        /// <summary>
        /// Проверяет присвоенный тип сметного объекта.
        /// </summary>
        [Test]
        public void Test_Kind()
        {
            dynamic kind = this.ProjID.Kind;

            // Значение 2 соответствует EA0ObjectKind.okProject.
            Assert.AreEqual(2, kind);
        }

        /// <summary>
        /// Проверяет родительский объект проекта.
        /// </summary>
        [Test]
        public void Test_Parent()
        {
            dynamic parent = this.ProjID.Parent;
            Assert.NotNull(parent);

            // Правильный тип
            // Значение 1 соответствует EA0ObjectKind.okComplex.
            Assert.AreEqual(parent.Kind, 1);
        }

        /// <summary>
        /// Проверяет Id родительского узла проекта.
        /// </summary>
        [Test]
        public void Test_ParentNodeID()
        {
            var parentNode = this.ProjID.ParentNodeID;
            Assert.NotNull(parentNode);

            // В правильном узле
            Assert.AreEqual(parentNode, this.HeadNodeID);
        }
    }
}