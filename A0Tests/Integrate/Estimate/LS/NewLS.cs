// $Date: 2020-09-21 13:33:22 +0300 (Пн, 21 сен 2020) $
// $Revision: 377 $
// $Author: agalkin $
// Создание ЛС для тестов

namespace A0Tests
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Базовый класс для создания тестируемой ЛС.
    /// </summary>
    public class NewLS : NewOS
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
    /// Базовый класс для создания тестируемой строки ЛС.
    /// </summary>
    public class NewLSString : NewLS
    {
        /// <summary>
        /// Получает или устанавливает строку ЛС.
        /// </summary>
        protected IA0LSString LSString { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LSString = this.LS.CreateTxtString(EA0StringKind.skWork, "1234", this.LS.Tree.Head.ID);
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