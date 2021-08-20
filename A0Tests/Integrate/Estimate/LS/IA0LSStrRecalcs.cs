// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты пересчетов строки локальных смет

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс пересчетов строки ЛС.
    /// </summary>
    public class Test_CustomLSStrRecalcs : Test_LSStringCustom
    {
        /// <summary>
        /// Получает или устанавливает пересчет строки ЛС.
        /// </summary>
        protected IA0LSStrRecalcs StrRecalcs { get; set; }

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
    public class Test_IA0LSStrRecalcs : Test_CustomLSStrRecalcs
    {
        /// <summary>
        /// Проверяет наличие пересчетов.
        /// </summary>
        [Test(Description = "Тест количества пересчетов"), Timeout(10000)]
        public void Test_Count()
        {
            Assert.IsTrue(this.StrRecalcs.Count > 0);
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к пересчету и его полям.
        /// </summary>
        [Test(Description = "Тест пересчетов"), Timeout(10000)]
        public void Test_Recalcs()
        {
            for (var i = 0; i < this.StrRecalcs.Count; ++i)
            {
                IA0LSStrRecalc recalc = this.StrRecalcs.Item[i];
                Assert.IsNotNull(recalc);
                Assert.IsTrue(recalc.Name.Length > 0);
                var kind = recalc.Kind;
            }
        }
    }
}