// $Date: 2020-12-18 13:15:50 +0300 (Пт, 18 дек 2020) $
// $Revision: 459 $
// $Author: agalkin $
// Создание проекта для тестов

namespace A0Tests.LateTests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс для создания тестируемого проекта.
    /// </summary>
    public class Test_NewProj : Test_Estimate
    {
        /// <summary>
        /// Получает или устанавливает проект.
        /// </summary>
        protected dynamic Proj { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.NotNull(this.Repo.Proj);
            Assert.NotNull(this.Repo.ProjID);
            this.Proj = this.Repo.Proj.New();
            Assert.NotNull(this.Proj);
            this.Proj.Title.Name = "Интеграционные тесты " + DateTime.Now.ToString();
            this.Repo.Proj.Save(this.Proj);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo.Proj);
            Assert.NotNull(this.Repo.ProjID);
            if (this.Proj != null)
            {
                this.Repo.Proj.Delete(this.Proj.ID.GUID);
            }

            this.Proj = null;
            base.TearDown();
        }
    }
}