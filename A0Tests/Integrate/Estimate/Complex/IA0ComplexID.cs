// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
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
        [Test, Timeout(10000)]
        public void Test_GUID()
        {
            Guid guid = this.ComplexID.GUID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id комплекса.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_ID()
        {
            int id = this.ComplexID.ID;
        }

        /// <summary>
        /// Проверяет тип сметного объекта.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Kind()
        {
            EA0ObjectKind kind = this.ComplexID.Kind;
            Assert.AreEqual(EA0ObjectKind.okComplex, kind);
        }

        /// <summary>
        /// Проверяет родительский объект комплекса.
        /// </summary>
        [Test, Timeout(10000)]
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
        [Test, Timeout(10000)]
        public void Test_ParentNodeID()
        {
            int parentNode = this.ComplexID.ParentNodeID;

            // В правильном узле
            Assert.AreEqual(parentNode, this.HeadNodeID);
        }
    }
}