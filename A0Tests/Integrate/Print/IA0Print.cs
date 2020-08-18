// $Date: 2020-08-06 15:36:12 +0300 (Чт, 06 авг 2020) $
// $Revision: 350 $
// $Author: agalkin $

namespace A0Tests
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Предоставляет методы подготовки среды тестирования.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Создает сметные объекты для тестирования.
        /// </summary>
        void Create();

        /// <summary>
        /// Удаляет сметные объекты после тестирования.
        /// </summary>
        void Delete();
    }

    /// <summary>
    /// Предоставляет Guid ЛС.
    /// </summary>
    public interface IContextLS : IContext
    {
        /// <summary>
        /// Получает Guid ЛС.
        /// </summary>
        Guid LsGuid { get; }
    }

    /// <summary>
    /// Предоставляет Guid акта.
    /// </summary>
    public interface IContextAct : IContextLS
    {
        /// <summary>
        /// Получает Guid акта.
        /// </summary>
        Guid ActGuid { get; }
    }

    /// <summary>
    /// Базовый класс для создания тестируемой ЛС.
    /// </summary>
    public class EstimateLSConxetCreator : IContextLS
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="EstimateLSConxetCreator"./>
        /// </summary>
        /// <param name="a0">API A0Service.</param>
        public EstimateLSConxetCreator(IAPI a0)
        {
            this.A0 = a0;
            if (this.A0 == null)
            {
                throw new ArgumentNullException("A0");
            }
        }

        /// <summary>
        /// Получает или устанавливает Guid проекта.
        /// </summary>
        public Guid ProjGUID { get; private set; }

        /// <summary>
        /// Получает или устанавливает Id узла проекта.
        /// </summary>
        public int ProjNodeID { get; private set; }

        /// <summary>
        /// Получает или устанавливает Guid ОС.
        /// </summary>
        public Guid OsGuid { get; private set; }

        /// <summary>
        /// Получает или устанавливает Id узла ОС.
        /// </summary>
        public int OsNodeId { get; private set; }

        /// <summary>
        /// Получает или устанавливает Guid ЛС.
        /// </summary>
        public Guid LsGuid { get; private set; }

        /// <summary>
        /// Получает или устанавливает API A0Service.
        /// </summary>
        protected IAPI A0 { get; private set; }

        /// <summary>
        /// Создает ЛС для тестирования.
        /// </summary>
        public virtual void Create()
        {
            // Создаем тестовый проект
            IA0Proj proj = this.A0.Estimate.Repo.Proj.New2(
                this.A0.Estimate.Repo.ComplexID.HeadComplexGUID,
                this.A0.Estimate.Repo.ComplexID.HeadNodeID);
            Assert.NotNull(proj);

            this.ProjGUID = proj.ID.GUID;
            this.ProjNodeID = this.A0.Estimate.Repo.ProjID.HeadNodeID;

            proj.Title.Name = "Интеграционные тесты печати " + DateTime.Now.ToString();

            this.A0.Estimate.Repo.Proj.Save(proj);

            // Создаем тестовую ОС
            IA0OS os = this.A0.Estimate.Repo.OS.New(this.ProjGUID, this.ProjNodeID);

            this.OsGuid = os.ID.GUID;
            this.OsNodeId = this.A0.Estimate.Repo.OSID.HeadNodeID;
            os.Title.Name = "Интеграционные тесты печати " + DateTime.Now.ToString();

            this.A0.Estimate.Repo.OS.Save(os);

            // Создаем тестовую ЛС
            IA0LS ls = this.A0.Estimate.Repo.LS.New(this.OsGuid, this.OsNodeId);

            this.LsGuid = ls.ID.GUID;
            ls.Title.Name = "Интеграционные тесты печати " + DateTime.Now.ToString();

            this.A0.Estimate.Repo.LS.Save(ls);
        }

        /// <summary>
        /// Удаляет сметные объекты после тестирования.
        /// </summary>
        public virtual void Delete()
        {
            // Удаляем тестовую ЛС
            this.A0.Estimate.Repo.LS.Delete(this.LsGuid);

            // Удаляем тестовую ОС
            this.A0.Estimate.Repo.OS.Delete(this.OsGuid);

            // Удаляем тестовый проект
            this.A0.Estimate.Repo.Proj.Delete(this.ProjGUID);
        }
    }

    /// <summary>
    /// Базовый класс для создания тестируемого акта.
    /// </summary>
    public class EstimateActConxetCreator : EstimateLSConxetCreator, IContextAct
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="EstimateActConxetCreator"./>
        /// </summary>
        /// <param name="a0">API A0Service.</param>
        public EstimateActConxetCreator(IAPI a0)
            : base(a0)
        {
        }

        /// <summary>
        /// Получает или устанавливает Guid акта.
        /// </summary>
        public Guid ActGuid { get; private set; }

        /// <summary>
        /// Создает акт для тестирования.
        /// </summary>
        public override void Create()
        {
            base.Create();

            // Акт для тестирования.
            IA0Act act = this.A0.Implement.Repo.Act.New(this.LsGuid, 0, 0, 100);
            Assert.NotNull(act);

            act.Title.Name = "Интеграционные тесты " + DateTime.Now.ToString();
            this.ActGuid = act.ID.GUID;

            // Наполнение строками.
            IA0ActString str = act.CreateTxtString(EA0StringKind.skMM, "1", act.Tree.Head.ID);
            str = act.CreateTxtString(EA0StringKind.skMK, "2", act.Tree.Head.ID);
            this.A0.Implement.Repo.Act.Save(act);
        }

        /// <summary>
        /// Удаляет сметные объекты после тестирования.
        /// </summary>
        public override void Delete()
        {
            this.A0.Implement.Repo.Act.Delete(this.ActGuid);
            base.Delete();
        }
    }

    namespace Print.Integrate
    {
        using System;
        using System.IO;
        using System.Linq;
        using A0Service;
        using NUnit.Framework;

        /// <summary>
        /// Содержит тесты проверки работоспособности вывода документа ЛС.
        /// </summary>
        [TestFixture(
            Category = "Integrate",
            Description = "Тесты проверки работоспособности IA0Print",
            Author = "agalkin")]
        public class Test_IA0PrintLS : Test_Base
        {
            /// <summary>
            /// Ссылка на контекст ЛС.
            /// </summary>
            private IContextLS context;

            /// <summary>
            /// Осуществляет операции проводимые перед тестированием.
            /// </summary>
            public override void SetUp()
            {
                base.SetUp();

                this.context = new EstimateLSConxetCreator(this.A0);
                this.context.Create();
            }

            /// <summary>
            /// Осуществляет операции проводимые по завершению тестирования.
            /// </summary>
            public override void TearDown()
            {
                this.context.Delete();
                base.TearDown();
            }

            /// <summary>
            /// Проверяет отсутствие ошибок при вызове метода подготовки вывода документа.
            /// </summary>
            [Test]
            public void Test_PrepareLib()
            {
                IA0Print print = this.A0.GetPrint(EA0PrintKind.pkLS, string.Empty);
                print.PrepareLib(this.context.LsGuid);
            }
        }

        /// <summary>
        ///  Содержит тесты проверки работоспособности вывода документа акта.
        /// </summary>
        [TestFixture(
            Category = "Integrate",
            Description = "Тесты проверки работоспособности IA0Print",
            Author = "agalkin")]
        public class Test_IA0PrintAct : Test_Base
        {
            /// <summary>
            /// Ссылка на контекст акта.
            /// </summary>
            private IContextAct context;

            /// <summary>
            /// Получает путь к шаблонам печати из файла настроек.
            /// </summary>
            private string TemplatePath => this.Configuration.Descendants("printTemplates").SingleOrDefault()?.Value;

            /// <summary>
            /// Осуществляет операции проводимые перед тестированием.
            /// </summary>
            public override void SetUp()
            {
                base.SetUp();

                this.context = new EstimateActConxetCreator(this.A0);
                this.context.Create();
            }

            /// <summary>
            /// Осуществляет операции проводимые по завершению тестирования.
            /// </summary>
            public override void TearDown()
            {
                this.context.Delete();
                base.TearDown();
            }

            /// <summary>
            /// Проверяет отсутствие ошибок при вызове метода подготовки вывода документа.
            /// </summary>
            [Test]
            public void Test_PrepareLib()
            {
                IA0Print print = this.A0.GetPrint(EA0PrintKind.pkAct, string.Empty);
                print.PrepareLib(this.context.ActGuid);
            }

            /// <summary>
            /// Проверяет работоспособность экспорта акта в формат .pdf.
            /// </summary>
            [Test]
            public void Test_ExportPDF()
            {
                IA0Print print = this.A0.GetPrint(EA0PrintKind.pkAct, this.TemplatePath);

                print.PrepareLib(this.context.ActGuid);
                Assert.Greater(print.Templates.Count, 0);

                Assert.Greater(print.TemplateSettings.Count, 0);

                IA0PrintTemplate template = print.Templates.Item[0];
                IA0TemplateSetting setting = print.TemplateSettings.Item[0];

                // Временный файл для отчета
                string outFileName = Path.GetTempFileName() + @".pdf";

                print.ExportPDF(template, setting, outFileName);

                Assert.IsTrue(File.Exists(outFileName), "Ожидается наличие файла");
                File.Delete(outFileName);
            }

            /// <summary>
            /// Проверяет работоспособность изменения подписей в отчете.
            /// </summary>
            [Test]
            public void Test_Signature()
            {
                // Проверка изменения подписей в отчете.
                // Создаются 2 отчета.
                //   1 с настройками по умолчанию
                //   2 с измененными настройками
                // Как автоматически узнать, что настройки применяются? Сравнить файлы?
                // Это относится не только к настройкам подписей, но и любым настройкам.
                IA0Print print = this.A0.GetPrint(EA0PrintKind.pkAct, this.TemplatePath);

                // Для тестирования шаблона с "шитрым устройством" - нужен концевик
                // Guid ActGUID2 = Guid.Parse("{CD590C33-021A-463F-A208-6CAD141A185B}");
                Guid actGuid = this.context.ActGuid;

                print.PrepareLib(actGuid);
                Assert.Greater(print.Templates.Count, 0);

                Assert.Greater(print.TemplateSettings.Count, 0);

                for (int ti = 0; ti < print.Templates.Count; ++ti)
                {
                    IA0PrintTemplate template = print.Templates.Item[ti];

                    // Этот шаблон тестируем
                    if (string.Compare(template.Caption, "КС-2 (15 граф)", true) != 0)
                    {
                        continue;
                    }

                    // Поиск настроек для шаблона по имени.
                    IA0TemplateSetting setting = null;
                    for (var i = 0; i < print.TemplateSettings.Count; ++i)
                    {
                        if (print.TemplateSettings.Item[i].TemplateName == template.Name)
                        {
                            setting = print.TemplateSettings.Item[i];
                            break;
                        }
                    }

                    Assert.NotNull(setting, "Не могу найти настройки");

                    // Подготовка шаблонов и настроек
                    print.Prepare(template, setting);

                    // файл для отчета 1
                    string outFileName = Path.Combine(Path.GetTempPath(), template.Caption + @".pdf");
                    print.ExportPDF(template, setting, outFileName);

                    Assert.IsTrue(File.Exists(outFileName), "Ожидается наличие файла");

                    // файл для отчета 2
                    string outFileName2 = Path.Combine(Path.GetTempPath(), template.Caption + @"-Подписи.pdf");

                    // Изменение свойств
                    for (int j = 0; j < setting.Propertys.Count; ++j)
                    {
                        IA0TemplateProperty item = setting.Propertys.Item[j];
                        switch (item.Name)
                        {
                            case @"chbPrintSmet":
                                item.Value = true;
                                break;
                            case @"edSmetchikActJob":
                                item.Value = @"Должность1";
                                break;
                            case @"edSmetchikAct":
                                item.Value = @"ФИО1";
                                break;
                            case @"chbAudit":
                                item.Value = true;
                                break;
                            case @"edAuditorActJob":
                                item.Value = @"Должность2";
                                break;
                            case @"edAuditorAct":
                                item.Value = @"ФИО2";
                                break;
                        }
                    }

                    print.ExportPDF(template, setting, outFileName2);

                    Assert.IsTrue(File.Exists(outFileName2), "Ожидается наличие файла");

                    File.Delete(outFileName);
                    File.Delete(outFileName2);

                    break;
                }
            }

            /// <summary>
            /// Проверяет работоспособность экспорта акта в формат .rtf.
            /// </summary>
            [Test]
            public void Test_ExportRTF()
            {
                IA0Print print = this.A0.GetPrint(EA0PrintKind.pkAct, this.TemplatePath);

                print.PrepareLib(this.context.ActGuid);
                Assert.Greater(print.Templates.Count, 0);

                Assert.Greater(print.TemplateSettings.Count, 0);

                IA0PrintTemplate template = print.Templates.Item[0];
                IA0TemplateSetting setting = print.TemplateSettings.Item[0];

                // Временный файл для отчета
                string outFileName = Path.GetTempFileName() + @".rtf";

                print.ExportRTF(template, setting, outFileName);

                Assert.IsTrue(File.Exists(outFileName), "Ожидается наличие файла");
                File.Delete(outFileName);
            }

            /// <summary>
            /// Проверяет работоспособность экспорта акта в формат .xls.
            /// </summary>
            [Test]
            public void Test_ExportXLS()
            {
                IA0Print print = this.A0.GetPrint(EA0PrintKind.pkAct, this.TemplatePath);

                print.PrepareLib(this.context.ActGuid);
                Assert.Greater(print.Templates.Count, 0);

                Assert.Greater(print.TemplateSettings.Count, 0);

                IA0PrintTemplate template = print.Templates.Item[0];
                IA0TemplateSetting setting = print.TemplateSettings.Item[0];

                // Временный файл для отчета
                string outFileName = Path.GetTempFileName() + @".xls";

                print.ExportXLS(template, setting, outFileName);

                Assert.IsTrue(File.Exists(outFileName), "Ожидается наличие файла");
                File.Delete(outFileName);
            }
        }
    }
}