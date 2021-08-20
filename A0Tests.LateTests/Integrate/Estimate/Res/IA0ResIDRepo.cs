// $Date: 2020-12-28 10:51:06 +0300 (Пн, 28 дек 2020) $
// $Revision: 474 $
// $Author: agalkin $
// Тесты каталога ресурсов

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога поиска ресурсов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ResIDRepo",
        Author = "agalkin")]
    public class Test_IA0ResIDRepo : Test_Estimate
    {
        /// <summary>
        /// Проверяет работоспособность чтения ресурсов.
        /// </summary>
        [Test(Description = "Тестирование метода Read")]
        public void Test_Read()
        {
            dynamic iterator = this.Repo.ResID.Read(this.GetProjGUID(), null, null, null);
            Assert.IsNotNull(iterator);
            int count = iterator.Count;
            Assert.NotZero(count, "Ожидается наличие ресурсов в проекте {0}", this.GetProjGUID());
            while (iterator.Next())
            {
                dynamic item = iterator.Current;
                Assert.NotNull(item);
            }
        }

        /// <summary>
        /// Проверяет работоспособность чтения дополнительных полей ресурсов.
        /// </summary>
        [Test(Description = "Чтение ресурсов с полями")]
        public void Test_ReadFields()
        {
            dynamic appFieldsReq = this.Repo.ResID.GetFieldRequest();
            appFieldsReq.Add("[LSStrRes].[Volume]");
            dynamic iter = this.Repo.ResID.Read(this.GetProjGUID(), appFieldsReq, null, null);
            Assert.NotNull(iter);
            int count = iter.Count;
            Assert.NotZero(count, "Ожидается наличие ресурсов в проекте {0}", this.GetProjGUID());
            while (iter.Next())
            {
                dynamic obj = iter.Current;
                Assert.NotNull(obj);
                dynamic fieldValue = obj.Value["Volume"];
            }
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного параметра запроса к дополнительным полям в метод Read.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром запроса к полям")]
        public void Test_ReadErrorFields()
        {
            dynamic appFieldsReq = this.Repo.ResID.GetFieldRequest();
            appFieldsReq.Add("Error");
            bool result = false;
            try
            {
                dynamic iterator = this.Repo.ResID.Read(this.GetProjGUID(), appFieldsReq, null, null);
                result = iterator.Next();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80040E14);
            }

            Assert.IsFalse(result);
        }

        /// <summary>
        ///  Проверяет чтение ресурсов с учетом фильтрующего запроса.
        /// </summary>
        [Test(Description = "Чтение договоров с фильтрацией")]
        public void Test_ReadWhere()
        {
            int projectID = 181;
            dynamic whereReq = this.Repo.ResID.GetWhereRequest();
            whereReq.Node.And3("[LSStrRes].[ProjID]", "=", projectID.ToString());
            dynamic iter = this.Repo.ResID.Read(this.GetProjGUID(), null, whereReq, null);
            Assert.NotNull(iter);
            int count = iter.Count;
            Assert.NotZero(count, "Ожидается наличие договоров в проекте {0}", this.GetProjGUID());
            bool exist = false;
            while (iter.Next())
            {
                dynamic fields = iter.Current;
                dynamic projID = fields.Value["ProjID"];
                Assert.AreEqual(projectID, projID);
                exist = true;
            }

            Assert.True(exist);
        }

        /// <summary>
        /// Проверяет обработку исключения при передаче некорректного значения поля в фильтрующий запрос.
        /// </summary>
        [Test(Description = "Тестирование метода Read с ошибочным параметром ID для фильтрации")]
        public void Test_ReadErrorIDWhere()
        {
            dynamic whereReq = this.Repo.ResID.GetWhereRequest();
            whereReq.Node.And3("[LSStrRes].[ProjID]", "=", "-1");
            bool result = false;
            try
            {
                dynamic iterator = this.Repo.ResID.Read(this.GetProjGUID(), null, whereReq, null);
                result = iterator.Next();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80040E14);
            }

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Проверяет чтение ресурсов с учетом сортирующего запроса.
        /// </summary>
        [Test(Description = "Чтение договоров с сортировкой")]
        public void Test_ReadOrder()
        {
            dynamic orderReq = this.Repo.ResID.GetOrderRequest();
            orderReq.Add("[LSStrRes].[ResName]");
            dynamic iter = this.Repo.ResID.Read(this.GetProjGUID(), null, null, orderReq);
            Assert.NotNull(iter);
            int count = iter.Count;
            Assert.NotZero(count, "Ожидается наличие договоров в проекте {0}", this.GetProjGUID());
            bool exist = false;
            while (iter.Next())
            {
                dynamic obj = iter.Current;
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
            dynamic orderReq = this.Repo.ResID.GetOrderRequest();
            orderReq.Add("Error");
            bool result = false;
            try
            {
                dynamic iterator = this.Repo.ResID.Read(this.GetProjGUID(), null, null, orderReq);
                result = iterator.Next();
            }
            catch (System.Runtime.InteropServices.COMException E)
            {
                Assert.AreEqual((uint)E.HResult, 0x80040E14);
            }

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Получает Guid проекта из БД, содержащий ресурсы.
        /// </summary>
        /// <returns>Guid проекта.</returns>
        protected virtual Guid GetProjGUID() => Guid.Parse("{03B5A6CF-FCB7-46A9-9A03-874517D9EE72}");
    }

    /// <summary>
    /// Наследует тесты проверки работоспособности запроса к дополнительным полям.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IAppFieldsRequest",
        Author = "agalkin")]
    public class Test_IAppResFieldsRequest : Test_IAppFieldsRequest
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.AppFieldsRequest = this.Repo.ResID.GetFieldRequest();
            Assert.NotNull(this.AppFieldsRequest);
        }
    }

    /// <summary>
    ///  Наследует тесты проверки работоспособности фильтрующего запроса.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности ISQLWhere",
        Author = "agalkin")]
    public class Test_ISQLWhereRes : Test_ISQLWhere
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Where = this.Repo.ResID.GetWhereRequest();
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
    public class Test_ISQLOrderRes : Test_ISQLOrder
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Order = this.Repo.ResID.GetOrderRequest();
            Assert.NotNull(this.Order);
        }
    }
}