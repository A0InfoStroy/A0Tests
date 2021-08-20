// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты строк локальных смет

namespace A0Tests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности списка строк Лс.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrings",
        Author = "agalkin")]
    public class Test_IA0LSStrings : NewLS
    {
        /// <summary>
        /// Получает или устанавливает значение списка строк ЛС.
        /// </summary>
        protected IA0LSStrings LSStrings { get; private set; }

        /// <summary>
        /// Получает или устанавливает строку ЛС.
        /// </summary>
        protected IA0LSString LSString { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LSString = this.LS.CreateTxtString(EA0StringKind.skWork, "1234", this.LS.Tree.Head.ID);
            this.LSStrings = this.LS.Strings;
            Assert.NotNull(this.LSStrings);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.IsNotNull(this.LSStrings);
            this.LSStrings = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность метода получаещего строку ЛС по Guid.
        /// </summary>
        [Test(Description = "Запрос по Guid"), Timeout(10000)]
        public void Test_ByGuid()
        {
            IA0LSString lsString = this.LSStrings.ByGUID(this.LSString.GUID);
            Assert.NotNull(lsString);
            Assert.AreEqual(this.LSString.GUID, lsString.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода удаления строки ЛС по Guid.
        /// </summary>
        [Test(Description = "Удаление строки"), Timeout(10000)]
        public void Test_Delete()
        {
            this.LSStrings.Delete(this.LSStrings.Count - 1);
            try
            {
                IA0LSString lsStr = this.LSStrings.ByGUID(this.LSString.GUID);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80004005);
            }
        }
    }
}