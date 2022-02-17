// $Date: 2022-01-31 14:18:57 +0300 (Пн, 31 янв 2022) $
// $Revision: 570 $
// $Author: vbutov $
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
        [Test(Description = "Создание текстовой строки"), Timeout(20000)]
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
        [Test(Description = "Удаление текстовой строки"), Timeout(20000)]
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
        [Test(Description = "Пересчет"), Timeout(20000)]
        public void Test_Recalc()
        {
            this.LS.Recalc();
        }

        /// <summary>
        /// Проверяет корректность создания строки работы ЛС.
        /// </summary>
        [Test(Description = "Создание строки работы"), Timeout(20000)]
        public void Test_CreateWorkString()
        {
            int stringsCount = this.LS.Strings.Count;

            // Пример расценки из НСИ.
            // БД: a0NSI_TER_01. Таблица: Works.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Деревообрабатывающее оборудование - 2553
            // ОБОРУДОВАНИЕ ОБЩЕГО НАЗНАЧЕНИЯ. ОБОРУДОВАНИЕ ЛЕСОПИЛЬНОГО ПРОИЗВОДСТВА. РАМА ЛЕСОПИЛЬНАЯ ОДНОЭТАЖНАЯ, МАССА 4,5Т - 787669 - 787668

            IA0LSString lsString = this.LS.CreateWorkString(aNSIID: 7, aFolderID: 2553, aWorkID: 787669, aNodeID: this.LS.Tree.Head.ID);
            Assert.NotNull(lsString);

            Assert.True(this.LS.Strings.Count == stringsCount + 1);

            IA0LSString lsStr = this.LS.Strings.Items[this.LS.Strings.Count - 1];
            Assert.AreEqual(lsString.GUID, lsStr.GUID);

            lsStr.Volume = 10;

            // Пересчитываем смету для актуализации итогов
            LS.Recalc();

            var total = LS.Totals.ByName["9 Сметная стоимость"];

            // Стоимость 40101
            Assert.AreEqual(40101, total.Total);
        }

        /// <summary>
        /// Проверяет корректность создания строки ресурса ЛС.
        /// </summary>
        [Test(Description = "Создание строки ресурса"), Timeout(20000)]
        public void Test_CreateResString()
        {
            int stringsCount = this.LS.Strings.Count;

            // Пример расценки из НСИ. 
            // БД: a0NSI_TER_01. Таблица: Resource.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Перевозка грузов для строительства - 907
            // АСФАЛЬТОБЕТОН, РАСТВОРЫ, БЕТОН ТОВАРНЫЙ-ПОГРУЗКА - 6197091

            IA0LSString lsString = this.LS.CreateResString(aNSIID: 7, aFolderID: 907, aResID: 6197091, aNodeID: this.LS.Tree.Head.ID);
            Assert.NotNull(lsString);

            Assert.True(this.LS.Strings.Count == stringsCount + 1);

            IA0LSString lsStr = this.LS.Strings.Items[this.LS.Strings.Count - 1];
            Assert.AreEqual(lsString.GUID, lsStr.GUID);

            lsStr.Volume = 10;
            lsStr.Resources.Items[0].Price = 10;

            // Пересчитываем смету для актуализации итогов
            LS.Recalc();

            var total = LS.Totals.ByName["9 Сметная стоимость"];

            // Стоимость 100
            Assert.AreEqual(100, total.Total);
        }
    }
}