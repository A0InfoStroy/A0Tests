// $Date: 2020-08-05 10:52:39 +0300 (Ср, 05 авг 2020) $
// $Revision: 343 $
// $Author: agalkin $
// Тесты дерева акта

namespace A0Tests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности узла дерева акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0TreeNode",
        Author = "agalkin")]
    public class Test_IA0TreeNode : NewAct
    {
        /// <summary>
        /// Получает или устанавливает дерево ЛС.
        /// </summary>
        protected IA0TreeNode ActTreeHead { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ActTreeHead = this.Act.Tree.Head;
            Assert.NotNull(this.ActTreeHead);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ActTreeHead);
            this.ActTreeHead = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутсвие ошибок при вызове метода добавления узла.
        /// </summary>
        [Test]
        public void Test_Add()
        {
            this.ActTreeHead.Add("node");
        }

        /// <summary>
        /// Проверяет наличие дочерних узлов в головном узле дерева и обращение к ним по индексу.
        /// </summary>
        [Test]
        public void Test_Count()
        {
            int count = this.ActTreeHead.Count;
            Assert.NotZero(count);
            for (int i = 0; i < count; i++)
            {
                IA0TreeNode treeNode = this.ActTreeHead.Item[i];
                Assert.NotNull(treeNode);
            }
        }

        /// <summary>
        /// Проверяет отсутсвие ошибок при обращении к Id головного узла дерева.
        /// </summary>
        [Test]
        public void Test_ID()
        {
            int id = this.ActTreeHead.ID;
        }

        /// <summary>
        /// Проверяет отсутсвие ошибок при обращении к уровню головного узла дерева.
        /// </summary>
        [Test]
        public void Test_Level()
        {
            int level = this.ActTreeHead.Level;
        }

        /// <summary>
        /// Проверяет отсутсвие ошибок при обращении к наименованию головного узла дерева.
        /// </summary>
        [Test]
        public void Test_Name()
        {
            Assert.NotNull(this.ActTreeHead.Name);
        }

        /// <summary>
        /// Проверяет работоспособность чтения концевика головного узла дерева.
        /// </summary>
        [Test]
        public void Test_Prog()
        {
            Assert.NotNull(this.ActTreeHead.Prog);
        }

        /// <summary>
        /// Проверяет отсутсвие ошибок при обращении к полям концевика головного узла дерева.
        /// </summary>
        [Test]
        public void Test_ProgCount()
        {
            int count = this.ActTreeHead.Prog.Count();
            Assert.NotZero(count);
            for (int i = 0; i < count; i++)
            {
                IA0ProgLine progLine = this.ActTreeHead.Prog.GetItem(i);
                Assert.NotNull(progLine);
                var active = progLine.Active();
                var caption = progLine.FactorCaption();
                var kind = progLine.Kind();
                var name = progLine.Name();
                var number = progLine.Number();
                var printing = progLine.Printing();
                var summary = progLine.Summary();
                var total = progLine.Total();
            }
        }

        /// <summary>
        /// Проверяет работоспособность метода удаления дочерних узлов по индексу.
        /// </summary>
        [Test]
        public void Test_Delete()
        {
            int count = this.ActTreeHead.Count;
            Assert.NotZero(count);
            this.ActTreeHead.Delete(0);
            Assert.AreEqual(count - 1, this.ActTreeHead.Count);
        }
    }
}