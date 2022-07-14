// $Date: 2020-12-18 16:57:27 +0300 (Пт, 18 дек 2020) $
// $Revision: 462 $
// $Author: agalkin $
// Создание ОС для тестов

namespace A0Tests.LateTests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для создания тестируемой ОС.
    /// </summary>
    public class Test_NewOS : Test_NewProj
    {
        /// <summary>
        /// Получает или устанавливает ОС.
        /// </summary>
        protected dynamic OS { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.NotNull(this.Repo.OS);
            Assert.NotNull(this.Repo.OSID);
            this.OS = this.CreateOS("Интеграционные тесты " + DateTime.Now.ToString());
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo.OS);
            Assert.NotNull(this.Repo.OSID);
            if (this.OS != null)
            {
                this.Repo.OS.Delete(this.OS.ID.GUID);
            }

            base.TearDown();
        }

        /// <summary>
        /// Создает объектную смету.
        /// </summary>
        /// <param name="name">Наименование ОС.</param>
        /// <returns>Ссылка на ОС.</returns>
        protected dynamic CreateOS(string name)
        {
            dynamic os = this.Repo.OS.New(this.Proj.ID.GUID, this.Repo.ProjID.HeadNodeID);
            Assert.NotNull(os);
            os.Title.Name = name;
            this.Repo.OS.Save(os);
            return os;
        }
    }
}