// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты реквизитов

namespace A0Tests.EarlyTests.Integrate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности списка реквизитов сметного объекта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0DocEntrys",
        Author = "agalkin")]
    public class Test_IA0DocEntrys : Test_NewAct
    {
        /// <summary>
        /// Получает или устанавливает список реквизитов сметного объекта.
        /// </summary>
        public IA0DocEntrys DocEntrys { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.DocEntrys = this.A0.GetA0DocEntrys();
            Assert.NotNull(this.DocEntrys);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.DocEntrys);
            this.DocEntrys = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи реквизитов проекта.
        /// </summary>
        [Test]
        public void Test_IA0DocEntrysProj()
        {
            this.CheckA0DocEntrys(this.Proj.ID.GUID, EA0EntityKind.ekProject, EA0DocEntry.edProj);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи реквизитов объектной сметы.
        /// </summary>
        [Test]
        public void Test_IA0DocEntrysOS()
        {
            this.CheckA0DocEntrys(this.OS.ID.GUID, EA0EntityKind.ekOS, EA0DocEntry.edOS);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи реквизитов локальной сметы.
        /// </summary>
        [Test]
        public void Test_IA0DocEntrysLS()
        {
            this.CheckA0DocEntrys(this.LS.ID.GUID, EA0EntityKind.ekLS, EA0DocEntry.edLS);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи реквизитов акта.
        /// </summary>
        [Test]
        public void Test_IA0DocEntrysAct()
        {
            this.CheckA0DocEntrys(this.Act.ID.GUID, EA0EntityKind.ekAct, EA0DocEntry.edAct);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи реквизитов сметного объекта.
        /// </summary>
        /// <param name="guid">Guid сметного объекта.</param>
        /// <param name="entityKind">Тип сметного объекта.</param>
        /// <param name="docEntry">Тип реквизитов.</param>
        private void CheckA0DocEntrys(Guid guid, EA0EntityKind entityKind, EA0DocEntry docEntry)
        {
            string entryValue = "newvalue";

            // Загрузка реквизитов.
            this.DocEntrys.Load(guid, entityKind);
            Assert.NotZero(this.DocEntrys.Count);

            for (int i = 0; i < this.DocEntrys.Count; i++)
            {
                IA0DocEntry item = this.DocEntrys.Item[i];
                string name = item.Name;
                string val = item.Value;

                // Изменение значения.
                item.Value = $"{entryValue}{i}";

                // Значение по умолчанию.
                string defaultValue = item.DefaultValue;

                EA0DocEntry kind = item.Kind;
                Assert.AreEqual(docEntry, kind);

                // Количество предопределенных значений.
                int count = item.PredefinedCount;
            }

            // Сохранение после изменения.
            this.DocEntrys.Save();

            // Загрузка и проверка сохраненных изменений.
            this.DocEntrys.Load(guid, entityKind);
            Assert.NotZero(this.DocEntrys.Count);
            for (var i = 0; i < this.DocEntrys.Count; i++)
            {
                string val = this.DocEntrys.Item[i].Value;
                Assert.AreEqual($"{entryValue}{i}", val);
            }
        }
    }
}