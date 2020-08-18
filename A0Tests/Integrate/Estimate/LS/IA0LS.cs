// $Date: 2020-07-28 13:36:08 +0300 (Вт, 28 июл 2020) $
// $Revision: 320 $
// $Author: agalkin $
// Тестирование ЛС

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности локальной сметы.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LS",
        Author = "agalkin")]
    public class Test_IA0LS : NewLS
    {
        /// <summary>
        /// Проверяет корректность создания текстовой строки ЛС.
        /// </summary>
        [Test(Description = "Создание текстовой строки")]
        public void Test_CreateTxtString()
        {
            int stringsCount = this.LS.Strings.Count;
            IA0LSString lsString = this.LS.CreateTxtString(EA0StringKind.skWork, "1", 0);
            Assert.NotNull(lsString);
            Assert.True(this.LS.Strings.Count == stringsCount + 1);
            IA0LSString lsStr = this.LS.Strings.Items[this.LS.Strings.Count - 1];
            Assert.AreEqual(lsString.GUID, lsStr.GUID);
        }

        /// <summary>
        /// Проверяет корректность удаления текстовой строки ЛС.
        /// </summary>
        [Test(Description = "Удаление текстовой строки")]
        public void Test_DeleteTxtString()
        {
            IA0LSString lsString = this.LS.CreateTxtString(EA0StringKind.skWork, "1", 0);
            int stringsCount = this.LS.Strings.Count;
            this.LS.DeleteString(lsString.GUID);
            Assert.True(this.LS.Strings.Count == stringsCount - 1);
        }

        /// <summary>
        /// Проверяет остутствие ошибок при вызове операции пересчета.
        /// </summary>
        [Test(Description = "Пересчет")]
        public void Test_Recalc()
        {
            this.LS.Recalc();
        }
    }
}