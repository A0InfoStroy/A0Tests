// $Date: 2021-01-29 13:51:24 +0300 (Пт, 29 янв 2021) $
// $Revision: 515 $
// $Author: agalkin $
// Тесты импорта

namespace A0Tests.Integrate.Estimate
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using A0Service;
    using NUnit.Framework;
    using static Config.FileStreamHelper;

    /// <summary>
    /// Содержит тест проверки импорта/экспорта ЛС в формате А0.
    /// </summary>
    [TestFixture(Category = "Functional",
        Description = "Тесты проверки работоспособности IA0EstimateLSService",
        Author = "agalkin")]
    public class Test_IA0EstimateLSService : NewLS
    {
        private const int BufferSize = 8192;

        /// <summary>
        /// Получает или устанавливает значение службы для импорта и экспорта ЛС.
        /// </summary>
        public IA0EstimateLSService LSService { get; private set; }

        /// <summary>
        /// Осуществляет подготовку к тестированию.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LSService = this.A0.Estimate.LS.Services;
            Assert.NotNull(this.LSService);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LSService);
            this.LSService = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет корректность импорта и экспорта ЛС.
        /// </summary>
        [Test]
        public void Test_ExportImport()
        {
            // Создаем в ЛС текстовые строки.
            this.LS.CreateTxtString(EA0StringKind.skWork, "1234", this.LS.Tree.Head.ID);
            this.LS.CreateTxtString(EA0StringKind.skTZ, "1234", this.LS.Tree.Head.ID);
            this.Repo.LS.Save(this.LS);

            // Экспортируем ЛС в поток.
            IStream stream = this.LSService.Export.ToA0(this.LS.ID.GUID) as IStream;

            // Временный файл для данных.
            string outFileName = Path.GetTempFileName();

            try
            {
                // Сохраняем данные.
                using (FileStream fs = new FileStream(outFileName, FileMode.Create))
                {
                    int read = 0;
                    IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(read));
                    try
                    {
                        Marshal.WriteInt32(ptr, read);
                        byte[] buffer = new byte[BufferSize];
                        do
                        {
                            stream.Read(buffer, buffer.Length, ptr);
                            read = Marshal.ReadInt32(ptr);
                            if (read > 0)
                            {
                                fs.Write(buffer, 0, read);
                            }
                        }
                        while (read > 0);
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(ptr);
                    }
                }

                // Открываем временный файл и загружаем ЛС.
                uint result = SHCreateStreamOnFileEx(outFileName, STGM_READ | STGM_SHARE_DENY_NONE, 0, false, null, out stream);
                Assert.NotNull(stream);
                Assert.AreEqual(result, 0, "Не могу открыть файл");

                // Импортируем ЛС из потока.
                IA0LSID lsid = this.LSService.Import.FromA0(this.OS.ID.GUID, 0, stream);
                Assert.NotNull(lsid);

                // Сравниваем данные исходной ЛС и полученной после экспорта/импорта.
                Assert.AreEqual(this.LS.ID.OSGUID, lsid.OSGUID);

                // Загружаем импортированную ЛС.
                IA0LS ls = this.Repo.LS.Load2(lsid.GUID);
                Assert.NotNull(ls);

                // Проверяем наличие 2-х строк, созданных в исходной ЛС.
                Assert.True(ls.Strings.Count == 2);

                // Проверяем типы строк ЛС.
                Assert.True(ls.Strings.Items[0].StringKind == EA0StringKind.skWork);
                Assert.True(ls.Strings.Items[1].StringKind == EA0StringKind.skTZ);
            }
            finally
            {
                // Удаляем временный файл
                File.Delete(outFileName);
            }
        }
    }
}