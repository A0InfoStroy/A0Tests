// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $

namespace A0Tests.EarlyTests.Integrate.App
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности IAppAttrExtensionDomain.
    /// </summary>
    [TestFixture(Category = "Integrate",
        Description = "Тесты проверки работоспособности IAppAttrExtensionRepo",
        Author = "vbutov")]
    public class Test_IAppAttrExtensionRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает значение IA0SOOperationManager.
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
        /// Тестирует каталог расширений аттрибутов.
        /// </summary>
        [Test(Description = "Получение расширений атрибутов")]
        public void Test_Read()
        {
            IAppAttrExtensionIterator it = this.Repo.Read();

            IAppAttrExtension attrExtension = it.Next();
            Assert.NotNull(attrExtension); // Должно быть хотя бы одно расширение
            while (attrExtension != null)
            {
                Assert.IsTrue(attrExtension.Title.Name.Length > 0);
                attrExtension = it.Next();
            }
        }
    }
}