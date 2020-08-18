// $Date: 2020-08-17 15:35:09 +0300 (Пн, 17 авг 2020) $
// $Revision: 374 $
// $Author: agalkin $
// Базовые тесты IA0LSLinksRepos

namespace A0Tests.Smoke.Links
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога связей ЛС.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0LSLinksRepos",
        Author = "agalkin")]
    public class Test_IA0LSLinksRepos : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог связей ЛС.
        /// </summary>
        protected IA0LSLinksRepos Repos { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repos = this.A0.Links.LS.Repos;
            Assert.NotNull(this.Repos);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repos);
            this.Repos = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу связей ЛС по изменению.
        /// </summary>
        [Test]
        public void Test_MutationID()
        {
            IA0LSMutationLinkRepoID mutationId = this.Repos.MutationID;
            Assert.NotNull(mutationId);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу связей ЛС по исполнению.
        /// </summary>
        [Test]
        public void Test_ExecutionID()
        {
            IA0LSExecutionLinkRepoID executionID = this.Repos.ExecutionID;
            Assert.NotNull(executionID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу связей ЛС по калькуляции.
        /// </summary>
        [Test]
        public void Test_CalculationID()
        {
            IA0LSCalculationLinkRepoID calculationID = this.Repos.CalculationID;
            Assert.NotNull(calculationID);
        }
    }
}