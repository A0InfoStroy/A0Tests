// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты IA0OSIDRepo

namespace A0Tests.Smoke.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога поиска ОС.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0OSIDRepo",
        Author = "agalkin")]
    public class Test_IA0OSIDRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог поиска ОС.
        /// </summary>
        protected IA0OSIDRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Estimate.Repo.OSID;
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
        [Test, Timeout(20000)]
        public void Test_GetFiledsRequest()
        {
            Assert.NotNull(this.Repo.GetFiledsRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с сортировкой.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_GetOrderRequest()
        {
            Assert.NotNull(this.Repo.GetOrderRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с фильтрацией.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_GetWhereRequest()
        {
            Assert.NotNull(this.Repo.GetWhereRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id головного узла.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_HeadNodeID()
        {
            int headNodeID = this.Repo.HeadNodeID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу ЛС в проекте или комплексе.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read(this.A0.Estimate.Repo.ComplexID.HeadComplexGUID, null, null, null));
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу ЛС в проекте или комплексе с учетом запросов.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Read2()
        {
            Assert.NotNull(this.Repo.Read(
                this.A0.Estimate.Repo.ComplexID.HeadComplexGUID, 
                this.Repo.GetFiledsRequest(), 
                this.Repo.GetWhereRequest(), 
                this.Repo.GetOrderRequest()));
        }
    }
}