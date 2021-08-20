// $Date: 2020-12-22 15:00:12 +0300 (Вт, 22 дек 2020) $
// $Revision: 463 $
// $Author: agalkin $
// Тесты каталога ИД локальных смет

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности каталога поиска ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSIDRepo",
        Author = "agalkin")]
    public class Test_IA0LSIDRepo : Test_NewLS
    {
        /// <summary>
        /// Проверяет корректность чтения данных ЛС в родительском проекте.
        /// </summary>
        [Test(Description = "Тестирование метода Read")]
        public void Test_Read()
        {
            dynamic iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, null);

            // Значение 4 соответствует EA0ObjectKind.okLS.
            this.ReadEstimateObject(iterator, 4);
        }

        /// <summary>
        /// Проверяет чтение данных и дополнительных аттрибутов ЛС в родительском проекте.
        /// </summary>
        [Test(Description = "Тестирование метода Read с запросом к полям")]
        public void Test_ReadAppField()
        {
            dynamic appFieldsReq = this.Repo.LSID.GetFiledsRequest();
            appFieldsReq.Add("[LSTitle].[UpdateMoment]");
            dynamic iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, appFieldsReq, null, null);
            this.ReadEstimateObjectFields(iterator, "UpdateMoment");
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректоного параметра запроса к дополнительным полям в метод Read.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром запроса к полям")]
        public void Test_ReadErrorFields()
        {
            dynamic appFieldsReq = this.Repo.LSID.GetFiledsRequest();
            appFieldsReq.Add("Error");
            dynamic iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, appFieldsReq, null, null);
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет чтение данных ЛС с учетом фильтрующего запроса.
        /// </summary>
        [Test(Description = "Тестирование метода Read с фильтрацией")]
        public void Test_ReadWhere()
        {
            dynamic where = this.Repo.LSID.GetWhereRequest();
            where.Node.And3("[LSTitle].[TotalNodeID]", "=", this.Repo.OSID.HeadNodeID.ToString());
            dynamic iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, null, where, null);
            Assert.NotNull(iterator);
            while (iterator.Next())
            {
                dynamic obj = iterator.Item;
                Assert.NotNull(obj);
                Assert.AreEqual(obj.ID.GUID, this.LS.ID.GUID);
            }
        }

        /// <summary>
        ///  Проверяет обработку исключения при передаче некорректного значения поля в фильтрующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром ID для фильтрации")]
        public void Test_ReadErrorIDWhere()
        {
            dynamic where = this.Repo.LSID.GetWhereRequest();
            where.Node.And3("[LSTitle].[TotalNodeID]", "=", "-1");
            dynamic iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, null, where, null);
            Assert.NotNull(iterator);
            bool exist = false;
            while (iterator.Next())
            {
                exist = true;
            }

            Assert.IsFalse(exist);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного наименования поля в фильтрующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным полем для фильтрации")]
        public void Test_ReadErrorFieldWhere()
        {
            dynamic where = this.Repo.LSID.GetWhereRequest();
            where.Node.And3("Error", "=", this.Repo.OSID.HeadNodeID.ToString());
            dynamic iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, null, where, null);
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет чтение данных ЛС с учетом сортирующего запроса.
        /// </summary>
        [Test(Description = "Тестирование метода Read с сортировкой")]
        public void Test_ReadOrder()
        {
            dynamic secondLS = this.CreateLS("Вторая ЛС " + DateTime.Now.ToString());

            // Сортировка по дате создания
            dynamic order = this.Repo.LSID.GetOrderRequest();
            order.Add("[LSTitle].[CreateMoment]");
            dynamic iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, order);

            // Первым элементом ожидается первая ЛС с названием "Интеграционные тесты"
            this.CheckFirstItem(iterator, this.LS.ID.GUID);
            order.Clear();

            // Сортировка по названию (в лексикографическом порядке)
            order.Add("[LSTitle].[LSName]");
            iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, order);
            Assert.NotNull(iterator);

            // Первым элементом ожидается вторая ЛС с названием "Вторая ЛС"
            this.CheckFirstItem(iterator, secondLS.ID.GUID);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра в сортирующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром сортировки")]
        public void Test_ReadErrorOrder()
        {
            dynamic order = this.Repo.LSID.GetOrderRequest();
            order.Add("Error");
            dynamic iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, order);
            this.TestIteratorError(iterator);
        }
    }

    /// <summary>
    ///  Наследует тесты проверки работоспособности запроса к дополнительным полям.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IAppLSFieldsRequest",
        Author = "agalkin")]
    public class Test_IAppLSFieldsRequest : Test_IAppOSFieldsRequest
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.AppLSFieldsRequest = this.Repo.LSID.GetFiledsRequest();
            Assert.NotNull(this.AppLSFieldsRequest);
        }
    }

    /// <summary>
    ///  Наследует тесты проверки работоспособности фильтрующего запроса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности ISQLWhere",
        Author = "agalkin")]
    public class Test_ISQLWhereLS : Test_ISQLWhere
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Where = this.Repo.LSID.GetWhereRequest();
            Assert.NotNull(this.Where);
            this.WhereNode = this.Where.Node;
            Assert.NotNull(this.WhereNode);
        }
    }

    /// <summary>
    ///  Наследует тесты проверки работоспособности сортирующего запроса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности ISQLOrder",
        Author = "agalkin")]
    public class Test_ISQLOrderLS : Test_ISQLOrder
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Order = this.Repo.LSID.GetOrderRequest();
            Assert.NotNull(this.Order);
        }
    }
}