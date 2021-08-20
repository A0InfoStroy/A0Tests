// $Date: 2021-01-29 14:15:46 +0300 (Пт, 29 янв 2021) $
// $Revision: 520 $
// $Author: agalkin $
// Тесты импорта

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
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
        protected dynamic Import { get; private set; }

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
            dynamic lsid = this.Import.From(this.OS.ID.GUID, this.OS.ID.ID, this.ImportConfig.ImportFrom);
            Assert.NotNull(lsid);

            // Значение 4 соответствует EA0ObjectKind.okLS.
            Assert.AreEqual(4, lsid.Kind);
            Assert.AreEqual(this.OS.ID.GUID, lsid.OSGUID);
        }

        /// <summary>
        /// Проверяет наличие импортированной ЛС в формате АРПС 1 в целевой ОС.
        /// </summary>
        [Test]
        public void Test_FromARPS()
        {
            this.Import.FromARPS(this.OS.ID.GUID, this.OS.ID.ID, this.ImportConfig.ImportFromARPS);
            dynamic lsIter = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, null);

            // Значение 4 соответствует EA0ObjectKind.okLS.
            this.ReadEstimateObject(lsIter, 4);
        }

        /// <summary>
        /// Проверяет наличие импортированной ЛС в формате XMLGrand в целевой ОС.
        /// </summary>
        [Test]
        public void Test_FromXMLGrand()
        {
            this.Import.FromXMLGrand(this.OS.ID.GUID, this.OS.ID.ID, this.ImportConfig.ImportFromXmlGrand);
            dynamic lsIter = this.Repo.LSID.Read(this.Proj.ID.GUID, null, null, null);

            // Значение 4 соответствует EA0ObjectKind.okLS.
            this.ReadEstimateObject(lsIter, 4);
        }
    }
}