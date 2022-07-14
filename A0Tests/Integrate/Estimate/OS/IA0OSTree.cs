// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты дерева ОС

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности дерева ОС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0OSTree",
        Author = "agalkin")]
    public class Test_IA0OSTree : NewOS
    {
        /// <summary>
        /// Получает или устанавливает дерево ОС.
        /// </summary>
        protected IA0Tree OSTree { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.OSTree = this.OS.Tree;
            Assert.NotNull(this.OSTree);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.OSTree);
            this.OSTree = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута"), Timeout(10000)]
        public void Test_AttrCore()
        {
            dynamic attr = this.OSTree.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута"), Timeout(10000)]
        public void Test_AttrExt()
        {
            dynamic attr = this.OSTree.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет работоспособность чтения головного узла дерева, его полей и операций.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Head()
        {
            IA0TreeNode head = this.OSTree.Head;
            Assert.NotNull(head);
            int count = head.Count;
            for (int i = 0; i < count; i++)
            {
                Assert.NotNull(head.Item[i]);
            }

            int id = head.ID;
            int level = head.Level;
            string name = head.Name;
            Assert.NotNull(name);
            IA0ProgLines prog = head.Prog;
            Assert.NotNull(prog);
            var progCount = prog.Count();
            for (int i = 0; i < progCount; i++)
            {
                Assert.NotNull(prog.GetItem(i));
            }

            head.Add("глава");
            Assert.AreEqual(count + 1, head.Count);
            head.Delete(head.Count - 1);
            Assert.AreEqual(count, head.Count);
        }
    }
}