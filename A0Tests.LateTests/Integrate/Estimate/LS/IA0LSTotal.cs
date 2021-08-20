// $Date: 2020-12-22 15:00:12 +0300 (Вт, 22 дек 2020) $
// $Revision: 463 $
// $Author: agalkin $
// Тесты итогов ЛС

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности итогов ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSTotal",
        Author = "agalkin")]
    public class Test_IA0LSTotal : Test_NewLS
    {
        /// <summary>
        /// Получает или устанавливает итоги ЛС.
        /// </summary>
        protected dynamic LSTotal { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            dynamic totals = this.LS.Totals;
            Assert.NotNull(totals);
            dynamic total = totals.Items[0];
            Assert.NotNull(total);
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
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.LSTotal.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.LSTotal.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Строительные".
        /// </summary>
        [Test]
        public void Test_Construction()
        {
            decimal construction = this.LSTotal.Construction;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Оборудование".
        /// </summary>
        [Test]
        public void Test_Equipment()
        {
            decimal equipment = this.LSTotal.Equipment;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Монтажные".
        /// </summary>
        [Test]
        public void Test_Mounting()
        {
            decimal mounting = this.LSTotal.Mounting;
        }

        /// <summary>
        /// Проверяет работоспособность чтения наименования итогов ЛС.
        /// </summary>
        [Test]
        public void Test_Name()
        {
            string name = this.LSTotal.Name;
            Assert.NotNull(name);
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Прочие".
        /// </summary>
        [Test]
        public void Test_Other()
        {
            decimal name = this.LSTotal.Other;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Всего".
        /// </summary>
        [Test]
        public void Test_Total()
        {
            decimal name = this.LSTotal.Total;
        }
    }
}