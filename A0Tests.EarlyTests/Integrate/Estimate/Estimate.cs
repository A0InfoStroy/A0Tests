﻿// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Базовые классы

namespace A0Tests.EarlyTests
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс для тестирования сметных данных.
    /// </summary>
    public abstract class Test_Estimate : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог обобщенных сметных объектов.
        /// </summary>
        protected IA0EstimateRepo Repo { get; private set; }

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
        protected void ReadEstimateObject(IA0ObjectIterator iterator, EA0ObjectKind kind)
        {
            Assert.NotNull(iterator);
            bool objectExist = false;
            while (iterator.Next())
            {
                IA0Object obj = iterator.Item;
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
        protected void TestIteratorError(IA0ObjectIterator iterator)
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
        protected void ReadEstimateObjectFields(IA0ObjectIterator iterator, string field)
        {
            Assert.NotNull(iterator);
            while (iterator.Next())
            {
                IA0Object obj = iterator.Item;
                Assert.NotNull(obj);
                dynamic fieldValue = obj.Fields.Value[field];
            }
        }

        /// <summary>
        /// Проверяет первый элемент итератора.
        /// </summary>
        /// <param name="iterator">Итератор по каталогу.</param>
        /// <param name="expected">Guid ожидаемого сметного объекта.</param>
        protected void CheckFirstItem(IA0ObjectIterator iterator, Guid expected)
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
        protected void CheckChildInParent(IA0ObjectIterator parentIterator, Guid childGuid)
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