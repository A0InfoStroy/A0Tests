// $Date: 2020-12-22 15:06:55 +0300 (Вт, 22 дек 2020) $
// $Revision: 464 $
// $Author: agalkin $
// Тесты строк локальных смет

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности списка строк Лс.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrings",
        Author = "agalkin")]
    public class Test_IA0LSStrings : Test_NewLS
    {
        /// <summary>
        /// Получает или устанавливает значение списка строк ЛС.
        /// </summary>
        protected dynamic LSStrings { get; private set; }

        /// <summary>
        /// Получает или устанавливает строку ЛС.
        /// </summary>
        protected dynamic LSString { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            // Значение 3 соответствует EA0StringKind.skMK.
            this.LSString = this.LS.CreateTxtString(3, "1234", this.LS.Tree.Head.ID);
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
        [Test(Description = "Запрос по Guid")]
        public void Test_ByGuid()
        {
            dynamic lsString = this.LSStrings.ByGUID(this.LSString.GUID);
            Assert.NotNull(lsString);
            Assert.AreEqual(this.LSString.GUID, lsString.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода удаления строки ЛС по Guid.
        /// </summary>
        [Test(Description = "Удаление строки")]
        public void Test_Delete()
        {
            this.LSStrings.Delete(this.LSStrings.Count - 1);
            try
            {
                dynamic lsStr = this.LSStrings.ByGUID(this.LSString.GUID);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80004005);
            }
        }
    }
}