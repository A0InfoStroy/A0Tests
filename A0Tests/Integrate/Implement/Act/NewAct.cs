// $Date: 2020-09-21 13:30:09 +0300 (Пн, 21 сен 2020) $
// $Revision: 376 $
// $Author: agalkin $
// Создание акта для тестов

namespace A0Tests
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для создания тестируемого акта.
    /// </summary>
    public class NewAct : NewLS
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
    public class NewActString : NewAct
    {
        /// <summary>
        /// Получает или устанавливает строку акта.
        /// </summary>
        protected IA0ActString ActString { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ActString = this.Act.CreateTxtString(EA0StringKind.skMK, "basing", this.Act.Tree.Head.ID);
            this.ActString.TotalVolume = 1;
            this.ActString.Volume = 100;
            this.ImplRepo.Act.Save(this.Act);
            Assert.IsNotNull(this.ActString);
            Assert.IsTrue(this.Act.Strings.Count > 0, "В тестовой ЛС нет строк");
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            this.Act.DeleteString(this.ActString.GUID);
            base.TearDown();
        }
    }
}