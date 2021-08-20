// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты ресурсов строки

namespace A0Tests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности списка ресурсов акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Resources",
        Author = "agalkin")]
    public class Test_IA0ActResources : NewActString
    {
        /// <summary>
        /// Получает или устанавливает список ресурсов.
        /// </summary>
        protected IA0Resources Resources { get; private set; }

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
        [Test, Timeout(20000)]
        public void Test_Count()
        {
            Assert.Greater(this.Resources.Count, 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения элементов в списке ресурсов.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Items()
        {
            for (var i = 0; i < this.Resources.Count; ++i)
            {
                IA0Resource resource = this.Resources.Items[i];
                Assert.NotNull(resource);
            }
        }

        /// <summary>
        /// Проверка работоспособности метода создания ресурса по обоснованию.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_CreateByBasing()
        {
            string basing = "ССЦ01-101-1865";
            string nsiBase = "ТЕР-01";
            int baseID = this.A0.Sys.NSI.Services.ByMark(nsiBase);

            IA0Resource resource = this.Resources.CreateByBasing(basing, baseID);
            Assert.NotNull(resource);
        }
    }
}