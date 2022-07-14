// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты дерева акта

namespace A0Tests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности дерева ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActTree",
        Author = "agalkin")]
    public class Test_IA0ActTree : NewAct
    {
        /// <summary>
        /// Получает или устанавливает дерево ЛС.
        /// </summary>
        protected IA0ActTree ActTree { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ActTree = this.Act.Tree;
            Assert.NotNull(this.ActTree);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ActTree);
            this.ActTree = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута"), Timeout(20000)]
        public void Test_AttrCore()
        {
            dynamic attr = this.ActTree.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута"), Timeout(20000)]
        public void Test_AttrExt()
        {
            dynamic attr = this.ActTree.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет работоспособность чтения головного узла дерева, его полей и операций.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Head()
        {
            IA0TreeNode head = this.ActTree.Head;
            Assert.NotNull(head);

            // Проверка дочерних элементов.
            int count = head.Count;
            for (int i = 0; i < count; i++)
            {
                Assert.NotNull(head.Item[i]);
            }

            int id = head.ID;
            int level = head.Level;
            string name = head.Name;
            Assert.NotNull(name);

            // Проверка концевика.
            IA0ProgLines prog = head.Prog;
            Assert.NotNull(prog);
            int progCount = prog.Count();
            for (int i = 0; i < progCount; i++)
            {
                Assert.NotNull(prog.GetItem(i));
            }

            // Добавление нового узла.
            head.Add("глава");
            Assert.AreEqual(count + 1, head.Count);

            // Удаление узла.
            head.Delete(head.Count - 1);
            Assert.AreEqual(count, head.Count);
        }

        /// <summary>
        /// Проверяет расположение строк акта на терминальных узлах.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_StringsInsideNodes()
        {
            // Создание раздела.
            IA0TreeNode head = this.ActTree.Head;
            head.Add("раздел");
            int sectionId = head.Item[0].ID;

            // Создание текстовой строки внутри раздела.
            IA0ActString str = this.Act.CreateTxtString(EA0StringKind.skMK, "1234", sectionId);
            this.ImplRepo.Act.Save(this.Act);

            // Проверка родительского узла строки ЛС.
            Assert.AreEqual(sectionId, str.ParentID);

            // Попытка создания строки на одном уровне с разделом.
            try
            {
                IA0ActString outsideStr = this.Act.CreateTxtString(EA0StringKind.skMK, "1234", this.Act.Tree.Head.ID);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 2147549183);
            }

            // Удаление строки.
            this.Act.DeleteString(str.GUID);

            // Удаление раздела.
            head.Delete(0);
        }
    }
}