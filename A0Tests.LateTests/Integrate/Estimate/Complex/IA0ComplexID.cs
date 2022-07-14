// $Date: 2020-12-17 15:06:51 +0300 (Чт, 17 дек 2020) $
// $Revision: 458 $
// $Author: agalkin $
// Тесты ИД комплекса

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности IA0ComplexID.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ComplexID",
        Author = "agalkin")]
    public class Test_IA0ComplexID : Test_NewComplex
    {
        /// <summary>
        /// Получает или устанавливает значение ComplexID.
        /// </summary>
        protected dynamic ComplexID { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ComplexID = this.Complex.ID;
            Assert.NotNull(this.ComplexID);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ComplexID);
            this.ComplexID = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid комплекса.
        /// </summary>
        [Test]
        public void Test_GUID()
        {
            Guid guid = this.ComplexID.GUID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id комплекса.
        /// </summary>
        [Test]
        public void Test_ID()
        {
            int id = this.ComplexID.ID;
        }

        /// <summary>
        /// Проверяет тип сметного объекта.
        /// </summary>
        [Test]
        public void Test_Kind()
        {
            dynamic kind = this.ComplexID.Kind;

            // Значение 1 соответствует EA0ObjectKind.okComplex.
            Assert.AreEqual(1, kind);
        }

        /// <summary>
        /// Проверяет родительский объект комплекса.
        /// </summary>
        [Test]
        public void Test_Parent()
        {
            dynamic parent = this.ComplexID.Parent;
            Assert.NotNull(parent);

            // Правильный тип
            // Значение 1 соответствует EA0ObjectKind.okComplex.
            Assert.AreEqual(parent.Kind, 1);
        }

        /// <summary>
        /// Проверяет Id узла родительского объекта.
        /// </summary>
        [Test]
        public void Test_ParentNodeID()
        {
            int parentNode = this.ComplexID.ParentNodeID;

            // В правильном узле
            Assert.AreEqual(parentNode, this.HeadNodeID);
        }
    }
}