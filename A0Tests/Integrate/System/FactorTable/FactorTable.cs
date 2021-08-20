// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты таблиц коэффициентов

namespace A0Tests.Integrate.Sys
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности таблиц коэффициентов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0SysFactorTableRepo",
        Author = "agalkin")]
    public class Test_IA0SysFactorTableRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает каталог таблиц коэффициентов.
        /// </summary>
        protected IA0SysFactorTableRepo Repo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.Sys.FactorTable.Repo;
            Assert.NotNull(this.Repo);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo);
            this.Repo = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность чтения таблицы из каталога по Id.
        /// </summary>
        [Test(Description = "Загрузка таблицы"), Timeout(10000)]
        public void Test_Read()
        {
            IA0SysFactorTable factorTable = this.Repo.Read(this.GetID());

            Assert.NotNull(factorTable);
        }

        /// <summary>
        /// Получает Id таблицы для проверки загрузки.
        /// </summary>
        /// <returns>Id таблицы.</returns>
        protected virtual int GetID() => 1305;
    }

    /// <summary>
    /// Содержит тесты проверки работоспособности таблиц коэффициентов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0SysFactorTable",
        Author = "vbutov")]
    public class Test_IA0SysFactorTable : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает таблицу коэффициентов.
        /// </summary>
        protected IA0SysFactorTable FactorTable { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            IA0SysFactorTableRepo repo = this.A0.Sys.FactorTable.Repo;
            this.FactorTable = repo.Read(this.GetID());
            Assert.NotNull(this.FactorTable);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.FactorTable);
            this.FactorTable = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям таблицы.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Title()
        {
            IA0SysFactorTableTitle title = this.FactorTable.Title;
            Assert.NotNull(title);
            DateTime createMoment = title.CreateMoment;
            string description = title.Description;
            EA0SysFactorTableKind destination = title.Destination;
            string guideLines = title.GuideLines;
            int id = title.ID;
            string mark = title.Mark;
            DateTime modifyMoment = title.ModifyMoment;
            string name = title.Name;
            string organization = title.Organization;
            string ownerName = title.OwnerName;
            string place = title.Place;
            string subdivision = title.Subdivision;
            string version = title.Version;
        }

        /// <summary>
        /// Проверяет работоспособность чтения полей столбцов таблицы.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Columns()
        {
            IA0SysFactorTableColumns columns = this.FactorTable.Columns;
            Assert.NotNull(columns);
            Assert.IsTrue(columns.Count > 0);
            for (int i = 0; i < columns.Count; ++i)
            {
                IA0SysFactorTableColumn item = columns.Item[i];
                Assert.NotNull(item);
                Assert.IsTrue(item.Name.Length > 0);
            }
        }

        /// <summary>
        /// Проверяет работоспособность чтения полей строчек таблицы.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Rows()
        {
            IA0SysFactorTableRows rows = this.FactorTable.Rows;
            Assert.NotNull(rows);
            IA0SysFactorTableColumns columns = this.FactorTable.Columns;
            Assert.NotNull(columns);

            Assert.IsTrue(rows.Count > 0);
            for (int i = 0; i < rows.Count; ++i)
            {
                IA0SysFactorTableRow item = rows.Item[i];
                for (int j = 0; j < columns.Count; ++j)
                {
                    dynamic value = item.Value[j];
                    Assert.NotNull(value);
                }
            }
        }

        /// <summary>
        /// Получает Id таблицы для проверки загрузки.
        /// </summary>
        /// <returns>Id таблицы.</returns>
        protected virtual int GetID() => 1305;
    }
}