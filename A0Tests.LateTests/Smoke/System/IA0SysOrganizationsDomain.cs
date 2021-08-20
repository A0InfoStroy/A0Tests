// $Date: 2020-12-11 14:38:49 +0300 (Пт, 11 дек 2020) $
// $Revision: 453 $
// $Author: agalkin $
// Базовые тесты IA0SysOrganizationsDomain

namespace A0Tests.LateTests.Smoke.Sys
{
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности домена организаций.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0SysOrganizationsDomain",
        Author = "agalkin")]
    public class Test_IA0SysOrganizationsDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен организаций.
        /// </summary>
        protected dynamic Domain { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Domain = this.A0.Sys.Organizations;
            Assert.NotNull(this.Domain);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Domain);
            this.Domain = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к сервисам домена организаций.
        /// </summary>
        [Test]
        public void Test_Services()
        {
            Assert.NotNull(this.Domain.Services);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу организаций.
        /// </summary>
        [Test]
        public void Test_Repo()
        {
            Assert.NotNull(this.Domain.Repo);
        }
    }

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталогов организаций.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0SysOrganizationsRepo",
        Author = "agalkin")]
    public class Test_IA0SysOrganizationsRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталоги организаций.
        /// </summary>
        protected dynamic Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Sys.Organizations.Repo;
            Assert.NotNull(this.Repo);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo);
            this.Repo = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу сотрудников.
        /// </summary>
        [Test]
        public void Test_EmployeeID()
        {
            Assert.NotNull(this.Repo.EmployeeID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу исполнителей.
        /// </summary>
        [Test]
        public void Test_ExecutorID()
        {
            Assert.NotNull(this.Repo.ExecutorID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу Id структур организаций.
        /// </summary>
        [Test]
        public void Test_TreeID()
        {
            Assert.NotNull(this.Repo.TreeID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу Id узлов структур организаций.
        /// </summary>
        [Test]
        public void Test_TreeNodeID()
        {
            Assert.NotNull(this.Repo.TreeNodeID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу структур организаций.
        /// </summary>
        [Test]
        public void Test_Tree()
        {
            Assert.NotNull(this.Repo.Tree);
        }
    }
}