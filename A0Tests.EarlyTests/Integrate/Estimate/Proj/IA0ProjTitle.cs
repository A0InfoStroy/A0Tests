﻿// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты полей заголовка проекта

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности заголовка проекта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ProjTitle",
        Author = "agalkin")]
    public class Test_IA0ProjTitle : Test_NewProj
    {
        /// <summary>
        /// Получает или устанавливает значение Test_IA0ProjTitle.
        /// </summary>
        protected IA0ProjTitle Title { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Title = this.Proj.Title;
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
        /// Проверяет отсутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.Title.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.Title.Attr["LGM.MainMetric"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Утверждаю" в заголовке.
        /// </summary>
        [Test]
        public void Test_Approval()
        {
            IA0TitleSign approval = this.Title.Approval;
            Assert.NotNull(approval);

            // Должность
            string job = approval.Job;

            // Ф.И.О
            string name = approval.Name;
        }

        /// <summary>
        /// Проверяет работоспособность получения и редактирования даты начала проекта.
        /// </summary>
        [Test]
        public void Test_BeginDate()
        {
            DateTime beginDate = this.Title.BeginDate;
            DateTime newBeginDate = beginDate.AddDays(1.0);
            this.Title.BeginDate = newBeginDate;
            Assert.AreEqual(newBeginDate, this.Title.BeginDate);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду строительства проекта.
        /// </summary>
        [Test]
        public void Test_BuildKindStr()
        {
            string buildKind = this.Title.BuildKindStr;
            Assert.NotNull(buildKind);
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
                if (stages.Item[i].Kind == EA0ObjectKind.okProject)
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
                if (stages.Item[i].Kind == EA0ObjectKind.okProject)
                {
                    id = stages.Item[i].ID;
                    break;
                }
            }

            Assert.AreEqual(id, busOpId);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к площадке комплекса.
        /// </summary>
        [Test]
        public void Test_City()
        {
            string city = this.Title.City;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при чтении и изменении комментария заголовка проекта.
        /// </summary>
        [Test]
        public void Test_Comment()
        {
            this.Title.Comment = "comment";
            string comment = this.Title.Comment;
            Assert.NotNull(comment);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к значениям подписей "Согласовано" в заголовке.
        /// </summary>
        [Test]
        public void Test_Conform()
        {
            IA0TitleSign conform = this.Title.Conform;
            Assert.NotNull(conform);

            // Должность
            string job = conform.Job;

            // Ф.И.О
            string name = conform.Name;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к дате создания проекта.
        /// </summary>
        [Test]
        public void Test_CreateMoment()
        {
            DateTime createMoment = this.Title.CreateMoment;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к значениям подписей "Заказчик" в заголовке.
        /// </summary>
        [Test]
        public void Test_Customer()
        {
            IA0TitleSign customer = this.Title.Customer;
            Assert.NotNull(customer);

            // Должность
            string job = customer.Job;

            // Ф.И.О
            string name = customer.Name;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к значению третьей строки подписей "Проектант" в заголовке.
        /// </summary>
        [Test]
        public void Test_DepartmHead()
        {
            IA0TitleSign departmHead = this.Title.DepartmHead;
            Assert.NotNull(departmHead);

            // Должность
            string job = departmHead.Job;

            // Ф.И.О
            string name = departmHead.Name;
        }

        /// <summary>
        /// Проверяет работоспособность получения и редактирования даты конца проекта.
        /// </summary>
        [Test]
        public void Test_EndDate()
        {
            DateTime endDate = this.Title.EndDate;
            DateTime newEndDate = endDate.AddDays(1.0);
            this.Title.EndDate = newEndDate;
            Assert.AreEqual(newEndDate, this.Title.EndDate);
        }

        /// <summary>
        /// Проверяет работоспособность получения значения графы "Финансир." заголовка.
        /// </summary>
        [Test]
        public void Test_Finance()
        {
            string finance = this.Title.Finance;
            Assert.NotNull(finance);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к статусу "Включить в итоги/Исключить из итогов".
        /// </summary>
        [Test]
        public void Test_IncludeInTotals()
        {
            bool include = this.Title.IncludeInTotals;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к значению "Составлен в ценах по состоянию на" заголовка.
        /// </summary>
        [Test]
        public void Test_InPrice()
        {
            int inPrice = this.Title.InPrice;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к значению "Зона КСЦ" заголовка.
        /// </summary>
        [Test]
        public void Test_KSCZone()
        {
            int kscZone = this.Title.KSCZone;
        }

        /// <summary>
        /// Проверяет работоспособность получения и редактирования шифра проекта.
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
        /// Проверяет работоспособность получения и редактирования наименования проекта.
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
        /// Проверяет отсутствие ошибок при обращении к значению второй строки подписей "Проектант" в заголовке.
        /// </summary>
        [Test]
        public void Test_ProjectEng()
        {
            IA0TitleSign projectEng = this.Title.ProjectEng;
            Assert.NotNull(projectEng);

            // Должность
            string job = projectEng.Job;

            // Ф.И.О
            string name = projectEng.Name;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к значению первой строки подписей "Проектант" в заголовке.
        /// </summary>
        [Test]
        public void Test_ProjectHead()
        {
            IA0TitleSign projectHead = this.Title.ProjectHead;
            Assert.NotNull(projectHead);

            // Должность
            string job = projectHead.Job;

            // Ф.И.О
            string name = projectHead.Name;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подразделению в заголовке проекта.
        /// </summary>
        [Test]
        public void Test_Subdivision()
        {
            string subDivision = this.Title.Subdivision;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подрайону в заголовке проекта.
        /// </summary>
        [Test]
        public void Test_SubRegStr()
        {
            string subRegStr = this.Title.SubRegStr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к району в заголовке проекта.
        /// </summary>
        [Test]
        public void Test_TerrRegStr()
        {
            string terrRegStr = this.Title.TerrRegStr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к дате модификации комплекса.
        /// </summary>
        [Test]
        public void Test_UpdateMoment()
        {
            DateTime updateMoment = this.Title.UpdateMoment;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к значениям подписей "Инвестор" в заголовке.
        /// </summary>
        [Test]
        public void Test_VeryRichSponsor()
        {
            IA0TitleSign veryRichSponsor = this.Title.VeryRichSponsor;
            Assert.NotNull(veryRichSponsor);

            // Должность
            string job = veryRichSponsor.Job;

            // Ф.И.О
            string name = veryRichSponsor.Name;
        }
    }
}