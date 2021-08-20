﻿// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты итогов комплекса

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности итогов комплекса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Totals",
        Author = "agalkin")]
    public class Test_IA0ComplexTotals : Test_NewComplex
    {
        /// <summary>
        /// Получает или устанавливает итоги комплекса.
        /// </summary>
        protected IA0Totals ComplexTotals { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ComplexTotals = this.Complex.Totals;
            Assert.NotNull(this.ComplexTotals);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ComplexTotals);
            this.ComplexTotals = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет наличие элементов итогов комплекса.
        /// </summary>
        [Test]
        public void Test_Count()
        {
            int count = this.ComplexTotals.Count;
            Assert.True(count > 0);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям элементов итогов.
        /// </summary>
        [Test]
        public void Test_Total()
        {
            for (int i = 0; i < this.ComplexTotals.Count; i++)
            {
                IA0Total item = this.ComplexTotals.Items[i];
                Assert.NotNull(item);
                dynamic attr = item.Attr["ProjID"];
                decimal construction = item.Construction; // Строительные
                decimal equipment = item.Equipment; // Оборудование
                decimal mounting = item.Mounting; // Монтаж
                string name = item.Name;
                Assert.NotNull(name);
                decimal other = item.Other; // Прочие
                decimal total = item.Total; // Всего
            }
        }

        /// <summary>
        /// Проверяет работоспособность метода возвращающего элемент итогов по индексу.
        /// </summary>
        [Test]
        public void Test_GetItem()
        {
            for (int i = 0; i < this.ComplexTotals.Count; i++)
            {
                IA0Total item = this.ComplexTotals.GetItem(i);
                Assert.NotNull(item);
                Assert.AreEqual(this.ComplexTotals.Items[i], item);
            }
        }

        /// <summary>
        /// Проверяет работоспособность метода возвращающего элемент итогов по имени.
        /// </summary>
        [Test]
        public void Test_ByName()
        {
            for (int i = 0; i < this.ComplexTotals.Count; i++)
            {
                string name = this.ComplexTotals.Items[i].Name;
                IA0Total byName = this.ComplexTotals.ByName[name];
                Assert.NotNull(byName);
                Assert.AreEqual(this.ComplexTotals.Items[i], byName);
            }
        }
    }
}