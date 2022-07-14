// $Date: 2022-07-13 20:58:34 +0300 (Ср, 13 июл 2022) $
// $Revision: 587 $
// $Author: eloginov $
// Тесты расценок строки (типа ресурс) локальной сметы.

namespace A0Tests.Integrate.Estimate.LSString.LSStrBasing
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности расценки строки (типа ресурс) ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrBasing строки (типа ресурс)",
        Author = "agalkin")]
    public class Test_ResLSString_IA0LSStrBasing : Test_IA0LSStrBasingBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateResLSString();
            Assert.IsNotNull(this.LSString.StrBasing, "В тестовой строке нет расценки");
            this.StrBasing = this.LSString.StrBasing;
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.StrBasing);
            this.StrBasing = null;
            base.TearDown();
        }
    }
}
