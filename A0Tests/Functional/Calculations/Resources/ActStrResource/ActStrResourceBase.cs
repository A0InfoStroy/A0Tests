// $Date: 2022-07-13 20:43:22 +0300 (Ср, 13 июл 2022) $
// $Revision: 583 $
// $Author: eloginov $
// Абстрактный класс тестов расчета параметров ресурса строки акта

namespace A0Tests.Functional.Calculations.Resources
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Абстрактная тестовая группа (класс) для функционального тестирования строки акта
    /// </summary>
    public abstract class Test_ActStrResourceBase : NewActStringBase
    {
        protected IA0Resource Resource { get; set; }

        [Test, Timeout(30000)]
        public virtual void Test_ResVolume()
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

        [Test, Timeout(30000)]
        public virtual void Test_ResCost()
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