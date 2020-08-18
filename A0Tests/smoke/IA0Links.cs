// $Date: 2020-08-17 15:31:37 +0300 (Пн, 17 авг 2020) $
// $Revision: 373 $
// $Author: agalkin $
// Базовые тесты IA0LinksDomain

namespace A0Tests.Smoke
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовые тесты проверки работоспособности домена связей между сметными данными.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0LinksDomain",
        Author = "agalkin")]
    public class Test_IA0LinksDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен связей между сметными данными.
        /// </summary>
        protected IA0LinksDomain Links { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Links = this.A0.Links;
            Assert.NotNull(this.Links);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Links);
            this.Links = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену связей между проектов.
        /// </summary>
        [Test]
        public void Test_Proj()
        {
            IA0ProjLinksDomain proj = this.Links.Proj;
            Assert.NotNull(proj);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену связей между ОС.
        /// </summary>
        [Test]
        public void Test_OS()
        {
            IA0OSLinksDomain os = this.Links.OS;
            Assert.NotNull(os);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену связей между ЛС.
        /// </summary>
        [Test]
        public void Test_LS()
        {
            IA0LSLinksDomain ls = this.Links.LS;
            Assert.NotNull(ls);
        }
    }

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности домена связей между проектами.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ProjLinksDomain",
        Author = "agalkin")]
    public class Test_IA0ProjLinksDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен связей между проектами.
        /// </summary>
        protected IA0ProjLinksDomain ProjLinks { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ProjLinks = this.A0.Links.Proj;
            Assert.NotNull(this.ProjLinks);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ProjLinks);
            this.ProjLinks = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталоку связей между проектами.
        /// </summary>
        [Test]
        public void Test_Repos()
        {
            IA0ProjLinksRepos repos = this.ProjLinks.Repos;
            Assert.NotNull(repos);
        }
    }

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности домена связей между ОС.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0OSLinksDomain",
        Author = "agalkin")]
    public class Test_IA0OSLinksDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен связей между ОС.
        /// </summary>
        protected IA0OSLinksDomain OSLinks { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.OSLinks = this.A0.Links.OS;
            Assert.NotNull(this.OSLinks);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.OSLinks);
            this.OSLinks = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталоку связей между проектами.
        /// </summary>
        [Test]
        public void Test_Repos()
        {
            IA0OSLinksRepos repos = this.OSLinks.Repos;
            Assert.NotNull(repos);
        }
    }

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности домена связей между ЛС.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0LSLinksDomain",
        Author = "agalkin")]
    public class Test_IA0LSLinksDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен связей между ЛС.
        /// </summary>
        protected IA0LSLinksDomain LSLinks { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LSLinks = this.A0.Links.LS;
            Assert.NotNull(this.LSLinks);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LSLinks);
            this.LSLinks = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталоку связей между проектами.
        /// </summary>
        [Test]
        public void Test_Repos()
        {
            IA0LSLinksRepos repos = this.LSLinks.Repos;
            Assert.NotNull(repos);
        }
    }
}