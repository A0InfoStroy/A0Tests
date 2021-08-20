// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты каталога Комплексов

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности каталога комплексов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ComplexRepo",
        Author = "agalkin")]
    public class Test_IA0ComplexRepo : Test_NewComplex
    {
        /// <summary>
        /// Проверяет корректность создания комплекса в головном комплексе.
        /// </summary>
        [Test(Description = "Создание/Удаление")]
        public void Test_New()
        {
            IA0Complex complex = this.Repo.Complex.New();
            Assert.NotNull(complex);
            complex.Title.Name = "Интеграционные тесты " + DateTime.Now.ToString();
            this.Repo.Complex.Save(complex);
            Assert.AreEqual(this.Complex.ID.ParentNodeID, complex.ID.ParentNodeID);
            IA0ObjectIterator iterator = this.Repo.ComplexID.Read2(this.HeadComplexGuid, null);
            this.CheckChildInParent(iterator, complex.ID.GUID);
            this.Repo.Complex.Delete(complex.ID.GUID);
        }

        /// <summary>
        /// Проверяет корректность создания комплекса в комплексе с указанным Guid.
        /// </summary>
        [Test(Description = "Создание/Удаление")]
        public void Test_New2()
        {
            IA0Complex complex = this.Repo.Complex.New2(this.HeadComplexGuid, this.HeadNodeID);
            Assert.NotNull(complex);
            complex.Title.Name = "Интеграционные тесты " + DateTime.Now.ToString();
            this.Repo.Complex.Save(complex);
            Assert.AreEqual(this.Complex.ID.ParentNodeID, complex.ID.ParentNodeID);
            IA0ObjectIterator iterator = this.Repo.ComplexID.Read2(this.HeadComplexGuid, null);
            this.CheckChildInParent(iterator, complex.ID.GUID);
            this.Repo.Complex.Delete(complex.ID.GUID);
        }

        /// <summary>
        /// Проверяет корректность удаления комплекса.
        /// </summary>
        [Test(Description = "Удаление")]
        public void Test_Delete()
        {
            Guid complexGuid = this.Complex.ID.GUID;

            // Удалили
            this.Repo.Complex.Delete(complexGuid);
            this.Complex = null;

            // Пробуем загрузить ещё раз
            try
            {
                var loadedProj = this.Repo.Complex.Load(complexGuid, EAccessKind.akRead);
            }
            catch (System.Runtime.InteropServices.COMException E)
            {
                Assert.AreEqual((uint)E.HResult, 0x80004005);
            }
        }

        /// <summary>
        /// Проверяет корректность изменения данных комплекса.
        /// </summary>
        [Test(Description = "Изменение/Загрузка")]
        public void Test_CreateUpdateReadDelete()
        {
            // Изменение
            string newName = this.Complex.Title.Name + " Изменено";
            this.Complex.Title.Name = newName;
            this.Repo.Complex.Save(this.Complex);

            // Проверка
            this.Complex = this.Repo.Complex.Load(this.Complex.ID.GUID, EAccessKind.akDelete);
            Assert.AreEqual(this.Complex.Title.Name, newName);
        }

        /// <summary>
        /// Проверяет наличие комплекса в головном комплексе после создания.
        /// </summary>
        [Test(Description = "Проверка создания комплекса в головном комплексе")]
        public void TestComplexInHeadComplex()
        {
            IA0ObjectIterator iterator = this.Repo.ComplexID.Read2(this.HeadComplexGuid, null);
            this.CheckChildInParent(iterator, this.Complex.ID.GUID);
        }

        /// <summary>
        /// Проверяет Guid родительского объекта созданного комплекса.
        /// </summary>
        [Test(Description = "Проверка родительского узла проекта после создания")]
        public void Test_ComplexIDParentAfterNew()
        {
            Assert.AreEqual(this.HeadComplexGuid, this.Complex.ID.Parent.GUID);
        }

        /// <summary>
        /// Проверяет наличие комплекса в головном комплексе после загрузки.
        /// </summary>
        [Test(Description = "Проверка родительского узла проекта после загрузки")]
        public void Test_ComplexIDParentAfterLoad()
        {
            this.Complex = this.Repo.Complex.Load(this.Complex.ID.GUID, EAccessKind.akDelete);
            Assert.AreEqual(this.HeadComplexGuid, this.Complex.ID.Parent.GUID);
        }
    }
}