// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты дерева проекта

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности дерева проекта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Tree",
        Author = "agalkin")]
    public class Test_IA0ProjTree : NewProj
    {
        /// <summary>
        /// Получает или устанавливает дерево проекта.
        /// </summary>
        protected IA0Tree ProjTree { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ProjTree = this.Proj.Tree;
            Assert.NotNull(this.ProjTree);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ProjTree);
            this.ProjTree = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому аттрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута"), Timeout(10000)]
        public void Test_AttrCore()
        {
            dynamic attr = this.ProjTree.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному аттрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута"), Timeout(10000)]
        public void Test_AttrExt()
        {
            dynamic attr = this.ProjTree.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет работоспособность головного элемента дерева.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Head()
        {
            IA0TreeNode head = this.ProjTree.Head;
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