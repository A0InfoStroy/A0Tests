// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты строки локальных смет

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности строки ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSString",
        Author = "agalkin")]
    public class Test_IA0LSString : Test_NewLSString
    {
        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к объему акта.
        /// </summary>
        [Test]
        public void Test_ActVolume()
        {
            double actVolume = this.LSString.ActVolume;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к приведенному объему.
        /// </summary>
        [Test]
        public void Test_AdjustedVolume()
        {
            double adjVolume = this.LSString.AdjustedVolume;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.LSString.Attr["LSStrTitleID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.LSString.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовой сметной стоимости.
        /// </summary>
        [Test]
        public void Test_BaseEstimate()
        {
            decimal baseEstimate = this.LSString.BaseEstimate;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому "База начисления НР".
        /// </summary>
        [Test]
        public void Test_BaseNRBase()
        {
            ENRBase baseNRBase = this.LSString.BaseNRBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому проценту начисления НР.
        /// </summary>
        [Test]
        public void Test_BaseNRProc()
        {
            double baseNRProc = this.LSString.BaseNRProc;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому "База начисления СП".
        /// </summary>
        [Test]
        public void Test_BaseSPBase()
        {
            ESPBase baseSPBase = this.LSString.BaseSPBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому проценту начисления СП.
        /// </summary>
        [Test]
        public void Test_BaseSPProc()
        {
            double baseSPProc = this.LSString.BaseSPProc;
        }

        /// <summary>
        /// Проверяет работоспособность чтения обоснования.
        /// </summary>
        [Test]
        public void Test_Basing()
        {
            string basing = this.LSString.Basing;
            Assert.NotNull(basing);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к режиму калькуляции.
        /// </summary>
        [Test]
        public void Test_CalcMode()
        {
            ECalcMode calcMode = this.LSString.CalcMode;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи комментария к обоснованию.
        /// </summary>
        [Test]
        public void Test_Comment()
        {
            string comment = this.LSString.Comment;
            this.LSString.Comment = "comment";
            Assert.AreEqual("comment", this.LSString.Comment);
            this.LSString.Comment = comment;
        }

        /// <summary>
        /// Проверяет работоспособность чтения набора комментариев к строке.
        /// </summary>
        [Test]
        public void Test_Comments()
        {
            this.LSString.Comment = "comment";
            IA0Comments comments = this.LSString.Comments;
            Assert.NotNull(comments);
            Assert.IsTrue(comments.Count > 0);
            for (int i = 0; i < comments.Count; i++)
            {
                string comment = comments.Item[i];
            }
        }

        /// <summary>
        /// Проверяет работоспособность чтения договора.
        /// </summary>
        [Test]
        public void Test_Contract()
        {
            string contract = this.LSString.Contract;
            Assert.NotNull(contract);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id договора.
        /// </summary>
        [Test]
        public void Test_ContractID()
        {
            int contractID = this.LSString.ContractID;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи номера строки сметы заказчика.
        /// </summary>
        [Test]
        public void Test_CustomerNumber()
        {
            this.LSString.CustomerNumber = "1";
            Assert.NotNull(this.LSString.CustomerNumber);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к текущей сметной стоимости.
        /// </summary>
        [Test]
        public void Test_Estimate()
        {
            decimal estimate = this.LSString.Estimate;
        }

        /// <summary>
        /// Проверяет работоспособность чтения выполнения по строке и его полей.
        /// </summary>
        [Test]
        public void Test_Executions()
        {
            Assert.NotNull(this.LSString.Executions);

            int count = this.LSString.Executions.Count;
        }

        /// <summary>
        /// Проверяет работоспособность чтения исполнителя.
        /// </summary>
        [Test]
        public void Test_Executor()
        {
            string executor = this.LSString.Executor;
            Assert.NotNull(executor);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id исполнителя.
        /// </summary>
        [Test]
        public void Test_ExecutorID()
        {
            int executorID = this.LSString.ExecutorID;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи вида затрат.
        /// </summary>
        [Test]
        public void Test_ExpenseKind()
        {
            EExpenseKind expenseKind = this.LSString.ExpenseKind;

            this.LSString.ExpenseKind = EExpenseKind.ekConstruction;
            Assert.AreEqual(EExpenseKind.ekConstruction, this.LSString.ExpenseKind);

            this.LSString.ExpenseKind = EExpenseKind.ekEquipment;
            Assert.AreEqual(EExpenseKind.ekEquipment, this.LSString.ExpenseKind);

            this.LSString.ExpenseKind = EExpenseKind.ekMounting;
            Assert.AreEqual(EExpenseKind.ekMounting, this.LSString.ExpenseKind);

            this.LSString.ExpenseKind = EExpenseKind.ekOther;
            Assert.AreEqual(EExpenseKind.ekOther, this.LSString.ExpenseKind);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи дополнительного номера строки.
        /// </summary>
        [Test]
        public void Test_ExtNumber()
        {
            this.LSString.ExtNumber = "1";
            Assert.NotNull(this.LSString.ExtNumber);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи дополнительного номера строки.
        /// </summary>
        [Test]
        public void Test_ExtraNumber()
        {
            this.LSString.ExtraNumber = "1";
            Assert.NotNull(this.LSString.ExtraNumber);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи формулы расчета объема.
        /// </summary>
        [Test]
        public void Test_Formula()
        {
            this.LSString.Formula = "formula";
            Assert.NotNull(this.LSString.Formula);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к шифру группы сборников.
        /// </summary>
        [Test]
        public void Test_Group()
        {
            string group = this.LSString.Group;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid строки.
        /// </summary>
        [Test]
        public void Test_GUID()
        {
            Guid guid = this.LSString.GUID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id строки.
        /// </summary>
        [Test]
        public void Test_ID()
        {
            int id = this.LSString.ID;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи режима включения в итоги.
        /// </summary>
        [Test]
        public void Test_IncludeKind()
        {
            this.LSString.IncludeKind = EIncludeKind.ikAdd;
            Assert.AreEqual(EIncludeKind.ikAdd, this.LSString.IncludeKind);

            this.LSString.IncludeKind = EIncludeKind.ikSub;
            Assert.AreEqual(EIncludeKind.ikSub, this.LSString.IncludeKind);

            this.LSString.IncludeKind = EIncludeKind.ikIgnore;
            Assert.AreEqual(EIncludeKind.ikIgnore, this.LSString.IncludeKind);
        }

        /// <summary>
        /// Проверяет работоспособность метода получения вида сметного объекта.
        /// </summary>
        [Test]
        public void Test_Kind()
        {
            EA0ObjectKind kind = this.LSString.Kind();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку текстовой строки.
        /// </summary>
        [Test]
        public void Test_Literal()
        {
            bool literal = this.LSString.Literal;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи номера строки.
        /// </summary>
        [Test]
        public void Test_LSNumber()
        {
            int num = 1;
            this.LSString.LSNumber = num;
            Assert.AreEqual(num, this.LSString.LSNumber);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи единицы измерения.
        /// </summary>
        [Test]
        public void Test_MUnit()
        {
            this.LSString.MUnit = "mUnit";
            Assert.NotNull(this.LSString.MUnit);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи наименования строки.
        /// </summary>
        [Test]
        public void Test_Name()
        {
            string newName = this.LSString.Name + "Изменено";
            this.LSString.Name = newName;
            Assert.AreEqual(newName, this.LSString.Name);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду базы начисления НР.
        /// </summary>
        [Test]
        public void Test_NRBase()
        {
            ENRBase nrBase = this.LSString.NRBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к проценту НР.
        /// </summary>
        [Test]
        public void Test_NRProc()
        {
            double nrProc = this.LSString.NRProc;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи наименования базы НСИ.
        /// </summary>
        [Test]
        public void Test_NSIBase()
        {
            this.LSString.NSIBase = "nsiBase";
            Assert.NotNull(this.LSString.NSIBase);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к родительскому объекту строки.
        /// </summary>
        [Test]
        public void Test_ParentID()
        {
            int parentID = this.LSString.ParentID;
        }

        /// <summary>
        /// Проверяет работоспособность источника цен.
        /// </summary>
        [Test]
        public void Test_PriceSource()
        {
            EPriceSource priceSource = this.LSString.PriceSource;

            this.LSString.PriceSource = EPriceSource.psNSI;
            Assert.AreEqual(EPriceSource.psNSI, this.LSString.PriceSource);

            this.LSString.PriceSource = EPriceSource.psPriceRef;
            Assert.AreEqual(EPriceSource.psPriceRef, this.LSString.PriceSource);

            this.LSString.PriceSource = priceSource;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ПЗ.
        /// </summary>
        [Test]
        public void Test_PZ()
        {
            decimal pz = this.LSString.PZ();
        }

        /// <summary>
        /// Проверяет работоспособность чтения пересчетов строки.
        /// </summary>
        [Test(Description = "Тест пересчетов")]
        public void Test_Recalcs()
        {
            Assert.NotNull(this.LSString.Recalcs);
        }

        /// <summary>
        /// Проверяет работоспособность чтения ресурсов строки.
        /// </summary>
        [Test]
        public void Test_Resources()
        {
            IA0Resources resources = this.LSString.Resources;
            Assert.NotNull(resources);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду базы начисления СП.
        /// </summary>
        [Test]
        public void Test_SPBase()
        {
            ESPBase spBase = this.LSString.SPBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к проценту СП.
        /// </summary>
        [Test]
        public void Test_SPProc()
        {
            double spProc = this.LSString.SPProc;
        }

        /// <summary>
        /// Проверяет работоспособность чтения копии расценки.
        /// </summary>
        [Test]
        public void Test_StrBasing()
        {
            IA0LSStrBasing strBasing = this.LSString.StrBasing;
            Assert.NotNull(strBasing);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к типу строки.
        /// </summary>
        [Test]
        public void Test_StringKind()
        {
            EA0StringKind stringKind = this.LSString.StringKind;
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогов строки.
        /// </summary>
        [Test]
        public void Test_Total()
        {
            IA0LSStrTotal total = this.LSString.Total;
            Assert.NotNull(total);
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогового объема строки.
        /// </summary>
        [Test]
        public void Test_TotalForVolume()
        {
            IA0LSStrTotal total = this.LSString.TotalForVolume;
            Assert.NotNull(total);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к сметной стоимости оборудования.
        /// </summary>
        [Test]
        public void Test_TotalOb()
        {
            decimal totalOb = this.LSString.TotalOb();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к прямым затратам.
        /// </summary>
        [Test]
        public void Test_TotalPZ()
        {
            decimal totalPz = this.LSString.TotalPZ();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к остатку объема.
        /// </summary>
        [Test]
        public void Test_TotalRemainder()
        {
            double totalRemainder = this.LSString.TotalRemainder;
        }

        /// <summary>
        /// Проверяет работоспособность получения коэффициента при единице измерения.
        /// </summary>
        [Test]
        public void Test_UnitCoef()
        {
            double defaultValue = 1d;
            double hundredSquareMetersValue = 100d;
            Assert.AreEqual(defaultValue, this.LSString.UnitCoef);
            this.LSString.MUnit = "100м2";
            Assert.AreEqual(hundredSquareMetersValue, this.LSString.UnitCoef);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи итогового объема.
        /// </summary>
        [Test]
        public void Test_TotalVolume()
        {
            double volume = 1d;
            this.LSString.TotalVolume = volume;
            Assert.AreEqual(volume, this.LSString.TotalVolume);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи объема.
        /// </summary>
        [Test]
        public void Test_Volume()
        {
            double volume = 1d;
            this.LSString.Volume = volume;
            Assert.AreEqual(volume, this.LSString.Volume);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду работ для НР и СП.
        /// </summary>
        [Test]
        public void Test_WorkKindNRSP()
        {
            string workKindNRSP = this.LSString.WorkKindNRSP;
        }
    }

    /// <summary>
    ///  Содержит тесты проверки работоспособности IA0Executions.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Executions",
        Author = "agalkin")]
    public class Test_IA0Executions : Test_LSCustom
    {
        /// <summary>
        ///  Получает или устанавливает выполнение.
        /// </summary>
        protected IA0Executions Executions { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.IsTrue(this.LS.Strings.Count > 0, "В тестовой ЛС нет строк");
            IA0LSString lsString = this.LS.Strings.Items[0];
            Assert.IsNotNull(lsString);
            this.Executions = lsString.Executions;
            Assert.NotNull(this.Executions);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Executions);
            this.Executions = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет количество выполнений.
        /// </summary>
        [Test]
        public void Test_Count()
        {
            int count = this.Executions.Count;
            Assert.Greater(count, 0, "В строке должно быть ненулевое выполнение");
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям выполнения.
        /// </summary>
        [Test]
        public void Test_Execution()
        {
            IA0Execution execution = this.Executions.Item[0];
            Assert.NotNull(execution);
            Guid actGUID = execution.ActGUID;
            Guid actStrGUID = execution.ActStrGUID;
            double adjVolume = execution.AdjustedVolume;
            string contract = execution.Contract;
            DateTime date = execution.Date;
            string executor = execution.Executor;
            EIncludeKind includeKind = execution.IncludeKind;
            string mark = execution.Mark;
            double totalVolume = execution.TotalVolume;
            double volume = execution.Volume;
        }
    }
}