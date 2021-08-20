// $Date: 2020-12-17 15:06:51 +0300 (Чт, 17 дек 2020) $
// $Revision: 458 $
// $Author: agalkin $
// Тесты дерева комплекса

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности дерева комплекса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Tree",
        Author = "agalkin")]
    public class Test_IA0ComplexTree : Test_NewComplex
    {
        /// <summary>
        /// Получает или устанавливает дерево комплекса.
        /// </summary>
        protected dynamic ComplexTree { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ComplexTree = this.Complex.Tree;
            Assert.NotNull(this.ComplexTree);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ComplexTree);
            this.ComplexTree = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому аттрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.ComplexTree.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному аттрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.ComplexTree.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет работоспособность головного элемента дерева.
        /// </summary>
        [Test]
        public void Test_Head()
        {
            dynamic head = this.ComplexTree.Head;
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