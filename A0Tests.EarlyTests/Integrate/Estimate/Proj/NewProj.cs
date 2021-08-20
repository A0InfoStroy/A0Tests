// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Создание проекта для тестов

namespace A0Tests.EarlyTests
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс для создания тестируемого проекта.
    /// </summary>
    public class Test_NewProj : Test_Estimate
    {
        /// <summary>
        /// Получает или устанавливает проект.
        /// </summary>
        protected IA0Proj Proj { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.NotNull(this.Repo.Proj);
            Assert.NotNull(this.Repo.ProjID);
            this.Proj = this.Repo.Proj.New2(this.HeadComplexGuid, this.HeadNodeID);
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