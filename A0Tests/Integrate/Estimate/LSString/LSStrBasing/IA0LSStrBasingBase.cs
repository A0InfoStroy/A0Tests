// $Date: 2022-07-13 20:58:34 +0300 (Ср, 13 июл 2022) $
// $Revision: 587 $
// $Author: eloginov $
// Абстрактный класс тестов расценки строки локальных смет.

namespace A0Tests.Integrate.Estimate.LSString.LSStrBasing
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Абстрактная тестовая группа (класс) тестов проверки работоспособности расценки строки ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrBasing",
        Author = "agalkin")]
    public abstract class Test_IA0LSStrBasingBase : NewLSStringBase
    {
        /// <summary>
        /// Получает или устанавливает расценку строки ЛС.
        /// </summary>
        protected IA0LSStrBasing StrBasing { get; set; }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к базовому атрибуту расценки.
        /// </summary>
        [Test(Description = "Тест базового атрибута"), Timeout(10000)]
        public virtual void Test_AttrCore()
        {
            dynamic attr = this.StrBasing.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к расширенному атрибуту расценки.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута"), Timeout(10000)]
        public virtual void Test_AttrExt()
        {
            dynamic V = StrBasing.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к обоснованию расценки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Basing()
        {
            string basing = this.StrBasing.Basing;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к наименованию расценки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_Name()
        {
            string name = this.StrBasing.Name;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к району/подрайону.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_RegSimb()
        {
            string regSimb = this.StrBasing.RegSimb;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к виду учета транспортировки.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_TransAcntKind()
        {
            ETransAcntKind transAcntKind = this.StrBasing.TransAcntKind;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к составу работ.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_WorkList()
        {
            string workList = this.StrBasing.WorkList;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к эксплуатации машин (ЭМ).
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_EM()
        {
            decimal em = this.StrBasing.EM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ЭМ с поправкой.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_EM_Corr()
        {
            decimal emCorr = this.StrBasing.EM_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ЭМ прямой.
        /// </summary>
        [Test, Timeout(30000)]
        public virtual void Test_EM_Direct()
        {
            decimal emDirect = this.StrBasing.EM_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к основной заработной плате (ОЗП).
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_OZP()
        {
            decimal ozp = this.StrBasing.OZP;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ОЗП с поправкой.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_OZP_Corr()
        {
            decimal ozpCorr = this.StrBasing.OZP_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ОЗП прямой.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_OZP_Direct()
        {
            decimal ozpDirect = this.StrBasing.OZP_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к прямым затратам (ПЗ).
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_PZ()
        {
            decimal pz = this.StrBasing.PZ;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ПЗ с поправкой.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_PZ_Corr()
        {
            decimal pzCorr = this.StrBasing.PZ_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ПЗ прямым.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_PZ_Direct()
        {
            decimal pzDirect = this.StrBasing.PZ_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к возврату материалов (ВМ).
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_VM()
        {
            decimal vm = this.StrBasing.VM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ВМ с поправкой.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_VM_Corr()
        {
            decimal vmCorr = this.StrBasing.VM_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ВМ прямым.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_VM_Direct()
        {
            decimal vmDirect = this.StrBasing.VM_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к "в т.ч. ЗП машинистов" (ЗПМ).
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ZPM()
        {
            decimal zpm = this.StrBasing.ZPM;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ЗПМ с поправкой.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ZPM_Corr()
        {
            decimal zpmCorr = this.StrBasing.ZPM_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к ЗПМ прямой.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_ZPM_Direct()
        {
            decimal zpmDirect = this.StrBasing.ZPM_Direct;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к материальным затратам (МЗ).
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_MZ()
        {
            decimal mz = this.StrBasing.MZ;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к МЗ с поправкой.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_MZ_Corr()
        {
            decimal mzCorr = this.StrBasing.MZ_Corr;
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к МЗ прямым.
        /// </summary>
        [Test, Timeout(10000)]
        public virtual void Test_MZ_Direct()
        {
            decimal mzDirect = this.StrBasing.MZ_Direct;
        }
    }
}