// $Date: 2021-01-15 15:09:15 +0300 (Пт, 15 янв 2021) $
// $Revision: 497 $
// $Author: agalkin $
// Тесты расчета параметров ресурса строки акта.

namespace A0Tests.LateTests.Functional.Calculations.Resources
{
    using NUnit.Framework;

    [TestFixture(
        Category = "Functional",
        Description = "Проверка расчетов параметров ресурса",
        Author = "agalkin")]
    public class Test_ActStrResource : Test_NewActString
    {
        protected dynamic Resource { get; private set; }

        public override void SetUp()
        {
            base.SetUp();
            Assert.IsTrue(this.ActString.Resources.Count > 0, "В тестовой строке нет ресурсов");
            this.Resource = this.ActString.Resources.Items[0];
            Assert.NotNull(this.Resource);
        }

        public override void TearDown()
        {
            Assert.NotNull(this.Resource);
            this.Resource = null;
            base.TearDown();
        }

        [Test]
        public void Test_ResVolume()
        {
            // Погрешность округления при расчете объема ресурса.
            double delta = 0.001;

            // Проверяем расчет расхода на объем ресурса.
            double volume = this.Resource.Norm_Calc * this.ActString.Volume / this.Resource.UnitCoef;
            Assert.AreEqual(volume, this.Resource.Volume, delta);

            // Изменяем коэффициет при единице измерения на произвольную величину.
            double coef = 9.7;
            this.Resource.MUnit = $"{coef}м";
            Assert.AreEqual(coef, this.Resource.UnitCoef);

            // Для пересчета объема ресурса this.Resource.Volume с новым коэффициентом при единице измерения
            // необходимо установить значение нормы расхода с поправкой (вызвать сэттер).
            this.Resource.Norm_Corr = 1;

            // Проверяем расчет расхода на объем ресурса с учетом измененного коэффициета при единице измерения.
            volume = this.Resource.Norm_Calc * this.ActString.Volume / this.Resource.UnitCoef;
            Assert.AreEqual(volume, this.Resource.Volume, delta);

            // Изменяем норму расхода с поправкой на произвольную величину.
            double normCorr = 5.8;
            this.Resource.Norm_Corr = normCorr;
            Assert.AreEqual(normCorr, this.Resource.Norm_Calc);

            // Проверяем расчет расхода на объем ресурса с учетом измененной нормы расхода.
            volume = this.Resource.Norm_Calc * this.ActString.Volume / this.Resource.UnitCoef;
            Assert.AreEqual(volume, this.Resource.Volume, delta);

            // Изменяем объем строки акта на произвольную величину.
            double newActStrVolume = 88.3;
            this.ActString.Volume = newActStrVolume;
            Assert.AreEqual(newActStrVolume, this.ActString.Volume);

            // Проверяем расчет расхода на объем ресурса с учетом измененного объема строки акта.
            volume = this.Resource.Norm_Calc * this.ActString.Volume / this.Resource.UnitCoef;
            Assert.AreEqual(volume, this.Resource.Volume, delta);
        }

        [Test]
        public void Test_ResCost()
        {
            // Меняем цену ресурса на произвольную величину.
            this.Resource.Price = 121.2;

            double resCost = this.Resource.Price * this.Resource.Volume;
            Assert.AreEqual(resCost, this.Resource.Cost);

            // Изменяем норму расхода с поправкой на произвольную величину.
            this.Resource.Norm_Corr = 15.5;

            resCost = this.Resource.Price * this.Resource.Volume;
            Assert.AreEqual(resCost, this.Resource.Cost);

            // Изменяем объем строки акта на произвольную величину.
            this.ActString.Volume = 74.3;

            resCost = this.Resource.Price * this.Resource.Volume;
            Assert.AreEqual(resCost, this.Resource.Cost);

            // Погрешность округления при расчете стоимости ресурса.
            double delta = 0.1;

            // Изменяем коэффициет при единице измерения на произвольную величину.
            this.Resource.MUnit = "22м";

            // Для пересчета объема ресурса this.Resource.Volume с новым коэффициентом при единице измерения
            // необходимо установить значение нормы расхода с поправкой (вызвать сэттер).
            this.Resource.Norm_Corr = 15.5;

            resCost = this.Resource.Price * this.Resource.Volume;
            Assert.AreEqual(resCost, (double)this.Resource.Cost, delta);
        }
    }
}