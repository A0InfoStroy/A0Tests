﻿// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты полей ЛС

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности заголовка ЛC.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSTitle",
        Author = "agalkin")]
    public class Test_IA0LSTitle : Test_NewLS
    {
        /// <summary>
        /// Получает или устанавливает заголовок ЛС.
        /// </summary>
        protected IA0LSTitle Title { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Title = this.LS.Title;
            Assert.NotNull(this.Title);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Title);
            this.Title = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.Title.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.Title.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет работоспособность получения наименования бизнес этапа.
        /// </summary>
        [Test]
        public void Test_BusOp()
        {
            string busOpName = this.Title.BusOp;
            Assert.NotNull(busOpName);
            IA0BussinessStages stages = this.A0.Sys.Repo.BussinnessStages;
            string name = null;
            for (int i = 0; i < stages.Count; i++)
            {
                if (stages.Item[i].Kind == EA0ObjectKind.okLS)
                {
                    name = stages.Item[i].Name;
                    break;
                }
            }

            Assert.AreEqual(name, busOpName);
        }

        /// <summary>
        ///  Проверяет работоспособность получения Id бизнес этапа.
        /// </summary>
        [Test]
        public void Test_BusOpId()
        {
            int busOpId = this.Title.BusOpID;
            IA0BussinessStages stages = this.A0.Sys.Repo.BussinnessStages;
            int id = -1;
            for (int i = 0; i < stages.Count; i++)
            {
                if (stages.Item[i].Kind == EA0ObjectKind.okLS)
                {
                    id = stages.Item[i].ID;
                    break;
                }
            }

            Assert.AreEqual(id, busOpId);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи шифра.
        /// </summary>
        [Test]
        public void Test_Mark()
        {
            string mark = this.Title.Mark;
            Assert.NotNull(mark);
            this.Title.Mark = "Mark";
            Assert.AreEqual("Mark", this.Title.Mark);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи наименования.
        /// </summary>
        [Test]
        public void Test_Name()
        {
            string name = this.Title.Name;
            Assert.NotNull(name);
            this.Title.Name = "Name";
            Assert.AreEqual("Name", this.Title.Name);
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к точности объема строки.
        /// </summary>
        [Test]
        public void Test_VolumeScale()
        {
            decimal volumeScale = this.Title.VolumeScale;
        }
    }
}