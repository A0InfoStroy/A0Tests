// $Date: 2020-08-17 15:31:37 +0300 (Пн, 17 авг 2020) $
// $Revision: 373 $
// $Author: agalkin $
// Базовые тесты IA0ActIDRepo

namespace A0Tests.Smoke.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога поиска актов.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ActIDRepo",
        Author = "agalkin")]

    public class Test_IA0ActIDRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог поиска актов.
        /// </summary>
        protected IA0ActIDRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Implement.Repo.ActID;
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
        /// Проверяет отсутствие ошибок при вызове метода получения запроса к дополнительным полям.
        /// </summary>
        [Test]
        public void Test_GetFiledsRequest()
        {
            Assert.NotNull(this.Repo.GetFiledsRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с сортировкой.
        /// </summary>
        [Test]
        public void Test_GetOrderRequest()
        {
            Assert.NotNull(this.Repo.GetOrderRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с фильтрацией.
        /// </summary>
        [Test]
        public void Test_GetWhereRequest()
        {
            Assert.NotNull(this.Repo.GetWhereRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id головного узла.
        /// </summary>
        [Test]
        public void Test_HeadNodeID()
        {
            int headNodeID = this.Repo.HeadNodeID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу актов в проекте или комплексе.
        /// </summary>
        [Test]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read(this.HeadComplexGuid, null, null, null));
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу актов в проекте или комплексе с учетом запросов.
        /// </summary>
        [Test]
        public void Test_Read2()
        {
            Assert.NotNull(this.Repo.Read(this.HeadComplexGuid, this.Repo.GetFiledsRequest(), this.Repo.GetWhereRequest(), this.Repo.GetOrderRequest()));
        }
    }
}