// $Date: 2020-08-05 10:48:22 +0300 (Ср, 05 авг 2020) $
// $Revision: 342 $
// $Author: agalkin $
// Тесты ресурса строки акта

namespace A0Tests.Integrate.Implement
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности ресурса строки акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Resource",
        Author = "agalkin")]
    public class Test_IA0ActResource : NewActString
    {
        /// <summary>
        /// Получает или устанавливает ресурс.
        /// </summary>
        protected IA0Resource Resource { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.IsTrue(this.ActString.Resources.Count > 0, "В тестовой строке нет ресурсов");
            this.Resource = this.ActString.Resources.Items[0];
            Assert.NotNull(this.Resource);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Resource);
            this.Resource = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращение к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.Resource.Attr["LSStrTitleID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращение к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            // Будет исключение такого поля нет в расширении
            dynamic attr = this.Resource.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращение к виду ресурса.
        /// </summary>
        [Test]
        public void Test_Kind()
        {
            EResourceKind kind = this.Resource.Kind;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращение к Id ресурса.
        /// </summary>
        [Test]
        public void Test_ID()
        {
            int id = this.Resource.ID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid ресурса.
        /// </summary>
        [Test]
        public void Test_GUID()
        {
            Guid guid = this.Resource.GUID;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи типа учета ресурса.
        /// </summary>
        [Test]
        public void Test_Accounting()
        {
            EA0ResAccounting acc = this.Resource.Accounting;
            this.Resource.Accounting = EA0ResAccounting.raExcluded;
            Assert.AreEqual(this.Resource.Accounting, EA0ResAccounting.raExcluded);

            this.Resource.Accounting = EA0ResAccounting.raIncluded;
            Assert.AreEqual(this.Resource.Accounting, EA0ResAccounting.raIncluded);

            this.Resource.Accounting = EA0ResAccounting.raProjRes;
            Assert.AreEqual(this.Resource.Accounting, EA0ResAccounting.raProjRes);

            this.Resource.Accounting = EA0ResAccounting.raWasExcluded;
            Assert.AreEqual(this.Resource.Accounting, EA0ResAccounting.raWasExcluded);

            this.Resource.Accounting = EA0ResAccounting.raAdded;
            Assert.AreEqual(this.Resource.Accounting, EA0ResAccounting.raAdded);

            this.Resource.Accounting = EA0ResAccounting.raReturn;
            Assert.AreEqual(this.Resource.Accounting, EA0ResAccounting.raReturn);
        }

        /// <summary>
        /// Проверяет работоспособность чтения группы сборников ресурса из обоснования.
        /// </summary>
        [Test]
        public void Test_Group()
        {
            string group = this.Resource.Group;
            Assert.NotNull(group);
        }

        /// <summary>
        /// Проверяет работоспособность чтения обоснования ресурса.
        /// </summary>
        [Test]
        public void Test_Basing()
        {
            string basing = this.Resource.Basing;
            Assert.NotNull(basing);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи единицы измерения ресурса.
        /// </summary>
        [Test]
        public void Test_MUnit()
        {
            string mUnit = this.Resource.MUnit;
            Assert.NotNull(mUnit);
            string newUnit = this.Resource.MUnit + " Изменено";
            this.Resource.MUnit = newUnit;
            Assert.AreEqual(this.Resource.MUnit, newUnit);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к норме расхода на единицу.
        /// </summary>
        [Test]
        public void Test_Norm()
        {
            double norm = this.Resource.Norm;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи нормы расхода с поправкой.
        /// </summary>
        [Test]
        public void Test_Norm_Corr()
        {
            double normCorr = this.Resource.Norm_Corr;
            double newNorm_Corr = normCorr + 10;
            this.Resource.Norm_Corr = newNorm_Corr;
            Assert.AreEqual(this.Resource.Norm_Corr, newNorm_Corr);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к норме расхода расчетной.
        /// </summary>
        [Test]
        public void Test_Norm_Calc()
        {
            double normCalc = this.Resource.Norm_Calc;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расходу на объем.
        /// </summary>
        [Test]
        public void Test_Volume()
        {
            double volume = this.Resource.Volume;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи фактического расхода.
        /// </summary>
        [Test]
        public void Test_Volume_Fact()
        {
            double volumeFact = this.Resource.Volume_Fact;
            double newVolume_Fact = volumeFact + 19;
            this.Resource.Volume_Fact = newVolume_Fact;
            Assert.AreEqual(this.Resource.Volume_Fact, newVolume_Fact);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи базовой или текущей цены (в зависимости от уровня цен строки).
        /// </summary>
        [Test]
        public void Test_Price()
        {
            double price = this.Resource.Price;
            double newPrice = price + 19;
            this.Resource.Price = newPrice;
            Assert.AreEqual(this.Resource.Price, newPrice);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовой или текущей стоимости (в зависимости от уровня цен строки).
        /// </summary>
        [Test]
        public void Test_Cost()
        {
            decimal cost = this.Resource.Cost;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к идентификатору справочника цен.
        /// </summary>
        [Test]
        public void Test_PriceRefMark()
        {
            int priceRefMark = this.Resource.PriceRefMark;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к к базовой или текущей ЗПМ (в зависимости от уровня цен строки).
        /// </summary>
        [Test]
        public void Test_ZPM()
        {
            decimal zpm = this.Resource.ZPM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расчетной ТЗМ.
        /// </summary>
        [Test]
        public void Test_TZM()
        {
            decimal tzm = this.Resource.TZM;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи массы ресурса.
        /// </summary>
        [Test]
        public void Test_Mass()
        {
            double mass = this.Resource.Mass;
            double newMass = mass + 19;
            this.Resource.Mass = newMass;
            Assert.AreEqual(this.Resource.Mass, newMass);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи наименования ресурса.
        /// </summary>
        [Test]
        public void Test_Name()
        {
            string name = this.Resource.Name;
            string newName = name + " Изменено";
            this.Resource.Name = newName;
            Assert.AreEqual(this.Resource.Name, newName);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи класса груза ресурса.
        /// </summary>
        [Test]
        public void Test_CargoClass()
        {
            int cargoClass = this.Resource.CargoClass;
            int newCargoClass = cargoClass + 2;
            this.Resource.CargoClass = newCargoClass;
            Assert.AreEqual(this.Resource.CargoClass, newCargoClass);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку стороннего ресурса.
        /// </summary>
        [Test]
        public void Test_Outside()
        {
            bool outside = this.Resource.Outside;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи статуса "Включить в расчет массы" ресурса.
        /// </summary>
        [Test]
        public void Test_IncToMassCalc()
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
        [Test]
        public void Test_Literal()
        {
            bool literal = this.Resource.Literal;
        }
    }
}