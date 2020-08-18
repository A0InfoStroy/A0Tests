// $Date: 2020-07-31 11:32:53 +0300 (Пт, 31 июл 2020) $
// $Revision: 334 $
// $Author: agalkin $
// Тесты ИД комплекса

namespace A0Tests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности IA0ComplexID.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ComplexID",
        Author = "agalkin")]
    public class Test_IA0ComplexID : NewComplex
    {
        /// <summary>
        /// Получает или устанавливает значение ComplexID.
        /// </summary>
        protected IA0ComplexID ComplexID { get; private set; }

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
            EA0ObjectKind kind = this.ComplexID.Kind;
            Assert.AreEqual(EA0ObjectKind.okComplex, kind);
        }

        /// <summary>
        /// Проверяет родительский объект комплекса.
        /// </summary>
        [Test]
        public void Test_Parent()
        {
            IA0ObjectID parent = this.ComplexID.Parent;
            Assert.NotNull(parent);

            // Правильный тип
            Assert.AreEqual(parent.Kind, EA0ObjectKind.okComplex);

            // Приводится
            IA0ComplexID complexID = parent as IA0ComplexID;
            Assert.NotNull(complexID);
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