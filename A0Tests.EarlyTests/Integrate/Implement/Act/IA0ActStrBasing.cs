// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты расценки строки акта

namespace A0Tests.EarlyTests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности расценки строки акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrBasing",
        Author = "agalkin")]
    public class Test_IA0ActStrBasing : Test_NewActString
    {
        /// <summary>
        /// Получает или устанавливает расценку строки акта.
        /// </summary>
        protected IA0LSStrBasing StrBasing { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.IsTrue(this.Act.Strings.Count > 0, "В тестовом акте нет строк");
            IA0ActString str = this.Act.Strings.Items[0];
            Assert.IsNotNull(str.StrBasing, "В тестовой строке нет расценки");
            this.StrBasing = str.StrBasing;
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.StrBasing);
            this.StrBasing = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому атрибуту расценки.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.StrBasing.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному атрибуту расценки.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            // dynamic V = m_StrBasing.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к обоснованию расценки.
        /// </summary>
        [Test]
        public void Test_Basing()
        {
            string basing = this.StrBasing.Basing;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к наименованию расценки.
        /// </summary>
        [Test]
        public void Test_Name()
        {
            string name = this.StrBasing.Name;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к району/подрайону.
        /// </summary>
        [Test]
        public void Test_RegSimb()
        {
            string regSimb = this.StrBasing.RegSimb;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду учета транспортировки.
        /// </summary>
        [Test]
        public void Test_TransAcntKind()
        {
            ETransAcntKind transAcntKind = this.StrBasing.TransAcntKind;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к составу работ.
        /// </summary>
        [Test]
        public void Test_WorkList()
        {
            string workList = this.StrBasing.WorkList;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к эксплуатации машин (ЭМ).
        /// </summary>
        [Test]
        public void Test_EM()
        {
            decimal em = this.StrBasing.EM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ЭМ с поправкой.
        /// </summary>
        [Test]
        public void Test_EM_Corr()
        {
            decimal emCorr = this.StrBasing.EM_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ЭМ прямой.
        /// </summary>
        [Test]
        public void Test_EM_Direct()
        {
            decimal emDirect = this.StrBasing.EM_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к основной заработной плате (ОЗП).
        /// </summary>
        [Test]
        public void Test_OZP()
        {
            decimal ozp = this.StrBasing.OZP;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ОЗП с поправкой.
        /// </summary>
        [Test]
        public void Test_OZP_Corr()
        {
            decimal ozpCorr = this.StrBasing.OZP_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ОЗП прямой.
        /// </summary>
        [Test]
        public void Test_OZP_Direct()
        {
            decimal ozpDirect = this.StrBasing.OZP_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к прямым затратам (ПЗ).
        /// </summary>
        [Test]
        public void Test_PZ()
        {
            decimal pz = this.StrBasing.PZ;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ПЗ с поправкой.
        /// </summary>
        [Test]
        public void Test_PZ_Corr()
        {
            decimal pzCorr = this.StrBasing.PZ_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ПЗ прямым.
        /// </summary>
        [Test]
        public void Test_PZ_Direct()
        {
            decimal pzDirect = this.StrBasing.PZ_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к возврату материалов (ВМ).
        /// </summary>
        [Test]
        public void Test_VM()
        {
            decimal vm = this.StrBasing.VM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ВМ с поправкой.
        /// </summary>
        [Test]
        public void Test_VM_Corr()
        {
            decimal vmCorr = this.StrBasing.VM_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ВМ прямым.
        /// </summary>
        [Test]
        public void Test_VM_Direct()
        {
            decimal vmDirect = this.StrBasing.VM_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к "в т.ч. ЗП машинистов" (ЗПМ).
        /// </summary>
        [Test]
        public void Test_ZPM()
        {
            decimal zpm = this.StrBasing.ZPM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ЗПМ с поправкой.
        /// </summary>
        [Test]
        public void Test_ZPM_Corr()
        {
            decimal zpmCorr = this.StrBasing.ZPM_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ЗПМ прямой.
        /// </summary>
        [Test]
        public void Test_ZPM_Direct()
        {
            decimal zpmDirect = this.StrBasing.ZPM_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к материальным затратам (МЗ).
        /// </summary>
        [Test]
        public void Test_MZ()
        {
            decimal mz = this.StrBasing.MZ;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к МЗ с поправкой.
        /// </summary>
        [Test]
        public void Test_MZ_Corr()
        {
            decimal mzCorr = this.StrBasing.MZ_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к МЗ прямым.
        /// </summary>
        [Test]
        public void Test_MZ_Direct()
        {
            decimal mzDirect = this.StrBasing.MZ_Direct;
        }
    }
}
