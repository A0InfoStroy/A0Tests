// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты каталога ИД объектных смет

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности каталога поиска ОС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0OSIDRepo",
        Author = "agalkin")]
    public class Test_IA0OSIDRepo : Test_NewOS
    {
        /// <summary>
        /// Проверяет корректность чтения данных ОС в родительском проекте.
        /// </summary>
        [Test(Description = "Тестирование метода Read")]
        public void Test_Read()
        {
            IA0ObjectIterator iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, null, null, null);
            this.ReadEstimateObject(iterator, EA0ObjectKind.okOS);
        }

        /// <summary>
        /// Проверяет чтение дополнительных аттрибутов ОС в родительском проекте.
        /// </summary>
        [Test(Description = "Тестирование метода Read с запросом к полям")]
        public void Test_ReadAppField()
        {
            IAppLSFieldsRequest appFieldsReq = this.Repo.OSID.GetFiledsRequest();
            appFieldsReq.Add("[OSTitle].[UpdateMoment]");
            IA0ObjectIterator iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, appFieldsReq, null, null);
            this.ReadEstimateObjectFields(iterator, "UpdateMoment");
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректоного параметра запроса к дополнительным полям в метод Read.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром запроса к полям")]
        public void Test_ReadErrorFields()
        {
            IAppLSFieldsRequest appFieldsReq = this.Repo.OSID.GetFiledsRequest();
            appFieldsReq.Add("Error");
            IA0ObjectIterator iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, appFieldsReq, null, null);
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет чтение данных ОС с учетом фильтрующего запроса.
        /// </summary>
        [Test(Description = "Тестирование метода Read с фильтрацией")]
        public void Test_ReadWhere()
        {
            ISQLWhere where = this.Repo.OSID.GetWhereRequest();
            where.Node.And3("[OSTitle].[TotalNodeID]", "=", this.Repo.ProjID.HeadNodeID.ToString());
            IA0ObjectIterator iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, null, where, null);
            Assert.NotNull(iterator);
            while (iterator.Next())
            {
                IA0Object obj = iterator.Item;
                Assert.NotNull(obj);
                Assert.AreEqual(obj.ID.GUID, this.OS.ID.GUID);
            }
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного значения поля в фильтрующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром ID для фильтрации")]
        public void Test_ReadErrorIDWhere()
        {
            ISQLWhere where = this.Repo.OSID.GetWhereRequest();
            where.Node.And3("[OSTitle].[TotalNodeID]", "=", "-1");
            IA0ObjectIterator iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, null, where, null);
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
            ISQLWhere where = this.Repo.OSID.GetWhereRequest();
            where.Node.And3("Error", "=", this.Repo.ProjID.HeadNodeID.ToString());
            IA0ObjectIterator iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, null, where, null);
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет чтение данных ОС с учетом сортирующего запроса.
        /// </summary>
        [Test(Description = "Тестирование метода Read с сортировкой")]
        public void Test_ReadOrder()
        {
            IA0OS secondOS = this.CreateOS("Вторая ОС " + DateTime.Now.ToString());

            // Сортировка по дате создания
            ISQLOrder order = this.Repo.OSID.GetOrderRequest();
            order.Add("[OSTitle].[CreateMoment]");
            IA0ObjectIterator iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, null, null, order);

            // Первым элементом ожидается первая ОС с названием "Интеграционные тесты"
            this.CheckFirstItem(iterator, this.OS.ID.GUID);
            order.Clear();

            // Сортировка по названию (в лексикографическом порядке)
            order.Add("[OSTitle].[OSName]");
            iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, null, null, order);
            Assert.NotNull(iterator);

            // Первым элементом ожидается вторая ОС с названием "Вторая ОС"
            this.CheckFirstItem(iterator, secondOS.ID.GUID);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра в сортирующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром сортировки")]
        public void Test_ReadErrorOrder()
        {
            ISQLOrder order = this.Repo.OSID.GetOrderRequest();
            order.Add("Error");
            IA0ObjectIterator iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, null, null, order);
            this.TestIteratorError(iterator);
        }
    }

    /// <summary>
    ///  Содержит тесты проверки работоспособности запроса к дополнительным полям.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IAppLSFieldsRequest",
        Author = "agalkin")]
    public class Test_IAppOSFieldsRequest : Test_Estimate
    {
        /// <summary>
        /// Получает или устанавливает запрос к дополнительным полям.
        /// </summary>
        protected IAppLSFieldsRequest AppLSFieldsRequest { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.AppLSFieldsRequest = this.Repo.OSID.GetFiledsRequest();
            Assert.NotNull(this.AppLSFieldsRequest);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.AppLSFieldsRequest);
            this.AppLSFieldsRequest = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления полей к запросу.
        /// </summary>
        [Test]
        public void Test_Add()
        {
            string field = "field";
            this.AppLSFieldsRequest.Add(field);
            int count = this.AppLSFieldsRequest.Count;
            Assert.NotZero(count);
            string item = this.AppLSFieldsRequest.Items[count - 1];
            Assert.AreEqual(field, item);
        }

        /// <summary>
        /// Проверяет работоспособность родительского узла запроса.
        /// </summary>
        [Test]
        public void Test_Parent()
        {
            string field = "field";
            this.AppLSFieldsRequest.Add(field);
            IAppFieldsReq parent = this.AppLSFieldsRequest.Parent;
            Assert.NotNull(parent);
            parent.Add(field);
            int count = parent.Count;
            Assert.NotZero(count);
            string item = parent.Items[count - 1];
            Assert.AreEqual(field, item);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к дереву запроса.
        /// </summary>
        [Test]
        public void Test_Tree()
        {
            string field = "field";
            this.AppLSFieldsRequest.Add(field);
            bool tree = this.AppLSFieldsRequest.Tree;
        }

        /// <summary>
        /// Проверяет работоспособность итогов запроса.
        /// </summary>
        [Test]
        public void Test_Totals()
        {
            string field = "field";
            this.AppLSFieldsRequest.Add(field);
            IAppFieldsReq totals = this.AppLSFieldsRequest.Totals;
            Assert.NotNull(totals);
        }
    }

    /// <summary>
    ///  Содержит тесты проверки работоспособности фильтрующего запроса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности ISQLWhere",
        Author = "agalkin")]
    public class Test_ISQLWhere : Test_Estimate
    {
        /// <summary>
        /// Получает или устанавливает фильтрующий запрос.
        /// </summary>
        protected ISQLWhere Where { get; set; }

        /// <summary>
        /// Получает или устанавливает узел фильтрующего запроса.
        /// </summary>
        protected ISQLWhereNode WhereNode { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Where = this.Repo.OSID.GetWhereRequest();
            Assert.NotNull(this.Where);
            this.WhereNode = this.Where.Node;
            Assert.NotNull(this.WhereNode);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.WhereNode);
            Assert.NotNull(this.Where);
            this.WhereNode = null;
            this.Where = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет остутствие ошибок при обращении к строке запроса.
        /// </summary>
        [Test]
        public void Test_Str()
        {
            string query = this.Where.Str;
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления одного аргумента к запросу.
        /// </summary>
        [Test]
        public void Test_Add1()
        {
            string firstArg = "firstArg";
            this.WhereNode = this.WhereNode.Add1(firstArg);
            Assert.AreEqual(firstArg, this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления двух аргументов к запросу.
        /// </summary>
        [Test]
        public void Test_Add2()
        {
            string firstArg = "firstArg";
            string secondArg = "secondArg";
            this.WhereNode = this.WhereNode.Add2(firstArg, secondArg);
            string expected = $"{firstArg} {secondArg}";
            Assert.AreEqual(expected, this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления трех аргументов к запросу.
        /// </summary>
        [Test]
        public void Test_Add3()
        {
            string firstArg = "firstArg";
            string secondArg = "secondArg";
            string thirdArg = "thirdArg";
            this.WhereNode = this.WhereNode.Add3(firstArg, secondArg, thirdArg);
            string expected = $"{firstArg} {secondArg} {thirdArg}";
            Assert.AreEqual(expected, this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления AND к запросу.
        /// </summary>
        [Test]
        public void Test_And()
        {
            this.WhereNode = this.WhereNode.And();
            Assert.AreEqual("AND", this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления AND и одного аргумента к запросу.
        /// </summary>
        [Test]
        public void Test_And1()
        {
            string firstArg = "firstArg";
            this.WhereNode = this.WhereNode.And1(firstArg);
            string expected = $"AND {firstArg}";
            Assert.AreEqual(expected, this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления AND и двух аргументов к запросу.
        /// </summary>
        [Test]
        public void Test_And2()
        {
            string firstArg = "firstArg";
            string secondArg = "secondArg";
            this.WhereNode = this.WhereNode.And2(firstArg, secondArg);
            string expected = $"AND {firstArg} {secondArg}";
            Assert.AreEqual(expected, this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления AND и трех аргументов к запросу.
        /// </summary>
        [Test]
        public void Test_And3()
        {
            string firstArg = "firstArg";
            string secondArg = "secondArg";
            string thirdArg = "thirdArg";
            this.WhereNode = this.WhereNode.And3(firstArg, secondArg, thirdArg);
            string expected = $"AND {firstArg} {secondArg} {thirdArg}";
            Assert.AreEqual(expected, this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления закрывающей скобки.
        /// </summary>
        [Test]
        public void Test_CloseBracket()
        {
            this.WhereNode = this.WhereNode.CloseBracket();
            Assert.AreEqual(")", this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность чтения количества узлов запроса.
        /// </summary>
        [Test]
        public void Test_Count()
        {
            string firstArg = "firstArg";
            int count = this.WhereNode.Count;
            this.WhereNode = this.WhereNode.Add1(firstArg);
            Assert.AreEqual(count + 1, this.WhereNode.Count);
        }

        /// <summary>
        /// Проверяет работоспособность чтения узла запроса.
        /// </summary>
        [Test]
        public void Test_Item()
        {
            string firstArg = "firstArg";
            this.WhereNode = this.WhereNode.Add1(firstArg);
            string item = this.WhereNode.Item[this.WhereNode.Count - 1];
            Assert.AreEqual(firstArg, item.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления открывающей скобки.
        /// </summary>
        [Test]
        public void Test_OpenBracket()
        {
            this.WhereNode = this.WhereNode.OpenBracket();
            Assert.AreEqual("(", this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления OR к запросу.
        /// </summary>
        [Test]
        public void Test_Or()
        {
            this.WhereNode = this.WhereNode.Or();
            Assert.AreEqual("OR", this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления OR и одного аргумента к запросу.
        /// </summary>
        [Test]
        public void Test_Or1()
        {
            string firstArg = "firstArg";
            this.WhereNode = this.WhereNode.Or1(firstArg);
            string expected = $"OR {firstArg}";
            Assert.AreEqual(expected, this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления OR и двух аргументов к запросу.
        /// </summary>
        [Test]
        public void Test_Or2()
        {
            string firstArg = "firstArg";
            string secondArg = "secondArg";
            this.WhereNode = this.WhereNode.Or2(firstArg, secondArg);
            string expected = $"OR {firstArg} {secondArg}";
            Assert.AreEqual(expected, this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность метода добавления OR и трех аргументов к запросу.
        /// </summary>
        [Test]
        public void Test_Or3()
        {
            string firstArg = "firstArg";
            string secondArg = "secondArg";
            string thirdArg = "thirdArg";
            this.WhereNode = this.WhereNode.Or3(firstArg, secondArg, thirdArg);
            string expected = $"OR {firstArg} {secondArg} {thirdArg}";
            Assert.AreEqual(expected, this.Where.Str.Trim());
        }

        /// <summary>
        /// Проверяет работоспособность чтения строки узла запроса.
        /// </summary>
        [Test]
        public void Test_NodeStr()
        {
            string firstArg = "firstArg";
            this.WhereNode = this.WhereNode.Add1(firstArg);
            var nodeStr = this.WhereNode.Str;
            Assert.AreEqual(firstArg, nodeStr.Trim());
        }
    }

    /// <summary>
    /// Содержит тесты проверки работоспособности сортирующего запроса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности ISQLOrder",
        Author = "agalkin")]
    public class Test_ISQLOrder : Test_Estimate
    {
        /// <summary>
        /// Получает или устанавливает сортирующий запрос.
        /// </summary>
        protected ISQLOrder Order { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Order = this.Repo.OSID.GetOrderRequest();
            Assert.NotNull(this.Order);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Order);
            this.Order = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность добавления критериев сортировки и очистки запроса.
        /// </summary>
        [Test]
        public void Test_AddClear()
        {
            string orderArg = "orderArg";
            this.Order.Add(orderArg);
            int count = this.Order.Count;
            Assert.NotZero(count);
            dynamic arg = this.Order.Item[count - 1];
            Assert.AreEqual(orderArg, arg);
            Assert.AreEqual(orderArg, this.Order.Str);
            this.Order.Clear();
            Assert.Zero(this.Order.Count);
        }

        /// <summary>
        /// Проверяет работоспособность удаления критериев сортировки из списка запроса.
        /// </summary>
        [Test]
        public void Test_Delete()
        {
            string orderArg = "orderArg";
            string secondOrderArg = "secondOrderArg";
            this.Order.Add(orderArg);
            this.Order.Add(secondOrderArg);
            string expected = $"{orderArg}, {secondOrderArg}";
            Assert.AreEqual(expected, this.Order.Str.Trim());
            Assert.AreEqual(2, this.Order.Count);
            this.Order.Delete(0);
            Assert.AreEqual(secondOrderArg, this.Order.Item[0]);
        }
    }
}