// $Date: 2022-07-13 20:57:53 +0300 (Ср, 13 июл 2022) $
// $Revision: 586 $
// $Author: eloginov $
// Тесты IA0LSString для текстовой строки.

namespace A0Tests.Integrate.Estimate.LSString
{
    using A0Service;
    using NUnit.Framework;
    using System;

    /// <summary>
    ///  Содержит тесты проверки работоспособности строки ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSString для текстовых строк",
        Author = "agalkin")]
    public class Test_TxtLSString_IA0LSString : Test_IA0LSStringBase
    {

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateTxtLSString();
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
    
    }
}
