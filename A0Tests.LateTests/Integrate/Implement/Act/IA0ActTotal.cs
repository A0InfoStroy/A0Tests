// $Date: 2020-12-29 14:24:46 +0300 (Вт, 29 дек 2020) $
// $Revision: 481 $
// $Author: agalkin $
// Тесты итогов акта

namespace A0Tests.LateTests.Integrate.Implement
{
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности итогов ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSTotal",
        Author = "vbutov")]
    public class Test_IA0ActTotal : Test_NewAct
    {
        /// <summary>
        /// Получает или устанавливает итоги акта.
        /// </summary>
        protected dynamic ActTotal { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            dynamic totals = this.Act.Totals;
            Assert.NotNull(totals);
            dynamic total = totals.Items[0];
            Assert.NotNull(total);
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
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.ActTotal.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.ActTotal.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Строительные".
        /// </summary>
        [Test]
        public void Test_Construction()
        {
            decimal construction = this.ActTotal.Construction;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Оборудование".
        /// </summary>
        [Test]
        public void Test_Equipment()
        {
            decimal equipment = this.ActTotal.Equipment;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Монтажные".
        /// </summary>
        [Test]
        public void Test_Mounting()
        {
            decimal mounting = this.ActTotal.Mounting;
        }

        /// <summary>
        /// Проверяет работоспособность чтения наименования итогов ЛС.
        /// </summary>
        [Test]
        public void Test_Name()
        {
            string name = this.ActTotal.Name;
            Assert.NotNull(name);
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Прочие".
        /// </summary>
        [Test]
        public void Test_Other()
        {
            decimal name = this.ActTotal.Other;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к графе "Всего".
        /// </summary>
        [Test]
        public void Test_Total()
        {
            decimal name = this.ActTotal.Total;
        }
    }
}