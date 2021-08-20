// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты поиска проектов

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности списка проектов для поиска строк.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ProjIDs",
        Author = "agalkin")]
    public class Test_IA0ProjIDs : Test_Estimate
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
        [Test]
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