// $Date: 2021-01-19 15:20:11 +0300 (Вт, 19 янв 2021) $
// $Revision: 500 $
// $Author: agalkin $
// Базовые тесты IA0LSCalculationLinkRepoID

namespace A0Tests.EarlyTests.Smoke.Links
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога связей ЛС по калькуляции.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0LSCalculationLinkRepoID",
        Author = "agalkin")]
    public class Test_IA0LSCalculationLinkRepoID : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог связей ЛС по калькуляции.
        /// </summary>
        protected IA0LSCalculationLinkRepoID Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Links.LS.Repos.CalculationID;
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
            IAppFieldsReq appRequest = this.Repo.GetFieldRequest();
            Assert.NotNull(appRequest);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с сортировкой.
        /// </summary>
        [Test]
        public void Test_GetOrderRequest()
        {
            ISQLOrder order = this.Repo.GetOrderRequest();
            Assert.NotNull(order);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с фильтрацией.
        /// </summary>
        [Test]
        public void Test_GetWhereRequest()
        {
            ISQLWhere where = this.Repo.GetWhereRequest();
            Assert.NotNull(where);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу в проекте или комплексе.
        /// </summary>
        [Test]
        public void Test_Read()
        {
            IA0FieldsIterator iter = this.Repo.Read(this.A0.Estimate.Repo.ComplexID.HeadComplexGUID, null, null, null);
            Assert.NotNull(iter);

            ISQLAsString sql = iter as ISQLAsString;
            Assert.NotNull(sql);
            Assert.NotNull(sql.Str);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу в проекте или комплексе с учетом запросов.
        /// </summary>
        [Test]
        public void Test_Read2()
        {
            IA0FieldsIterator iter = this.Repo.Read(
                this.A0.Estimate.Repo.ComplexID.HeadComplexGUID,
                this.Repo.GetFieldRequest(),
                this.Repo.GetWhereRequest(),
                this.Repo.GetOrderRequest());
            Assert.NotNull(iter);

            ISQLAsString sql = iter as ISQLAsString;
            Assert.NotNull(sql);
            Assert.NotNull(sql.Str);
        }
    }
}