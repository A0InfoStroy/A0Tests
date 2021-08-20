// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $

namespace A0Tests.EarlyTests.Integrate.Implement
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
        Author = "agalkin")]
    public class Test_IA0Act : Test_NewAct
    {
        /// <summary>
        /// Проверяет работоспособность метода удаления текстовой строки акта по Guid.
        /// </summary>
        [Test]
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
        [Test]
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
        [Test]
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
        [Test]
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
        [Test]
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
        [Test]
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
    }
}