// $Date: 2020-12-17 15:06:51 +0300 (Чт, 17 дек 2020) $
// $Revision: 458 $
// $Author: agalkin $
// Тесты каталога ИД комплексов

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности каталога поиска комплексов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ComplexIDRepo",
        Author = "agalkin")]
    public class Test_IA0ComplexIDRepo : Test_NewComplex
    {
        /// <summary>
        /// Проверяет чтение данных комплексов в головном комплексе.
        /// </summary>
        [Test(Description = "Тестирование метода Read с одним параметром")]
        public void Test_Read()
        {
            dynamic iterator = this.Repo.ComplexID.Read(null);
            this.ReadEstimateObject(iterator, 1);
        }

        /// <summary>
        /// Проверяет чтение данных комплексов внутри другого комплекса с указанным Guid.
        /// </summary>
        [Test(Description = "Тестирование метода Read с двумя параметрами")]
        public void Test_Read2()
        {
            dynamic iterator = this.Repo.ComplexID.Read2(this.HeadComplexGuid, null);
            this.ReadEstimateObject(iterator, 1);
        }

        /// <summary>
        /// Проверяет чтение данных комплексов внутри другого комплекса с указанным Guid и Id узла.
        /// </summary>
        [Test(Description = "Тестирование метода Read с тремя параметрами")]
        public void Test_Read3()
        {
            dynamic iterator = this.Repo.ComplexID.Read3(this.HeadComplexGuid, this.HeadNodeID, null);
            this.ReadEstimateObject(iterator, 1);
        }

        /// <summary>
        /// Проверяет чтение дополнительных атрибутов комплексов в головном комплексе.
        /// </summary>
        [Test(Description = "Тестирование метода Read с одним параметром с запросом к полям")]
        public void Test_ReadFields()
        {
            string field = "CreateDate";
            dynamic iterator = this.Repo.ComplexID.Read(this.CreateRequest(field));
            this.ReadEstimateObjectFields(iterator, field);
        }

        /// <summary>
        /// Проверяет чтение дополнительных атрибутов комплексов внутри другого комплекса с указанным Guid.
        /// </summary>
        [Test(Description = "Тестирование метода Read с двумя параметрами с запросом к полям")]
        public void Test_Read2Fields()
        {
            string field = "CreateDate";
            dynamic iterator = this.Repo.ComplexID.Read2(this.HeadComplexGuid, this.CreateRequest(field));
            this.ReadEstimateObjectFields(iterator, field);
        }

        /// <summary>
        /// Проверяет чтение дополнительных атрибутов комплексов внутри другого комплекса с указанным Guid и Id узла.
        /// </summary>
        [Test(Description = "Тестирование метода Read с тремя параметрами с запросом к полям")]
        public void Test_Read3Fields()
        {
            string field = "CreateDate";
            dynamic iterator = this.Repo.ComplexID.Read3(this.HeadComplexGuid, this.HeadNodeID, this.CreateRequest(field));
            this.ReadEstimateObjectFields(iterator, field);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра запроса к дополнительным полям в метод Read.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром")]
        public void Test_ReadErrorFields()
        {
            dynamic iterator = this.Repo.ComplexID.Read(this.CreateRequest("Error"));
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра запроса к дополнительным полям в метод Read2.
        /// </summary>
        [Test(Description = "Тестирование метода Read2 с ошибочным параметром")]
        public void Test_Read2ErrorFields()
        {
            dynamic iterator = this.Repo.ComplexID.Read2(this.HeadComplexGuid, this.CreateRequest("Error"));
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра запроса к дополнительным полям в метод Read3.
        /// </summary>
        [Test(Description = "Тестирование метода Read3 с ошибочным параметром")]
        public void Test_Read3ErrorFields()
        {
            dynamic iterator = this.Repo.ComplexID.Read3(this.HeadComplexGuid, this.HeadNodeID, this.CreateRequest("Error"));
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Создает запрос к дополнительным полям комплекса.
        /// </summary>
        /// <param name="field">Наименование поля из БД.</param>
        /// <returns>Запрос с добавленными полями.</returns>
        private dynamic CreateRequest(string field)
        {
            dynamic appFieldsReq = this.Repo.ComplexID.GetFiledRequest();
            appFieldsReq.Add(field);
            return appFieldsReq;
        }
    }

    /// <summary>
    ///  Содержит тесты проверки работоспособности запросов к дополнительным полям.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IAppFieldsRequest",
        Author = "agalkin")]
    public class Test_IAppFieldsRequest : Test_Estimate
    {
        /// <summary>
        /// Получает или устанавливает запрос к дополнительным полям.
        /// </summary>
        protected dynamic AppFieldsRequest { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.AppFieldsRequest = this.Repo.ComplexID.GetFiledRequest();
            Assert.NotNull(this.AppFieldsRequest);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.AppFieldsRequest);
            this.AppFieldsRequest = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления полей к запросу.
        /// </summary>
        [Test]
        public void Test_Add()
        {
            string field = "CreateDate";
            this.AppFieldsRequest.Add(field);
            int count = this.AppFieldsRequest.Count;
            Assert.NotZero(count);
            string item = this.AppFieldsRequest.Items[count - 1];
            Assert.AreEqual(field, item);
        }

        /// <summary>
        /// Проверяет работоспособность свойства Parent запроса.
        /// </summary>
        [Test]
        public void Test_Parent()
        {
            string field = "CreateDate";
            this.AppFieldsRequest.Add(field);
            dynamic parent = this.AppFieldsRequest.Parent;
            Assert.NotNull(parent);
            parent.Add(field);
            int count = parent.Count;
            Assert.NotZero(count);
            string item = parent.Items[count - 1];
            Assert.AreEqual(field, item);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Tree запроса.
        /// </summary>
        [Test]
        public void Test_Tree()
        {
            string field = "CreateDate";
            this.AppFieldsRequest.Add(field);
            bool tree = this.AppFieldsRequest.Tree;
        }
    }
}