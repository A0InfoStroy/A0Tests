// $Date: 2022-02-16 17:23:11 +0300 (Ср, 16 фев 2022) $
// $Revision: 572 $
// $Author: vbutov $

namespace A0Tests.Integrate.Implement
{
    using System;
    using System.Collections.Generic;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Act",
        Author = "vbutov")]
    public class Test_IA0Act : NewAct
    {
        /// <summary>
        /// Проверяет работоспособность метода удаления текстовой строки акта по Guid.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_DeleteString()
        {
            // Терминальный узел для создания строки.
            IA0TreeNode node = this.GetNodeID(this.Act.Tree.Head);
            Assert.NotNull(node);

            // Создание строки.
            IA0ActString str = this.Act.CreateTxtString(EA0StringKind.skWork, "1", node.ID);
            Guid strGuid = str.GUID;
            Assert.NotNull(this.Act.Strings.ByGUID(strGuid));
            this.Act.DeleteString(strGuid);

            // Проверка отсутствия строки.
            Assert.Null(this.Act.Strings.ByGUID(strGuid));
        }

        /// <summary>
        /// Проверяет работоспособность метода удаления текстовых строк акта по индексу.
        /// </summary>
        [Test, Timeout(30000)]
        public void Test_DeleteStrings()
        {
            // Терминальный узел для создания строки.
            IA0TreeNode node = this.GetNodeID(this.Act.Tree.Head);
            Assert.NotNull(node);

            // Создание строк.
            IA0ActString str1 = this.Act.CreateTxtString(EA0StringKind.skWork, "1", node.ID);
            Guid strGuid1 = str1.GUID;
            Assert.NotNull(this.Act.Strings.ByGUID(strGuid1));
            IA0ActString str2 = this.Act.CreateTxtString(EA0StringKind.skWork, "2", node.ID);
            Guid strGuid2 = str2.GUID;
            Assert.NotNull(this.Act.Strings.ByGUID(strGuid2));
            int count = this.Act.Strings.Count;
            for (int i = 0; i < count; i++)
            {
                this.Act.Strings.Delete(i);
            }

            // Проверка отсутствия строк.
            Assert.Null(this.Act.Strings.ByGUID(strGuid1));
            Assert.Null(this.Act.Strings.ByGUID(strGuid2));
        }

        /// <summary>
        /// Проверяет работоспособность метода создания текстовых строк и их позиций в списке.
        /// </summary>
        [Test, Timeout(30000)]
        public void Test_CreateStr()
        {
            // Терминальный узел для создания строки.
            IA0TreeNode node = this.GetNodeID(this.Act.Tree.Head);
            Assert.NotNull(node);

            // Создание строк.
            for (int i = 1; i <= 10; ++i)
            {
                IA0ActString str = this.Act.CreateTxtString(EA0StringKind.skWork, $"{i} строка", node.ID);

                int actNumber = 1; // Желаемая позиция строки в акте, начинается с 1.

                // Перемещение остальных строки ниже, у которых actNumber >= желаемого.
                for (var j = 0; j < this.Act.Strings.Count; ++j)
                {
                    if (this.Act.Strings.Items[j].ActNumber >= actNumber)
                    {
                        this.Act.Strings.Items[j].ActNumber += 1;
                    }
                }

                // Желаемая позиция строки в акте, начинается с 1.
                str.ActNumber = actNumber;
            }

            // Сохранение акта.
            this.A0.Implement.Repo.Act.Save(this.Act);
            this.A0.Implement.Repo.Act.UnLock(this.Act.ID.GUID);

            IA0Act newAct = this.A0.Implement.Repo.Act.Load(this.Act.ID.GUID, EAccessKind.akRead);

            // Проверка позиции строк.
            for (var i = 0; i < this.Act.Strings.Count; ++i)
            {
                IA0ActString actStr = this.Act.Strings.Items[i];
                IA0ActString newActStr = newAct.Strings.ByGUID(actStr.GUID);

                Assert.NotNull(newActStr);
                Assert.AreEqual(actStr.ActNumber, newActStr.ActNumber);
            }
        }

        /// <summary>
        /// Проверяет работоспособность редактирования типа учета ресурсов строки акта.
        /// </summary>
        [Test, Timeout(30000)]
        public void Test_ResAccounting()
        {
            // Терминальный узел для создания строки
            IA0TreeNode node = this.GetNodeID(this.Act.Tree.Head);
            Assert.NotNull(node);

            // Создание строк.
            IA0ActString str = this.Act.CreateTxtString(
                EA0StringKind.skMK,
                "Строка для тестирования ресурса",
                node.ID);

            Assert.Greater(str.Resources.Count, 0, "У тестовой строки нет ресурсов");

            List<EA0ResAccounting> accountings = new List<EA0ResAccounting>();
            accountings.Add(EA0ResAccounting.raIncluded);
            accountings.Add(EA0ResAccounting.raExcluded);
            accountings.Add(EA0ResAccounting.raReturn);

            IA0Act newAct = this.Act;

            foreach (var acc in accountings)
            {
                // Изменение типа учета ресурсов.
                str.Resources.Items[0].Accounting = acc;

                // Сохранение акта.
                this.A0.Implement.Repo.Act.Save(newAct);
                this.A0.Implement.Repo.Act.UnLock(newAct.ID.GUID);

                // Загрузка на редактирование.
                newAct = this.A0.Implement.Repo.Act.Load(newAct.ID.GUID, EAccessKind.akEdit);

                // Поиск строки в загруженом акте.
                str = newAct.Strings.ByGUID(str.GUID);

                Assert.NotNull(str);
                Assert.AreEqual(str.Resources.Items[0].Accounting, acc);
            }
        }

        /// <summary>
        /// Проверяет работоспособность концевика акта.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Prog()
        {
            IA0ProgEstimate prog = this.Act.Prog;
            Assert.NotNull(prog);
            IA0SysTreeLevelList levelList = this.A0.Sys.Repo.GetSysTreeLevelList();
            IA0ProgSysTree sysTree = this.A0.Sys.Repo.GetProgSysTree(levelList);
            Assert.True(sysTree.HeadNode.Items.Count > 0);
            prog.Set(sysTree.HeadNode.Items.Item[0].ID);
            prog.Delele();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода пересчета.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Recalc()
        {
            this.Act.Recalc();
        }

        /// <summary>
        /// Проверяет работоспособность метода получения Id терминального узла.
        /// </summary>
        private IA0TreeNode GetNodeID(IA0TreeNode node)
        {
            if (node.Count == 0)
            {
                return node;
            }
            else
            {
                for (int i = 0; i < node.Count; i++)
                {
                    IA0TreeNode nodeItem = this.GetNodeID(node.Item[i]);
                    if (nodeItem != null)
                    {
                        return nodeItem;
                    }
                }
            }

            return null;
        }
        /// <summary>
        /// Проверяет корректность создания строки работы ЛС.
        /// </summary>
        [Test(Description = "Создание строки работы"), Timeout(30000)]
        public void Test_CreateWorkString()
        {
            int stringsCount = this.LS.Strings.Count;

            // Пример расценки из НСИ.
            // БД: a0NSI_TER_01. Таблица: Works.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Деревообрабатывающее оборудование - 2553
            // ОБОРУДОВАНИЕ ОБЩЕГО НАЗНАЧЕНИЯ. ОБОРУДОВАНИЕ ЛЕСОПИЛЬНОГО ПРОИЗВОДСТВА. РАМА ЛЕСОПИЛЬНАЯ ОДНОЭТАЖНАЯ, МАССА 4,5Т - 787669 - 787668

            IA0ActString actString = this.Act.CreateWorkString(aNSIID: 7, aFolderID: 2553, aWorkID: 787669, aNodeID: this.LS.Tree.Head.ID);
            Assert.NotNull(actString);

            Assert.True(this.Act.Strings.Count == stringsCount + 1);

            IA0ActString actStr = this.Act.Strings.Items[this.Act.Strings.Count - 1];
            Assert.AreEqual(actString.GUID, actStr.GUID);

            actStr.Volume = 10;

            // Пересчитываем смету для актуализации итогов
            Act.Recalc();

            var total = Act.Totals.ByName["9 Сметная стоимость"];

            // Стоимость 40101
            Assert.AreEqual(40101, total.Total);
        }

        /// <summary>
        /// Проверяет корректность создания строки ресурса ЛС.
        /// </summary>
        [Test(Description = "Создание строки ресурса"), Timeout(30000)]
        public void Test_CreateResString()
        {
            int stringsCount = this.LS.Strings.Count;

            // Пример расценки из НСИ. 
            // БД: a0NSI_TER_01. Таблица: Resource.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Перевозка грузов для строительства - 907
            // АСФАЛЬТОБЕТОН, РАСТВОРЫ, БЕТОН ТОВАРНЫЙ-ПОГРУЗКА - 6197091

            IA0ActString actString = this.Act.CreateResString(aNSIID: 7, aFolderID: 907, aResID: 6197091, aNodeID: this.LS.Tree.Head.ID);
            Assert.NotNull(actString);

            Assert.True(this.Act.Strings.Count == stringsCount + 1);

            IA0ActString actStr = this.Act.Strings.Items[this.Act.Strings.Count - 1];
            Assert.AreEqual(actString.GUID, actStr.GUID);

            actStr.Volume = 10;
            actStr.Resources.Items[0].Price = 10;

            // Пересчитываем смету для актуализации итогов
            Act.Recalc();

            var total = Act.Totals.ByName["9 Сметная стоимость"];

            // Стоимость 100
            Assert.AreEqual(100, total.Total);
        }
    }
}