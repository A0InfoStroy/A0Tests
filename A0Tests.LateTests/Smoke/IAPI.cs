// $Date: 2020-12-07 13:40:15 +0300 (Пн, 07 дек 2020) $
// $Revision: 448 $
// $Author: agalkin $
// Базовые тесты

namespace A0Tests.LateTests.Smoke
{
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности основного интерфейс A0 API.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IAPI",
        Author = "agalkin")]
    public class Test_IAPI : Test_Base
    {
        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к оповещению об ошибке обновления сессии.
        /// </summary>
        [Test]
        public void Test_ACExitNotify()
        {
            dynamic acExitNotify = this.A0.ACExitNotify;
            Assert.Null(acExitNotify);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения списка реквизитом сметного объекта.
        /// </summary>
        [Test]
        public void Test_GetA0DocEntrys()
        {
            dynamic docEntrys = this.A0.GetA0DocEntrys();
            Assert.NotNull(docEntrys);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения списка реквизитов системных данных.
        /// </summary>
        [Test]
        public void Test_GetA0SysDocEntrys()
        {
            dynamic sysDocEntrys = this.A0.GetA0SysDocEntrys();
            Assert.NotNull(sysDocEntrys);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену приложения А0.
        /// </summary>
        [Test]
        public void Test_App()
        {
            dynamic app = this.A0.App;
            Assert.NotNull(app);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену сущностей.
        /// </summary>
        [Test]
        public void Test_Entities()
        {
            dynamic entities = this.A0.Entities;
            Assert.NotNull(entities);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену сметных данных.
        /// </summary>
        [Test]
        public void Test_Estimate()
        {
            dynamic estimate = this.A0.Estimate;
            Assert.NotNull(estimate);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену сметных данных выполнения.
        /// </summary>
        [Test]
        public void Test_Implement()
        {
            dynamic implement = this.A0.Implement;
            Assert.NotNull(implement);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену системных данных.
        /// </summary>
        [Test]
        public void Test_System()
        {
            dynamic system = this.A0.Sys;
            Assert.NotNull(system);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к пользователю системы разделения доступа.
        /// </summary>
        [Test]
        public void Test_CurrentUser()
        {
            dynamic currentUser = this.A0.CurrentUser;
            Assert.NotNull(currentUser);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к домену связей между сметными данными.
        /// </summary>
        [Test]
        public void Test_Links()
        {
            dynamic links = this.A0.Links;
            Assert.NotNull(links);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения системы печати для актов.
        /// </summary>
        [Test]
        public void Test_PrintAct()
        {
            // 1 - соответствует EA0PrintKind.pkAct
            dynamic print = this.A0.GetPrint(1, string.Empty);
            Assert.NotNull(print);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения системы печати для ЛС.
        /// </summary>
        [Test]
        public void Test_PrintLS()
        {
            // 0 - соответствует EA0PrintKind.pkLS
            dynamic print = this.A0.GetPrint(0, string.Empty);
            Assert.NotNull(print);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода генерации исключения для тестирования протокла.
        /// </summary>
        [Test]
        public void Test_Exception()
        {
            try
            {
                this.A0.TestException();
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("Тестовое исключение", ex.Message);
            }
        }
    }
}