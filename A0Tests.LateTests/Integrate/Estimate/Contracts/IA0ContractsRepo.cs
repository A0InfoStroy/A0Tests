// $Date: 2020-12-25 16:37:46 +0300 (Пт, 25 дек 2020) $
// $Revision: 473 $
// $Author: agalkin $
// Тесты каталога Договоров

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога договоров.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ContractsRepo",
        Author = "agalkin")]
    public class Test_IA0ContractsRepo : Test_NewContract
    {
        /// <summary>
        /// Получает или устанавливает каталог договоров.
        /// </summary>
        protected dynamic ContractRepo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ContractRepo = this.A0.Estimate.Repo.Contracts;
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
        /// Проверяет корректность чтения данных договоров.
        /// </summary>
        [Test(Description = "Чтение договоров")]
        public void Test_Read()
        {
            // Каталог поиска для получения всех договоров
            dynamic repoID = this.A0.Estimate.Repo.ContractsID;

            // Поля для договора, которые надо получить из БД
            dynamic fields = repoID.GetFieldRequest();
            fields.Add("Number");
            fields.Add("Status");

            // Итератор по данным Каталога поиска
            dynamic iter = repoID.Read(this.Proj.ID.GUID, fields, null, null);
            Assert.NotNull(iter);
            Assert.NotZero(iter.Count, "Ожидается наличие договоров в проекте {0}", this.Proj.ID.GUID);

            while (iter.Next())
            {
                // Поля договора только для чтения
                dynamic field = iter.Current;
                dynamic projID = field.Value["ProjID"];
                dynamic contractID = field.Value["ContractID"];
                Guid projGuid = Guid.Parse(field.Value["ProjGuid"]);
                Guid contrGuid = Guid.Parse(field.Value["ContrGuid"]);
                dynamic number = field.Value["Number"];
                dynamic status = field.Value["Status"];

                // Значение 1 соответствует EAccessKind.akEdit.
                dynamic contract = this.ContractRepo.Read(contrGuid, 1);

                // Проверяем соответствие полей в каталогах
                Assert.AreEqual(contract.ProjID, field.Value["ProjID"]);
                Assert.AreEqual(contract.ContractID, field.Value["ContractID"]);
                Assert.AreEqual(contract.ProjGUID, projGuid);
                Assert.AreEqual(contract.ContrGUID, contrGuid);
                Assert.AreEqual(contract.Number, number);
                Assert.AreEqual(contract.ContractStatus, status);
            }
        }

        /// <summary>
        /// Проверяет корректность удаления договоров.
        /// </summary>
        [Test(Description = "Удаление договоров")]
        public void Test_Delete()
        {
            this.ContractRepo.UnLock(this.Contract.ContrGUID);
            this.ContractRepo.Delete(this.Contract.ContrGUID);

            // Попытка прочитать контракт после удаления.
            try
            {
                // Значение 0 соответствует EAccessKind.akRead.
                dynamic contract = this.ContractRepo.Read(this.Contract.ContrGUID, 0);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual(ex.HResult, -2147418113);
            }

            this.Contract = null;
        }
    }
}