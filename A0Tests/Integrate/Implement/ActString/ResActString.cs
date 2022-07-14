// $Date: 2022-07-13 21:07:55 +0300 (Ср, 13 июл 2022) $
// $Revision: 592 $
// $Author: eloginov $

namespace A0Tests.Integrate.Implement.ActString.ResActString_IA0ActString
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности строки ресурса акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActString для строки ресурса",
        Author = "vbutov")]
    class Test_ResActString_IA0ActString : Test_IA0ActStringBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateResActString();
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
        /// Сторонний ресурс в строке ресурсе
        /// </summary>
        [Test, Timeout(12000)]
        public void Test_OutsideRes()
        {
            // Пример расценки из НСИ. 
            // БД: a0NSI_TER_01. Таблица: Resource.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Оборудование лифтов - 924
            // ЛИФТЫ ПАССАЖИРСКИЕ Г/П 400 КГ - 262300

            IA0ActString actString = this.Act.CreateResString(aNSIID: 7, aFolderID: 924, aResID: 262300, aNodeID: this.LS.Tree.Head.ID);
            Assert.NotNull(actString);

            Assert.IsTrue(actString.AllowOutside);

            actString.Outside = true;
            Assert.IsTrue(actString.Outside);

            // В строке ожидается 1 ресурс с флагом Outside
            Assert.AreEqual(1, actString.Resources.Count);
            Assert.IsTrue(actString.Resources.Items[0].Outside);


            actString.Outside = false;
            Assert.IsFalse(actString.Outside);
            Assert.IsFalse(actString.Resources.Items[0].Outside);
        }
    }
}
