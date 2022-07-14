// $Date: 2022-07-13 21:09:23 +0300 (Ср, 13 июл 2022) $
// $Revision: 594 $
// $Author: eloginov $
// Тесты пересчетов текстовой строки акта.

namespace A0Tests.Integrate.Implement.ActString.ActStrRecalcs
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности пересчета текстовой строки акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActStrRecalc текстовой строки акта",
        Author = "agalkin")]
    class Test_TxtActString_IA0ActStrRecalcs : Test_IA0ActStrRecalcsBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateTxtActString();
            Assert.IsTrue(this.Act.Strings.Count > 0, "В тестовом акте нет строк");
            var str = this.Act.Strings.Items[0];
            this.StrRecalcs = str.Recalcs;
            Assert.IsNotNull(this.StrRecalcs);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.IsNotNull(this.StrRecalcs);
            this.StrRecalcs = null;
            base.TearDown();
        }
    }
}
