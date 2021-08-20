// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты IAppAttrExtensionDomain

namespace A0Tests.Smoke.App
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
        [Test, Timeout(15000)]
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
        [Test, Timeout(15000)]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read());
        }
    }
}