// $Date: 2022-07-13 20:41:15 +0300 (Ср, 13 июл 2022) $
// $Revision: 582 $
// $Author: eloginov $
// Класс тестов расчета параметров ресурса строки (типа ресурс) сметы.

namespace A0Tests.Functional.Calculations.Resources.LSStrResource
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Группа тестов расчета параметров ресурса строки (типа ресурс) сметы.
    /// </summary>
    [TestFixture(
          Category = "Functional",
          Description = "Проверка расчетов параметров ресурса строки (типа ресурс)",
          Author = "agalkin")]
    public class Test_ResLSString_LSStrResource : Test_LSStrResourceBase
    {
        public override void SetUp()
        {
            base.SetUp();
            CreateResLSString();
            Assert.IsTrue(this.LSString.Resources.Count > 0, "В тестовой строке нет ресурсов");
            this.Resource = this.LSString.Resources.Items[0];
            Assert.NotNull(this.Resource);
        }

        public override void TearDown()
        {
            Assert.NotNull(this.Resource);
            this.Resource = null;
            Assert.NotNull(this.LSString);
            this.LSString = null;
            base.TearDown();
        }
    }
}
