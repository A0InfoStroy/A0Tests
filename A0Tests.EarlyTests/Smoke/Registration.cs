// $Date: 2021-01-19 15:20:11 +0300 (Вт, 19 янв 2021) $
// $Revision: 500 $
// $Author: agalkin $
// Тесты проверки регистрации A0Service

namespace A0Tests.EarlyTests.Smoke
{
    using System.Runtime.InteropServices;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки регистрации A0Service.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Проверка регистрации A0Service",
        Author = "agalkin")]
    public class Test_Registration
    {
        /// <summary>
        /// Проверяет работособность создания IAPI.
        /// </summary>
        [Test(Description = "Проверка создания IAPI")]
        public void Create()
        {
            // Создание A0 API.
            IAPI a0 = new API();
            Assert.NotNull(a0, "Не могу получить интерфейс доступа к API A0");
            Marshal.ReleaseComObject(a0);
            a0 = null;
        }
    }
}