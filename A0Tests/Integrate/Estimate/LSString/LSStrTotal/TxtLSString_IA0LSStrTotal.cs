// $Date: 2022-07-13 21:00:09 +0300 (Ср, 13 июл 2022) $
// $Revision: 589 $
// $Author: eloginov $
// Абстрактный класс тестов итогов текстовой строки локальных смет.

namespace A0Tests.Integrate.Estimate.LSString.LSStrTotal
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности итогов текстовой строки ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrTotal текстовой строки ЛС",
        Author = "agalkin")]
    class Test_TxtLSString_IA0LSStrTotal : Test_IA0LSStrTotalBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateTxtLSString();
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
