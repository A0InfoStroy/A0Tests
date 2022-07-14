﻿// $Date: 2022-07-13 20:57:53 +0300 (Ср, 13 июл 2022) $
// $Revision: 586 $
// $Author: eloginov $
// Базовые тесты IA0LSString.

namespace A0Tests.Integrate.Estimate.LSString
{
    using A0Service;
    using NUnit.Framework;
    using System;

    /// <summary>
    /// Базовый класс для создания абстрактной тестируемой строки ЛС.
    /// </summary>
    public abstract class Test_IA0LSStringBase : NewLSStringBase
    {
        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к объему акта.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ActVolume()
        {
            double actVolume = this.LSString.ActVolume;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к приведенному объему.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_AdjustedVolume()
        {
            double adjVolume = this.LSString.AdjustedVolume;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута"), Timeout(10000)]
        public virtual void Test_AttrCore()
        {
            dynamic attr = this.LSString.Attr["LSStrTitleID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута"), Timeout(10000)]
        public virtual void Test_AttrExt()
        {
            dynamic attr = this.LSString.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовой сметной стоимости.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_BaseEstimate()
        {
            decimal baseEstimate = this.LSString.BaseEstimate;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому "База начисления НР".
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_BaseNRBase()
        {
            ENRBase baseNRBase = this.LSString.BaseNRBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому проценту начисления НР.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_BaseNRProc()
        {
            double baseNRProc = this.LSString.BaseNRProc;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому "База начисления СП".
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_BaseSPBase()
        {
            ESPBase baseSPBase = this.LSString.BaseSPBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому проценту начисления СП.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_BaseSPProc()
        {
            double baseSPProc = this.LSString.BaseSPProc;
        }

        /// <summary>
        /// Проверяет работоспособность чтения обоснования.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Basing()
        {
            string basing = this.LSString.Basing;
            Assert.NotNull(basing);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к режиму калькуляции.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_CalcMode()
        {
            ECalcMode calcMode = this.LSString.CalcMode;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи комментария к обоснованию.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Comment()
        {
            string comment = this.LSString.Comment;
            this.LSString.Comment = "comment";
            Assert.AreEqual("comment", this.LSString.Comment);
            this.LSString.Comment = comment;
        }

        /// <summary>
        /// Проверяет работоспособность чтения набора комментариев к строке.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Comments()
        {
            Console.WriteLine("in base Test_comments()");
            IA0Comments comments = this.LSString.Comments;
            Assert.NotNull(comments);
            Assert.IsTrue(comments.Count > 0);
            for (int i = 0; i < comments.Count; i++)
            {
                Assert.NotNull(comments.Item[i]);
            }
        }

        /// <summary>
        /// Проверяет работоспособность чтения договора.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Contract()
        {
            string contract = this.LSString.Contract;
            Assert.NotNull(contract);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id договора.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ContractID()
        {
            int contractID = this.LSString.ContractID;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи номера строки сметы заказчика.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_CustomerNumber()
        {
            this.LSString.CustomerNumber = "1";
            Assert.NotNull(this.LSString.CustomerNumber);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к текущей сметной стоимости.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Estimate()
        {
            decimal estimate = this.LSString.Estimate;
        }

        /// <summary>
        /// Проверяет работоспособность чтения выполнения по строке и его полей.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Executions()
        {
            Assert.NotNull(this.LSString.Executions);

            int count = this.LSString.Executions.Count;
        }

        /// <summary>
        /// Проверяет работоспособность чтения исполнителя.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Executor()
        {
            string executor = this.LSString.Executor;
            Assert.NotNull(executor);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id исполнителя.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ExecutorID()
        {
            int executorID = this.LSString.ExecutorID;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи вида затрат.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ExpenseKind()
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
        [Test, Timeout(10000)]
        public virtual void Test_ExtNumber()
        {
            this.LSString.ExtNumber = "1";
            Assert.NotNull(this.LSString.ExtNumber);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи дополнительного номера строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ExtraNumber()
        {
            this.LSString.ExtraNumber = "1";
            Assert.NotNull(this.LSString.ExtraNumber);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи формулы расчета объема.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Formula()
        {
            this.LSString.Formula = "formula";
            Assert.NotNull(this.LSString.Formula);
        }

        /// <summary>
        /// Проверяет работоспособность чтения шифра группы сборников.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Group()
        {
            string group = this.LSString.Group;
            Assert.NotNull(group);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_GUID()
        {
            Guid guid = this.LSString.GUID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ID()
        {
            int id = this.LSString.ID;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи режима включения в итоги.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_IncludeKind()
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
        [Test, Timeout(10000)]
        public virtual void Test_Kind()
        {
            EA0ObjectKind kind = this.LSString.Kind();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к признаку текстовой строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Literal()
        {
            bool literal = this.LSString.Literal;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи номера строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_LSNumber()
        {
            int num = 1;
            this.LSString.LSNumber = num;
            Assert.AreEqual(num, this.LSString.LSNumber);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи единицы измерения.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_MUnit()
        {
            this.LSString.MUnit = "mUnit";
            Assert.NotNull(this.LSString.MUnit);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи наименования строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Name()
        {
            string newName = this.LSString.Name + "Изменено";
            this.LSString.Name = newName;
            Assert.AreEqual(newName, this.LSString.Name);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду базы начисления НР.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_NRBase()
        {
            ENRBase nrBase = this.LSString.NRBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к проценту НР.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_NRProc()
        {
            double nrProc = this.LSString.NRProc;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи наименования базы НСИ.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_NSIBase()
        {
            this.LSString.NSIBase = "nsiBase";
            Assert.NotNull(this.LSString.NSIBase);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к родительскому объекту строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ParentID()
        {
            int parentID = this.LSString.ParentID;
        }

        /// <summary>
        /// Проверяет работоспособность источника цен.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_PriceSource()
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
        [Test, Timeout(10000)]
        public virtual void Test_PZ()
        {
            decimal pz = this.LSString.PZ();
        }

        /// <summary>
        /// Проверяет работоспособность чтения пересчетов строки.
        /// </summary>
        [Test(Description = "Тест пересчетов"), Timeout(10000)]
        public virtual void Test_Recalcs()
        {
            Assert.NotNull(this.LSString.Recalcs);
        }

        /// <summary>
        /// Проверяет работоспособность чтения ресурсов строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Resources()
        {
            IA0Resources resources = this.LSString.Resources;
            Assert.NotNull(resources);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду базы начисления СП.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_SPBase()
        {
            ESPBase spBase = this.LSString.SPBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к проценту СП.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_SPProc()
        {
            double spProc = this.LSString.SPProc;
        }

        /// <summary>
        /// Проверяет работоспособность чтения копии расценки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_StrBasing()
        {
            IA0LSStrBasing strBasing = this.LSString.StrBasing;
            Assert.NotNull(strBasing);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к типу строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_StringKind()
        {
            EA0StringKind stringKind = this.LSString.StringKind;
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогов строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Total()
        {
            IA0LSStrTotal total = this.LSString.Total;
            Assert.NotNull(total);
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогового объема строки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_TotalForVolume()
        {
            IA0LSStrTotal total = this.LSString.TotalForVolume;
            Assert.NotNull(total);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к сметной стоимости оборудования.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_TotalOb()
        {
            decimal totalOb = this.LSString.TotalOb();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к прямым затратам.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_TotalPZ()
        {
            decimal totalPz = this.LSString.TotalPZ();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к остатку объема.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_TotalRemainder()
        {
            double totalRemainder = this.LSString.TotalRemainder;
        }

        /// <summary>
        /// Проверяет работоспособность получения коэффициента при единице измерения.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_UnitCoef()
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
        [Test, Timeout(10000)]
        public virtual void Test_TotalVolume()
        {
            double volume = 1d;
            this.LSString.TotalVolume = volume;
            Assert.AreEqual(volume, this.LSString.TotalVolume);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи объема.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Volume()
        {
            double volume = 1d;
            this.LSString.Volume = volume;
            Assert.AreEqual(volume, this.LSString.Volume);
        }

        /// <summary>
        /// Проверяет работоспособность чтения вида работ для НР и СП.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_WorkKindNRSP()
        {
            string workKindNRSP = this.LSString.WorkKindNRSP;
            Assert.NotNull(workKindNRSP);
        }

    }
}
