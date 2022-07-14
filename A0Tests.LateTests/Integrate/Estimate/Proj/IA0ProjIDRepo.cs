// $Date: 2020-12-18 13:18:31 +0300 (Пт, 18 дек 2020) $
// $Revision: 460 $
// $Author: agalkin $
// Тесты каталога проектов

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;
    using System;

    /// <summary>
    ///  Содержит тесты проверки работоспособности каталога поиска проектов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ProjIDRepo",
        Author = "agalkin")]
    public class Test_IA0ProjIDRepo : Test_NewProj
    {
        /// <summary>
        /// Проверяет корректность чтения данных проекта в головном комплексе.
        /// </summary>
        [Test(Description = "Тестирование метода Read с одним параметром")]
        public void Test_Read()
        {
            dynamic iterator = this.Repo.ProjID.Read(null);
            this.ReadEstimateObject(iterator, 2);
        }

        /// <summary>
        /// Проверяет чтение данных проекта внутри другого комплекса с указанным Guid.
        /// </summary>
        [Test(Description = "Тестирование метода Read с двумя параметрами")]
        public void Test_Read2()
        {
            dynamic iterator = this.Repo.ProjID.Read2(this.HeadComplexGuid, null);
            this.ReadEstimateObject(iterator, 2);
        }

        /// <summary>
        /// Проверяет чтение данных проекта внутри комплекса с указанным Guid и Id узла.
        /// </summary>
        [Test(Description = "Тестирование метода Read с тремя параметрами")]
        public void Test_Read3()
        {
            dynamic iterator = this.Repo.ProjID.Read3(this.HeadComplexGuid, this.HeadNodeID, null);
            this.ReadEstimateObject(iterator, 2);
        }

        /// <summary>
        /// Проверяет чтение дополнительных полей проекта в головном комплексе.
        /// </summary>
        [Test(Description = "Тестирование метода Read с одним параметром с запросом к полям")]
        public void Test_ReadFields()
        {
            string field = "CreateDate";
            dynamic iterator = this.Repo.ProjID.Read(this.CreateRequest(field));
            this.ReadEstimateObjectFields(iterator, field);
        }

        /// <summary>
        /// Проверяет чтение дополнительных полей проектов внутри комплекса с указанным Guid.
        /// </summary>
        [Test(Description = "Тестирование метода Read с двумя параметрами с запросом к полям")]
        public void Test_Read2Fields()
        {
            string field = "CreateDate";
            dynamic iterator = this.Repo.ProjID.Read2(this.HeadComplexGuid, this.CreateRequest(field));
            this.ReadEstimateObjectFields(iterator, field);
        }

        /// <summary>
        /// Проверяет дополнительных полей проектов внутри комплекса с указанным Guid и Id узла.
        /// </summary>
        [Test(Description = "Тестирование метода Read с тремя параметрами с запросом к полям")]
        public void Test_Read3Fields()
        {
            string field = "CreateDate";
            dynamic iterator = this.Repo.ProjID.Read3(this.HeadComplexGuid, this.HeadNodeID, this.CreateRequest(field));
            this.ReadEstimateObjectFields(iterator, field);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректоного параметра запроса к дополнительным полям в метод Read.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром")]
        public void Test_ReadErrorFields()
        {
            dynamic iterator = this.Repo.ProjID.Read(this.CreateRequest("Error"));
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректоного параметра запроса к дополнительным полям в метод Read2.
        /// </summary>
        [Test(Description = "Тестирование метода Read2 с ошибочным параметром")]
        public void Test_Read2ErrorFields()
        {
            dynamic iterator = this.Repo.ProjID.Read2(this.HeadComplexGuid, this.CreateRequest("Error"));
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректоного параметра запроса к дополнительным полям в метод Read3.
        /// </summary>
        [Test(Description = "Тестирование метода Read3 с ошибочным параметром")]
        public void Test_Read3ErrorFields()
        {
            dynamic iterator = this.Repo.ProjID.Read3(this.HeadComplexGuid, this.HeadNodeID, this.CreateRequest("Error"));
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Создает запрос к дополнительным полям проекта.
        /// </summary>
        /// <param name="field">Наименование поля из БД.</param>
        /// <returns>Запрос с добавленными полями.</returns>
        private dynamic CreateRequest(string field)
        {
            dynamic appFieldsReq = this.Repo.ProjID.GetFieldRequest();
            appFieldsReq.Add(field);
            return appFieldsReq;
        }
    }

    /// <summary>
    ///  Наследует тесты проверки работоспособности запросов к дополнительным полям.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IAppFieldsRequest",
        Author = "agalkin")]
    public class Test_IAppProjFieldsRequest : Test_IAppFieldsRequest
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.AppFieldsRequest = this.Repo.ProjID.GetFieldRequest();
            Assert.NotNull(this.AppFieldsRequest);
        }
    }
}