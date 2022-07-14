// $Date: 2022-07-13 21:09:23 +0300 (Ср, 13 июл 2022) $
// $Revision: 594 $
// $Author: eloginov $
// Абстрактный класс тестов пересчетов строки акта.

namespace A0Tests.Integrate.Implement.ActString.ActStrRecalcs
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Абстрактная тестовая группа (класс) проверки работоспособности пересчета строки акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActStrRecalc",
        Author = "agalkin")]
    public abstract class Test_IA0ActStrRecalcsBase : NewActStringBase
    {
        /// <summary>
        /// Получает или устанавливает пересчет строки ЛС.
        /// </summary>
        protected IA0LSStrRecalcs StrRecalcs { get; set; }

        /// <summary>
        /// Проверяет наличие пересчетов.
        /// </summary>
        [Test(Description = "Тест количества пересчетов"), Timeout(20000)]
        public virtual void Test_Count()
        {
            Assert.IsTrue(this.StrRecalcs.Count > 0);
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к пересчету и его полям.
        /// </summary>
        [Test(Description = "Тест пересчетов"), Timeout(20000)]
        public virtual void Test_Recalcs()
        {
            for (var i = 0; i < this.StrRecalcs.Count; ++i)
            {
                IA0LSStrRecalc recalc = this.StrRecalcs.Item[i];
                Assert.IsNotNull(recalc);
                Assert.IsTrue(recalc.Name.Length > 0);
                EA0LSStrRecalcKind kind = recalc.Kind;
            }
        }
    }
}