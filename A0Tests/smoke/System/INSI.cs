﻿// $Date: 2020-08-17 15:35:09 +0300 (Пн, 17 авг 2020) $
// $Revision: 374 $
// $Author: agalkin $
// Базовые тесты INSI

namespace A0Tests.Smoke.Sys
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности домена НСИ в системе.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности INSI",
        Author = "agalkin")]
    public class Test_INSI : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен НСИ в системе.
        /// </summary>
        protected INSI NSI { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.NSI = this.A0.Sys.NSI;
            Assert.NotNull(this.NSI);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.NSI);
            this.NSI = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базам НСИ.
        /// </summary>
        [Test]
        public void Test_Bases()
        {
            Assert.NotNull(this.NSI.Bases);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к сервисам домена.
        /// </summary>
        [Test]
        public void Test_Services()
        {
            Assert.NotNull(this.NSI.Services);
        }
    }
}