// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты поиска проектов

namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности списка проектов для поиска строк.
    /// </summary>
    public class Test_IA0ProjIDs : Test_EstimateCustom
    {
        /// <summary>
        /// Получает или устанавливает значение списка проектов для поиска строк.
        /// </summary>
        protected IA0ProjIDs ProjIDs { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ProjIDs = this.Repo.Searcher.GetProjIDs();
            Assert.NotNull(this.ProjIDs);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ProjIDs);
            this.ProjIDs = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность операций со списком проектов.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_AddDelete()
        {
            string id = "0";
            this.ProjIDs.Add(id);
            int count = this.ProjIDs.Count;
            Assert.NotZero(count);
            string projID = this.ProjIDs.Item[count - 1];
            Assert.AreEqual(id, projID);
            this.ProjIDs.Delete(count - 1);
            Assert.AreEqual(count - 1, this.ProjIDs.Count);
        }
    }
}