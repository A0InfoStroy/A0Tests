// $Date: 2020-08-17 15:31:37 +0300 (Пн, 17 авг 2020) $
// $Revision: 373 $
// $Author: agalkin $
// Тесты поиска

namespace A0Tests.Smoke.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности поиска строк.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0Searcher",
        Author = "agalkin")]
    public class Test_IA0Searcher : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог поиска ресурсов.
        /// </summary>
        protected IA0Searcher Searcher { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Searcher = this.A0.Estimate.Repo.Searcher;
            Assert.NotNull(this.Searcher);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Searcher);
            this.Searcher = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения списка проектов.
        /// </summary>
        [Test]
        public void Test_GetProjIDs()
        {
            Assert.NotNull(this.Searcher.GetProjIDs());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода поиска.
        /// </summary>
        [Test]
        public void Test_Search()
        {
            Assert.NotNull(this.Searcher.Search(null, null, null, null, this.Searcher.GetProjIDs()));
        }
    }
}