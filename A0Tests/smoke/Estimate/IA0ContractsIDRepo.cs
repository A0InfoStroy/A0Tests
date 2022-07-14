// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты IA0ContractsIDRepo

namespace A0Tests.Smoke.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит базовые тесты проверки работоспособности каталога контрактов.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ContractsIDRepo",
        Author = "agalkin")]
    public class Test_IA0ContractsIDRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог контрактов.
        /// </summary>
        protected IA0ContractsIDRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Estimate.Repo.ContractsID;
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
        [Test, Timeout(15000)]
        public void Test_GetFiledsRequest()
        {
            Assert.NotNull(this.Repo.GetFieldRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с сортировкой.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_GetOrderRequest()
        {
            Assert.NotNull(this.Repo.GetOrderRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с фильтрацией.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_GetWhereRequest()
        {
            Assert.NotNull(this.Repo.GetWhereRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу контрактов в проекте или комплексе.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read(this.A0.Estimate.Repo.ComplexID.HeadComplexGUID, null, null, null));
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу контрактов в проекте или комплексе с учетом запросов.
        /// </summary>
        [Test, Timeout(15000)]
        public void Test_Read2()
        {
            Assert.NotNull(this.Repo.Read(
                this.A0.Estimate.Repo.ComplexID.HeadComplexGUID, 
                this.Repo.GetFieldRequest(), 
                this.Repo.GetWhereRequest(), 
                this.Repo.GetOrderRequest()));
        }
    }
}