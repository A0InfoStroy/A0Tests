// $Date: 2022-07-13 21:09:23 +0300 (Ср, 13 июл 2022) $
// $Revision: 594 $
// $Author: eloginov $
// Тесты пересчетов строки (типа ресурс) акта.

namespace A0Tests.Integrate.Implement.ActString.ActStrRecalcs
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности пересчета строки (типа ресурс) акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActStrRecalc строки (типа ресурс) акта",
        Author = "agalkin")]
    class Tets_ResActString_IA0ActStrRecalcs : Test_IA0ActStrRecalcsBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateResActString();
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
