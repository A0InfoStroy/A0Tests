// $Date: 2020-12-18 13:15:50 +0300 (Пт, 18 дек 2020) $
// $Revision: 459 $
// $Author: agalkin $
// Тесты итогов проекта

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности итогов комплекса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Totals",
        Author = "agalkin")]
    public class Test_IA0ProjTotals : Test_NewProj
    {
        /// <summary>
        /// Получает или устанавливает итоги проекта.
        /// </summary>
        protected dynamic ProjTotals { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ProjTotals = this.Proj.Totals;
            Assert.NotNull(this.ProjTotals);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ProjTotals);
            this.ProjTotals = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет наличие элементов итогов проекта.
        /// </summary>
        [Test]
        public void Test_Count()
        {
            int count = this.ProjTotals.Count;
            Assert.True(count > 0);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям элементов итогов.
        /// </summary>
        [Test]
        public void Test_Total()
        {
            for (int i = 0; i < this.ProjTotals.Count; i++)
            {
                dynamic item = this.ProjTotals.Items[i];
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
        /// Проверяет работоспособность метода возвращающего элемент итогов по индексу.
        /// </summary>
        [Test]
        public void Test_GetItem()
        {
            for (int i = 0; i < this.ProjTotals.Count; i++)
            {
                dynamic item = this.ProjTotals.GetItem(i);
                Assert.NotNull(item);
                Assert.AreEqual(this.ProjTotals.Items[i], item);
            }
        }

        /// <summary>
        /// Проверяет работоспособность метода возвращающего элемент итогов по имени.
        /// </summary>
        [Test]
        public void Test_ByName()
        {
            for (int i = 0; i < this.ProjTotals.Count; i++)
            {
                string name = this.ProjTotals.Items[i].Name;
                dynamic byName = this.ProjTotals.ByName[name];
                Assert.NotNull(byName);
                Assert.AreEqual(this.ProjTotals.Items[i], byName);
            }
        }
    }
}