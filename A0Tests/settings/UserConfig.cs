// $Date: 2020-08-07 12:17:29 +0300 (Пт, 07 авг 2020) $
// $Revision: 357 $
// $Author: agalkin $
// Тесты пользовательских настроек

namespace A0Tests.settings
{
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки корректности пользовательских настроек.
    /// </summary>
    [TestFixture(
        Category = "settings",
        Description = "Тесты проверки корректности пользовательских настроек",
        Author = "agalkin")]
    public class Test_UserConfig : A0Config
    {
        /// <summary>
        /// Проверяет работоспособность чтения строки подключения к БД.
        /// </summary>
        [Test(Description = "Строка подключения к БД")]
        public void Test_ConnString()
        {
            Assert.NotNull(this.ConnStr);
        }

        /// <summary>
        /// Проверяет работоспособность чтения логина пользователя.
        /// </summary>
        [Test(Description = "Логин пользователя")]
        public void Test_UserName()
        {
            Assert.NotNull(this.UserName);
        }

        /// <summary>
        /// Проверяет работоспособность чтения пароля пользователя.
        /// </summary>
        [Test(Description = "Пароль пользователя")]
        public void Test_Password()
        {
            Assert.NotNull(this.Password);
        }

        /// <summary>
        /// Проверяет работоспособность чтения количества одновременно устанавливаемых соединений и валидности его значения.
        /// </summary>
        [Test(Description = "Количество одновременно устанавливаемых соединений")]
        public void Test_MultiConnectCount()
        {
            string multiConnectCount = this.Configuration?.Descendants("testMultiConnectCount")?.SingleOrDefault()?.Value;
            Assert.NotNull(multiConnectCount);
            Assert.True(int.TryParse(multiConnectCount, out int result));
            Assert.True(result > 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения времени ожидания установки соединения и валидности его значения.
        /// </summary>
        [Test(Description = "Время ожидания установки соединения")]
        public void Test_ConnectTimeOutMS()
        {
            string timeOutMs = this.Configuration?.Descendants("testConnectTimeOutMS")?.SingleOrDefault()?.Value;
            Assert.NotNull(timeOutMs);
            Assert.True(int.TryParse(timeOutMs, out int result));
            Assert.True(result >= 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения времени ожидания установки соединения для параллельных соединений и валидности его значения.
        /// </summary>
        [Test(Description = "Время ожидания установки соединения для параллельных соединений")]
        public void Test_MultiConnectTimeOutMS()
        {
            string timeOutMs = this.Configuration?.Descendants("testMultiConnectTimeOutMS")?.SingleOrDefault()?.Value;
            Assert.NotNull(timeOutMs);
            Assert.True(int.TryParse(timeOutMs, out int result));
            Assert.True(result >= 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения пути и наличие файла ЛС в формате А0.
        /// </summary>
        [Test(Description = "Импортируемый файла формата А0")]
        public void Test_ImportFrom()
        {
            string importFrom = this.Configuration?.Descendants("importFrom")?.SingleOrDefault()?.Value;
            Assert.NotNull(importFrom);
            Assert.True(File.Exists(importFrom), "Файл не найден");
        }

        /// <summary>
        /// Проверяет работоспособность чтения пути и наличие файла ЛС в формате АПРС 1.
        /// </summary>
        [Test(Description = "Импортируемый файла формата АПРС 1")]
        public void Test_ImportFromAPRS()
        {
            string importFrom = this.Configuration?.Descendants("impormFromARPS")?.SingleOrDefault()?.Value;
            Assert.NotNull(importFrom);
            Assert.True(File.Exists(importFrom), "Файл не найден");
        }

        /// <summary>
        /// Проверяет работоспособность чтения пути и наличие файла ЛС в формате XMLGrand.
        /// </summary>
        [Test(Description = "Импортируемый файла формата XMLGrand")]
        public void Test_ImportFromXMLGrand()
        {
            string importFrom = this.Configuration?.Descendants("importFromXMLGrand")?.SingleOrDefault()?.Value;
            Assert.NotNull(importFrom);
            Assert.True(File.Exists(importFrom), "Файл не найден");
        }

        /// <summary>
        /// Проверяет работоспособность чтения пути и наличие файлов шаблонов печати.
        /// </summary>
        [Test(Description = "Файлы шаблонов печати")]
        public void Test_PrintTemplates()
        {
            string printTemplates = this.Configuration?.Descendants("printTemplates")?.SingleOrDefault()?.Value;
            Assert.NotNull(printTemplates);
            Assert.True(Directory.Exists(printTemplates), "Файлы шаблонов печати не найдены");
        }
    }
}