// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты каталога Проектов

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога проектов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ProjRepo",
        Author = "agalkin")]
    public class Test_IA0ProjRepo : Test_NewProj
    {
        /// <summary>
        /// Проверяет работоспособность метода создания проекта.
        /// </summary>
        [Test(Description = "Создание/Удаление")]
        public void Test_New()
        {
            // Создание проекта в головном комплексе.
            IA0Proj proj = this.Repo.Proj.New();
            Assert.NotNull(proj);
            proj.Title.Name = "Интеграционные тесты " + DateTime.Now.ToString();
            this.Repo.Proj.Save(proj);

            Assert.AreEqual(this.Proj.ID.ParentNodeID, proj.ID.ParentNodeID);
            IA0ObjectIterator iterator = this.Repo.ProjID.Read2(this.HeadComplexGuid, null);
            this.CheckChildInParent(iterator, proj.ID.GUID);
            this.Repo.Proj.Delete(proj.ID.GUID);
        }

        /// <summary>
        /// Проверяет корректность удаления проекта.
        /// </summary>
        [Test(Description = "Удаление")]
        public void Test_CreateDelete()
        {
            Guid projGuid = this.Proj.ID.GUID;

            // Удаление.
            this.Repo.Proj.Delete(projGuid);
            this.Proj = null;

            // Попытка загрузки удаленного проекта.
            try
            {
                IA0Proj loadedProj = this.Repo.Proj.Load(projGuid, false);
            }
            catch (System.Runtime.InteropServices.COMException E)
            {
                Assert.AreEqual((uint)E.HResult, 0x80004005);
            }
        }

        /// <summary>
        /// Проверяет корректность изменения данных проекта.
        /// </summary>
        [Test(Description = "Изменение/Загрузка")]
        public void Test_UpdateRead()
        {
            // Изменение.
            string newName = this.Proj.Title.Name + " Изменено";
            this.Proj.Title.Name = newName;
            this.Repo.Proj.Save(this.Proj);

            // Проверка.
            this.Proj = this.Repo.Proj.Load(this.Proj.ID.GUID, false);
            Assert.AreEqual(this.Proj.Title.Name, newName);
        }

        /// <summary>
        /// Проверяет наличие проекта в головном комплексе после создания.
        /// </summary>
        [Test(Description = "Проверка создания проекта в головном комплексе")]
        public void Test_ProjectParent()
        {
            IA0ObjectIterator iterator = this.Repo.ProjID.Read2(this.HeadComplexGuid, null);
            this.CheckChildInParent(iterator, this.Proj.ID.GUID);
        }

        /// <summary>
        /// Проверяет Guid родительского объекта созданного проекта.
        /// </summary>
        [Test(Description = "Проверка родительского узла проекта после создания")]
        public void Test_ProjIDParentAfterNew()
        {
            Assert.AreEqual(this.HeadComplexGuid, this.Proj.ID.Parent.GUID);
        }

        /// <summary>
        /// Проверяет наличие проекта в головном комплексе после загрузки.
        /// </summary>
        [Test(Description = "Проверка родительского узла проекта после загрузки")]
        public void Test_ProjIDParentAfterLoad()
        {
            this.Proj = this.Repo.Proj.Load(this.Proj.ID.GUID, false);
            Assert.AreEqual(this.HeadComplexGuid, this.Proj.ID.Parent.GUID);
        }
    }
}