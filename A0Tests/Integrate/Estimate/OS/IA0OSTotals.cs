// $Date: 2020-07-29 16:24:16 +0300 (Ср, 29 июл 2020) $
// $Revision: 326 $
// $Author: agalkin $
// Тесты итогов ОС

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности итогов ОС.
    /// </summary>
    public class Test_IA0OSTotals : NewOS
    {
        /// <summary>
        /// Получает или устанавливает итоги ОС.
        /// </summary>
        protected IA0Totals OSTotals { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.OSTotals = this.OS.Totals;
            Assert.NotNull(this.OSTotals);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.OSTotals);
            this.OSTotals = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения количества элементов итогов.
        /// </summary>
        [Test]
        public void Test_Count()
        {
            int count = this.OSTotals.Count;
            Assert.True(count > 0);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям элемента итогов.
        /// </summary>
        [Test]
        public void Test_Total()
        {
            for (int i = 0; i < this.OSTotals.Count; i++)
            {
                IA0Total item = this.OSTotals.Items[i];
                Assert.NotNull(item);
                dynamic attr = item.Attr["ProjID"];
                decimal construction = item.Construction;
                decimal equipment = item.Equipment;
                decimal mounting = item.Mounting;
                string name = item.Name;
                Assert.NotNull(name);
                decimal other = item.Other;
                decimal total = item.Total;
            }
        }

        /// <summary>
        /// Проверяет работоспособность метода получения элемента итогов по индексу.
        /// </summary>
        [Test]
        public void Test_GetItem()
        {
            for (int i = 0; i < this.OSTotals.Count; i++)
            {
                IA0Total item = this.OSTotals.GetItem(i);
                Assert.NotNull(item);
                Assert.AreEqual(this.OSTotals.Items[i], item);
            }
        }

        /// <summary>
        /// Проверяет работоспособность метода получения элемента итогов по имени.
        /// </summary>
        [Test]
        public void Test_ByName()
        {
            for (int i = 0; i < this.OSTotals.Count; i++)
            {
                string name = this.OSTotals.Items[i].Name;
                IA0Total byName = this.OSTotals.ByName[name];
                Assert.NotNull(byName);
                Assert.AreEqual(this.OSTotals.Items[i], byName);
            }
        }
    }
}