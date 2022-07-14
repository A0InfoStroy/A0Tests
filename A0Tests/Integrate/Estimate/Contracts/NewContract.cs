// $Date: 2020-11-06 11:38:15 +0300 (Пт, 06 ноя 2020) $
// $Revision: 412 $
// $Author: agalkin $
// Создание тестового договора

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;
    using System;

    public class NewContract : NewProj
    {
        /// <summary>
        /// Получает или устанавливает договор.
        /// </summary>
        protected IA0Contract Contract { get; set; }

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
        protected IA0Contract CreateContract(string number)
        {
            IA0Contract contract = this.Repo.Contracts.New(this.Proj.ID.GUID);
            Assert.NotNull(contract);
            contract.Number = number;

            contract.ExecutorID = 1;
            contract.CustomerID = 1;
            this.Repo.Contracts.Save(contract);
            return contract;
        }
    }
}