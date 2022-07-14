// $Date: 2022-07-13 21:07:55 +0300 (Ср, 13 июл 2022) $
// $Revision: 592 $
// $Author: eloginov $

namespace A0Tests.Integrate.Implement.ActString.TxtActString_IA0ActString
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности текстовой строки акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActString для текстовой строки",
        Author = "vbutov")]
    public class Test_TxtActString_IA0ActString : Test_IA0ActStringBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateTxtActString();
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

    }
}
