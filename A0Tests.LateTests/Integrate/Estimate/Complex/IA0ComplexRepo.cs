// $Date: 2020-12-17 14:34:16 +0300 (Чт, 17 дек 2020) $
// $Revision: 457 $
// $Author: agalkin $
// Тесты каталога Комплексов

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
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
            dynamic complex = this.Repo.Complex.New();
            Assert.NotNull(complex);
            complex.Title.Name = "Интеграционные тесты " + DateTime.Now.ToString();
            this.Repo.Complex.Save(complex);
            Assert.AreEqual(this.Complex.ID.ParentNodeID, complex.ID.ParentNodeID);
            dynamic iterator = this.Repo.ComplexID.Read2(this.HeadComplexGuid, null);
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
                // Значение 0 соответствует EAccessKind.akRead.
                var loadedProj = this.Repo.Complex.Load(complexGuid, 0);
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
            // Значение 2 соответствует EAccessKind.akDelete.
            this.Complex = this.Repo.Complex.Load(this.Complex.ID.GUID, 2);
            Assert.AreEqual(this.Complex.Title.Name, newName);
        }

        /// <summary>
        /// Проверяет наличие комплекса в головном комплексе после создания.
        /// </summary>
        [Test(Description = "Проверка создания комплекса в головном комплексе")]
        public void TestComplexInHeadComplex()
        {
            dynamic iterator = this.Repo.ComplexID.Read2(this.HeadComplexGuid, null);
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
            // Значение 2 соответствует EAccessKind.akDelete.
            this.Complex = this.Repo.Complex.Load(this.Complex.ID.GUID, 2);
            Assert.AreEqual(this.HeadComplexGuid, this.Complex.ID.Parent.GUID);
        }
    }
}