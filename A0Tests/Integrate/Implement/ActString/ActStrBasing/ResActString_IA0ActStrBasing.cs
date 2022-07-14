// $Date: 2022-07-13 21:08:44 +0300 (Ср, 13 июл 2022) $
// $Revision: 593 $
// $Author: eloginov $
// Тесты расценки строки (типа ресурс) акта.

namespace A0Tests.Integrate.Implement.ActString.ActStrBasing
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности расценки строки (типа ресурс) акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrBasing строки (типа ресурс) акта",
        Author = "agalkin")]
    class Test_ResActString_IA0ActStrBasing : Test_IA0ActStrBasingBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateResActString();
            Assert.IsTrue(this.Act.Strings.Count > 0, "В тестовом акте нет строк");
            IA0ActString str = this.Act.Strings.Items[0];
            Assert.IsNotNull(str.StrBasing, "В тестовой строке нет расценки");
            this.StrBasing = str.StrBasing;
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
