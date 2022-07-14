// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты итогов ЛС

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности итогов ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSTotal",
        Author = "agalkin")]
    public class Test_IA0LSTotal : NewLS
    {
        /// <summary>
        /// Получает или устанавливает итоги ЛС.
        /// </summary>
        protected IA0LSTotal LSTotal { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            IA0Totals totals = this.LS.Totals;
            Assert.NotNull(totals);
            IA0Total total = totals.Items[0];
            Assert.NotNull(total);
            this.LSTotal = total as IA0LSTotal;
            Assert.NotNull(this.LSTotal);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LSTotal);
            this.LSTotal = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута"), Timeout(10000)]
        public void Test_AttrCore()
        {
            dynamic attr = this.LSTotal.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута"), Timeout(10000)]
        public void Test_AttrExt()
        {
            dynamic attr = this.LSTotal.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Строительные".
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Construction()
        {
            decimal construction = this.LSTotal.Construction;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Оборудование".
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Equipment()
        {
            decimal equipment = this.LSTotal.Equipment;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Монтажные".
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Mounting()
        {
            decimal mounting = this.LSTotal.Mounting;
        }

        /// <summary>
        /// Проверяет работоспособность чтения наименования итогов ЛС.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Name()
        {
            string name = this.LSTotal.Name;
            Assert.NotNull(name);
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Прочие".
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Other()
        {
            decimal name = this.LSTotal.Other;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Всего".
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Total()
        {
            decimal name = this.LSTotal.Total;
        }
    }
}