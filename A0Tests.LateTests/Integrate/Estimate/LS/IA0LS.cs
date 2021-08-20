// $Date: 2020-12-22 15:00:12 +0300 (Вт, 22 дек 2020) $
// $Revision: 463 $
// $Author: agalkin $
// Тестирование ЛС

namespace A0Tests.LateTests.Integrate.Estimate
{
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности локальной сметы.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LS",
        Author = "agalkin")]
    public class Test_IA0LS : Test_NewLS
    {
        /// <summary>
        /// Проверяет корректность создания текстовой строки ЛС.
        /// </summary>
        [Test(Description = "Создание текстовой строки")]
        public void Test_CreateTxtString()
        {
            int stringsCount = this.LS.Strings.Count;
            // Значение 3 соответствует EA0StringKind.skMK.
            dynamic lsString = this.LS.CreateTxtString(3, "1", 0);
            Assert.NotNull(lsString);
            Assert.True(this.LS.Strings.Count == stringsCount + 1);
            dynamic lsStr = this.LS.Strings.Items[this.LS.Strings.Count - 1];
            Assert.AreEqual(lsString.GUID, lsStr.GUID);
        }

        /// <summary>
        /// Проверяет корректность удаления текстовой строки ЛС.
        /// </summary>
        [Test(Description = "Удаление текстовой строки")]
        public void Test_DeleteTxtString()
        {
            // Значение 3 соответствует EA0StringKind.skMK.
            dynamic lsString = this.LS.CreateTxtString(3, "1", 0);
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