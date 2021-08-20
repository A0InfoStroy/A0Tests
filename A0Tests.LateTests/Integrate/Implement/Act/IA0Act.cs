// $Date: 2020-12-29 14:21:33 +0300 (Вт, 29 дек 2020) $
// $Revision: 480 $
// $Author: agalkin $

namespace A0Tests.LateTests.Integrate.Implement
{
    using System;
    using System.Collections.Generic;
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
            dynamic node = this.GetNodeID(this.Act.Tree.Head);
            Assert.NotNull(node);

            // Создание строки.
            dynamic str = this.Act.CreateTxtString(0, "1", node.ID);
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
            dynamic node = this.GetNodeID(this.Act.Tree.Head);
            Assert.NotNull(node);

            // Создание строк.
            dynamic str1 = this.Act.CreateTxtString(0, "1", node.ID);
            Guid strGuid1 = str1.GUID;
            Assert.NotNull(this.Act.Strings.ByGUID(strGuid1));
            dynamic str2 = this.Act.CreateTxtString(0, "2", node.ID);
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
            dynamic node = this.GetNodeID(this.Act.Tree.Head);
            Assert.NotNull(node);

            // Создание строк.
            for (int i = 1; i <= 10; ++i)
            {
                dynamic str = this.Act.CreateTxtString(0, $"{i} строка", node.ID);

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

            dynamic newAct = this.A0.Implement.Repo.Act.Load(this.Act.ID.GUID, 0);

            // Проверка позиции строк.
            for (var i = 0; i < this.Act.Strings.Count; ++i)
            {
                dynamic actStr = this.Act.Strings.Items[i];
                dynamic newActStr = newAct.Strings.ByGUID(actStr.GUID);

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
            dynamic node = this.GetNodeID(this.Act.Tree.Head);
            Assert.NotNull(node);

            // Создание строк.
            dynamic str = this.Act.CreateTxtString(
                3,
                "Строка для тестирования ресурса",
                node.ID); // Значение 3 соответствует EA0StringKind.skMK.

            Assert.Greater(str.Resources.Count, 0, "У тестовой строки нет ресурсов");

            List<dynamic> accountings = new List<dynamic>();
            accountings.Add(0); // EA0ResAccounting.raIncluded
            accountings.Add(5); // EA0ResAccounting.raExcluded
            accountings.Add(3); // EA0ResAccounting.raReturn

            dynamic newAct = this.Act;

            foreach (var acc in accountings)
            {
                // Изменение типа учета ресурсов.
                str.Resources.Items[0].Accounting = acc;

                // Сохранение акта.
                this.A0.Implement.Repo.Act.Save(newAct);
                this.A0.Implement.Repo.Act.UnLock(newAct.ID.GUID);

                // Загрузка на редактирование.
                newAct = this.A0.Implement.Repo.Act.Load(newAct.ID.GUID, 1);

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
            dynamic prog = this.Act.Prog;
            Assert.NotNull(prog);
            dynamic levelList = this.A0.Sys.Repo.GetSysTreeLevelList();
            dynamic sysTree = this.A0.Sys.Repo.GetProgSysTree(levelList);
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
        private dynamic GetNodeID(dynamic node)
        {
            if (node.Count == 0)
            {
                return node;
            }
            else
            {
                for (int i = 0; i < node.Count; i++)
                {
                    dynamic nodeItem = this.GetNodeID(node.Item[i]);
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