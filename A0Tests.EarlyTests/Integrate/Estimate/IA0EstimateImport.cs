// $Date: 2021-01-26 14:24:17 +0300 (Вт, 26 янв 2021) $
// $Revision: 509 $
// $Author: agalkin $
// Тесты импорта

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Xml.Linq;
    using A0Service;
    using A0Tests.Config;
    using NUnit.Framework;
    using static A0Tests.Config.UserConfigParser;

    /// <summary>
    ///  Содержит тесты проверки работоспособности импорта ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0EstimateImport",
        Author = "agalkin")]
    public class Test_IA0EstimateImport : Test_NewOS
    {
        /// <summary>
        /// Получает и устанавливает настройки импорта.
        /// </summary>
        protected IA0ImportConfig ImportConfig { get; private set; }

        /// <summary>
        /// Получает или устанавливает сущность предоставляющую операции импорта ЛС.
        /// </summary>
        protected IA0EstimateImport Import { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            string error = null;
            try
            {
                this.ImportConfig = GetA0ImportConfig(this.XmlText);
            }
            catch (System.Runtime.Serialization.SerializationException ex)
            {
                error = "Ошибка наименования элементов в xml-файле пользовательских настроек" + ex.Message;
            }
            catch (Exception ex)
            {
                error = "Ошибка чтения xml-файла пользовательских настроек" + ex.Message;
            }

            Assert.NotNull(this.ImportConfig, error);
            this.Import = this.A0.Estimate.Repo.Import;
            Assert.NotNull(this.Import);
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
            IA0LSID lsid = this.Import.From(this.OS.ID.GUID, this.OS.ID.ID, this.ImportConfig.ImportFrom);
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
            this.Import.FromARPS(this.OS.ID.GUID, this.OS.ID.ID, this.ImportConfig.ImportFromARPS);
            IA0ObjectIterator lsIter = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, null);
            this.ReadEstimateObject(lsIter, EA0ObjectKind.okLS);
        }

        /// <summary>
        /// Проверяет наличие импортированной ЛС в формате XMLGrand в целевой ОС.
        /// </summary>
        [Test]
        public void Test_FromXMLGrand()
        {
            this.Import.FromXMLGrand(this.OS.ID.GUID, this.OS.ID.ID, this.ImportConfig.ImportFromXmlGrand);
            IA0ObjectIterator lsIter = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, null);
            this.ReadEstimateObject(lsIter, EA0ObjectKind.okLS);
        }
    }
}