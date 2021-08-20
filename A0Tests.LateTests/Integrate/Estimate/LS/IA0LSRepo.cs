// $Date: 2020-12-22 15:00:12 +0300 (Вт, 22 дек 2020) $
// $Revision: 463 $
// $Author: agalkin $
// Тесты каталога локальных смет

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности каталога локальных смет.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSRepo",
        Author = "agalkin")]
    public class Test_IA0LSRepo : Test_NewLS
    {
        /// <summary>
        /// Проверяет корректность удаления ЛС.
        /// </summary>
        [Test(Description = "Удаление")]
        public void Test_CreateDelete()
        {
            Guid lsGuid = this.LS.ID.GUID;
            this.Repo.LS.Delete(lsGuid);
            this.LS = null;

            // Попытка загрузить после удаления.
            try
            {
                dynamic loadedLS = this.Repo.LS.Load2(lsGuid);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80004005);
            }
        }

        /// <summary>
        /// Проверяет корректность редактирования данных ЛС.
        /// </summary>
        [Test(Description = "Изменение/Загрузка")]
        public void Test_CreateUpdateReadDelete()
        {
            // Изменение
            string newName = this.LS.Title.Name + " Изменено";
            this.LS.Title.Name = newName;
            this.Repo.LS.Save(this.LS);

            // Проверка
            this.LS = this.Repo.LS.Load2(this.LS.ID.GUID);
            Assert.AreEqual(this.LS.Title.Name, newName);
        }

        /// <summary>
        /// Проверяет родительский проект ЛС.
        /// </summary>
        [Test(Description = "Проверка создания ЛС в проекте")]
        public void Test_LsInProject()
        {
            dynamic iterator = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, null);
            this.CheckChildInParent(iterator, this.LS.ID.GUID);
        }

        /// <summary>
        /// Проверяет родительскую объектную смету ЛС после создания.
        /// </summary>
        [Test(Description = "Проверка родительского узла ЛС после создания")]
        public void Test_LSIDParentAfterNew()
        {
            Assert.AreEqual(this.OS.ID.GUID, this.LS.ID.Parent.GUID);
        }

        /// <summary>
        /// Проверяет родительскую объектную смету ЛС после загрузки.
        /// </summary>
        [Test(Description = "Проверка родительского узла ОС после загрузки")]
        public void Test_OSIDParentAfterLoad()
        {
            this.LS = this.Repo.LS.Load2(this.LS.ID.GUID);
            Assert.AreEqual(this.OS.ID.GUID, this.LS.ID.Parent.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода загрузки ЛС по Id.
        /// </summary>
        [Test(Description = "Проверка загрузки ЛС по ID")]
        public void Test_LoadByID()
        {
            dynamic ls = this.Repo.LS.Load(this.LS.ID);
            Assert.AreEqual(this.LS.ID.GUID, ls.ID.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода копирования ЛС.
        /// </summary>
        [Test(Description = "Проверка копирования ЛС")]
        public void Test_Copy()
        {
            dynamic lsCopy = this.Repo.LS.Copy(this.LS.ID.GUID, this.OS.ID.GUID);
            this.Repo.LS.Save(lsCopy);
            Assert.AreEqual(this.LS.Title.Name, lsCopy.Title.Name);
            this.Repo.LS.Delete(lsCopy.ID.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода перемещения ЛС.
        /// </summary>
        [Test(Description = "Проверка перемещения ЛС")]
        public void Test_Move()
        {
            // Создание ОС в которую будет произведено перемещение.
            dynamic targetOS = this.Repo.OS.New(this.Proj.ID.GUID, this.Repo.ProjID.HeadNodeID);
            this.Repo.OS.Save(targetOS);

            // Создание ЛС для перемещения.
            dynamic ls = this.Repo.LS.New(this.OS.ID.GUID, this.Repo.OSID.HeadNodeID);
            this.Repo.LS.Save(ls);

            // Перемещение ЛС из первой ОС во вторую.
            dynamic reIdent = this.Repo.LS.Move(ls.ID.GUID, targetOS.ID.GUID, this.Repo.OSID.HeadNodeID);
            this.Repo.LS.Save(ls);
            Assert.NotNull(reIdent);
            int count = reIdent.Count;
            for (int i = 0; i < count; i++)
            {
                string n = reIdent.New[i];
                Assert.NotNull(n);
                string o = reIdent.Old[i];
                Assert.NotNull(o);
                string old = reIdent.OldByNew(o);
                Assert.NotNull(old);
                Assert.AreEqual(n, old);
            }

            // Проверка наличия перемещенной ЛС.
            dynamic iter = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, null);
            bool check = false;
            while (iter.Next())
            {
                dynamic lsid = iter.Item.ID;
                if (lsid.Parent.GUID == targetOS.ID.GUID)
                {
                    check = true;
                }
            }

            this.Repo.OS.Delete(targetOS.ID.GUID);
            Assert.True(check);
        }
    }
}