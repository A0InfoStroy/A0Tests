// $Date: 2021-02-17 11:56:30 +0300 (Ср, 17 фев 2021) $
// $Revision: 523 $
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
        /// Проверяет отсутствие ошибок при обращении к подписям "Утверждаю" в заголовке.
        /// </summary>
        [Test]
        public void Test_Approval()
        {
            IA0TitleSign approval = this.ActTitle.Approval;
            Assert.NotNull(approval);

            // Должность
            string job = "Job";
            approval.Job = job;
            Assert.AreEqual(job, approval.Job);

            // Ф.И.О
            string name = "Name";
            approval.Name = name;
            Assert.AreEqual(name, approval.Name);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Проверил" в заголовке.
        /// </summary>
        [Test]
        public void Test_Auditor()
        {
            IA0TitleSign audihor = this.ActTitle.Auditor;
            Assert.NotNull(audihor);

            // Должность
            string job = "Job";
            audihor.Job = job;
            Assert.AreEqual(job, audihor.Job);

            // Ф.И.О
            string name = "Name";
            audihor.Name = name;
            Assert.AreEqual(name, audihor.Name);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Составил" в заголовке.
        /// </summary>
        [Test]
        public void Test_Author()
        {
            IA0TitleSign author = this.ActTitle.Author;
            Assert.NotNull(author);

            // Должность
            string job = "Job";
            author.Job = job;
            Assert.AreEqual(job, author.Job);

            // Ф.И.О
            string name = "Name";
            author.Name = name;
            Assert.AreEqual(name, author.Name);
        }

        /// <summary>
        /// Проверяет работоспособность получения наименования бизнес этапа.
        /// </summary>
        [Test]
        public void Test_BusOp()
        {
            string busOpName = this.ActTitle.BusOp;
            Assert.NotNull(busOpName);
            IA0BussinessStages stages = this.A0.Sys.Repo.BussinnessStages;
            string name = null;
            for (int i = 0; i < stages.Count; i++)
            {
                if (stages.Item[i].Kind == EA0ObjectKind.okAct)
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
            int busOpId = this.ActTitle.BusOpID;
            IA0BussinessStages stages = this.A0.Sys.Repo.BussinnessStages;
            int id = -1;
            for (int i = 0; i < stages.Count; i++)
            {
                if (stages.Item[i].Kind == EA0ObjectKind.okAct)
                {
                    id = stages.Item[i].ID;
                    break;
                }
            }

            Assert.AreEqual(id, busOpId);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Согласовано" в заголовке.
        /// </summary>
        [Test]
        public void Test_Conform()
        {
            IA0TitleSign conform = this.ActTitle.Conform;
            Assert.NotNull(conform);

            // Должность
            string job = "Job";
            conform.Job = job;
            Assert.AreEqual(job, conform.Job);

            // Ф.И.О
            string name = "Name";
            conform.Name = name;
            Assert.AreEqual(name, conform.Name);
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

        /// <summary>
        /// Проверяет корректность сохранения в БД изменений в заголовке.
        /// </summary>
        [Test]
        public void Test_LoadTitleChanges()
        {
            string titleName = "name";
            string mark = "mark";
            DateTime periodBegin = this.ActTitle.PeriodBegin.AddDays(1.0);
            DateTime periodEnd = this.ActTitle.PeriodEnd.AddDays(2.0);

            this.ActTitle.Name = titleName;
            this.ActTitle.Mark = mark;
            this.ActTitle.PeriodBegin = periodBegin;
            this.ActTitle.PeriodEnd = periodEnd;

            this.ActTitle.Approval.Job = "apJob";
            this.ActTitle.Auditor.Job = "audJob";
            this.ActTitle.Author.Job = "autJob";
            this.ActTitle.Conform.Job = "conJob";

            this.ActTitle.Approval.Name = "apName";
            this.ActTitle.Auditor.Name = "audName";
            this.ActTitle.Author.Name = "autName";
            this.ActTitle.Conform.Name = "conName";


            this.ImplRepo.Act.Save(this.Act);

            this.ImplRepo.Act.UnLock(this.Act.ID.GUID);
            IA0Act act = this.ImplRepo.Act.Load(this.Act.ID.GUID, EAccessKind.akRead);

            Assert.AreEqual(titleName, act.Title.Name);
            Assert.AreEqual(mark, act.Title.Mark);
            Assert.AreEqual(periodBegin, act.Title.PeriodBegin);
            Assert.AreEqual(periodEnd, act.Title.PeriodEnd);

            Assert.AreEqual("apJob", act.Title.Approval.Job);
            Assert.AreEqual("audJob", act.Title.Auditor.Job);
            Assert.AreEqual("autJob", act.Title.Author.Job);
            Assert.AreEqual("conJob", act.Title.Conform.Job);

            Assert.AreEqual("apName", act.Title.Approval.Name);
            Assert.AreEqual("audName", act.Title.Auditor.Name);
            Assert.AreEqual("autName", act.Title.Author.Name);
            Assert.AreEqual("conName", act.Title.Conform.Name);
        }
    }
}