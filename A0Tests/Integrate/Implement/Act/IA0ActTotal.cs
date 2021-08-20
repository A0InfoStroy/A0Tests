// $Date: 2020-08-05 10:52:39 +0300 (Ср, 05 авг 2020) $
// $Revision: 343 $
// $Author: agalkin $
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