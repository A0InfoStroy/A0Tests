// $Date: 2020-07-22 13:16:46 +0300 (Ср, 22 июл 2020) $
// $Revision: 318 $
// $Author: agalkin $
// Тесты каталога Договоров

namespace A0Tests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога договоров.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ContractsRepo",
        Author = "agalkin")]
    public class Test_IA0ContractsRepo : Test_EstimateCustom
    {
        /// <summary>
        /// Получает или устанавливает каталог договоров.
        /// </summary>
        protected IA0ContractsRepo ContractRepo { get; private set; }

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
            IA0ContractsIDRepo repoID = this.A0.Estimate.Repo.ContractsID;

            // Поля для договора, которые надо получить из БД
            IAppFieldsRequest fields = repoID.GetFieldRequest();
            fields.Add("Number");
            fields.Add("Status");

            // Итератор по данным Каталога поиска
            IA0FieldsIterator iter = repoID.Read(this.GetProjGUID(), fields, null, null);
            Assert.NotNull(iter);

            Assert.NotZero(iter.Count, "Ожидается наличие договоров в проекте {0}", this.GetProjGUID());

            while (iter.Next())
            {
                // Поля договора только для чтения
                IFields field = iter.Current;
                dynamic projID = field.Value["ProjID"];
                dynamic contractID = field.Value["ContractID"];
                Guid projGuid = Guid.Parse(field.Value["ProjGuid"]);
                Guid contrGuid = Guid.Parse(field.Value["ContrGuid"]);
                dynamic number = field.Value["Number"];
                EContractStatus status = (EContractStatus)field.Value["Status"];

                // Каталог договоров.
                // При чтении договора он будет заблокирован.
                IA0Contract contract = this.ContractRepo.Read(contrGuid, EAccessKind.akRead);
                Assert.NotNull(contract);
                try
                {
                    // Проверяем соответствие полей в каталогах
                    Assert.AreEqual(contract.ProjID, field.Value["ProjID"]);
                    Assert.AreEqual(contract.ContractID, field.Value["ContractID"]);
                    Assert.AreEqual(contract.ProjGUID, projGuid);
                    Assert.AreEqual(contract.ContrGUID, contrGuid);
                    Assert.AreEqual(contract.Number, number);
                    Assert.AreEqual(contract.ContractStatus, status);
                }
                finally
                {
                    // Надо разблокировать договор.
                    this.ContractRepo.UnLock(contract.ContrGUID);
                }
            }
        }

        /// <summary>
        /// Получает Guid проекта из БД, содержащий контракт.
        /// </summary>
        /// <returns>Guid проекта.</returns>
        protected virtual Guid GetProjGUID() => Guid.Parse("{4F137BDF-2C26-480D-8114-F6742D9BC33F}");
    }
}