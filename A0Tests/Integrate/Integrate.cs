// $Date: 2020-08-07 12:16:46 +0300 (Пт, 07 авг 2020) $
// $Revision: 356 $
// $Author: agalkin $
// Базовые классы для интергационных тестов

namespace A0Tests.Integrate
{
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс для интеграционного тестирования.
    /// </summary>
    public abstract class Test_IntegrateCustom : Test_Base
    {
        /// <summary>
        /// Выполняется перед запуском всех тестов из всего тестового класса.
        /// </summary>
        [OneTimeSetUp]
        public virtual void BeforeTestSuit()
        {
        }

        /// <summary>
        /// Выполняется после прохождения всех тестов из всего тестового класса.
        /// </summary>
        [OneTimeTearDown]
        public virtual void AfterTestSuit()
        {
        }
    }
}