// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты результа поиска

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности поиска информации в системе.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0SearchResult",
        Author = "agalkin")]
    public class Test_IA0SearchResult : Test_NewProj
    {
        /// <summary>
        /// Получает или устанавливает сущность предоставляющую поиск информации в системе.
        /// </summary>
        protected IA0SearchResult SearchResult { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            IA0ProjIDs projIds = this.Repo.Searcher.GetProjIDs();
            projIds.Add(this.Proj.ID.ID.ToString());
            this.SearchResult = this.Repo.Searcher.Search(null, null, null, null, projIds);
            Assert.NotNull(this.SearchResult);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.SearchResult);
            this.SearchResult = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения количества элементов списка результатов поиска.
        /// </summary>
        [Test]
        public void Test_Count()
        {
            int count = this.SearchResult.Count;
            Assert.NotZero(count);
        }

        /// <summary>
        /// Проверяет работоспособность чтения данных списка с результатами поиска.
        /// </summary>
        [Test]
        public void Test_Next()
        {
            for (int i = 0; i < this.SearchResult.Count; i++)
            {
                IA0SearchItemTariff next = this.SearchResult.Next();
                Assert.NotNull(next);
                string basing = next.Basing;
                string folder = next.Folder;
                string folderGroup = next.FolderGroup;
                IA0SearchItemsLSStr lsStrings = next.LSStrings;
                string name = next.Name;
            }
        }
    }
}