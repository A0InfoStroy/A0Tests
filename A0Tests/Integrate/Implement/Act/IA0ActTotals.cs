﻿// $Date: 2020-08-05 10:48:22 +0300 (Ср, 05 авг 2020) $
// $Revision: 342 $
// $Author: agalkin $
// Тесты итогов акта

namespace A0Tests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности списка итогов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Totals",
        Author = "agalkin")]
    public class Test_IA0ActTotals : NewAct
    {
        /// <summary>
        /// Получает или устанавливает список итогов ЛС.
        /// </summary>
        protected IA0Totals Totals { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Totals = this.LS.Totals;
            Assert.NotNull(this.Totals);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Totals);
            this.Totals = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения количества итогов.
        /// </summary>
        [Test(Description = "Тест итогов акта")]
        public void Test_Count()
        {
            Assert.IsTrue(this.Totals.Count > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогов и их полей по индексу.
        /// </summary>
        [Test(Description = "Тест итогов акта")]
        public void Test_Totals()
        {
            for (var i = 0; i < this.Totals.Count; ++i)
            {

                IA0Total total = this.Totals.Items[i];
                Assert.NotNull(total);
                IA0ActTotal actTotal = total as IA0ActTotal;
                Assert.NotNull(actTotal);
                this.CheckActTotal(actTotal);
            }
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогов и их полей по имени.
        /// </summary>
        [Test(Description = "Тест итогов Акта по имени")]
        public void Test_ByName()
        {
            string[] totalNames = new[]
            {
                "1 Прямые затраты",
                "5 Материальные затраты",
                "3 Эксплуатация машин",
                "4 в т.ч. зарплата машинистов",
                "2 Основная зарплата",
                "7 Накладные расходы",
                "8 Сметная прибыль",
                "9 Сметная стоимость",
                "10 ИТОГО",
                "6 Транспортировка",
            };

            // Перебираем все итоги
            foreach (var totalName in totalNames)
            {
                IA0ActTotal total = this.Totals.ByName[totalName] as IA0ActTotal;
                Assert.NotNull(total, "Ожидается итог для {0}", totalName);
                this.CheckActTotal(total);

                Assert.AreEqual(totalName, total.Name.Trim());
            }
        }

        /// <summary>
        /// Проверка работоспособности метода получения итогов по индексу.
        /// </summary>
        [Test(Description = "Тест итогов Акта")]
        public void Test_GetItem()
        {
            for (int i = 0; i < this.Totals.Count; i++)
            {
                IA0Total actTotal = this.Totals.GetItem(i);
                Assert.NotNull(actTotal);
                this.CheckActTotal(actTotal);
            }
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям итогов акта.
        /// </summary>
        /// <param name="actTotal">Итог акта.</param>
        private void CheckActTotal(IA0Total actTotal)
        {
            decimal construction = actTotal.Construction;
            decimal quipment = actTotal.Equipment;
            decimal mounting = actTotal.Mounting;
            string name = actTotal.Name;
            decimal other = actTotal.Other;
            decimal total = actTotal.Total;
            Assert.IsTrue(name.Length > 0);
        }
    }
}