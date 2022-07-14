// $Date: 2022-07-13 20:57:53 +0300 (Ср, 13 июл 2022) $
// $Revision: 586 $
// $Author: eloginov $
// Тесты IA0LSString для строки (типа ресурс).

namespace A0Tests.Integrate.Estimate.LSString
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности строки ресурса ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSString для строки ресурса",
        Author = "agalkin")]
    public class Test_ResLSString_IA0LSString : Test_IA0LSStringBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateResLSString();
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

            // Создание строки типа Ресурс, на основе расценки из БД НСИ
            IA0LSString lsString = this.LS.CreateResString(aNSIID: 7, aFolderID: 924, aResID: 262300, aNodeID: this.LS.Tree.Head.ID);
            Assert.NotNull(lsString);

            // В строке типа Ресурс должно быть разрешено назначение ресурса сторонним
            Assert.IsTrue(lsString.AllowOutside);

            // Назначаем ресурс сторонним
            lsString.Outside = true;
            Assert.IsTrue(lsString.Outside);

            // В строке ожидается 1 ресурс с флагом Outside
            Assert.AreEqual(1, lsString.Resources.Count);
            // В ресурсе флаг должен соответствовать флагу в строке
            Assert.IsTrue(lsString.Resources.Items[0].Outside);

            lsString.Outside = false;
            Assert.IsFalse(lsString.Outside);
            Assert.IsFalse(lsString.Resources.Items[0].Outside);
        }
    }
}
