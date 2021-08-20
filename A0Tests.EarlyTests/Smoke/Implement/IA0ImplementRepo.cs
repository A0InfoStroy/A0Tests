// $Date: 2021-01-19 15:20:11 +0300 (Вт, 19 янв 2021) $
// $Revision: 500 $
// $Author: agalkin $
// Базовые тесты IA0ImplementRepo

namespace A0Tests.EarlyTests.Smoke.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога выполнения.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ImplementRepo",
        Author = "agalkin")]
    public class Test_IA0ImplementRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог выполнения.
        /// </summary>
        protected IA0ImplementRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Implement.Repo;
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
        /// Проверяет отсутствие ошибок при обращении к каталогу операций над актами в системе.
        /// </summary>
        [Test(Description = "Act Repo")]
        public void Test_Act()
        {
            IA0ActRepo actRepo = this.Repo.Act;
            Assert.NotNull(actRepo);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу поиска актов.
        /// </summary>
        [Test(Description = "ActID Repo")]
        public void Test_ActID()
        {
            IA0ActIDRepo actIdRepo = this.Repo.ActID;
            Assert.NotNull(actIdRepo);
        }
    }
}