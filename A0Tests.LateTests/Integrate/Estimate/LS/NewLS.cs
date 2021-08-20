// $Date: 2020-12-22 15:06:55 +0300 (Вт, 22 дек 2020) $
// $Revision: 464 $
// $Author: agalkin $
// Создание ЛС для тестов

namespace A0Tests.LateTests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для создания тестируемой ЛС.
    /// </summary>
    public class Test_NewLS : Test_NewOS
    {
        /// <summary>
        /// Получает или устанавливает ЛС.
        /// </summary>
        protected dynamic LS { get; set; }

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
        protected dynamic CreateLS(string name)
        {
            dynamic ls = this.Repo.LS.New(this.OS.ID.GUID, this.Repo.OSID.HeadNodeID);
            Assert.NotNull(ls);
            ls.Title.Name = name;
            this.Repo.LS.Save(ls);
            return ls;
        }
    }

    /// <summary>
    /// Базовый класс для создания тестируемой строки ЛС.
    /// </summary>
    public class Test_NewLSString : Test_NewLS
    {
        /// <summary>
        /// Получает или устанавливает строку ЛС.
        /// </summary>
        protected dynamic LSString { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            // Значение 3 соответствует EA0StringKind.skMK.
            this.LSString = this.LS.CreateTxtString(3, "1234", this.LS.Tree.Head.ID);
            this.LSString.TotalVolume = 1;
            this.LSString.Volume = 100.0; // Устанавливаем объём для строки (необходим для создания акта)
            this.Repo.LS.Save(this.LS);
            Assert.IsNotNull(this.LSString);
            Assert.IsTrue(this.LS.Strings.Count > 0, "В тестовой ЛС нет строк");
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LSString);
            this.LSString = null;
            base.TearDown();
        }
    }
}