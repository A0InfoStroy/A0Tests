// $Date: 2022-07-13 21:14:16 +0300 (Ср, 13 июл 2022) $
// $Revision: 598 $
// $Author: eloginov $
// Создание ОС для тестов

namespace A0Tests
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для создания тестируемой ОС.
    /// </summary>
    public abstract class NewOS : NewProj
    {
        /// <summary>
        /// Получает или устанавливает ОС.
        /// </summary>
        protected IA0OS OS { get; set; }

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
        protected IA0OS CreateOS(string name)
        {
            IA0OS os = this.Repo.OS.New(this.Proj.ID.GUID, this.Repo.ProjID.HeadNodeID);
            Assert.NotNull(os);
            os.Title.Name = name;
            this.Repo.OS.Save(os);
            return os;
        }
    }
}