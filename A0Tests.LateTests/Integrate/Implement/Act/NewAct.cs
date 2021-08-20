// $Date: 2020-12-29 14:24:46 +0300 (Вт, 29 дек 2020) $
// $Revision: 481 $
// $Author: agalkin $
// Создание акта для тестов

namespace A0Tests.LateTests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для создания тестируемого акта.
    /// </summary>
    public class Test_NewAct : Test_NewLS
    {
        /// <summary>
        /// Получает или устанавливает акт.
        /// </summary>
        protected dynamic Act { get; set; }

        /// <summary>
        /// Получает или устанавливает каталог Implement.
        /// </summary>
        protected dynamic ImplRepo { get; private set; }

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
        protected dynamic CreateAct(string name)
        {
            dynamic act = this.ImplRepo.Act.New(this.LS.ID.GUID, 0, 0, 100);
            Assert.NotNull(act);
            act.Title.Name = name;
            this.ImplRepo.Act.Save(act);
            return act;
        }
    }

    /// <summary>
    /// Базовый класс для создания тестируемой строки акта.
    /// </summary>
    public class Test_NewActString : Test_NewAct
    {
        /// <summary>
        /// Получает или устанавливает строку акта.
        /// </summary>
        protected dynamic ActString { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            // Значение 3 соответствует EA0StringKind.skMK.
            this.ActString = this.Act.CreateTxtString(3, "basing", this.Act.Tree.Head.ID);
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