// $Date: 2022-03-28 11:09:32 +0300 (Пн, 28 мар 2022) $
// $Revision: 577 $
// $Author: eloginov $
// Тестирование ЛС

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;
    using System;

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
        /// Проверяет работоспособность метода удаления текстовых строк по индексу.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_DeleteStrings()
        {
            // Создание строк.
            IA0LSString str1 = this.LS.CreateTxtString(EA0StringKind.skWork, "1", this.LS.Tree.Head.ID);
            Guid strGuid1 = str1.GUID;
            Assert.NotNull(this.LS.Strings.ByGUID(strGuid1));

            IA0LSString str2 = this.LS.CreateTxtString(EA0StringKind.skWork, "2", this.LS.Tree.Head.ID);
            Guid strGuid2 = str2.GUID;
            Assert.NotNull(this.LS.Strings.ByGUID(strGuid2));

            IA0LSString actWorkString = this.LS.CreateWorkString(aNSIID: 7, aFolderID: 2553, aWorkID: 787669, aNodeID: this.LS.Tree.Head.ID);
            Guid workStrGuid = actWorkString.GUID;
            Assert.NotNull(this.LS.Strings.ByGUID(actWorkString.GUID));

            IA0LSString actResString = LS.CreateResString(aNSIID: 7, aFolderID: 907, aResID: 6197091, aNodeID: this.LS.Tree.Head.ID);
            Guid resStrGuid = actResString.GUID;
            Assert.NotNull(this.LS.Strings.ByGUID(actResString.GUID));

            // Устанавливаем объем для строк
            actWorkString.Volume = 10;
            actResString.Volume = 10;
            actResString.Resources.Items[0].Price = 10;

            // Пересчитываем смету для актуализации итогов
            LS.Recalc();

            var total = LS.Totals.ByName["9 Сметная стоимость"];

            //Проерка корректности полученной стоимости 40101+100
            Assert.AreEqual(40201, total.Total);

            int count = this.LS.Strings.Count;
            for (int i = 0; i < count; i++)
            {
                this.LS.Strings.Delete(i);
            }

            // Проверка отсутствия строк.
            Assert.Null(this.LS.Strings.ByGUID(strGuid1));
            Assert.Null(this.LS.Strings.ByGUID(strGuid2));
            Assert.Null(this.LS.Strings.ByGUID(workStrGuid));
            Assert.Null(this.LS.Strings.ByGUID(resStrGuid));

            //После удаления строк стоимость должна быть 0
            Assert.AreEqual(0, total.Total);
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

        /// <summary>
        /// Проверяет корректность удаления строки работы ЛС.
        /// </summary>
        [Test(Description = "Удаление строки работы"), Timeout(20000)]
        public void Test_DeleteWorkString()
        {
            IA0LSString lsString = this.LS.CreateWorkString(aNSIID: 7, aFolderID: 2553, aWorkID: 787669, aNodeID: this.LS.Tree.Head.ID);
            int stringsCount = this.LS.Strings.Count;

            lsString.Volume = 10;

            // Пересчитываем смету для актуализации итогов
            LS.Recalc();
            var total = LS.Totals.ByName["9 Сметная стоимость"];

            //Проерка корректности полученной стоимости 40101
            Assert.AreEqual(40101, total.Total);


            this.LS.DeleteString(lsString.GUID);
            Assert.True(this.LS.Strings.Count == stringsCount - 1);

            //После удаления строки стоимость должна быть 0
            Assert.AreEqual(0, total.Total);

            // Проверка отсутствия строки.
            Assert.Null(this.LS.Strings.ByGUID(lsString.GUID));
        }

        /// <summary>
        /// Проверяет корректность удаления строки ресурса ЛС.
        /// </summary>
        [Test(Description = "Удаление строки ресурса"), Timeout(20000)]
        public void Test_DeleteResString()
        {
            IA0LSString lsString = this.LS.CreateResString(aNSIID: 7, aFolderID: 907, aResID: 6197091, aNodeID: this.LS.Tree.Head.ID);
            int stringsCount = this.LS.Strings.Count;

            lsString.Volume = 10;
            lsString.Resources.Items[0].Price = 10;

            // Пересчитываем смету для актуализации итогов
            LS.Recalc();
            var total = LS.Totals.ByName["9 Сметная стоимость"];

            //Проерка корректности полученной стоимости 100
            Assert.AreEqual(100, total.Total);

            this.LS.DeleteString(lsString.GUID);
            Assert.True(this.LS.Strings.Count == stringsCount - 1);

            //После удаления строки стоимость должна быть 0
            Assert.AreEqual(0, total.Total);

            // Проверка отсутствия строки.
            Assert.Null(this.LS.Strings.ByGUID(lsString.GUID));
        }
    }
}