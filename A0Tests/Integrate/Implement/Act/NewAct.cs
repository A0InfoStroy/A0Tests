// $Date: 2022-07-13 21:06:16 +0300 (Ср, 13 июл 2022) $
// $Revision: 591 $
// $Author: eloginov $
// Создание акта для тестов

namespace A0Tests
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для создания тестируемого акта.
    /// </summary>
    public abstract class NewAct : NewLS
    {
        /// <summary>
        /// Получает или устанавливает акт.
        /// </summary>
        protected IA0Act Act { get; set; }

        /// <summary>
        /// Получает или устанавливает каталог Implement.
        /// </summary>
        protected IA0ImplementRepo ImplRepo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ImplRepo = this.A0.Implement.Repo;
            Assert.NotNull(this.ImplRepo.Act);
            Assert.NotNull(this.ImplRepo.ActID);
            this.Act = this.CreateAct("Интеграционные тесты " + DateTime.Now.ToString());
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ImplRepo.Act);
            Assert.NotNull(this.ImplRepo.ActID);
            if (this.Act != null)
            {
                this.ImplRepo.Act.UnLock(this.Act.ID.GUID);
                this.ImplRepo.Act.Delete(this.Act.ID.GUID);
                this.Act = null;
            }

            base.TearDown();
        }

        /// <summary>
        /// Создает акт.
        /// </summary>
        /// <param name="name">Наименование акта.</param>
        /// <returns>Ссылка на акт.</returns>
        protected IA0Act CreateAct(string name)
        {
            IA0Act act = this.ImplRepo.Act.New(this.LS.ID.GUID, 0, 0, 100);
            Assert.NotNull(act);
            act.Title.Name = name;
            this.ImplRepo.Act.Save(act);
            return act;
        }
    }

    /// <summary>
    /// Базовый класс для создания тестируемой строки акта.
    /// </summary>
    public abstract class NewActStringBase : NewAct
    {
        /// <summary>
        /// Получает или устанавливает строку акта.
        /// </summary>
        protected IA0ActString ActString { get;  set; }

        /// <summary>
        /// Создание текстовой строки акта.
        /// </summary>
        protected void CreateTxtActString()
        {
            int stringsCount = this.Act.Strings.Count;
            this.ActString = this.Act.CreateTxtString(EA0StringKind.skMK, "basing", this.Act.Tree.Head.ID);
            this.ActString.TotalVolume = 1;
            this.ActString.Volume = 100;
            this.ImplRepo.Act.Save(this.Act);
            Assert.IsNotNull(this.ActString);
            Assert.IsTrue(this.Act.Strings.Count == stringsCount + 1, "В тестовом акте количество строк не изменилось");
        }

        /// <summary>
        /// Создание строки работы акта.
        /// </summary>
        protected void CreateWorkActString()
        {
            int stringsCount = this.Act.Strings.Count;

            // Расценка работа.
            // БД: a0NSI_TER_01. Таблица: Works.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Деревообрабатывающее оборудование - 2553
            // ОБОРУДОВАНИЕ ОБЩЕГО НАЗНАЧЕНИЯ. ОБОРУДОВАНИЕ ЛЕСОПИЛЬНОГО ПРОИЗВОДСТВА. РАМА ЛЕСОПИЛЬНАЯ ОДНОЭТАЖНАЯ, МАССА 4,5Т - 787669 - 787668

            this.ActString = this.Act.CreateWorkString(aNSIID: 7, aFolderID: 2553, aWorkID: 787669, aNodeID: this.LS.Tree.Head.ID);
            this.ActString.TotalVolume = 1;
            this.ActString.Volume = 100;
            this.ImplRepo.Act.Save(this.Act);
            Assert.IsNotNull(this.ActString);
            Assert.IsTrue(this.Act.Strings.Count == stringsCount + 1);
        }

        /// <summary>
        /// Создание строки русурса акта.
        /// </summary>
        protected void CreateResActString()
        {
            int stringsCount = this.Act.Strings.Count;

            // Пример расценки из НСИ. 
            // БД: a0NSI_TER_01. Таблица: Resource.
            // Получать надо из System.NSI
            // ТЕР-01 - 7
            // Оборудование лифтов - 924
            // ЛИФТЫ ПАССАЖИРСКИЕ Г/П 400 КГ - 262300

            this.ActString = this.Act.CreateResString(aNSIID: 7, aFolderID: 924, aResID: 262300, aNodeID: this.LS.Tree.Head.ID);
            this.ActString.TotalVolume = 1;
            this.ActString.Volume = 100;
            this.ImplRepo.Act.Save(this.Act);
            Assert.IsNotNull(this.ActString);
            Assert.IsTrue(this.Act.Strings.Count == stringsCount + 1);
        }

    }
}