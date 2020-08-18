// $Date: 2020-08-17 15:31:37 +0300 (Пн, 17 авг 2020) $
// $Revision: 373 $
// $Author: agalkin $
// Базовые тесты IAppAttributesDomain

namespace A0Tests.Smoke.App
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности домена объектов с атрибутами.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IAppAttributesDomain",
        Author = "agalkin")]
    public class Test_IA0EntityDomain : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен объектов с атрибутами.
        /// </summary>
        protected IAppAttributesDomain Domain { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Domain = this.A0.App.Attributes;
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
        /// Проверяет отсутствие ошибок при обращении к каталогу объектов с атрибутами.
        /// </summary>
        [Test]
        public void Test_Repo()
        {
            Assert.NotNull(this.Domain.Repo);
        }
    }

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога объектов с атрибутами.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IAppAttrObjRepo",
        Author = "agalkin")]
    public class Test_IAppAttrObjRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог объектов с атрибутами.
        /// </summary>
        protected IAppAttrObjRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.App.Attributes.Repo;
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
        /// Проверяет отсутствие ошибок при вызове метода чтения каталога объектов с атрибутами.
        /// </summary>
        [Test]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения описателя объекта по имени объекта.
        /// </summary>
        [Test]
        public void Test_Get()
        {
            Assert.NotNull(this.Repo.Get("A0ProjTitle"));
        }
    }
}