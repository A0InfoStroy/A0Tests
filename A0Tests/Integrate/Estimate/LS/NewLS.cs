// $Date: 2022-07-13 20:54:50 +0300 (Ср, 13 июл 2022) $
// $Revision: 585 $
// $Author: eloginov $
// Создание ЛС для тестов

namespace A0Tests
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для создания тестируемой ЛС.
    /// </summary>
    public abstract class NewLS : NewOS
    {
        /// <summary>
        /// Получает или устанавливает ЛС.
        /// </summary>
        protected IA0LS LS { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.NotNull(this.Repo.LS);
            Assert.NotNull(this.Repo.LSID);

            // Создание ЛС для тестов.
            this.LS = this.CreateLS("Интеграционные тесты " + DateTime.Now.ToString());
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo.LS);
            Assert.NotNull(this.Repo.LSID);

            // Удаление ЛС просле прохождения тестов.
            if (this.LS != null)
            {
                this.Repo.LS.Delete(this.LS.ID.GUID);
            }

            base.TearDown();
        }

        /// <summary>
        /// Создает локальную смету.
        /// </summary>
        /// <param name="name">Наименование ЛС.</param>
        /// <returns>Ссылка на ЛС.</returns>
        protected IA0LS CreateLS(string name)
        {
            IA0LS ls = this.Repo.LS.New(this.OS.ID.GUID, this.Repo.OSID.HeadNodeID);
            Assert.NotNull(ls);
            ls.Title.Name = name;
            this.Repo.LS.Save(ls);
            return ls;
        }
    }

    /// <summary>
    /// Базовый класс для создания абстрактной тестируемой строки ЛС.
    /// </summary>
    public abstract class NewLSStringBase : NewLS
    {
        /// <summary>
        /// Получает или устанавливает строку ЛС.
        /// </summary>
        protected IA0LSString LSString { get; set; }

        /// <summary>
        /// Создание текстовой строки ЛС.
        /// </summary>
        protected void CreateTxtLSString()
        {
            int stringsCount = this.LS.Strings.Count;
            LSString = this.LS.CreateTxtString(EA0StringKind.skMK, "1234", this.LS.Tree.Head.ID);
            LSString.TotalVolume = 1;
            LSString.Volume = 100.0; // Устанавливаем объём для строки (необходим для создания акта)
            this.Repo.LS.Save(this.LS);
            Assert.IsNotNull(LSString);
            Assert.IsTrue(this.LS.Strings.Count == stringsCount + 1, "В тестовой ЛС количество строк не изменилось");
        }

        /// <summary>
        /// Создание строки работы ЛС.
        /// </summary>
        protected void CreateWorkLSString()
        {
            int stringsCount = this.LS.Strings.Count;

            // Пример расценки из НСИ.
            // БД: a0NSI_TER_01. Таблица: Works.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Деревообрабатывающее оборудование - 2553
            // ОБОРУДОВАНИЕ ОБЩЕГО НАЗНАЧЕНИЯ. ОБОРУДОВАНИЕ ЛЕСОПИЛЬНОГО ПРОИЗВОДСТВА. РАМА ЛЕСОПИЛЬНАЯ ОДНОЭТАЖНАЯ, МАССА 4,5Т - 787669 - 787668

            LSString = this.LS.CreateWorkString(aNSIID: 7, aFolderID: 2553, aWorkID: 787669, aNodeID: this.LS.Tree.Head.ID);
            LSString.TotalVolume = 1;
            LSString.Volume = 100.0; // Устанавливаем объём для строки (необходим для создания акта)
            this.Repo.LS.Save(this.LS);
            Assert.NotNull(LSString);
            Assert.IsTrue(this.LS.Strings.Count == stringsCount + 1, "В тестовой ЛС количество строк не изменилось");
        }

        /// <summary>
        /// Создание строки ресураса ЛС.
        /// </summary>
        protected void CreateResLSString()
        {
            int stringsCount = this.LS.Strings.Count;

            // Пример расценки из НСИ. 
            // БД: a0NSI_TER_01. Таблица: Resource.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Перевозка грузов для строительства - 907
            // АСФАЛЬТОБЕТОН, РАСТВОРЫ, БЕТОН ТОВАРНЫЙ-ПОГРУЗКА - 6197091

            LSString = this.LS.CreateResString(aNSIID: 7, aFolderID: 907, aResID: 6197091, aNodeID: this.LS.Tree.Head.ID);
            LSString.TotalVolume = 1;
            LSString.Volume = 100.0; // Устанавливаем объём для строки (необходим для создания акта)
            this.Repo.LS.Save(this.LS);
            Assert.NotNull(LSString);
            Assert.IsTrue(this.LS.Strings.Count == stringsCount + 1, "В тестовой ЛС количество строк не изменилось");

        }
    }
}