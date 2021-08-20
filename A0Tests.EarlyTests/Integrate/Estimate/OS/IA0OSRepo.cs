﻿// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты каталога объектных смет

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога ОС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0OSRepo",
        Author = "agalkin")]
    public class Test_IA0OSRepo : Test_NewOS
    {
        /// <summary>
        /// Проверяет корректность удаления ОС.
        /// </summary>
        [Test(Description = "Создание/Удаление")]
        public void Test_Delete()
        {
            Guid osGuid = this.OS.ID.GUID;

            // Удаление
            this.Repo.OS.Delete(osGuid);
            this.OS = null;

            // Попытка загрузить удаленную ОС.
            try
            {
                IA0OS loadedOS = this.Repo.OS.Load(osGuid, false);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80004005);
            }
        }

        /// <summary>
        /// Проверяет корректность редактирования данных ОС.
        /// </summary>
        [Test(Description = "Изменение/Загрузка")]
        public void Test_UpdateRead()
        {
            // Изменение
            string newName = this.OS.Title.Name + " Изменено";
            this.OS.Title.Name = newName;
            this.Repo.OS.Save(this.OS);

            // Проверка
            this.OS = this.Repo.OS.Load(this.OS.ID.GUID, false);
            Assert.AreEqual(this.OS.Title.Name, newName);
        }

        /// <summary>
        /// Проверяет родительский проект ОС.
        /// </summary>
        [Test(Description = "Проверка создания ОС в проекте")]
        public void Test_OSParent()
        {
            IA0ObjectIterator iterator = this.Repo.OSID.Read(this.Proj.ID.GUID, null, null, null);
            this.CheckChildInParent(iterator, this.OS.ID.GUID);
        }

        /// <summary>
        /// Проверяет родительский проект ОС после создания.
        /// </summary>
        [Test(Description = "Проверка родительского узла ОС после создания")]
        public void Test_OSIDParentAfterNew()
        {
            Assert.AreEqual(this.Proj.ID.GUID, this.OS.ID.Parent.GUID);
        }

        /// <summary>
        /// Проверяет родительский проект ОС после загрузки.
        /// </summary>
        [Test(Description = "Проверка родительского узла ОС после загрузки")]
        public void Test_OSIDParentAfterLoad()
        {
            this.OS = this.Repo.OS.Load(this.OS.ID.GUID, false);
            Assert.AreEqual(this.Proj.ID.GUID, this.OS.ID.Parent.GUID);
        }
    }
}