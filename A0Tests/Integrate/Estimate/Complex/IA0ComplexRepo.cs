// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты каталога Комплексов

namespace A0Tests.Integrate.Estimate
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
    public class Test_IA0ComplexRepo : NewComplex
    {
        /// <summary>
        /// Проверяет корректность создания комплекса в головном комплексе.
        /// </summary>
        [Test(Description = "Создание/Удаление"), Timeout(20000)]
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
        /// Проверяет корректность создания комплекса.
        /// </summary>
        [Test(Description = "Создание/Удаление"), Timeout(20000)]
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
        [Test(Description = "Удаление"), Timeout(20000)]
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
        [Test(Description = "Изменение/Загрузка"), Timeout(20000)]
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
        [Test(Description = "Проверка создания комплекса в головном комплексе"), Timeout(20000)]
        public void TestComplexInHeadComplex()
        {
            IA0ObjectIterator iterator = this.Repo.ComplexID.Read2(this.HeadComplexGuid, null);
            this.CheckChildInParent(iterator, this.Complex.ID.GUID);
        }

        /// <summary>
        /// Проверяет Guid родительского объекта созданного комплекса.
        /// </summary>
        [Test(Description = "Проверка родительского узла проекта после создания"), Timeout(20000)]
        public void Test_ComplexIDParentAfterNew()
        {
            Assert.AreEqual(this.HeadComplexGuid, this.Complex.ID.Parent.GUID);
        }

        /// <summary>
        /// Проверяет наличие комплекса в головном комплексе после загрузки.
        /// </summary>
        [Test(Description = "Проверка родительского узла проекта после загрузки"), Timeout(5000)]
        public void Test_ComplexIDParentAfterLoad()
        {
            this.Complex = this.Repo.Complex.Load(this.Complex.ID.GUID, EAccessKind.akDelete);
            Assert.AreEqual(this.HeadComplexGuid, this.Complex.ID.Parent.GUID);
        }
    }
}