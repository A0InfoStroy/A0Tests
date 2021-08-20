// $Date: 2020-12-17 15:06:51 +0300 (Чт, 17 дек 2020) $
// $Revision: 458 $
// $Author: agalkin $
// Базовые классы

namespace A0Tests.LateTests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс для тестирования сметных данных.
    /// </summary>
    public abstract class Test_Estimate : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог обобщенных сметных объектов.
        /// </summary>
        protected dynamic Repo { get; private set; }

        /// <summary>
        /// Получает значение Guid головного комплекса.
        /// </summary>
        protected Guid HeadComplexGuid => this.A0.Estimate.Repo.ComplexID.HeadComplexGUID;

        /// <summary>
        /// Получает id головного узла комплекса.
        /// </summary>
        protected int HeadNodeID => this.A0.Estimate.Repo.ComplexID.HeadNodeID;

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Estimate.Repo;
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            this.Repo = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения данных обобщенного сметного объекта.
        /// </summary>
        /// <param name="iterator">Итератор по каталогу.</param>
        /// <param name="kind">Тип сметного объекта.</param>
        protected void ReadEstimateObject(dynamic iterator, dynamic kind)
        {
            Assert.NotNull(iterator);
            bool objectExist = false;
            while (iterator.Next())
            {
                dynamic obj = iterator.Item;
                Assert.NotNull(obj);
                Assert.NotNull(obj.ID);
                objectExist = true;
                Assert.IsTrue(obj.Kind == kind, $"Тип должен быть {kind}, найдено {obj.Kind}");
                string mark = obj.Mark;
                string name = obj.Name;
                string businessStage = obj.BusinessStage();
                string comment = obj.Comment;
                DateTime createMoment = obj.CreateMoment();
            }

            Assert.IsTrue(objectExist);
        }

        /// <summary>
        /// Проверяет обработку исключения при попытке получения элемента итератора.
        /// </summary>
        /// <param name="iterator">Итератор по каталогу.</param>
        protected void TestIteratorError(dynamic iterator)
        {
            bool result = false;
            Assert.NotNull(iterator);
            try
            {
                result = iterator.Next();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80040E14);
            }

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Проверяет работоспособность чтения дополнительных полей обобщенного сметного объекта.
        /// </summary>
        /// <param name="iterator">Итератор по каталогу.</param>
        /// <param name="field">Наименование допольнительного поля.</param>
        protected void ReadEstimateObjectFields(dynamic iterator, string field)
        {
            Assert.NotNull(iterator);
            while (iterator.Next())
            {
                dynamic obj = iterator.Item;
                Assert.NotNull(obj);
                dynamic fieldValue = obj.Fields.Value[field];
            }
        }

        /// <summary>
        /// Проверяет первый элемент итератора.
        /// </summary>
        /// <param name="iterator">Итератор по каталогу.</param>
        /// <param name="expected">Guid ожидаемого сметного объекта.</param>
        protected void CheckFirstItem(dynamic iterator, Guid expected)
        {
            while (iterator.Next())
            {
                Assert.AreEqual(expected, iterator.Item.ID.GUID);
                break;
            }
        }

        /// <summary>
        /// Проверяет наличие дочернего сметного объекта в каталоге родительского.
        /// </summary>
        /// <param name="parentIterator">Итератор по каталогу родительского сметного объекта.</param>
        /// <param name="childGuid">Guid дочернего сметного объекта.</param>
        protected void CheckChildInParent(dynamic parentIterator, Guid childGuid)
        {
            bool exist = false;
            while (parentIterator.Next())
            {
                if (parentIterator.Item.ID.GUID == childGuid)
                {
                    exist = true;
                }
            }

            Assert.IsTrue(exist);
        }
    }
}