// $Date: 2020-12-29 14:21:33 +0300 (Вт, 29 дек 2020) $
// $Revision: 480 $
// $Author: agalkin $
// Тесты ресурсов строки

namespace A0Tests.LateTests.Integrate.Implement
{
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности списка ресурсов акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Resources",
        Author = "agalkin")]
    public class Test_IA0ActResources : Test_NewActString
    {
        /// <summary>
        /// Получает или устанавливает список ресурсов.
        /// </summary>
        protected dynamic Resources { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Resources = this.ActString.Resources;
            Assert.NotNull(this.Resources);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Resources);
            this.Resources = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет наличие элементов в списке ресурсов.
        /// </summary>
        [Test]
        public void Test_Count()
        {
            Assert.Greater(this.Resources.Count, 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения элементов в списке ресурсов.
        /// </summary>
        [Test]
        public void Test_Items()
        {
            for (var i = 0; i < this.Resources.Count; ++i)
            {
                dynamic resource = this.Resources.Items[i];
                Assert.NotNull(resource);
            }
        }

        /// <summary>
        /// Проверка работоспособности метода создания ресурса по обоснованию.
        /// </summary>
        [Test]
        public void Test_CreateByBasing()
        {
            string basing = "ССЦ01-101-1865";
            string nsiBase = "ТЕР-01";
            int baseID = this.A0.Sys.NSI.Services.ByMark(nsiBase);

            dynamic resource = this.Resources.CreateByBasing(basing, baseID);
            Assert.NotNull(resource);
        }
    }
}