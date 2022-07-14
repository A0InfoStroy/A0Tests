// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Базовые тесты IA0ProjIDRepo

namespace A0Tests.Smoke.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога поиска проектов.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ProjIDRepo",
        Author = "agalkin")]
    public class Test_IA0ProjIDRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог поиска проектов.
        /// </summary>
        protected IA0ProjIDRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Estimate.Repo.ProjID;
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
        /// Проверяет отсутствие ошибок при вызове метода получения запроса к дополнительным полям.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_GetFiledsRequest()
        {
            Assert.NotNull(this.Repo.GetFieldRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id головного узла.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_HeadNodeID()
        {
            int headNodeID = this.Repo.HeadNodeID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid головного комплекса.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_HeadComplexGUID()
        {
            System.Guid headComplexGUID = this.Repo.HeadComplexGUID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу комплексов в системе.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read(this.Repo.GetFieldRequest()));
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу комплексов для комплекса.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Read2()
        {
            Assert.NotNull(this.Repo.Read2(this.Repo.HeadComplexGUID, this.Repo.GetFieldRequest()));
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу комплексов для узла комплекса.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Read3()
        {
            Assert.NotNull(this.Repo.Read3(this.Repo.HeadComplexGUID, this.Repo.HeadNodeID, this.Repo.GetFieldRequest()));
        }
    }
}