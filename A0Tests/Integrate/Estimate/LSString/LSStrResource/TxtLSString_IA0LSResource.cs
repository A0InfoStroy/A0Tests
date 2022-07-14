// $Date: 2022-07-13 20:59:22 +0300 (Ср, 13 июл 2022) $
// $Revision: 588 $
// $Author: eloginov $
// Тесты ресурса текстовой строки локальных смет.

namespace A0Tests.Integrate.Estimate.LSString.LSStrResource
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности ресурса текстовой строки ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Resource текстовой строки ЛС",
        Author = "agalkin")]
    public class Test_TxtLSString_IA0LSResource : Test_IA0LSResourceBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateTxtLSString();
            Assert.IsTrue(this.LSString.Resources.Count > 0, "В тестовой строке нет ресурсов");
            this.Resource = this.LSString.Resources.Items[0];
            Assert.NotNull(this.Resource);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Resource);
            this.Resource = null;
            base.TearDown();
        }
    }
}
