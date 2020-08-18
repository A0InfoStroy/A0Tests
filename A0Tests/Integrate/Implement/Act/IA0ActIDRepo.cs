// $Date: 2020-08-05 10:48:22 +0300 (Ср, 05 авг 2020) $
// $Revision: 342 $
// $Author: agalkin $
// Тесты каталога ИД акта смет

namespace A0Tests.Integrate.Implement
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога поиска актов.
    /// </summary>
    [TestFixture(
        Category = "Implement",
        Description = "Тесты проверки работоспособности IA0ActIDRepo",
        Author = "agalkin")]
    public class Test_IA0ActIDRepo : NewAct
    {
        /// <summary>
        /// Проверяет корректность чтения данных акта в родительском проекте.
        /// </summary>
        [Test(Description = "Тестирование метода Read")]
        public void Test_Read()
        {
            IA0ObjectIterator iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, null, null);
            this.ReadEstimateObject(iterator, EA0ObjectKind.okAct);
        }

        /// <summary>
        /// Проверяет чтение дополнительных аттрибутов акта в родительском проекте.
        /// </summary>
        [Test(Description = "Тестирование метода Read с запросом к полям")]
        public void Test_ReadAppField()
        {
            IAppLSFieldsRequest appFieldsReq = this.ImplRepo.ActID.GetFiledsRequest();
            appFieldsReq.Add("[LSTitle].[CreateMoment]");
            IA0ObjectIterator iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, appFieldsReq, null, null);
            this.ReadEstimateObjectFields(iterator, "CreateMoment");
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректоного параметра запроса к дополнительным полям в метод Read.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром запроса к полям")]
        public void Test_ReadErrorFields()
        {
            IAppLSFieldsRequest appFieldsReq = this.ImplRepo.ActID.GetFiledsRequest();
            appFieldsReq.Add("Error");
            IA0ObjectIterator iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, appFieldsReq, null, null);
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет чтение данных акта с учетом фильтрующего запроса.
        /// </summary>
        [Test(Description = "Тестирование метода Read с фильтрацией")]
        public void Test_ReadWhere()
        {
            ISQLWhere where = this.ImplRepo.ActID.GetWhereRequest();
            where.Node.And3("[LSTitle].[TotalNodeID]", "=", this.Repo.OSID.HeadNodeID.ToString());
            IA0ObjectIterator iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, where, null);
            Assert.NotNull(iterator);
            while (iterator.Next())
            {
                IA0Object obj = iterator.Item;
                Assert.NotNull(obj);
                Assert.AreEqual(obj.ID.GUID, this.Act.ID.GUID);
            }
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного значения поля в фильтрующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром ID для фильтрации")]
        public void Test_ReadErrorIDWhere()
        {
            ISQLWhere where = this.ImplRepo.ActID.GetWhereRequest();
            where.Node.And3("[LSTitle].[TotalNodeID]", "=", "-1");
            IA0ObjectIterator iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, where, null);
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
            ISQLWhere where = this.ImplRepo.ActID.GetWhereRequest();
            where.Node.And3("Error", "=", this.Repo.OSID.HeadNodeID.ToString());
            IA0ObjectIterator iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, where, null);
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет чтение данных акта с учетом сортирующего запроса.
        /// </summary>
        [Test(Description = "Тестирование метода Read с сортировкой")]
        public void Test_ReadOrder()
        {
            IA0Act secondAct = this.CreateAct("Второй акт " + DateTime.Now.ToString());

            // Сортировка по дате создания
            ISQLOrder order = this.ImplRepo.ActID.GetOrderRequest();
            order.Add("CreateMoment");
            IA0ObjectIterator iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, null, order);

            // Первым элементом ожидается первый акт с названием "Интеграционные тесты"
            this.CheckFirstItem(iterator, this.Act.ID.GUID);
            order.Clear();
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра в сортирующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром сортировки")]
        public void Test_ReadErrorOrder()
        {
            ISQLOrder order = this.ImplRepo.ActID.GetOrderRequest();
            order.Add("Error");
            IA0ObjectIterator iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, null, order);
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет корректность чтения данных акта и его дополнительных полей при наличии всех запросов.
        /// </summary>
        [Test(Description = "Акты в проекте")]
        public void Test_ReadWithAllArgs()
        {
            // TODO: GUID Проекта
            Guid projGUID = Guid.Parse("{BDDE8E43-F089-4761-B9A2-A95A4639465E}");

            // Дополнительные поля для запроса из БД.
            // Можно запрашивать все поля из таблицы LSTitle.
            // Поддерживаемые системой поля для Акта можно узнать из документации
            // или получив описатель из каталога A0.App.Attributes.Repo для IA0ActTitle.
            IAppLSFieldsRequest fields = this.ImplRepo.ActID.GetFiledsRequest();
            fields.Add("[LSTitle].[ContractID]");

            // Итоги акта.
            // Содержат поля интерфейса IA0ActTotal
            fields.Totals.Add("PZ_S"); // ПЗ строит
            fields.Totals.Add("PZ_M"); // ПЗ монтаж
            fields.Totals.Add("PZ_E"); // ПЗ оборуд
            fields.Totals.Add("PZ_O"); // ПЗ прочие

            // Дополнительные уловия фильтрации
            ISQLWhere where = this.ImplRepo.ActID.GetWhereRequest();
            where.Node.And3("[LSTitle].[ContractID]", ">", 10.ToString()).
                And3("[LSTotal].PZ_S", ">=", 0.ToString()).
                And3("[LSTotal].PZ_M", ">=", 0.ToString());

            // Условия сортировки
            ISQLOrder order = this.ImplRepo.ActID.GetOrderRequest();
            order.Add("[LSTitle].[ContractID]");

            IA0ObjectIterator p = this.ImplRepo.ActID.Read(projGUID, fields, where, order);
            Assert.NotNull(p);

            // Если необходимо контролировать сформированный запрос
            ISQLAsString sql = p as ISQLAsString;
            Assert.NotNull(sql);
            Assert.IsNotEmpty(sql.Str);

            while (p.Next())
            {
                IA0Object obj = p.Item;
                Assert.NotNull(obj);
                Assert.NotNull(obj.ID);
                EA0ObjectKind kind = obj.Kind;
                Assert.IsTrue(kind == EA0ObjectKind.okAct, "Тип должен быть okAct, найдено {0}", kind);

                string mark = obj.Mark;
                string name = obj.Name;
                string businessStage = obj.BusinessStage();
                string comment = obj.Comment;

                var contractID = p.Item.Fields.Value["ContractID"];

                var pz_S = p.Item.Fields.Value["Totals_PZ_S"];
                var pz_M = p.Item.Fields.Value["Totals_PZ_M"];
                var pz_E = p.Item.Fields.Value["Totals_PZ_E"];
                var pz_O = p.Item.Fields.Value["Totals_PZ_O"];

                DateTime createMoment = obj.CreateMoment();
            }
        }
    }
}