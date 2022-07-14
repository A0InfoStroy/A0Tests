// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты IA0OSLinksRepos

namespace A0Tests.Smoke.Links
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога связей ОС.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0OSLinksRepos",
        Author = "agalkin")]
    public class Test_IA0OSLinksRepos : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог связей ОС.
        /// </summary>
        protected IA0OSLinksRepos Repos { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repos = this.A0.Links.OS.Repos;
            Assert.NotNull(this.Repos);
        }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repos);
            this.Repos = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу связей ОС по исполнению.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_ExecutionID()
        {
            IA0OSExecutionLinkRepoID executionID = this.Repos.ExecutionID;
            Assert.NotNull(executionID);
        }
    }
}