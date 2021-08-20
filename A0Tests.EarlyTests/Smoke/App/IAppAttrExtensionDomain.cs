// $Date: 2021-01-19 15:20:11 +0300 (Вт, 19 янв 2021) $
// $Revision: 500 $
// $Author: agalkin $
// Базовые тесты IAppAttrExtensionDomain

namespace A0Tests.EarlyTests.Smoke.App
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовые тесты проверки работоспособности домена расширений атрибутов.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IAppAttrExtensionDomain",
        Author = "agalkin")]
    public class Test_IAppAttrExtensionDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен расширений атрибутов.
        /// </summary>
        protected IAppAttrExtensionDomain Domain { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Domain = this.A0.App.AttrExtension;
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
        /// Проверяет отсутствие ошибок при обращении к каталогу расширений атрибутов.
        /// </summary>
        [Test]
        public void Test_Repo()
        {
            IAppAttrExtensionRepo p = this.Domain.Repo;
            Assert.NotNull(p);
        }
    }

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога расширений атрибутов.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IAppAttrExtensionRepo",
        Author = "agalkin")]
    public class Test_IAppAttrExtensionRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог расширений атрибутов.
        /// </summary>
        protected IAppAttrExtensionRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.App.AttrExtension.Repo;
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
        /// Проверяет отсутствие ошибок при вызове метода чтения каталога.
        /// </summary>
        [Test]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read());
        }
    }
}