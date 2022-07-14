// $Date: 2020-12-25 16:37:46 +0300 (Пт, 25 дек 2020) $
// $Revision: 473 $
// $Author: agalkin $
// Создание тестового договора

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;
    using System;

    public class Test_NewContract : Test_NewProj
    {
        /// <summary>
        /// Получает или устанавливает договор.
        /// </summary>
        protected dynamic Contract { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.NotNull(this.Repo.Contracts);
            Assert.NotNull(this.Repo.ContractsID);
            this.Contract = this.CreateContract("Интеграционные тесты " + DateTime.Now.ToString());
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo.Contracts);
            Assert.NotNull(this.Repo.ContractsID);
            if (this.Contract != null)
            {
                this.Repo.Contracts.UnLock(this.Contract.ContrGUID);
                this.Repo.Contracts.Delete(this.Contract.ContrGUID);
            }

            base.TearDown();
        }

        /// <summary>
        /// Создает договор.
        /// </summary>
        /// <param name="number">Наименование ОС.</param>
        /// <returns>Ссылка на ОС.</returns>
        protected dynamic CreateContract(string number)
        {
            dynamic contract = this.Repo.Contracts.New(this.Proj.ID.GUID);
            Assert.NotNull(contract);
            contract.Number = number;
            contract.ExecutorID = 1;
            contract.CustomerID = 1;
            this.Repo.Contracts.Save(contract);
            return contract;
        }
    }
}