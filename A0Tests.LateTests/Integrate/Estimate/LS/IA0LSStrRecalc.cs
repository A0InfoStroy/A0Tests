// $Date: 2020-12-29 14:03:02 +0300 (Вт, 29 дек 2020) $
// $Revision: 478 $
// $Author: agalkin $
// Тесты пересчета строки локальных смет

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для тестов пересчета строки ЛС.
    /// </summary>
    public abstract class Test_LSStrRecalc : Test_IA0LSStrRecalcs
    {
        /// <summary>
        /// Получает или устанавливает пересчет строки ЛС.
        /// </summary>
        protected dynamic StrRecalc { get; set; }


        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            this.StrRecalc = null;

            // Пересчет определенного типа для конкретного теста.
            for (var i = 0; i < this.StrRecalcs.Count; ++i)
            {
                if (this.StrRecalcs.Item[i].Kind == this.RecalcKind)
                {
                    this.StrRecalc = this.StrRecalcs.Item[i];
                    break;
                }
            }

            Assert.IsNotNull(this.StrRecalc, "Не могу найти теребуемый тип пересчета");
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.IsNotNull(this.StrRecalc);
            this.StrRecalc = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к типу пересчета.
        /// </summary>
        [Test(Description = "Тест Kind")]
        public void Test_Kind()
        {
            dynamic kind = this.StrRecalc.Kind;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к наименованию пересчета.
        /// </summary>
        [Test(Description = "Тест Name")]
        public void Test_Name()
        {
            string name = this.StrRecalc.Name;
            Assert.NotNull(name);
        }

        /// <summary>
        /// Получает тип пересчета.
        /// </summary>
        protected abstract int RecalcKind { get; }
    }

    /// <summary>
    ///  Содержит тесты пересчета "Индексация ПЗ".
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тест пересчета Индексация ПЗ")]
    public class Test_IA0LSStrRecalcIndex : Test_LSStrRecalc
    {
        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id таблицы.
        /// </summary>
        [Test(Description = "Тест TableID")]
        public void Test_TableID()
        {
            int tableID = this.StrRecalc.TableID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id строки таблицы.
        /// </summary>
        [Test(Description = "Тест TableRow")]
        public void Test_TableRow()
        {
            int tableRow = this.StrRecalc.TableRow;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Только базовые итоги".
        /// </summary>
        [Test(Description = "Тест BaseTotals")]
        public void Test_BaseTotals()
        {
            bool baseTotals = this.StrRecalc.BaseTotals;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Только стандартные итоги".
        /// </summary>
        [Test(Description = "Тест StandardTotals")]
        public void Test_StandardTotals()
        {
            bool standardTotals = this.StrRecalc.StandardTotals;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку составного индекса.
        /// </summary>
        [Test(Description = "Тест CompositeIndex")]
        public void Test_CompositeIndex()
        {
            bool standardTotals = this.StrRecalc.CompositeIndex;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку операции деления.
        /// </summary>
        [Test(Description = "Тест Divide")]
        public void Test_Divide()
        {
            bool divide = this.StrRecalc.Divide;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Применить к нормам ТЗР".
        /// </summary>
        [Test(Description = "Тест ChangeTZOR")]
        public void Test_ChangeTZOR()
        {
            bool divide = this.StrRecalc.ChangeTZOR;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Применить к нормам ЭМ".
        /// </summary>
        [Test(Description = "Тест ChangeMashMech")]
        public void Test_ChangeMashMech()
        {
            bool changeMashMech = this.StrRecalc.ChangeMashMech;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Применить к нормам ТЗМ".
        /// </summary>
        [Test(Description = "Тест ChangeTZM")]
        public void Test_ChangeTZM()
        {
            bool changeTZM = this.StrRecalc.ChangeTZM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Применить к нормам МЗ".
        /// </summary>
        [Test(Description = "Тест ChangeMaterial")]
        public void Test_ChangeMaterial()
        {
            bool changeMaterial = this.StrRecalc.ChangeMaterial;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Применить к нормам всех ресурсов".
        /// </summary>
        [Test(Description = "Тест ChangeAllResources")]
        public void Test_ChangeAllResources()
        {
            bool changeAllResources = this.StrRecalc.ChangeAllResources;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ОЗП.
        /// </summary>
        [Test(Description = "Тест ToOZP")]
        public void Test_ToOZP()
        {
            double toOZP = this.StrRecalc.ToOZP;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ЭМ.
        /// </summary>
        [Test(Description = "Тест ToEM")]
        public void Test_ToEM()
        {
            double toEM = this.StrRecalc.ToEM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ЗПМ.
        /// </summary>
        [Test(Description = "Тест ToZPM")]
        public void Test_ToZPM()
        {
            double toZPM = this.StrRecalc.ToZPM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для МЗ.
        /// </summary>
        [Test(Description = "Тест ToMZ")]
        public void Test_ToMZ()
        {
            double toMZ = this.StrRecalc.ToMZ;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к единому индексу.
        /// </summary>
        [Test(Description = "Тест Uni")]
        public void Test_Uni()
        {
            double uni = this.StrRecalc.Uni;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "К 'чистой' ЭМ".
        /// </summary>
        [Test(Description = "Тест UsePureEM")]
        public void Test_UsePureEM()
        {
            bool usePureEM = this.StrRecalc.UsePureEM;
        }

        /// <summary>
        /// Получает тип пересчета.
        /// </summary>
        protected override int RecalcKind => 0; // Значение 0 соответствует EA0LSStrRecalcKind.lsrIndex
    }

    /// <summary>
    ///  Содержит тесты пересчёта "Индексация по обоснованию".
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тест пересчёта Индексация по обоснованию")]
    public class Test_IA0LSStrRecalcByBasing : Test_LSStrRecalc
    {
        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Только базовые итоги".
        /// </summary>
        [Test(Description = "Тест BaseTotals")]
        public void Test_BaseTotals()
        {
            bool baseTotals = this.StrRecalc.BaseTotals;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Только основные итоги".
        /// </summary>
        [Test(Description = "Тест StandardTotals")]
        public void Test_StandardTotals()
        {
            bool standardTotals = this.StrRecalc.StandardTotals;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку составного индекса.
        /// </summary>
        [Test(Description = "Тест CompositeIndex")]
        public void Test_CompositeIndex()
        {
            bool compositeIndex = this.StrRecalc.CompositeIndex;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку операции деления.
        /// </summary>
        [Test(Description = "Тест Divide")]
        public void Test_Divide()
        {
            bool divide = this.StrRecalc.Divide;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ОЗП.
        /// </summary>
        [Test(Description = "Тест ToOZP")]
        public void Test_ToOZP()
        {
            double toOZP = this.StrRecalc.ToOZP;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ЭМ.
        /// </summary>
        [Test(Description = "Тест ToEM")]
        public void Test_ToEM()
        {
            double toEM = this.StrRecalc.ToEM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ЗПМ.
        /// </summary>
        [Test(Description = "Тест ToZPM")]
        public void Test_ToZPM()
        {
            double toZPM = this.StrRecalc.ToZPM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для МЗ.
        /// </summary>
        [Test(Description = "Тест ToMZ")]
        public void Test_ToMZ()
        {
            double toMZ = this.StrRecalc.ToMZ;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к единому индексу.
        /// </summary>
        [Test(Description = "Тест Uni")]
        public void Test_Uni()
        {
            double uni = this.StrRecalc.Uni;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "К 'чистой' ЭМ".
        /// </summary>
        [Test(Description = "Тест UsePureEM")]
        public void Test_UsePureEM()
        {
            bool usePureEM = this.StrRecalc.UsePureEM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id справочника индексов.
        /// </summary>
        [Test(Description = "Тест IndexRefID")]
        public void Test_IndexRefID()
        {
            int indexRefID = this.StrRecalc.IndexRefID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к шифру справочника цен.
        /// </summary>
        [Test(Description = "Тест IndexRefMark")]
        public void Test_IndexRefMark()
        {
            string indexRefMark = this.StrRecalc.IndexRefMark;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к наименования справочника цен.
        /// </summary>
        [Test(Description = "Тест IndexRefName")]
        public void Test_IndexRefName()
        {
            string indexRefName = this.StrRecalc.IndexRefName;
        }

        /// <summary>
        /// Получает тип пересчета.
        /// </summary>
        protected override int RecalcKind => 1; // Значение 1 соответствует EA0LSStrRecalcKind.lsrByBasing
    }

    /// <summary>
    /// Содержит тесты пересчёта в цены 1991 года.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тест пересчёта в цены 1991 года")]
    public class Test_IA0LSStrRecalcTo91Prices : Test_LSStrRecalc
    {
        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ОЗП.
        /// </summary>
        [Test(Description = "Тест ToOZP")]
        public void Test_ToOZP()
        {
            double toOZP = this.StrRecalc.ToOZP;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ЭМ.
        /// </summary>
        [Test(Description = "Тест ToEM")]
        public void Test_ToEM()
        {
            double toEM = this.StrRecalc.ToEM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ЗПМ.
        /// </summary>
        [Test(Description = "Тест ToZPM")]
        public void Test_ToZPM()
        {
            double toZPM = this.StrRecalc.ToZPM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для МЗ из РП.
        /// </summary>
        [Test(Description = "Тест ToMZFromRP")]
        public void Test_ToMZFromRP()
        {
            double toMZFromRP = this.StrRecalc.ToMZFromRP;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для МЗ из УВР.
        /// </summary>
        [Test(Description = "Тест ToMZFromUVR")]
        public void Test_ToMZFromUVR()
        {
            double toMZFromUVR = this.StrRecalc.ToMZFromUVR;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для оборудования.
        /// </summary>
        [Test(Description = "Тест ToEq")]
        public void Test_ToEq()
        {
            double toEq = this.StrRecalc.ToEq;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ОМ.
        /// </summary>
        [Test(Description = "Тест ToOM")]
        public void ToOM()
        {
            double toOM = this.StrRecalc.ToOM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу для ТР.
        /// </summary>
        [Test(Description = "Тест ToTr")]
        public void ToTr()
        {
            double toTr = this.StrRecalc.ToTr;
        }

        /// <summary>
        /// Получает тип пересчета.
        /// </summary>
        protected override int RecalcKind => 2; // Значение 2 соответствует EA0LSStrRecalcKind.lsrTo91Prices
    }

    /// <summary>
    ///  Содержит тесты пересчёта "Индексация НР и СП".
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тест пересчёта Индексация НР и СП")]
    public class Test_IA0LSStrRecalcIndexNRSP : Test_LSStrRecalc
    {
        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Только базовые итоги".
        /// </summary>
        [Test(Description = "Тест BaseTotals")]
        public void Test_BaseTotals()
        {
            bool baseTotals = this.StrRecalc.BaseTotals;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку "Только основные итоги".
        /// </summary>
        [Test(Description = "Тест StandardTotals")]
        public void Test_StandardTotals()
        {
            bool standardTotals = this.StrRecalc.StandardTotals;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку операции деления.
        /// </summary>
        [Test(Description = "Тест Divide")]
        public void Test_Divide()
        {
            bool divide = this.StrRecalc.Divide;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу к НР.
        /// </summary>
        [Test(Description = "Тест ToNR")]
        public void Test_ToNR()
        {
            double toNR = this.StrRecalc.ToNR;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к индексу к СП.
        /// </summary>
        [Test(Description = "Тест ToSP")]
        public void Test_ToSP()
        {
            double toSP = this.StrRecalc.ToSP;
        }

        /// <summary>
        /// Получает тип пересчета.
        /// </summary>
        protected override int RecalcKind => 3; // Значение 3 соответствует EA0LSStrRecalcKind.lsrIndexNRSP
    }
}