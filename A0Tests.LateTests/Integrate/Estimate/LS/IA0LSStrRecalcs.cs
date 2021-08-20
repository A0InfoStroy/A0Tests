// $Date: 2020-12-23 12:29:56 +0300 (Ср, 23 дек 2020) $
// $Revision: 466 $
// $Author: agalkin $
// Тесты пересчетов строки локальных смет

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс пересчетов строки ЛС.
    /// </summary>
    public class Test_LSStrRecalcs : Test_NewLSString
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
            Assert.IsTrue(this.LS.Strings.Count > 0, "В тестовой ЛС нет строк");
            var str = this.LS.Strings.Items[0];
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

    /// <summary>
    ///  Содержит тесты проверки работоспособности пересчета строки ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrRecalc",
        Author = "vbutov")]
    public class Test_IA0LSStrRecalcs : Test_LSStrRecalcs
    {
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
                var kind = recalc.Kind;
            }
        }
    }
}