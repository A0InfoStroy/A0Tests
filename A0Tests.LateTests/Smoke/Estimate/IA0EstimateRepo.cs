// $Date: 2020-12-10 17:18:29 +0300 (Чт, 10 дек 2020) $
// $Revision: 451 $
// $Author: agalkin $
// Базовые тесты IA0EstimateRepo

namespace A0Tests.LateTests.Smoke.Estimate
{
    using NUnit.Framework;

    /// <summary>
    ///  Базовые тесты проверки работоспособности каталога сметных данных в системе.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0EstimateRepo",
        Author = "agalkin")]
    public class Test_IA0EstimateRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог сметных данных в системе.
        /// </summary>
        protected dynamic EstimateRepo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.EstimateRepo = this.A0.Estimate.Repo;
            Assert.NotNull(this.EstimateRepo);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.EstimateRepo);
            this.EstimateRepo = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к проектам.
        /// </summary>
        [Test]
        public void Test_Proj()
        {
            Assert.NotNull(this.EstimateRepo.Proj);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к объектным сметам.
        /// </summary>
        [Test]
        public void Test_OS()
        {
            Assert.NotNull(this.EstimateRepo.OS);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к комплексам.
        /// </summary>
        [Test]
        public void Test_Complex()
        {
            Assert.NotNull(this.EstimateRepo.Complex);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к локальным сметам.
        /// </summary>
        [Test]
        public void Test_LS()
        {
            Assert.NotNull(this.EstimateRepo.LS);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к поисковику строк.
        /// </summary>
        [Test]
        public void Test_Searcher()
        {
            Assert.NotNull(this.EstimateRepo.Searcher);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к импорту сметных данных.
        /// </summary>
        [Test]
        public void Test_Import()
        {
            Assert.NotNull(this.EstimateRepo.Import);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу поиска ОС.
        /// </summary>
        [Test]
        public void Test_OSID()
        {
            Assert.NotNull(this.EstimateRepo.OSID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу поиска ЛС.
        /// </summary>
        [Test]
        public void Test_LSID()
        {
            Assert.NotNull(this.EstimateRepo.LSID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу поиска ресурсов.
        /// </summary>
        [Test]
        public void Test_ResID()
        {
            Assert.NotNull(this.EstimateRepo.ResID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу поиска проектов.
        /// </summary>
        [Test]
        public void Test_ProjID()
        {
            Assert.NotNull(this.EstimateRepo.ProjID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу поиска комплексов.
        /// </summary>
        [Test]
        public void Test_ComplexID()
        {
            Assert.NotNull(this.EstimateRepo.ComplexID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу поиска строк ЛС.
        /// </summary>
        [Test]
        public void Test_LSStrID()
        {
            Assert.NotNull(this.EstimateRepo.LSStrID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к каталогу поиска договоров.
        /// </summary>
        [Test]
        public void Test_ContractsID()
        {
            Assert.NotNull(this.EstimateRepo.ContractsID);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к договорам.
        /// </summary>
        [Test]
        public void Test_Contracts()
        {
            Assert.NotNull(this.EstimateRepo.Contracts);
        }
    }
}