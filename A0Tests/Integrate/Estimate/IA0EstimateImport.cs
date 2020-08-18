// $Date: 2020-07-31 11:37:52 +0300 (Пт, 31 июл 2020) $
// $Revision: 337 $
// $Author: agalkin $
// Тесты импорта

namespace A0Tests.Integrate.Estimate
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Xml.Linq;
    using A0Service;
    using NUnit.Framework;
    using static FileStreamHelper;

    /// <summary>
    ///  Содержит тесты проверки работоспособности импорта ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0EstimateImport",
        Author = "agalkin")]
    public class Test_IA0EstimateImport : NewOS
    {
        /// <summary>
        /// Получает или устанавливает сущность предоставляющую операции импорта ЛС.
        /// </summary>
        protected IA0EstimateImport Import { get; private set; }

        /// <summary>
        /// Получает или устанавливает документ с пользовательскими настройками.
        /// </summary>
        protected XDocument Config { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Import = this.A0.Estimate.Repo.Import;
            Assert.NotNull(this.Import);

            // Получение пути к xml-файлу пользовательских настроек размещенному в выходной директории.
            string xmlFilePath = AppDomain.CurrentDomain.BaseDirectory + @"settings\userConfig.xml";

            this.Config = XDocument.Load(xmlFilePath);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Import);
            this.Import = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет наличие импортированной ЛС в формате А0 в целевой ОС.
        /// </summary>
        [Test]
        public void Test_From()
        {
            IStream stream = this.GetStream("importFrom");
            IA0LSID lsid = this.Import.From(this.OS.ID.GUID, this.OS.ID.ID, stream);
            Assert.NotNull(lsid);
            Assert.AreEqual(EA0ObjectKind.okLS, lsid.Kind);
            Assert.AreEqual(this.OS.ID.GUID, lsid.OSGUID);
        }

        /// <summary>
        /// Проверяет наличие импортированной ЛС в формате АРПС 1 в целевой ОС.
        /// </summary>
        [Test]
        public void Test_FromARPS()
        {
            IStream stream = this.GetStream("impormFromARPS");
            this.Import.FromARPS(this.OS.ID.GUID, this.OS.ID.ID, stream);
            IA0ObjectIterator lsIter = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, null);
            this.ReadEstimateObject(lsIter, EA0ObjectKind.okLS);
        }

        /// <summary>
        /// Проверяет наличие импортированной ЛС в формате XMLGrand в целевой ОС.
        /// </summary>
        [Test]
        public void Test_FromXMLGrand()
        {
            IStream stream = this.GetStream("importFromXMLGrand");
            this.Import.FromXMLGrand(this.OS.ID.GUID, this.OS.ID.ID, stream);
            IA0ObjectIterator lsIter = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, null);
            this.ReadEstimateObject(lsIter, EA0ObjectKind.okLS);
        }

        /// <summary>
        /// Осуществляет чтение файла импортируемой ЛС.
        /// </summary>
        /// <param name="fileType">Формат ЛС.</param>
        /// <returns>Поток из файла импортируемой ЛС.</returns>
        private IStream GetStream(string fileType)
        {
            string importPath = this.Config.Descendants(fileType).SingleOrDefault()?.Value;
            Assert.NotNull(importPath);
            uint result = SHCreateStreamOnFileEx(importPath, STGM_READ | STGM_SHARE_DENY_NONE, 0, false, null, out IStream stream);
            Assert.NotNull(stream);
            Assert.AreEqual(result, 0, "Не могу открыть файл");

            return stream;
        }
    }
}