// $Date: 2022-07-13 21:00:09 +0300 (Ср, 13 июл 2022) $
// $Revision: 589 $
// $Author: eloginov $
// Абстрактный класс тестов итогов строки (типа ресурс) локальных смет.

namespace A0Tests.Integrate.Estimate.LSString.LSStrTotal
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности итогов строки (типа ресурс) ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrTotal строки (типа ресурс) ЛС",
        Author = "agalkin")]
    class Test_ResLSString_IA0LSStrTotal : Test_IA0LSStrTotalBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateResLSString();
            this.LSStrTotal = this.LSString.Total;
            Assert.NotNull(this.LSStrTotal);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LSStrTotal);
            this.LSStrTotal = null;
            base.TearDown();
        }
    }
}
