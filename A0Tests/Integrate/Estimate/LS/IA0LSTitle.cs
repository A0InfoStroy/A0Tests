// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
// Тесты полей ЛС

namespace A0Tests.Integrate.Estimate
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
    public class Test_IA0LSTitle : NewLS
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
        [Test(Description = "Тест базового атрибута"), Timeout(10000)]
        public void Test_AttrCore()
        {
            dynamic attr = this.Title.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута"), Timeout(10000)]
        public void Test_AttrExt()
        {
            dynamic attr = this.Title.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к подписям "Утверждаю" в заголовке.
        /// </summary>
        [Test, Timeout(10000)]
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
        /// Проверяет отсутствие ошибок при обращении к подписям "Проверил" в заголовке.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_Auditor()
        {
            IA0TitleSign audihor = this.Title.Auditor;
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
        [Test, Timeout(10000)]
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
        /// Проверяет работоспособность получения наименования бизнес этапа.
        /// </summary>
        [Test, Timeout(10000)]
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
        [Test, Timeout(10000)]
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
        /// Проверяет отсутствие ошибок при обращении к подписям "Согласовано" в заголовке.
        /// </summary>
        [Test, Timeout(10000)]
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
        /// Проверяет работоспособность чтения и записи шифра.
        /// </summary>
        [Test, Timeout(10000)]
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
        [Test, Timeout(10000)]
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
        [Test, Timeout(10000)]
        public void Test_VolumeScale()
        {
            decimal volumeScale = this.Title.VolumeScale;
        }

        /// <summary>
        /// Проверяет корректность сохранения в БД изменений в заголовке.
        /// </summary>
        [Test, Timeout(10000)]
        public void Test_LoadTitleChanges()
        {
            string titleName = "name";
            string mark = "mark";

            this.Title.Name = titleName;
            this.Title.Mark = mark;

            this.Title.Approval.Job = "apJob";
            this.Title.Auditor.Job = "audJob";
            this.Title.Author.Job = "autJob";          
            this.Title.Conform.Job = "conJob";

            this.Title.Approval.Name = "apName";
            this.Title.Auditor.Name = "audName";
            this.Title.Author.Name = "autName";
            this.Title.Conform.Name = "conName";


            this.Repo.LS.Save(this.LS);

            IA0LS ls = this.Repo.LS.Load(this.LS.ID);

            Assert.AreEqual(titleName, ls.Title.Name);
            Assert.AreEqual(mark, ls.Title.Mark);

            Assert.AreEqual("apJob", ls.Title.Approval.Job);
            Assert.AreEqual("audJob", ls.Title.Auditor.Job);
            Assert.AreEqual("autJob", ls.Title.Author.Job);
            Assert.AreEqual("conJob", ls.Title.Conform.Job);

            Assert.AreEqual("apName", ls.Title.Approval.Name);
            Assert.AreEqual("audName", ls.Title.Auditor.Name);
            Assert.AreEqual("autName", ls.Title.Author.Name);
            Assert.AreEqual("conName", ls.Title.Conform.Name);
        }
    }
}