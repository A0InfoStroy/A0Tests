// $Date: 2020-08-05 10:34:49 +0300 (Ср, 05 авг 2020) $
// $Revision: 340 $
// $Author: agalkin $
// Тесты итогов ЛС

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности списка итогов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Totals",
        Author = "vbutov")]
    public class Test_IA0LSTotals : NewLS
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
        [Test(Description = "Тест итогов ЛС")]
        public void Test_Count()
        {
            Assert.IsTrue(this.Totals.Count > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогов и их полей по индексу.
        /// </summary>
        [Test(Description = "Тест итогов ЛС")]
        public void Test_Totals()
        {
            // Перебор всех итогов.
            for (int i = 0; i < this.Totals.Count; i++)
            {
                IA0Total total = this.Totals.Items[i];
                Assert.NotNull(total);
                IA0LSTotal lsTotal = total as IA0LSTotal;
                Assert.NotNull(lsTotal);
                this.CheckLSTotal(lsTotal);
            }
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогов и их полей по имени.
        /// </summary>
        [Test(Description = "Тест итогов ЛС по имени")]
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

            // Перебор всех итогов.
            foreach (string tn in totalNames)
            {
                IA0LSTotal lsTotal = this.Totals.ByName[tn] as IA0LSTotal;
                Assert.NotNull(lsTotal);
                this.CheckLSTotal(lsTotal);
            }
        }

        /// <summary>
        /// Проверка работоспособности метода получения итогов по индексу.
        /// </summary>
        [Test(Description = "Тест итогов ЛС")]
        public void Test_GetItem()
        {
            for (var i = 0; i < this.Totals.Count; i++)
            {
                IA0Total lsTotal = this.Totals.GetItem(i);
                Assert.NotNull(lsTotal);
                Assert.AreEqual(this.Totals.Items[i], lsTotal);
                this.CheckLSTotal(lsTotal);
            }
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям итогов ЛС.
        /// </summary>
        /// <param name="lsTotal">Итог акта.</param>
        private void CheckLSTotal(IA0Total lsTotal)
        {
            decimal construction = lsTotal.Construction;
            decimal quipment = lsTotal.Equipment;
            decimal mounting = lsTotal.Mounting;
            string name = lsTotal.Name;
            decimal other = lsTotal.Other;
            decimal total = lsTotal.Total;
            Assert.IsTrue(name.Length > 0);
        }
    }
}