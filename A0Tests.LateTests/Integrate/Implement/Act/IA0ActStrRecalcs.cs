// $Date: 2020-12-29 14:24:46 +0300 (Вт, 29 дек 2020) $
// $Revision: 481 $
// $Author: agalkin $
// Тесты пересчетов строки Акта

namespace A0Tests.LateTests.Integrate.Implement
{
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности пересчета строки акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActStrRecalc",
        Author = "agalkin")]
    public class Test_IA0ActStrRecalcs : Test_NewActString
    {
        /// <summary>
        /// Получает или устанавливает пересчет строки ЛС.
        /// </summary>
        protected dynamic StrRecalcs { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
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

        /// <summary>
        /// Проверяет наличие пересчетов.
        /// </summary>
        [Test(Description = "Тест количества пересчетов")]
        public void Test_Count()
        {
            Assert.IsTrue(this.StrRecalcs.Count > 0);
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к пересчету и его полям.
        /// </summary>
        [Test(Description = "Тест пересчетов")]
        public void Test_Recalcs()
        {
            for (var i = 0; i < this.StrRecalcs.Count; ++i)
            {
                dynamic recalc = this.StrRecalcs.Item[i];
                Assert.IsNotNull(recalc);
                Assert.IsTrue(recalc.Name.Length > 0);
                dynamic kind = recalc.Kind;
            }
        }
    }
}