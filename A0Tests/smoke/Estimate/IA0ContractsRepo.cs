// $Date: 2020-08-17 15:35:09 +0300 (Пн, 17 авг 2020) $
// $Revision: 374 $
// $Author: agalkin $
// Базовые тесты IA0ContractsRepo

namespace A0Tests.Smoke.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовые тесты проверки работоспособности каталога операций с контрактами.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ContractsRepo",
        Author = "agalkin")]
    public class Test_IA0ContractsRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог операций с договорами.
        /// </summary>
        protected IA0ContractsRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Estimate.Repo.Contracts;
            Assert.NotNull(this.Repo);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo);
            this.Repo = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода загрузки договора по Guid.
        /// </summary>
        [Test]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read(Guid.Empty, EAccessKind.akRead));
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода разблокировки договора.
        /// </summary>
        [Test]
        public void Test_Unlock()
        {
            this.Repo.UnLock(Guid.Empty);
        }
    }
}