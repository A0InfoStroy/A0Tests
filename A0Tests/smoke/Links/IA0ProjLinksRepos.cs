// $Date: 2020-08-17 15:31:37 +0300 (Пн, 17 авг 2020) $
// $Revision: 373 $
// $Author: agalkin $
// Базовые тесты IA0ProjLinksRepos

namespace A0Tests.Smoke
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога связей проектов.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ProjLinksRepos",
        Author = "agalkin")]
    public class Test_IA0ProjLinksRepos : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог связей проектов.
        /// </summary>
        protected IA0ProjLinksRepos Repos { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repos = this.A0.Links.Proj.Repos;
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
        /// Проверяет отсутствие ошибок при обращении к каталогу связей проектов по исполнению.
        /// </summary>
        [Test]
        public void Test_ExecutionID()
        {
            IA0ProjExecutionLinkRepoID executionID = this.Repos.ExecutionID;
            Assert.NotNull(executionID);
        }
    }
}