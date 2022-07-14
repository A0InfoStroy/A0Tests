// $Date: 2022-07-13 20:57:53 +0300 (Ср, 13 июл 2022) $
// $Revision: 586 $
// $Author: eloginov $
// Тесты IA0LSString для строки (типа работа).

namespace A0Tests.Integrate.Estimate.LSString
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности строки работы ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSString для строк работы",
        Author = "agalkin")]
    public class Test_WorkLSString_IA0LSString : Test_IA0LSStringBase
    {

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateWorkLSString();
            Assert.IsTrue(this.LS.Strings.Count > 0, "В тестовой ЛС нет строк");
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LSString);
            this.LSString = null;
            base.TearDown();
        }

        /// <summary>
        /// Сторонний ресурс в строке работе
        /// </summary>
        [Test, Timeout(30000)]
        public void Test_OutsideWork()
        {
            // Расценка работа.
            // БД: a0NSI_TER_01. Таблица: Works.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Деревообрабатывающее оборудование - 2553
            // ОБОРУДОВАНИЕ ОБЩЕГО НАЗНАЧЕНИЯ. ОБОРУДОВАНИЕ ЛЕСОПИЛЬНОГО ПРОИЗВОДСТВА. РАМА ЛЕСОПИЛЬНАЯ ОДНОЭТАЖНАЯ, МАССА 4,5Т - 787669 - 787668

            // Создание строки типа Работа, на основе расценки из БД НСИ
            IA0LSString lsString = this.LS.CreateWorkString(aNSIID: 7, aFolderID: 2553, aWorkID: 787669, aNodeID: this.LS.Tree.Head.ID);
            Assert.NotNull(lsString);

            // В строке такого типа ресурс назначить сторонним нельзя
            Assert.IsFalse(lsString.AllowOutside);
        }
    }
}
