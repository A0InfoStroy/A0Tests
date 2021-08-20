// $Date: 2020-12-29 14:21:33 +0300 (Вт, 29 дек 2020) $
// $Revision: 480 $
// $Author: agalkin $
// Тесты строки Акта

namespace A0Tests.LateTests.Integrate.Implement
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности строки акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActString",
        Author = "vbutov")]
    public class Test_IA0ActString : Test_NewActString
    {
        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic val = this.ActString.Attr["LSStrTitleID"];
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи расширенных атрибутов.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            // Описатель атрибутов по имению объекта
            dynamic attr = this.A0.App.Attributes.Repo.Get("IA0ActString");

            // Должно быть A0ActString, без первой буквы I
            string name = attr.Name;
            Assert.AreEqual("A0ActString", name);

            // Оставим только рассширенные атрибуты
            List<dynamic> extAttrs = new List<dynamic>();
            for (var i = 0; i < attr.Attributes.Count; ++i)
            {
                // Значение 1 соответствует EA0AttrKind.akExt.
                if (attr.Attributes.Items[i].Kind == 1)
                {
                    extAttrs.Add(attr.Attributes.Items[i]);
                }
            }

            Assert.Greater(extAttrs.Count, 0);

            // Изменяем значение расширенного атрибута
            foreach (var extAttr in extAttrs)
            {
                dynamic val = this.ActString.Attr[extAttr.Name];

                // Значение 1 соответствует EA0AttrValueKind.avkFloat, 0 - EA0AttrValueKind.avkInteger.
                if (extAttr.ValueKind == 1 || extAttr.ValueKind == 0)
                {
                    this.ActString.Attr[extAttr.Name] = val + 1;
                }
            }
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи номера по акту.
        /// </summary>
        [Test]
        public void Test_ActNumber()
        {
            int actNumber = this.ActString.ActNumber;
            this.ActString.ActNumber += 1;
            Assert.AreEqual(this.ActString.ActNumber, actNumber + 1);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к объему по акту.
        /// </summary>
        [Test]
        public void Test_ActVolume_Nat()
        {
            double actVolume_Nat = this.ActString.ActVolume_Nat();
        }

        /// <summary>
        /// Проверяет работоспособность метода получения вида строки (сметы или акта).
        /// </summary>
        [Test]
        public void Test_Kind()
        {
            dynamic kind = this.ActString.Kind();
            Assert.AreEqual(kind, 5); // 5 - EA0ObjectKind.okAct
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к прямым затратам на единицу измерения.
        /// </summary>
        [Test]
        public void Test_PZ()
        {
            decimal pz = this.ActString.PZ();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к остатку.
        /// </summary>
        [Test]
        public void Test_Remainder()
        {
            double remainder = this.ActString.TotalRemainder;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к сметной стоимости оборудования.
        /// </summary>
        [Test]
        public void Test_TotalOb()
        {
            decimal totalOb = this.ActString.TotalOb();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к прямым затратам.
        /// </summary>
        [Test]
        public void Test_TotalPZ()
        {
            decimal totalPZ = this.ActString.TotalPZ();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к источнику цен.
        /// </summary>
        [Test]
        public void Test_PriceSource()
        {
            dynamic priceSource = this.ActString.PriceSource;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи источника цен.
        /// </summary>
        [Test]
        public void Test_PriceSource_psNSI()
        {
            this.ActString.PriceSource = 1; // 1 - EPriceSource.psNSI
            Assert.AreEqual(this.ActString.PriceSource, 1);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи источника цен.
        /// </summary>
        [Test]
        public void Test_PriceSource_psPriceRef()
        {
            this.ActString.PriceSource = 0; // 0 - EPriceSource.psPriceRef
            Assert.AreEqual(this.ActString.PriceSource, 0);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к наименованию БД НСИ.
        /// </summary>
        [Test]
        public void Test_NSIBase()
        {
            string nsiBase = this.ActString.NSIBase;
        }

        /// <summary>
        /// Проверяет признак текстовой строки.
        /// </summary>
        [Test]
        public void Test_Literal()
        {
            bool literal = this.ActString.Literal;
            Assert.IsTrue(literal);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи наименования.
        /// </summary>
        [Test]
        public void Test_Name()
        {
            string name = this.ActString.Name;
            name = name + " Изменено";
            this.ActString.Name = name;
            Assert.AreEqual(name, this.ActString.Name);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи единицы измерения.
        /// </summary>
        [Test]
        public void Test_MUnit()
        {
            string mUnit = this.ActString.MUnit;
            mUnit = mUnit + " Изменено";
            this.ActString.MUnit = mUnit;
            Assert.AreEqual(mUnit, this.ActString.MUnit);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи режима включения в итоги.
        /// </summary>
        [Test]
        public void Test_IncludeKind()
        {
            dynamic includeKind = this.ActString.IncludeKind;

            this.ActString.IncludeKind = 0; // 0 - EIncludeKind.ikAdd
            Assert.AreEqual(0, this.ActString.IncludeKind);
            this.ActString.IncludeKind = 1; // 1 - EIncludeKind.ikSub
            Assert.AreEqual(1, this.ActString.IncludeKind);
            this.ActString.IncludeKind = 2; // 2 - EIncludeKind.ikIgnore
            Assert.AreEqual(2, this.ActString.IncludeKind);
            this.ActString.IncludeKind = 3; // 3 - EIncludeKind.ikReturn
            Assert.AreEqual(3, this.ActString.IncludeKind);
            this.ActString.IncludeKind = 4; // 4 - EIncludeKind.ikReturnWithExclude
            Assert.AreEqual(4, this.ActString.IncludeKind);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи формулы расчета объема.
        /// </summary>
        [Test]
        public void Test_Formula()
        {
            this.ActString.Formula = "formula";
            Assert.NotNull(this.ActString.Formula);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи дополнительного номера строки.
        /// </summary>
        [Test]
        public void Test_ExtNumber()
        {
            string extNumber = this.ActString.ExtNumber;
            extNumber = extNumber + " Изменено";
            this.ActString.ExtNumber = extNumber;
            Assert.AreEqual(extNumber, this.ActString.ExtNumber);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи вида затрат.
        /// </summary>
        [Test]
        public void Test_ExpenseKindd()
        {
            dynamic expenseKind = this.ActString.ExpenseKind;

            this.ActString.ExpenseKind = 0; // 0 - EExpenseKind.ekConstruction
            Assert.AreEqual(0, this.ActString.ExpenseKind);

            this.ActString.ExpenseKind = 2; // 2 - EExpenseKind.ekEquipment
            Assert.AreEqual(2, this.ActString.ExpenseKind);

            this.ActString.ExpenseKind = 1; // 1 - EExpenseKind.ekMounting
            Assert.AreEqual(1, this.ActString.ExpenseKind);

            this.ActString.ExpenseKind = 3; // 3 - EExpenseKind.ekOther
            Assert.AreEqual(3, this.ActString.ExpenseKind);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к объему по акту (факт) в сметных ед. изм.
        /// </summary>
        [Test]
        public void Test_ActVolume()
        {
            double actVolume = this.ActString.ActVolume;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id исполнителя.
        /// </summary>
        [Test]
        public void Test_ExecutorID()
        {
            int executorID = this.ActString.ExecutorID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id договора.
        /// </summary>
        [Test]
        public void Test_ContractID()
        {
            int contractID = this.ActString.ContractID;
        }

        /// <summary>
        /// Проверяет работоспособность чтения обоснования.
        /// </summary>
        [Test]
        public void Test_Basing()
        {
            string basing = this.ActString.Basing;
            Assert.NotNull(basing);
        }

        /// <summary>
        /// Проверяет работоспособность чтения договора.
        /// </summary>
        [Test]
        public void Test_Contract()
        {
            string contract = this.ActString.Contract;
            Assert.NotNull(contract);
        }

        /// <summary>
        /// Проверяет работоспособность чтения исполнителя.
        /// </summary>
        [Test]
        public void Test_Executor()
        {
            string executor = this.ActString.Executor;
            Assert.NotNull(executor);
        }

        /// <summary>
        /// Проверяет работоспособность чтения шифра группы сборников.
        /// </summary>
        [Test]
        public void Test_Group()
        {
            string group = this.ActString.Group;
            Assert.NotNull(group);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к номеру по смете.
        /// </summary>
        [Test]
        public void Test_LSNumber()
        {
            int lsNumber = this.ActString.LSNumber;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id родительского объекта.
        /// </summary>
        [Test]
        public void Test_ParentID()
        {
            int parentID = this.ActString.ParentID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к типу строки.
        /// </summary>
        [Test]
        public void Test_StringKind()
        {
            dynamic stringKind = this.ActString.StringKind;
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи итогового объема строки.
        /// </summary>
        [Test]
        public void Test_TotalVolume()
        {
            double totalVolume = this.ActString.TotalVolume;
            double newTotalVolume = totalVolume + 20;
            this.ActString.TotalVolume = newTotalVolume;
            Assert.AreEqual(this.ActString.TotalVolume, newTotalVolume);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к приведенному объему.
        /// </summary>
        [Test]
        public void Test_AdjustedVolume()
        {
            double adjustedVolume = this.ActString.AdjustedVolume;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к приведенному объему по смете.
        /// </summary>
        [Test]
        public void Test_ParentAdjustedVolume()
        {
            double parentAdjustedVolume = this.ActString.ParentTotalVolume;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id строки акта.
        /// </summary>
        [Test]
        public void Test_ID()
        {
            int id = this.ActString.ID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid строки акта.
        /// </summary>
        [Test]
        public void Test_GUID()
        {
            Guid guid = this.ActString.GUID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду работ для НР/СП.
        /// </summary>
        [Test]
        public void Test_WorkKindNRSP()
        {
            string workKindNRSP = this.ActString.WorkKindNRSP;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базе начисления НР.
        /// </summary>
        [Test]
        public void Test_NRBase()
        {
            dynamic nrBase = this.ActString.NRBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к проценту НР.
        /// </summary>
        [Test]
        public void Test_NRProc()
        {
            double nrProc = this.ActString.NRProc;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому "База начисления НР".
        /// </summary>
        [Test]
        public void Test_BaseNRBase()
        {
            dynamic baseNRBase = this.ActString.BaseNRBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому проценту начисления НР.
        /// </summary>
        [Test]
        public void Test_BaseNRProc()
        {
            double baseNRProc = this.ActString.BaseNRProc;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому "База начисления СП".
        /// </summary>
        [Test]
        public void Test_BaseSPBase()
        {
            dynamic baseSPBase = this.ActString.BaseSPBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому проценту начисления СП.
        /// </summary>
        [Test]
        public void Test_BaseSPProc()
        {
            double baseSPProc = this.ActString.BaseSPProc;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду базы начисления СП.
        /// </summary>
        [Test]
        public void Test_SPBase()
        {
            dynamic spBase = this.ActString.SPBase;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к проценту СП.
        /// </summary>
        [Test]
        public void Test_SPProc()
        {
            double spProc = this.ActString.SPProc;
        }

        /// <summary>
        /// Проверяет работоспособность чтения обоснования расценки.
        /// </summary>
        [Test]
        public void Test_StrBasing()
        {
            dynamic strBasing = this.ActString.StrBasing;
            Assert.NotNull(strBasing);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к режиму расчета.
        /// </summary>
        [Test]
        public void Test_CalcMode()
        {
            dynamic calcMode = this.ActString.CalcMode;
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогов строки (с начислением).
        /// </summary>
        [Test]
        public void Test_Total()
        {
            dynamic total = this.ActString.Total;
            Assert.NotNull(total);
        }

        /// <summary>
        /// Проверяет работоспособность чтения итогов строки (по строке).
        /// </summary>
        [Test]
        public void Test_TotalForVolume()
        {
            dynamic totalForVolume = this.ActString.TotalForVolume;
            Assert.NotNull(totalForVolume);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к сметной стоимости (основые итоги).
        /// </summary>
        [Test]
        public void Test_Estimate()
        {
            decimal estimate = this.ActString.Estimate;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к сметной стоимости (базовые итоги).
        /// </summary>
        [Test]
        public void Test_BaseEstimate()
        {
            decimal baseEstimate = this.ActString.BaseEstimate;
        }

        /// <summary>
        /// Проверяет работоспособность чтения ресурсов строки.
        /// </summary>
        [Test]
        public void Test_Resources()
        {
            dynamic resources = this.ActString.Resources;
            Assert.NotNull(resources);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи дополнительного номера строки.
        /// </summary>
        [Test]
        public void Test_ExtraNumber()
        {
            string extraNumber = this.ActString.ExtraNumber;
            extraNumber = extraNumber + "Изменено";
            this.ActString.ExtraNumber = extraNumber;
            Assert.AreEqual(extraNumber, this.ActString.ExtraNumber);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи номера строки сметы заказчика.
        /// </summary>
        [Test]
        public void Test_CustomerNumber()
        {
            string customerNumber = this.ActString.CustomerNumber;
            customerNumber = customerNumber + "Изменено";
            this.ActString.CustomerNumber = customerNumber;
            Assert.AreEqual(customerNumber, this.ActString.CustomerNumber);
        }

        /// <summary>
        /// Проверяет работоспособность чтения списка комментариев.
        /// </summary>
        [Test]
        public void Test_Comments()
        {
            dynamic comments = this.ActString.Comments;
            Assert.NotNull(comments);
            Assert.IsTrue(comments.Count > 0);
            for (int i = 0; i < comments.Count; i++)
            {
                string comment = comments.Item[i];
                Assert.NotNull(comment);
            }
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи комментария к строке акта.
        /// </summary>
        [Test]
        public void Test_Comment()
        {
            string comment = this.ActString.Comment;
            comment = comment + "Изменено";
            this.ActString.Comment = comment;
            Assert.AreEqual(comment, this.ActString.Comment);
        }

        /// <summary>
        /// Проверяет работоспособность чтения пересчетов.
        /// </summary>
        [Test]
        public void Test_Recalcs()
        {
            dynamic recalcs = this.ActString.Recalcs;
            Assert.NotNull(recalcs);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи объема.
        /// </summary>
        [Test]
        public void Test_Volume()
        {
            double volume = this.ActString.Volume;
            double newVolume = volume + 100;
            this.ActString.Volume = newVolume;
            Assert.AreEqual(this.ActString.Volume, newVolume);
        }

        /// <summary>
        /// Проверяет работоспособность чтения исполнения по родительской строке ЛС.
        /// </summary>
        [Test]
        public void Test_ParentExecutions()
        {
            dynamic parentExecutions = this.ActString.ParentExecutions;
            Assert.NotNull(parentExecutions);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id строки ЛС.
        /// </summary>
        [Test]
        public void Test_ParentStrID()
        {
            int parentStrId = this.ActString.ParentStrID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid строки ЛС.
        /// </summary>
        [Test]
        public void Test_ParentStrGUID()
        {
            Guid parentStrGuid = this.ActString.ParentStrGUID;
        }

        /// <summary>
        /// Проверяет работоспособность получения коэффициента при единице измерения.
        /// </summary>
        [Test]
        public void Test_UnitCoef()
        {
            double defaultValue = 1d;
            double hundredSquareMetersValue = 100d;
            Assert.AreEqual(defaultValue, this.ActString.UnitCoef);
            this.ActString.MUnit = "100м2";
            Assert.AreEqual(hundredSquareMetersValue, this.ActString.UnitCoef);
        }
    }
}