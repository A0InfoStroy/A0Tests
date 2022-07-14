// $Date: 2020-12-11 14:53:06 +0300 (Пт, 11 дек 2020) $
// $Revision: 454 $
// $Author: agalkin $
// Базовые тесты IA0SystemRepo

namespace A0Tests.LateTests.Smoke.Sys
{
    using NUnit.Framework;

    /// <summary>
    /// Содержит базовые тесты проверки работоспособности каталога системных данных в системе.
    /// </summary>
    [TestFixture(
        Category = "smoke",
        Description = "Базовые тесты проверки работоспособности IA0SystemRepo",
        Author = "agalkin")]
    public class Test_IA0SystemRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает домен организаций.
        /// </summary>
        protected dynamic Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Sys.Repo;
            Assert.NotNull(this.Repo);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo);
            this.Repo = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к бизнес-этапам в системе.
        /// </summary>
        [Test]
        public void Test_BussinnessStages()
        {
            Assert.NotNull(this.Repo.BussinnessStages);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения списка уровней группировки.
        /// </summary>
        [Test]
        public void Test_GetSysTreeLevelList()
        {
            Assert.NotNull(this.Repo.GetSysTreeLevelList());
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при вызове метода получения группированного дерева концевиков.
        /// </summary>
        [Test]
        public void Test_GetProgSysTree()
        {
            dynamic levelList = this.Repo.GetSysTreeLevelList();
            Assert.NotNull(this.Repo.GetProgSysTree(levelList));
        }
    }
}