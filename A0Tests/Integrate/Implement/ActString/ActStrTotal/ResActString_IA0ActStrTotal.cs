// $Date: 2022-07-13 21:11:28 +0300 (Ср, 13 июл 2022) $
// $Revision: 597 $
// $Author: eloginov $
// Тесты итогов строки (типа русурс) акта

namespace A0Tests.Integrate.Implement.ActString.ActStrTotal
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности итогов строки (типа ресурс) акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrTotal строки (типа ресурс) акта",
        Author = "agalkin")]
    class Test_ResActString_IA0ActStrTotal : Test_IA0ActStrTotalBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateResActString();
            this.ActStrTotal = this.ActString.Total;
            Assert.NotNull(this.ActStrTotal);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ActStrTotal);
            this.ActStrTotal = null;
            base.TearDown();
        }
    }
}
