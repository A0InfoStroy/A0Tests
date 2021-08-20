// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты каталога Договоров

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога поиска договоров.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ContractsIDRepo",
        Author = "agalkin")]
    public class Test_IA0ContractsIDRepo : Test_Estimate
    {
        /// <summary>
        /// Получает или устанавливает каталог поиска договоров.
        /// </summary>
        protected IA0ContractsIDRepo ContractRepo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ContractRepo = this.Repo.ContractsID;
            Assert.NotNull(this.ContractRepo);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ContractRepo);
            this.ContractRepo = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет остутствие ошибок при обращении к полям договоров.
        /// </summary>
        [Test(Description = "Чтение договоров")]
        public void Test_Read()
        {
            IA0FieldsIterator iter = this.ContractRepo.Read(this.GetProjGUID(), null, null, null);
            this.CheckNonEmptyIterator(iter);
            while (iter.Next())
            {
                IFields fields = iter.Current;
                dynamic projID = fields.Value["ProjID"];
                dynamic contractID = fields.Value["ContractID"];
                dynamic projGuid = fields.Value["ProjGuid"];
                dynamic contrGuid = fields.Value["ContrGuid"];

                iter.Next();
            }
        }

        /// <summary>
        /// Проверяет корректность чтения полей договоров, полученных через запрос к дополнительным полям.
        /// </summary>
        [Test(Description = "Чтение договоров с полями")]
        public void Test_ReadFields()
        {
            IAppFieldsRequest appFieldsReq = this.ContractRepo.GetFieldRequest();
            appFieldsReq.Add("[Contracts].[Number]");
            IA0FieldsIterator iter = this.ContractRepo.Read(this.GetProjGUID(), appFieldsReq, null, null);
            this.CheckNonEmptyIterator(iter);
            while (iter.Next())
            {
                IFields obj = iter.Current;
                Assert.NotNull(obj);
                var fieldValue = obj.Value["Number"];
                Assert.NotNull(fieldValue);
            }
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра в запрос к дополнительным полям.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром запроса к полям")]
        public void Test_ReadErrorFields()
        {
            IAppFieldsRequest appFieldsReq = this.ContractRepo.GetFieldRequest();
            appFieldsReq.Add("Error");
            bool result = false;
            try
            {
                IA0FieldsIterator iterator = this.ContractRepo.Read(this.GetProjGUID(), appFieldsReq, null, null);
                result = iterator.Next();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80040E14);
            }

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Проверяет корректность чтения полей договоров, полученных через фильтрующий запрос.
        /// </summary>
        [Test(Description = "Чтение договоров с фильтрацией")]
        public void Test_ReadWhere()
        {
            int projectID = 133;
            ISQLWhere whereReq = this.ContractRepo.GetWhereRequest();
            whereReq.Node.And3("[Contracts].[ProjID]", "=", projectID.ToString());
            IA0FieldsIterator iter = this.ContractRepo.Read(this.GetProjGUID(), null, whereReq, null);
            this.CheckNonEmptyIterator(iter);
            bool exist = false;
            while (iter.Next())
            {
                IFields fields = iter.Current;
                dynamic projID = fields.Value["ProjID"];
                Assert.AreEqual(projectID, projID);
                exist = true;
            }

            Assert.True(exist);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра в фильтрующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром ID для фильтрации")]
        public void Test_ReadErrorIDWhere()
        {
            ISQLWhere whereReq = this.ContractRepo.GetWhereRequest();
            whereReq.Node.And3("[Contracts].[ProjID]", "=", "-1");
            bool result = false;
            try
            {
                IA0FieldsIterator iterator = this.ContractRepo.Read(this.GetProjGUID(), null, whereReq, null);
                result = iterator.Next();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80040E14);
            }

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Проверяет корректность чтения полей договоров, полученных через сортирующий запрос.
        /// </summary>
        [Test(Description = "Чтение договоров с сортировкой")]
        public void Test_ReadOrder()
        {
            ISQLOrder orderReq = this.ContractRepo.GetOrderRequest();
            orderReq.Add("[Contracts].[ContractID]");
            IA0FieldsIterator iter = this.ContractRepo.Read(this.GetProjGUID(), null, null, orderReq);
            this.CheckNonEmptyIterator(iter);
            bool exist = false;
            while (iter.Next())
            {
                IFields obj = iter.Current;
                Assert.NotNull(obj);
                exist = true;
            }

            Assert.True(exist);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра в сортирующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром сортировки")]
        public void Test_ReadErrorOrder()
        {
            ISQLOrder orderReq = this.ContractRepo.GetOrderRequest();
            orderReq.Add("Error");
            bool result = false;
            try
            {
                IA0FieldsIterator iterator = this.ContractRepo.Read(this.GetProjGUID(), null, null, orderReq);
                result = iterator.Next();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80040E14);
            }

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Проверка работоспособности свойства EOF.
        /// </summary>
        [Test(Description = "Чтение договоров")]
        public void Test_EOF()
        {
            IA0FieldsIterator iter = this.ContractRepo.Read(this.GetProjGUID(), null, null, null);
            this.CheckNonEmptyIterator(iter);
            for (int i = 0; i < iter.Count; i++)
            {
                Assert.AreEqual(!iter.Next(), iter.EOF);
            }
        }

        /// <summary>
        /// Проверка работоспособности свойств BOF и EOF для пустого итератора.
        /// </summary>
        [Test(Description = "Чтение договоров")]
        public void Test_EmptyIterator()
        {
            IA0FieldsIterator iter = this.ContractRepo.Read(Guid.Empty, null, null, null);
            Assert.True(iter.BOF);
            Assert.True(iter.EOF);
        }

        /// <summary>
        /// Проверка работоспособности метода получения строчек.
        /// </summary>
        [Test(Description = "Чтение договоров")]
        public void Test_GetRows()
        {
            IA0FieldsIterator iter = this.ContractRepo.Read(this.GetProjGUID(), null, null, null);
            this.CheckNonEmptyIterator(iter);
            int rowsCount = 1;
            int startRow = 0;
            dynamic rows = iter.GetRows(rowsCount, startRow, "ProjID");
        }

        /// <summary>
        /// Получает Guid проекта из БД, содержащий договор.
        /// </summary>
        /// <returns>Guid проекта.</returns>
        protected virtual Guid GetProjGUID() => Guid.Parse("{4F137BDF-2C26-480D-8114-F6742D9BC33F}");

        /// <summary>
        /// Проверяет наличие элементов у итератора.
        /// </summary>
        /// <param name="iter">Итератор по каталогу поиска договоров.</param>
        private void CheckNonEmptyIterator(IA0FieldsIterator iter)
        {
            Assert.NotNull(iter);
            var count = iter.Count;
            Assert.NotZero(count, "Ожидается наличие договоров в проекте {0}", this.GetProjGUID());
        }
    }

    /// <summary>
    /// Наследует тесты проверки работоспособности запроса к дополнительным полям.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IAppFieldsRequest",
        Author = "agalkin")]
    public class Test_IAppContractsFieldsRequest : Test_IAppFieldsRequest
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.AppFieldsRequest = this.Repo.ContractsID.GetFieldRequest();
            Assert.NotNull(this.AppFieldsRequest);
        }
    }

    /// <summary>
    /// Наследует тесты проверки работоспособности фильтрующего запроса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности ISQLWhere",
        Author = "agalkin")]
    public class Test_ISQLWhereContracts : Test_ISQLWhere
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Where = this.Repo.ContractsID.GetWhereRequest();
            Assert.NotNull(this.Where);
            this.WhereNode = this.Where.Node;
            Assert.NotNull(this.WhereNode);
        }
    }

    /// <summary>
    /// Наследует тесты проверки работоспособности сортирующего запроса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности ISQLOrder",
        Author = "agalkin")]
    public class Test_ISQLOrderContracts : Test_ISQLOrder
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Order = this.Repo.ContractsID.GetOrderRequest();
            Assert.NotNull(this.Order);
        }
    }
}