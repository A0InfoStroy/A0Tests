﻿// $Date: 2020-12-10 17:18:29 +0300 (Чт, 10 дек 2020) $
// $Revision: 451 $
// $Author: agalkin $
// Базовые тесты IA0ComplexIDRepo

namespace A0Tests.LateTests.Smoke.Estimate
{
    using NUnit.Framework;

    /// <summary>
    ///  Содержит базовые тесты проверки работоспособности каталога комплексов.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ComplexIDRepo",
        Author = "agalkin")]
    public class Test_IA0ComplexIDRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог комплексов.
        /// </summary>
        protected dynamic Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Estimate.Repo.ComplexID;
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
        [Test]
        public void Test_GetFiledsRequest()
        {
            Assert.NotNull(this.Repo.GetFiledRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Id головного узла.
        /// </summary>
        [Test]
        public void Test_HeadNodeID()
        {
            int headNodeID = this.Repo.HeadNodeID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к Guid головного комплекса.
        /// </summary>
        [Test]
        public void Test_HeadComplexGUID()
        {
            System.Guid headComplexGUID = this.A0.Estimate.Repo.ComplexID.HeadComplexGUID;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу комплексов в системе.
        /// </summary>
        [Test]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read(this.Repo.GetFiledRequest()));
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу комплексов для комплекса.
        /// </summary>
        [Test]
        public void Test_Read2()
        {
            Assert.NotNull(this.Repo.Read2(this.Repo.HeadComplexGUID, this.Repo.GetFiledRequest()));
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу комплексов для узла комплекса.
        /// </summary>
        [Test]
        public void Test_Read3()
        {
            Assert.NotNull(this.Repo.Read3(this.Repo.HeadComplexGUID, this.Repo.HeadNodeID, this.Repo.GetFiledRequest()));
        }
    }
}