// $Date: 2020-12-18 16:57:27 +0300 (Пт, 18 дек 2020) $
// $Revision: 462 $
// $Author: agalkin $
// Тесты дерева ОС

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности дерева ОС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0OSTree",
        Author = "agalkin")]
    public class Test_IA0OSTree : Test_NewOS
    {
        /// <summary>
        /// Получает или устанавливает дерево ОС.
        /// </summary>
        protected dynamic OSTree { get; set; }

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
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.OSTree.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.OSTree.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет работоспособность чтения головного узла дерева, его полей и операций.
        /// </summary>
        [Test]
        public void Test_Head()
        {
            dynamic head = this.OSTree.Head;
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
            dynamic prog = head.Prog;
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