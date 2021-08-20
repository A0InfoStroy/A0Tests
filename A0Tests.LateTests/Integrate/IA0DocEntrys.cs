// $Date: 2020-12-31 13:30:35 +0300 (Чт, 31 дек 2020) $
// $Revision: 484 $
// $Author: agalkin $
// Тесты реквизитов

namespace A0Tests.LateTests.Integrate
{
    using System;
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
        public dynamic DocEntrys { get; private set; }

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
            // 1 - EA0EntityKind.ekProject, 0 - EA0DocEntry.edProj.
            this.CheckA0DocEntrys(this.Proj.ID.GUID, 1, 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи реквизитов объектной сметы.
        /// </summary>
        [Test]
        public void Test_IA0DocEntrysOS()
        {
            // 2 - EA0EntityKind.ekOS, 1 - EA0DocEntry.edOS.
            this.CheckA0DocEntrys(this.OS.ID.GUID, 2, 1);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи реквизитов локальной сметы.
        /// </summary>
        [Test]
        public void Test_IA0DocEntrysLS()
        {
            // 3 - EA0EntityKind.ekLS, 2 - EA0DocEntry.edLS.
            this.CheckA0DocEntrys(this.LS.ID.GUID, 3, 2);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи реквизитов акта.
        /// </summary>
        [Test]
        public void Test_IA0DocEntrysAct()
        {
            // 4 - EA0EntityKind.ekAct, 3 - EA0DocEntry.edAct.
            this.CheckA0DocEntrys(this.Act.ID.GUID, 4, 3);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи реквизитов сметного объекта.
        /// </summary>
        /// <param name="guid">Guid сметного объекта.</param>
        /// <param name="entityKind">Тип сметного объекта.</param>
        /// <param name="docEntry">Тип реквизитов.</param>
        private void CheckA0DocEntrys(Guid guid, int entityKind, int docEntry)
        {
            string entryValue = "newvalue";

            // Загрузка реквизитов.
            this.DocEntrys.Load(guid, entityKind);
            Assert.NotZero(this.DocEntrys.Count);

            for (int i = 0; i < this.DocEntrys.Count; i++)
            {
                dynamic item = this.DocEntrys.Item[i];
                string name = item.Name;
                string val = item.Value;

                // Изменение значения.
                item.Value = $"{entryValue}{i}";

                // Значение по умолчанию.
                string defaultValue = item.DefaultValue;

                dynamic kind = item.Kind;
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