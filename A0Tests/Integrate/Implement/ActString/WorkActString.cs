// $Date: 2022-07-13 21:07:55 +0300 (Ср, 13 июл 2022) $
// $Revision: 592 $
// $Author: eloginov $

namespace A0Tests.Integrate.Implement.ActString.WorkActString_IA0ActString
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности строки работы акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActString для строки работы",
        Author = "vbutov")]
    class Test_WorkActString_IA0ActString : Test_IA0ActStringBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateWorkActString();
            Assert.IsTrue(this.Act.Strings.Count > 0, "В тестовой ЛС нет строк");
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ActString);
            this.ActString = null;
            base.TearDown();
        }

        /// <summary>
        /// Сторонний ресурс в строке работе
        /// </summary>
        [Test, Timeout(12000)]
        public void Test_OutsideWork()
        {
            // Расценка работа.
            // БД: a0NSI_TER_01. Таблица: Works.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Деревообрабатывающее оборудование - 2553
            // ОБОРУДОВАНИЕ ОБЩЕГО НАЗНАЧЕНИЯ. ОБОРУДОВАНИЕ ЛЕСОПИЛЬНОГО ПРОИЗВОДСТВА. РАМА ЛЕСОПИЛЬНАЯ ОДНОЭТАЖНАЯ, МАССА 4,5Т - 787669 - 787668

            IA0ActString actString = this.Act.CreateWorkString(aNSIID: 7, aFolderID: 2553, aWorkID: 787669, aNodeID: this.LS.Tree.Head.ID);
            Assert.NotNull(actString);

            Assert.IsFalse(actString.AllowOutside);
        }
    }
}
