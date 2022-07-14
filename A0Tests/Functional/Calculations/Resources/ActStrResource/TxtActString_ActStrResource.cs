// $Date: 2022-07-13 20:43:22 +0300 (Ср, 13 июл 2022) $
// $Revision: 583 $
// $Author: eloginov $
// Класс тестов расчета параметров ресурса текстовой строки ресурса.

namespace A0Tests.Functional.Calculations.Resources.ActStrResource
{
    using NUnit.Framework;

    /// <summary>
    /// Группа тестов расчета параметров ресурса строки акта.
    /// </summary>
    [TestFixture(
        Category = "Functional",
        Description = "Проверка расчетов параметров ресурса текстовой строки акта",
        Author = "agalkin")]
    class Test_TxtActString_ActStrResource : Test_ActStrResourceBase
    {
        public override void SetUp()
        {
            base.SetUp();
            CreateTxtActString();
            Assert.IsTrue(this.ActString.Resources.Count > 0, "В тестовой строке нет ресурсов");
            this.Resource = this.ActString.Resources.Items[0];
            Assert.NotNull(this.Resource);
        }

        public override void TearDown()
        {
            Assert.NotNull(this.Resource);
            this.Resource = null;
            Assert.NotNull(this.ActString);
            this.ActString = null;
            base.TearDown();
        }
    }
}
