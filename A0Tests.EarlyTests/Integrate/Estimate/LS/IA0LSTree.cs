// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты дерева локальной смет

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности дерева ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSTree",
        Author = "agalkin")]
    public class Test_IA0LSTree : Test_NewLS
    {
        /// <summary>
        /// Получает или устанавливает дерево ЛС.
        /// </summary>
        protected IA0LSTree LSTree { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LSTree = this.LS.Tree;
            Assert.NotNull(this.LSTree);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LSTree);
            this.LSTree = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.LSTree.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.LSTree.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет работоспособность чтения головного узла дерева, его полей и операций.
        /// </summary>
        [Test]
        public void Test_Head()
        {
            IA0TreeNode head = this.LSTree.Head;
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
        /// Проверяет расположение строк ЛС на терминальных узлах.
        /// </summary>
        [Test]
        public void Test_StringsInsideNodes()
        {
            // Создание раздела.
            IA0TreeNode head = this.LSTree.Head;
            head.Add("раздел");
            int sectionId = head.Item[0].ID;

            // Создание текстовой строки внутри раздела.
            IA0LSString str = this.LS.CreateTxtString(EA0StringKind.skMK, "1234", sectionId);
            this.Repo.LS.Save(this.LS);

            // Проверка родительского узла строки ЛС.
            Assert.AreEqual(sectionId, str.ParentID);

            // Попытка создания строки на одном уровне с разделом.
            try
            {
                IA0LSString outsideStr = this.LS.CreateTxtString(EA0StringKind.skMK, "1234", this.LS.Tree.Head.ID);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 2147549183);
            }

            // Удаление строки.
            this.LS.DeleteString(str.GUID);

            // Удаление раздела.
            head.Delete(0);
        }
    }
}