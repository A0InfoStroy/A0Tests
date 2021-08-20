// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты итогов акта

namespace A0Tests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности итогов ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSTotal",
        Author = "vbutov")]
    public class Test_IA0ActTotal : NewAct
    {
        /// <summary>
        /// Получает или устанавливает итоги акта.
        /// </summary>
        protected IA0ActTotal ActTotal { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            IA0Totals totals = this.Act.Totals;
            Assert.NotNull(totals);
            IA0Total total = totals.Items[0];
            Assert.NotNull(total);
            this.ActTotal = total as IA0ActTotal;
            Assert.NotNull(this.ActTotal);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ActTotal);
            this.ActTotal = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута"), Timeout(20000)]
        public void Test_AttrCore()
        {
            dynamic attr = this.ActTotal.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута"), Timeout(20000)]
        public void Test_AttrExt()
        {
            dynamic attr = this.ActTotal.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Строительные".
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Construction()
        {
            decimal construction = this.ActTotal.Construction;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Оборудование".
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Equipment()
        {
            decimal equipment = this.ActTotal.Equipment;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Монтажные".
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Mounting()
        {
            decimal mounting = this.ActTotal.Mounting;
        }

        /// <summary>
        /// Проверяет работоспособность чтения наименования итогов ЛС.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Name()
        {
            string name = this.ActTotal.Name;
            Assert.NotNull(name);
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Прочие".
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Other()
        {
            decimal name = this.ActTotal.Other;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Всего".
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Total()
        {
            decimal name = this.ActTotal.Total;
        }
    }
}