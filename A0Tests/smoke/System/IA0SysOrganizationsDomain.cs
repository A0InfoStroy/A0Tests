// $Date: 2020-08-17 15:31:37 +0300 (Пн, 17 авг 2020) $
// $Revision: 373 $
// $Author: agalkin $
// Базовые тесты IA0SysOrganizationsDomain

namespace A0Tests.Smoke.Sys
{
    using A0Service;
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
        protected IA0SysOrganizationsDomain Domain { get; private set; }

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
        protected IA0SysOrganizationsRepo Repo { get; private set; }

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