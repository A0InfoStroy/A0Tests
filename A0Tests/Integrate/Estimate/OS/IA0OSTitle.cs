// $Date: 2020-10-30 15:25:42 +0300 (Пт, 30 окт 2020) $
// $Revision: 403 $
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
            string job = approval.Job;

            // Ф.И.О
            string name = approval.Name;
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
            string job = author.Job;

            // Ф.И.О
            string name = author.Name;
        }

        /// <summary>
        /// Проверяет работоспособность чтения вида строительства.
        /// </summary>
        [Test]
        public void Test_BuildKindStr()
        {
            string buildKind = this.Title.BuikdKindStr;
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
            string job = chiefEngineer.Job;

            // Ф.И.О
            string name = chiefEngineer.Name;
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
            string job = conform.Job;

            // Ф.И.О
            string name = conform.Name;
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
            string job = headOfDep.Job;

            // Ф.И.О
            string name = headOfDep.Name;
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
        ///  Проверяет работоспособность чтения графы "Составлена в ценах".
        /// </summary>
        [Test]
        public void Test_InPrice()
        {
            string inPrice = this.Title.InPrice;
            Assert.NotNull(inPrice);
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
            string job = inspector.Job;

            // Ф.И.О
            string name = inspector.Name;
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
    }
}