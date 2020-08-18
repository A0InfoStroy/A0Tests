// $Date: 2020-07-31 11:36:05 +0300 (Пт, 31 июл 2020) $
// $Revision: 335 $
// $Author: agalkin $
// Тесты ИД проекта

namespace A0Tests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности IA0ProjectID.
    /// </summary>
    public class Test_IA0ProjID : NewProj
    {
        /// <summary>
        /// Получает или устанавливает значение IA0OSID.
        /// </summary>
        protected IA0ProjectID ProjID { get; private set; }

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
            EA0ObjectKind kind = this.ProjID.Kind;
            Assert.AreEqual(EA0ObjectKind.okProject, kind);
        }

        /// <summary>
        /// Проверяет родительский объект проекта.
        /// </summary>
        [Test]
        public void Test_Parent()
        {
            IA0ObjectID parent = this.ProjID.Parent;
            Assert.NotNull(parent);

            // Правильный тип
            Assert.AreEqual(parent.Kind, EA0ObjectKind.okComplex);

            // Приводится
            IA0ComplexID complexID = parent as IA0ComplexID;
            Assert.NotNull(complexID);
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