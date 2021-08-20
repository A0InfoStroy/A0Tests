// $Date: 2020-12-29 14:21:33 +0300 (Вт, 29 дек 2020) $
// $Revision: 480 $
// $Author: agalkin $
// Тесты каталога ИД акта смет

namespace A0Tests.LateTests.Integrate.Implement
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога поиска актов.
    /// </summary>
    [TestFixture(
        Category = "Implement",
        Description = "Тесты проверки работоспособности IA0ActIDRepo",
        Author = "agalkin")]
    public class Test_IA0ActIDRepo : Test_NewAct
    {
        /// <summary>
        /// Проверяет корректность чтения данных акта в родительском проекте.
        /// </summary>
        [Test(Description = "Тестирование метода Read")]
        public void Test_Read()
        {
            dynamic iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, null, null);
            this.ReadEstimateObject(iterator, 5);
        }

        /// <summary>
        /// Проверяет чтение дополнительных аттрибутов акта в родительском проекте.
        /// </summary>
        [Test(Description = "Тестирование метода Read с запросом к полям")]
        public void Test_ReadAppField()
        {
            dynamic appFieldsReq = this.ImplRepo.ActID.GetFiledsRequest();
            appFieldsReq.Add("[LSTitle].[CreateMoment]");
            dynamic iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, appFieldsReq, null, null);
            this.ReadEstimateObjectFields(iterator, "CreateMoment");
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректоного параметра запроса к дополнительным полям в метод Read.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром запроса к полям")]
        public void Test_ReadErrorFields()
        {
            dynamic appFieldsReq = this.ImplRepo.ActID.GetFiledsRequest();
            appFieldsReq.Add("Error");
            dynamic iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, appFieldsReq, null, null);
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет чтение данных акта с учетом фильтрующего запроса.
        /// </summary>
        [Test(Description = "Тестирование метода Read с фильтрацией")]
        public void Test_ReadWhere()
        {
            dynamic where = this.ImplRepo.ActID.GetWhereRequest();
            where.Node.And3("[LSTitle].[TotalNodeID]", "=", this.Repo.OSID.HeadNodeID.ToString());
            dynamic iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, where, null);
            Assert.NotNull(iterator);
            while (iterator.Next())
            {
                dynamic obj = iterator.Item;
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
            dynamic where = this.ImplRepo.ActID.GetWhereRequest();
            where.Node.And3("[LSTitle].[TotalNodeID]", "=", "-1");
            dynamic iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, where, null);
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
            dynamic where = this.ImplRepo.ActID.GetWhereRequest();
            where.Node.And3("Error", "=", this.Repo.OSID.HeadNodeID.ToString());
            dynamic iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, where, null);
            this.TestIteratorError(iterator);
        }

        /// <summary>
        /// Проверяет чтение данных акта с учетом сортирующего запроса.
        /// </summary>
        [Test(Description = "Тестирование метода Read с сортировкой")]
        public void Test_ReadOrder()
        {
            dynamic secondAct = this.CreateAct("Второй акт " + DateTime.Now.ToString());

            // Сортировка по дате создания
            dynamic order = this.ImplRepo.ActID.GetOrderRequest();
            order.Add("CreateMoment");
            dynamic iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, null, order);

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
            dynamic order = this.ImplRepo.ActID.GetOrderRequest();
            order.Add("Error");
            dynamic iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, null, order);
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
            dynamic fields = this.ImplRepo.ActID.GetFiledsRequest();
            fields.Add("[LSTitle].[ContractID]");

            // Итоги акта.
            // Содержат поля интерфейса IA0ActTotal
            fields.Totals.Add("PZ_S"); // ПЗ строит
            fields.Totals.Add("PZ_M"); // ПЗ монтаж
            fields.Totals.Add("PZ_E"); // ПЗ оборуд
            fields.Totals.Add("PZ_O"); // ПЗ прочие

            // Дополнительные уловия фильтрации
            dynamic where = this.ImplRepo.ActID.GetWhereRequest();
            where.Node.And3("[LSTitle].[ContractID]", ">", 10.ToString()).
                And3("[LSTotal].PZ_S", ">=", 0.ToString()).
                And3("[LSTotal].PZ_M", ">=", 0.ToString());

            // Условия сортировки
            dynamic order = this.ImplRepo.ActID.GetOrderRequest();
            order.Add("[LSTitle].[ContractID]");

            dynamic p = this.ImplRepo.ActID.Read(projGUID, fields, where, order);
            Assert.NotNull(p);

            while (p.Next())
            {
                dynamic obj = p.Item;
                Assert.NotNull(obj);
                Assert.NotNull(obj.ID);
                dynamic kind = obj.Kind;
                Assert.IsTrue(kind == 5, "Тип должен быть okAct, найдено {0}", kind);

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

        [Test(Description = "Выборка актов по бизнес этапу")]
        public void Test_SelectActsByBussinesStage()
        {
            List<dynamic> stages = new List<dynamic>();
            dynamic bs = this.A0.Sys.Repo.BussinnessStages;
            for (int i = 0; i < bs.Count; i++)
            {
                // Значение 5 соответствует EA0ObjectKind.okAct.
                if (bs.Item[i].Kind == 5)
                {
                    stages.Add(bs.Item[i]);
                }
            }

            Assert.True(stages.Count > 0);
            bool exist = false;

            foreach (dynamic stage in stages)
            {
                int busOpId = stage.ID;
                string bsName = stage.Name;
                dynamic where = this.ImplRepo.ActID.GetWhereRequest();
                where.Node.And3("[LSTitle].[BusOpID]", "=", busOpId.ToString());
                dynamic iterator = this.ImplRepo.ActID.Read(this.Proj.ID.GUID, null, where, null);
                Assert.NotNull(iterator);
                while (iterator.Next())
                {
                    exist = true;
                    dynamic obj = iterator.Item;
                    Assert.NotNull(obj);
                    Assert.AreEqual(bsName, obj.BusinessStage());
                }
            }

            Assert.True(exist, "Акты по бизнес этапу не найдены");
        }
    }
}