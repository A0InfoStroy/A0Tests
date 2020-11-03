﻿// $Date: 2020-11-03 12:29:48 +0300 (Вт, 03 ноя 2020) $
// $Revision: 408 $
// $Author: agalkin $
// Базовые тесты IA0Log

namespace A0Tests.Smoke.App
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовые тесты проверки работоспособности протокола А0.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0Log",
        Author = "agalkin")]
    public class Test_IA0Log : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает протокол А0.
        /// </summary>
        protected IA0LogDomain Log { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Log = this.A0.App.Log;
            Assert.NotNull(this.Log);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Log);
            this.Log = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения имени файла протокола.
        /// </summary>
        [Test]
        public void Test_FileName()
        {
            string fileName = this.Log.FileName;
            Assert.NotNull(fileName);
            Assert.IsTrue(fileName.Length > 0);
        }
    }
}