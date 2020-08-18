// $Date: 2020-08-05 10:52:39 +0300 (Ср, 05 авг 2020) $
// $Revision: 343 $
// $Author: agalkin $
// Тесты пересчета строки Акта

namespace A0Tests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для тестов пересчета строки акта.
    /// </summary>
    /// <typeparam name="T">Тип пересчета.</typeparam>
    public abstract class Test_CustomActStrRecalc<T> : Test_IA0ActStrRecalcs
        where T : class, IA0LSStrRecalc
    {
        /// <summary>
        /// Получает или устанавливает пересчет строки ЛС.
        /// </summary>
        protected IA0LSStrRecalc StringRecalc { get; set; }

        /// <summary>
        /// Получает пересчет строки ЛС.
        /// </summary>
        protected T StrRecalc => this.StringRecalc as T;

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            this.StringRecalc = null;

            // Пересчет определенного типа для конкретного теста.
            for (var i = 0; i < this.StrRecalcs.Count; ++i)
            {
                if (this.StrRecalcs.Item[i].Kind == this.GetKind())
                {
                    this.StringRecalc = this.StrRecalcs.Item[i];
                    break;
                }
            }

            Assert.IsNotNull(this.StringRecalc, "Не могу найти теребуемый тип пересчета");

            // Проверка приведения элемента к нужному типу.
            Assert.IsNotNull(this.StrRecalc);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.IsNotNull(this.StringRecalc);
            this.StringRecalc = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к типу пересчета.
        /// </summary>
        [Test(Description = "Тест Kind")]
        public void Test_Kind()
        {
            EA0LSStrRecalcKind kind = this.StrRecalc.Kind;
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
        /// <returns>Тип пересчета.</returns>
        protected abstract EA0LSStrRecalcKind GetKind();
    }

    /// <summary>
    ///  Содержит тесты пересчета "Индексация ПЗ".
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тест пересчета Индексация ПЗ")]
    public class Test_IA0ActStrRecalcIndex : Test_CustomActStrRecalc<IA0LSStrRecalcIndex>
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
        /// <returns>Тип пересчета "Индексация ПЗ".</returns>
        protected override EA0LSStrRecalcKind GetKind() => EA0LSStrRecalcKind.lsrIndex;
    }

    /// <summary>
    ///  Содержит тесты пересчёта "Индексация по обоснованию".
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тест пересчёта Индексация по обоснованию")]
    public class Test_IA0ActStrRecalcByBasing : Test_CustomActStrRecalc<IA0LSStrRecalcByBasing>
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
        /// <returns>Тип пересчета "Индексация по обоснованию".</returns>
        protected override EA0LSStrRecalcKind GetKind()
        {
            return EA0LSStrRecalcKind.lsrByBasing;
        }
    }

    /// <summary>
    /// Содержит тесты пересчёта в цены 1991 года.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тест пересчёта в цены 1991 года")]
    public class Test_IA0ActStrRecalcTo91Prices : Test_CustomActStrRecalc<IA0LSStrRecalcTo91Prices>
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
        /// <returns>Тип пересчета в цены 1991 года.</returns>
        protected override EA0LSStrRecalcKind GetKind()
        {
            return EA0LSStrRecalcKind.lsrTo91Prices;
        }
    }

    /// <summary>
    ///  Содержит тесты пересчёта "Индексация НР и СП".
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тест пересчёта Индексация НР и СП")]
    public class Test_IA0ActStrRecalcIndexNRSP : Test_CustomActStrRecalc<IA0LSStrRecalcIndexNRSP>
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
        /// <returns>Тип пересчета "Индексация НР и СП".</returns>
        protected override EA0LSStrRecalcKind GetKind()
        {
            return EA0LSStrRecalcKind.lsrIndexNRSP;
        }
    }
}