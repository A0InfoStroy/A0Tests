// $Date: 2022-07-13 20:59:22 +0300 (Ср, 13 июл 2022) $
// $Revision: 588 $
// $Author: eloginov $
// Абстрактный класс тестов ресурса строки локальных смет.

namespace A0Tests.Integrate.Estimate.LSString.LSStrResource
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Абстрактная тестовая группа (класс) для тестов проверки работоспособности ресурса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Resource",
        Author = "agalkin")]
    public abstract class Test_IA0LSResourceBase : NewLSStringBase
    {
        /// <summary>
        /// Получает или устанавливает ресурс.
        /// </summary>
        protected IA0Resource Resource { get; set; }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращение к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута"), Timeout(20000)]
        public virtual void Test_AttrCore()
        {
            dynamic attr = this.Resource.Attr["LSStrTitleID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращение к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута"), Timeout(20000)]
        public virtual void Test_AttrExt()
        {
            // Будет исключение такого поля нет в расширении
            dynamic attr = this.Resource.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращение к виду ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Kind()
        {
            EResourceKind kind = this.Resource.Kind;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращение к Id ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_ID()
        {
            int id = this.Resource.ID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_GUID()
        {
            Guid guid = this.Resource.GUID;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи типа учета ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Accounting()
        {
            // По умолчанию присваивается значение EA0ResAccounting.raIncluded
            EA0ResAccounting acc = this.Resource.Accounting;
            Assert.AreEqual(EA0ResAccounting.raIncluded, acc);

            this.Resource.Accounting = EA0ResAccounting.raExcluded;
            Assert.AreEqual(EA0ResAccounting.raExcluded, this.Resource.Accounting);
            this.Resource.Accounting = acc;

            this.Resource.Accounting = EA0ResAccounting.raReturn;
            Assert.AreEqual(EA0ResAccounting.raReturn, this.Resource.Accounting);
            this.Resource.Accounting = acc;

            // Значение EA0ResAccounting.raProjRes не присваивается.
            this.Resource.Accounting = EA0ResAccounting.raProjRes;
            Assert.AreEqual(EA0ResAccounting.raIncluded, this.Resource.Accounting);

            // Значение EA0ResAccounting.raWasExcluded не присваивается.
            this.Resource.Accounting = EA0ResAccounting.raWasExcluded;
            Assert.AreEqual(EA0ResAccounting.raIncluded, this.Resource.Accounting);

            // Значение EA0ResAccounting.raAdded не присваивается.
            this.Resource.Accounting = EA0ResAccounting.raAdded;
            Assert.AreEqual(EA0ResAccounting.raIncluded, this.Resource.Accounting);
        }

        /// <summary>
        /// Проверяет работоспособность чтения группы сборников ресурса из обоснования.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Group()
        {
            string group = this.Resource.Group;
        }

        /// <summary>
        /// Проверяет работоспособность чтения обоснования ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Basing()
        {
            string basing = this.Resource.Basing;
            Assert.NotNull(basing);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи единицы измерения ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_MUnit()
        {
            string newUnit = this.Resource.MUnit + " Изменено";
            this.Resource.MUnit = newUnit;
            Assert.AreEqual(this.Resource.MUnit, newUnit);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к норме расхода на единицу.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Norm()
        {
            double norm = this.Resource.Norm;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи нормы расхода с поправкой.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Norm_Corr()
        {
            double normCorr = this.Resource.Norm_Corr;
            double newNorm_Corr = normCorr + 10;
            this.Resource.Norm_Corr = newNorm_Corr;
            Assert.AreEqual(this.Resource.Norm_Corr, newNorm_Corr);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к норме расхода расчетной.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Norm_Calc()
        {
            double normCalc = this.Resource.Norm_Calc;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расходу на объем.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Volume()
        {
            double volume = this.Resource.Volume;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи фактического расхода.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Volume_Fact()
        {
            double volumeFact = this.Resource.Volume_Fact;
            double newVolume_Fact = volumeFact + 19;
            this.Resource.Volume_Fact = newVolume_Fact;
            Assert.AreEqual(this.Resource.Volume_Fact, newVolume_Fact);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи базовой или текущей цены (в зависимости от уровня цен строки).
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Price()
        {
            double price = this.Resource.Price;
            double newPrice = price + 19;
            this.Resource.Price = newPrice;
            Assert.AreEqual(this.Resource.Price, newPrice);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовой или текущей стоимости (в зависимости от уровня цен строки).
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Cost()
        {
            decimal cost = this.Resource.Cost;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к идентификатору справочника цен.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_PriceRefMark()
        {
            int priceRefMark = this.Resource.PriceRefMark;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к к базовой или текущей ЗПМ (в зависимости от уровня цен строки).
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_ZPM()
        {
            decimal zpm = this.Resource.ZPM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расчетной ТЗМ.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_TZM()
        {
            decimal tzm = this.Resource.TZM;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи массы ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Mass()
        {
            double mass = this.Resource.Mass;
            double newMass = mass + 19;
            this.Resource.Mass = newMass;
            Assert.AreEqual(this.Resource.Mass, newMass);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи наименования ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Name()
        {
            string name = this.Resource.Name;
            string newName = name + " Изменено";
            this.Resource.Name = newName;
            Assert.AreEqual(this.Resource.Name, newName);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи класса груза ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_CargoClass()
        {
            int cargoClass = this.Resource.CargoClass;
            int newCargoClass = cargoClass + 2;
            this.Resource.CargoClass = newCargoClass;
            Assert.AreEqual(this.Resource.CargoClass, newCargoClass);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку стороннего ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Outside()
        {
            bool outside = this.Resource.Outside;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи статуса "Включить в расчет массы" ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_IncToMassCalc()
        {
            bool inc = this.Resource.IncToMassCalc;
            bool newIncToMassCalc = true;
            this.Resource.IncToMassCalc = newIncToMassCalc;
            Assert.AreEqual(this.Resource.IncToMassCalc, newIncToMassCalc);

            newIncToMassCalc = false;
            this.Resource.IncToMassCalc = newIncToMassCalc;
            Assert.AreEqual(this.Resource.IncToMassCalc, newIncToMassCalc);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку текстового ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Literal()
        {
            bool literal = this.Resource.Literal;
        }

        /// <summary>
        /// Проверяет изменение прямых затрат строки акта при изменении цены ресурса.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_ChangesTotalPZ()
        {
            IA0LSString lsString = this.LS.Strings.Items[0];
            double price = this.Resource.Price;
            Assert.AreEqual(price, 0);

            decimal totalPZ = this.LSString.TotalPZ();
            Assert.AreEqual(totalPZ, 0);

            // Изменение цены ресурса на произвольное значение.
            double newPrice = price + 19;
            this.Resource.Price = newPrice;

            // Проверка изменения прямых затрат.
            Assert.AreEqual(this.Resource.Cost, newPrice * this.Resource.Volume);
            Assert.AreEqual(this.SumResourceCosts(), this.LSString.TotalPZ());

            // Изменение цены ресурса на произвольное значение.
            newPrice += 32;
            this.Resource.Price = newPrice;

            // Проверка изменения прямых затрат.
            Assert.AreEqual(this.Resource.Cost, newPrice * this.Resource.Volume);
            Assert.AreEqual(this.SumResourceCosts(), this.LSString.TotalPZ());
        }

        /// <summary>
        /// Проверяет работоспособность получения коэффициента при единице измерения.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_UnitCoef()
        {
            double defaultValue = 1d;
            double hundredSquareMetersValue = 100d;
            Assert.AreEqual(defaultValue, this.Resource.UnitCoef);
            this.Resource.MUnit = "100м2";
            Assert.AreEqual(hundredSquareMetersValue, this.Resource.UnitCoef);
        }

        private decimal SumResourceCosts()
        {
            decimal actStringTotalPZ = 0;
            for (int i = 0; i < this.LSString.Resources.Count; i++)
            {
                IA0Resource resource = this.LSString.Resources.Items[i];
                actStringTotalPZ += resource.Cost;
            }

            return actStringTotalPZ;
        }
    }
}