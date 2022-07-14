// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты списка уровней дерева системных объектов

namespace A0Tests.Integrate.Sys
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности списка уровней дерева системных объектов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0SysTreeLevelList",
        Author = "agalkin")]
    public class Test_IA0SysTreeLevelList : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает список уровней дерева системных объектов.
        /// </summary>
        protected IA0SysTreeLevelList LevelList { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LevelList = this.A0.Sys.Repo.GetSysTreeLevelList();
            Assert.NotNull(this.LevelList);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LevelList);
            this.LevelList = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к количеству элементов списка.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Count()
        {
            int count = this.LevelList.Count;
        }

        /// <summary>
        /// Проверяет работоспособность добавления и удаления элементов списка по индексу.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_AddDelete()
        {
            A0SysTreeLevel level = A0SysTreeLevel.Destination;
            int count = this.LevelList.Count;
            this.LevelList.Add(level);
            Assert.AreEqual(count + 1, this.LevelList.Count);
            for (int i = 0; i < this.LevelList.Count; i++)
            {
                A0SysTreeLevel item = this.LevelList.Items[i];
                Assert.AreEqual(level, item);
            }

            this.LevelList.Delete(this.LevelList.Count - 1);
            Assert.AreEqual(count, this.LevelList.Count);
        }
    }
}