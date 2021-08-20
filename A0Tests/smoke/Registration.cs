// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты проверки регистрации A0Service

namespace A0Tests.Smoke
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
        [Test(Description = "Проверка создания IAPI"), Timeout(15000)]
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