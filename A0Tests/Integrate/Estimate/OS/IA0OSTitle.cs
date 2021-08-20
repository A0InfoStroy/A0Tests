// $Date: 2021-02-17 11:02:24 +0300 (Ср, 17 фев 2021) $
// $Revision: 521 $
// $Author: agalkin $
// Тесты полей ОС

namespace A0Tests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности заголовка ОС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0OSTitle",
        Author = "agalkin")]
    public class Test_IA0OSTitle : NewOS
    {
        /// <summary>
        /// Получает или устанавливает заголовок ОС.
        /// </summary>
        protected IA0OSTitle Title { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Title = this.OS.Title;
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
        /// Проверяет отсутствие ошибок при обращении к распределенной сметной стоимости.
        /// </summary>
        [Test]
        public void Test_AllocatedEstimate()
        {
            decimal allocated = this.Title.AllocatedEstimate;
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
            string job = "Job";
            approval.Job = job;
            Assert.AreEqual(job, approval.Job);

            // Ф.И.О
            string name = "Name";
            approval.Name = name;
            Assert.AreEqual(name, approval.Name);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Составил" в заголовке.
        /// </summary>
        [Test]
        public void Test_Author()
        {
            IA0TitleSign author = this.Title.Author;
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
        /// Проверяет отсутствие ошибок при обращении к виду строительства.
        /// </summary>
        [Test]
        public void Test_BuildKindStr()
        {
            string buildKind = this.Title.BuikdKindStr;
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
                if (stages.Item[i].Kind == EA0ObjectKind.okOS)
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
                if (stages.Item[i].Kind == EA0ObjectKind.okOS)
                {
                    id = stages.Item[i].ID;
                    break;
                }
            }

            Assert.AreEqual(id, busOpId);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Гл. инж." в заголовке.
        /// </summary>
        [Test]
        public void Test_ChiefEngineer()
        {
            IA0TitleSign chiefEngineer = this.Title.ChiefEngineer;
            Assert.NotNull(chiefEngineer);

            // Должность
            string job = "Job";
            chiefEngineer.Job = job;
            Assert.AreEqual(job, chiefEngineer.Job);

            // Ф.И.О
            string name = "Name";
            chiefEngineer.Name = name;
            Assert.AreEqual(name, chiefEngineer.Name);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи комментария к ОС.
        /// </summary>
        [Test]
        public void Test_Comment()
        {
            this.Title.Comment = "comment";
            string comment = this.Title.Comment;
            Assert.NotNull(comment);
            Assert.AreEqual("comment", comment);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Согласовано" в заголовке.
        /// </summary>
        [Test]
        public void Test_Conform()
        {
            IA0TitleSign conform = this.Title.Conform;
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
        /// Проверяет отсутствие ошибок при обращении к дате создания.
        /// </summary>
        [Test]
        public void Test_CreateMoment()
        {
            DateTime createMoment = this.Title.CreateMoment;
        }

        /// <summary>
        /// Проверяет работоспособность чтения исполнителя.
        /// </summary>
        [Test]
        public void Test_ExecutorStr()
        {
            string executorStr = this.Title.ExecutorStr;
            Assert.NotNull(executorStr);
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Нач. отдела" в заголовке.
        /// </summary>
        [Test]
        public void Test_HeadOfDep()
        {
            IA0TitleSign headOfDep = this.Title.HeadOfDep;
            Assert.NotNull(headOfDep);

            // Должность
            string job = "Job";
            headOfDep.Job = job;
            Assert.AreEqual(job, headOfDep.Job);

            // Ф.И.О
            string name = "Name";
            headOfDep.Name = name;
            Assert.AreEqual(name, headOfDep.Name);
        }

        /// <summary>
        /// Проверяет работоспособность чтения и записи статуса "Включать в итоги".
        /// </summary>
        [Test]
        public void Test_IncludeInTotals()
        {
            bool include = this.Title.IncludeInTotals;
            this.Title.IncludeInTotals = true;
            Assert.True(this.Title.IncludeInTotals);
            this.Title.IncludeInTotals = false;
            Assert.False(this.Title.IncludeInTotals);
        }

        /// <summary>
        ///  Проверяет отсутствие ошибок при обращении к графе "Составлена в ценах".
        /// </summary>
        [Test]
        public void Test_InPrice()
        {
            string inPrice = this.Title.InPrice;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Проверил" в заголовке.
        /// </summary>
        [Test]
        public void Test_Inspector()
        {
            IA0TitleSign inspector = this.Title.Inspector;
            Assert.NotNull(inspector);

            // Должность
            string job = "Job";
            inspector.Job = job;
            Assert.AreEqual(job, inspector.Job);

            // Ф.И.О
            string name = "Name";
            inspector.Name = name;
            Assert.AreEqual(name, inspector.Name);
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
        /// Проверяет отсутствие ошибок при обращении к нераспределенной сметной стоимости.
        /// </summary>
        [Test]
        public void Test_NotAllocatedEstimate()
        {
            decimal projectEng = this.Title.NotAllocatedEstimate;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к дате модификации.
        /// </summary>
        [Test]
        public void Test_UpdateMoment()
        {
            DateTime updateMoment = this.Title.UpdateMoment;
        }

        /// <summary>
        /// Проверяет корректность сохранения в БД изменений в заголовке.
        /// </summary>
        [Test]
        public void Test_LoadTitleChanges()
        {
            string titleName = "name";
            string mark = "mark";
            string comment = "comment";

            this.Title.Name = titleName;
            this.Title.Mark = mark;
            this.Title.Comment = comment;

            this.Title.Approval.Job = "apJob";
            this.Title.Author.Job = "autJob";
            this.Title.ChiefEngineer.Job = "chfJob";
            this.Title.Conform.Job = "conJob";                   
            this.Title.HeadOfDep.Job = "hodJob";
            this.Title.Inspector.Job = "insJob";

            this.Title.Approval.Name = "apName";
            this.Title.Author.Name = "autName";
            this.Title.ChiefEngineer.Name = "chfName";
            this.Title.Conform.Name = "conName";
            this.Title.HeadOfDep.Name = "hodName";
            this.Title.Inspector.Name = "insName";

            this.Repo.OS.Save(this.OS);

            IA0OS os = this.Repo.OS.Load(this.OS.ID.GUID, false);

            Assert.AreEqual(titleName, os.Title.Name);
            Assert.AreEqual(mark, os.Title.Mark);
            Assert.AreEqual(comment, os.Title.Comment);

            Assert.AreEqual("apJob", os.Title.Approval.Job);
            Assert.AreEqual("autJob", os.Title.Author.Job);
            Assert.AreEqual("chfJob", os.Title.ChiefEngineer.Job);
            Assert.AreEqual("conJob", os.Title.Conform.Job);
            Assert.AreEqual("hodJob", os.Title.HeadOfDep.Job);
            Assert.AreEqual("insJob", os.Title.Inspector.Job);

            Assert.AreEqual("apName", os.Title.Approval.Name);
            Assert.AreEqual("autName", os.Title.Author.Name);
            Assert.AreEqual("chfName", os.Title.ChiefEngineer.Name);
            Assert.AreEqual("conName", os.Title.Conform.Name);           
            Assert.AreEqual("hodName", os.Title.HeadOfDep.Name);
            Assert.AreEqual("insName", os.Title.Inspector.Name);
        }
    }
}