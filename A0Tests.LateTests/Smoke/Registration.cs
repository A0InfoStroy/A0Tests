// $Date: 2020-12-07 13:33:29 +0300 (Пн, 07 дек 2020) $
// $Revision: 447 $
// $Author: agalkin $
// Тесты проверки регистрации A0Service

namespace A0Tests.LateTests.Smoke
{
    using System;
    using System.Runtime.InteropServices;
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
            // Получаем тип объекта API.
            Type a0Type = Type.GetTypeFromProgID("A0Service.API");

            // Создаем A0 API.
            dynamic a0 = Activator.CreateInstance(a0Type);

            Assert.NotNull(a0, "Не могу получить интерфейс доступа к API A0");
            Marshal.ReleaseComObject(a0);
            a0 = null;
        }
    }
}