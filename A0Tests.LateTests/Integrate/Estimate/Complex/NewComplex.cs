// $Date: 2020-12-17 14:34:16 +0300 (Чт, 17 дек 2020) $
// $Revision: 457 $
// $Author: agalkin $
// Создание комплекса для тестов

namespace A0Tests.LateTests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс для создания тестового комплекса.
    /// </summary>
    public class Test_NewComplex : Test_Estimate
    {
        /// <summary>
        /// Получает или устанавливает комплекс.
        /// </summary>
        protected dynamic Complex { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.NotNull(this.Repo.Complex);
            Assert.NotNull(this.Repo.ComplexID);

            // Создание комплекса для тестов.
            this.Complex = this.Repo.Complex.New();
            Assert.NotNull(this.Complex);

            this.Complex.Title.Name = "Интеграционные тесты " + DateTime.Now.ToString();
            this.Repo.Complex.Save(this.Complex);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo.Complex);
            Assert.NotNull(this.Repo.ComplexID);
            if (this.Complex != null)
            {
                // Удаление комплекса.
                this.Repo.Complex.Delete(this.Complex.ID.GUID);
            }

            this.Complex = null;
            base.TearDown();
        }
    }
}