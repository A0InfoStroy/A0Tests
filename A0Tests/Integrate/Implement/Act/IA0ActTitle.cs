﻿// $Date: 2020-08-05 10:48:22 +0300 (Ср, 05 авг 2020) $
// $Revision: 342 $
// $Author: agalkin $
// Тесты полей акта

namespace A0Tests.Integrate.Implement
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности заголовка акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActTitle",
        Author = "agalkin")]
    public class Test_IA0ActTitle : NewAct
    {
        /// <summary>
        /// Получает или устанавливает заголовок акта.
        /// </summary>
        protected IA0ActTitle ActTitle { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ActTitle = this.Act.Title;
            Assert.NotNull(this.ActTitle);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ActTitle);
            this.ActTitle = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.ActTitle.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.ActTitle.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи графы "За период с".
        /// </summary>
        [Test]
        public void Test_PeriodBegin()
        {
            DateTime date = this.ActTitle.PeriodBegin;
            DateTime tomorrowDate = date.AddDays(1);
            this.ActTitle.PeriodBegin = tomorrowDate;

            Assert.AreEqual(this.ActTitle.PeriodBegin, tomorrowDate);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи графы "За период по".
        /// </summary>
        [Test]
        public void Test_PeriodEnd()
        {
            DateTime date = this.ActTitle.PeriodEnd;
            DateTime tomorrowDate = date.AddDays(1);
            this.ActTitle.PeriodEnd = tomorrowDate;

            Assert.AreEqual(this.ActTitle.PeriodEnd, tomorrowDate);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи даты акта.
        /// </summary>
        [Test]
        public void Test_ActDate()
        {
            DateTime date = this.ActTitle.ActDate;
            DateTime tomorrowDate = date.AddDays(1);
            this.ActTitle.ActDate = tomorrowDate;

            Assert.AreEqual(this.ActTitle.ActDate, tomorrowDate);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи шифра.
        /// </summary>
        [Test]
        public void Test_Mark()
        {
            string mark = this.ActTitle.Mark;
            Assert.NotNull(mark);
            this.ActTitle.Mark = "Mark";
            Assert.AreEqual("Mark", this.ActTitle.Mark);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи наименования.
        /// </summary>
        [Test]
        public void Test_Name()
        {
            string name = this.ActTitle.Name;
            Assert.NotNull(name);
            this.ActTitle.Name = "Name";
            Assert.AreEqual("Name", this.ActTitle.Name);
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к точности объема строки.
        /// </summary>
        [Test]
        public void Test_VolumeScale()
        {
            int volumeScale = this.ActTitle.VolumeScale;
        }
    }
}