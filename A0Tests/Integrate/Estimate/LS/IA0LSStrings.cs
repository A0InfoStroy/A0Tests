// $Date: 2022-03-28 11:17:57 +0300 (Пн, 28 мар 2022) $
// $Revision: 578 $
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
        /// Получает или устанавливает текстовую строку ЛС.
        /// </summary>
        protected IA0LSString LSTxtString { get; private set; }

        /// <summary>
        /// Получает или устанавливает текстовую строку ЛС на основе расценки работы из БД НСИ.
        /// </summary>
        protected IA0LSString LSWorkString { get; private set; }

        /// <summary>
        /// Получает или устанавливает текстовую строку ЛС на основе расценки ресурса из БД НСИ.
        /// </summary>
        protected IA0LSString LSResString { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LSTxtString = this.LS.CreateTxtString(EA0StringKind.skWork, "1234", this.LS.Tree.Head.ID);
            this.LSWorkString = this.LS.CreateWorkString(aNSIID: 7, aFolderID: 2553, aWorkID: 787669, aNodeID: this.LS.Tree.Head.ID);
            this.LSResString = this.LS.CreateResString(aNSIID: 7, aFolderID: 907, aResID: 6197091, aNodeID: this.LS.Tree.Head.ID);
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
        /// Проверяет работоспособность метода получаещего строки ЛС по Guid.
        /// </summary>
        [Test(Description = "Запрос по Guid"), Timeout(10000)]
        public void Test_ByGuid()
        {
            IA0LSString lsTxtString = this.LSStrings.ByGUID(this.LSTxtString.GUID);
            IA0LSString lsWorkString = this.LSStrings.ByGUID(this.LSWorkString.GUID);
            IA0LSString lsResString = this.LSStrings.ByGUID(this.LSResString.GUID);

            Assert.NotNull(lsTxtString);
            Assert.NotNull(lsWorkString);
            Assert.NotNull(lsResString);

            Assert.AreEqual(this.LSTxtString.GUID, lsTxtString.GUID);
            Assert.AreEqual(this.LSWorkString.GUID, lsWorkString.GUID);
            Assert.AreEqual(this.LSResString.GUID, lsResString.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода удаления строк ЛС по Guid.
        /// </summary>
        [Test(Description = "Удаление строк")]
        public void Test_Delete()
        {
            this.LSStrings.Delete(this.LSStrings.Count - 1);
            try
            {
                IA0LSString lsStr = this.LSStrings.ByGUID(this.LSTxtString.GUID);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80004005);
            }

            this.LSStrings.Delete(this.LSStrings.Count - 1);
            try
            {
                IA0LSString lsStr = this.LSStrings.ByGUID(this.LSWorkString.GUID);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80004005);
            }

            this.LSStrings.Delete(this.LSStrings.Count - 1);
            try
            {
                IA0LSString lsStr = this.LSStrings.ByGUID(this.LSResString.GUID);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80004005);
            }
        }
    }
}