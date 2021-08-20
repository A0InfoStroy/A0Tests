﻿// $Date: 2020-12-10 17:18:29 +0300 (Чт, 10 дек 2020) $
// $Revision: 451 $
// $Author: agalkin $
// Базовые тесты IA0ResIDRepo

namespace A0Tests.LateTests.Smoke.Estimate
{
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога поиска ресурсов.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0ResIDRepo",
        Author = "agalkin")]
    public class Test_IA0ResIDRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог поиска ресурсов.
        /// </summary>
        protected dynamic Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Estimate.Repo.ResID;
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
            Assert.NotNull(this.Repo.GetFieldRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с сортировкой.
        /// </summary>
        [Test]
        public void Test_GetOrderRequest()
        {
            Assert.NotNull(this.Repo.GetOrderRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения запроса с фильтрацией.
        /// </summary>
        [Test]
        public void Test_GetWhereRequest()
        {
            Assert.NotNull(this.Repo.GetWhereRequest());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу ЛС в проекте или комплексе.
        /// </summary>
        [Test]
        public void Test_Read()
        {
            Assert.NotNull(this.Repo.Read(this.A0.Estimate.Repo.ComplexID.HeadComplexGUID, null, null, null));
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения итератора по каталогу ЛС в проекте или комплексе с учетом запросов.
        /// </summary>
        [Test]
        public void Test_Read2()
        {
            Assert.NotNull(this.Repo.Read(this.A0.Estimate.Repo.ComplexID.HeadComplexGUID, 
                this.Repo.GetFieldRequest(), 
                this.Repo.GetWhereRequest(), 
                this.Repo.GetOrderRequest()));
        }
    }
}