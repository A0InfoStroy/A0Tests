// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $
// Тесты итогов строки акта

namespace A0Tests.EarlyTests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности итогов строки акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LSStrTotal",
        Author = "agalkin")]
    public class Test_IA0ActStrTotal : Test_NewActString
    {
        /// <summary>
        /// Получает или устанавливает итоги строки акта.
        /// </summary>
        protected IA0LSStrTotal ActStrTotal { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ActStrTotal = this.ActString.Total;
            Assert.NotNull(this.ActStrTotal);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ActStrTotal);
            this.ActStrTotal = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому атрибуту.
        /// </summary>
        [Test(Description = "Тест базового атрибута")]
        public void Test_AttrCore()
        {
            dynamic attr = this.ActStrTotal.Attr["ProjID"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к расширенному атрибуту.
        /// </summary>
        [Test(Description = "Тест расширенного атрибута")]
        public void Test_AttrExt()
        {
            dynamic attr = this.ActStrTotal.Attr["LGM.TZNorm"];
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "3 Эксплуатация машин".
        /// </summary>
        [Test]
        public void Test_BaseEM()
        {
            decimal baseEM = this.ActStrTotal.BaseEM;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому ФОТ.
        /// </summary>
        [Test]
        public void Test_BaseFot()
        {
            decimal baseFot = this.ActStrTotal.BaseFot;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "5 Материальные затраты".
        /// </summary>
        [Test]
        public void Test_BaseMZ()
        {
            decimal baseMZ = this.ActStrTotal.BaseMZ;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "7 Накладные расходы".
        /// </summary>
        [Test]
        public void Test_BaseNR()
        {
            decimal baseNR = this.ActStrTotal.BaseNR;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "2 Основная зарплата".
        /// </summary>
        [Test]
        public void Test_BaseOZP()
        {
            decimal baseOZP = this.ActStrTotal.BaseOZP;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "1 Прямые затраты".
        /// </summary>
        [Test]
        public void Test_BasePZ()
        {
            decimal basePZ = this.ActStrTotal.BasePZ;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "8 Сметная прибыль".
        /// </summary>
        [Test]
        public void Test_BaseSP()
        {
            decimal baseSP = this.ActStrTotal.BaseSP;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "28 Сметная стоимость оборудования".
        /// </summary>
        [Test]
        public void Test_BaseSSob()
        {
            decimal baseSSob = this.ActStrTotal.BaseSSob;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "45 Возврат материалов".
        /// </summary>
        [Test]
        public void Test_BaseVM()
        {
            decimal baseVM = this.ActStrTotal.BaseVM;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "56 Зимнее удорожание".
        /// </summary>
        [Test]
        public void Test_BaseWinterRise()
        {
            decimal baseWinterRise = this.ActStrTotal.BaseWinterRise;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "4 в т.ч. з/п машинистов".
        /// </summary>
        [Test]
        public void Test_BaseZPM()
        {
            decimal baseZPM = this.ActStrTotal.BaseZPM;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к базовому "13 Сметная ЗП в НР".
        /// </summary>
        [Test]
        public void Test_BaseZPnr()
        {
            decimal baseZPnr = this.ActStrTotal.BaseZPnr;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "37 Другие затраты по оборудованию".
        /// </summary>
        [Test]
        public void Test_DRZob()
        {
            decimal drZob = this.ActStrTotal.DRZob;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "3 Эксплуатация машин".
        /// </summary>
        [Test]
        public void Test_EM()
        {
            decimal em = this.ActStrTotal.EM;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "62 Сторонняя ЭМ".
        /// </summary>
        [Test]
        public void Test_EMOutside()
        {
            decimal emOutside = this.ActStrTotal.EMOutside;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "22 Прочие машины".
        /// </summary>
        [Test]
        public void Test_EMpr()
        {
            decimal empr = this.ActStrTotal.EMpr;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "18 Эксплуатация машин по справочникам".
        /// </summary>
        [Test]
        public void Test_EMts()
        {
            decimal emts = this.ActStrTotal.EMts;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "19 Эксплуатация машин ВПТ".
        /// </summary>
        [Test]
        public void Test_EMvt()
        {
            decimal emvt = this.ActStrTotal.EMvt;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "65 Сторонее оборудование".
        /// </summary>
        [Test]
        public void Test_EquipOutside()
        {
            decimal equipOutside = this.ActStrTotal.EquipOutside;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "25 Материалы ЭСН по справочникам".
        /// </summary>
        [Test]
        public void Test_ESNts()
        {
            decimal esNts = this.ActStrTotal.ESNts;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "36 Работы-затраты".
        /// </summary>
        [Test]
        public void Test_Expense()
        {
            decimal expense = this.ActStrTotal.Expense;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к ФОТ.
        /// </summary>
        [Test]
        public void Test_FOT()
        {
            decimal fot = this.ActStrTotal.FOT;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "35 Комплектация".
        /// </summary>
        [Test]
        public void Test_Kob()
        {
            decimal kob = this.ActStrTotal.Kob;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "20 Косвенные расходы".
        /// </summary>
        [Test]
        public void Test_KR()
        {
            decimal kr = this.ActStrTotal.KR;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "21 Количество машин ВПТ".
        /// </summary>
        [Test]
        public void Test_MSHvt()
        {
            decimal msHvt = this.ActStrTotal.MSHvt;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "5 Материальные затраты".
        /// </summary>
        [Test]
        public void Test_MZ()
        {
            decimal mz = this.ActStrTotal.MZ;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "61 Сторонние МЗ".
        /// </summary>
        [Test]
        public void Test_MZOutside()
        {
            decimal mzOutside = this.ActStrTotal.MZOutside;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "27 Прочие материалы".
        /// </summary>
        [Test]
        public void Test_MZpr()
        {
            decimal mzpr = this.ActStrTotal.MZpr;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "24 Материалы по справочникам".
        /// </summary>
        [Test]
        public void Test_MZts()
        {
            decimal mzts = this.ActStrTotal.MZts;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "7 Накладные расходы".
        /// </summary>
        [Test]
        public void Test_NR()
        {
            decimal nr = this.ActStrTotal.NR;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "29 Отпускная стоимость оборудования".
        /// </summary>
        [Test]
        public void Test_OSob()
        {
            decimal oSob = this.ActStrTotal.OSob;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "30 Оборудование по справочникам".
        /// </summary>
        [Test]
        public void Test_Ots()
        {
            decimal ots = this.ActStrTotal.Ots;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "2 Основная зарплата".
        /// </summary>
        [Test]
        public void Test_OZP()
        {
            decimal ozp = this.ActStrTotal.OZP;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "64 Сторонняя ОЗП".
        /// </summary>
        [Test]
        public void Test_OZPOutside()
        {
            decimal ozpOutside = this.ActStrTotal.OZPOutside;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "14 ОЗП по справочникам".
        /// </summary>
        [Test]
        public void Test_OZPts()
        {
            decimal ozpts = this.ActStrTotal.OZPts;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "16 ЗП рабочих ВПТ".
        /// </summary>
        [Test]
        public void Test_OZPvt()
        {
            decimal ozpvt = this.ActStrTotal.OZPvt;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "1 Прямые затраты".
        /// </summary>
        [Test]
        public void Test_PZ()
        {
            decimal pz = this.ActStrTotal.PZ;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "23 Неучтенные материалы".
        /// </summary>
        [Test]
        public void Test_RP()
        {
            decimal rp = this.ActStrTotal.RP;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "26 Материалы РП по справочникам".
        /// </summary>
        [Test]
        public void Test_RPts()
        {
            decimal rpts = this.ActStrTotal.RPts;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "8 Сметная прибыль".
        /// </summary>
        [Test]
        public void Test_SP()
        {
            decimal sp = this.ActStrTotal.SP;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "28 Сметная стоимость оборудования".
        /// </summary>
        [Test]
        public void Test_SSob()
        {
            decimal ssob = this.ActStrTotal.SSob;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "33 Транспортировка оборудования".
        /// </summary>
        [Test]
        public void Test_TRob()
        {
            decimal tRob = this.ActStrTotal.TRob;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "32 Тара и упаковка".
        /// </summary>
        [Test]
        public void Test_TUob()
        {
            decimal tUob = this.ActStrTotal.TUob;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "Затраты труда".
        /// </summary>
        [Test]
        public void Test_TZ()
        {
            decimal tz = this.ActStrTotal.TZ;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "41 Трудозатраты машинистов".
        /// </summary>
        [Test]
        public void Test_TZM()
        {
            double tzm = this.ActStrTotal.TZM;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "43 Трудозатраты машинистов ВПТ".
        /// </summary>
        [Test]
        public void Test_TZMvt()
        {
            double tzMvt = this.ActStrTotal.TZMvt;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "44 Нормативная трудоемкость в НР".
        /// </summary>
        [Test]
        public void Test_TZnr()
        {
            double tznr = this.ActStrTotal.TZnr;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "40 Трудозатраты основных рабочих".
        /// </summary>
        [Test]
        public void Test_TZO()
        {
            double tzo = this.ActStrTotal.TZO;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "42 Трудозатраты рабочих ВПТ".
        /// </summary>
        [Test]
        public void Test_TZOvt()
        {
            double tzOvt = this.ActStrTotal.TZOvt;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "34 Заготов.-складские расходы".
        /// </summary>
        [Test]
        public void Test_TZRob()
        {
            decimal tzRob = this.ActStrTotal.TZRob;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "38 Услуги посредников".
        /// </summary>
        [Test]
        public void Test_UPob()
        {
            decimal uPob = this.ActStrTotal.UPob;
        }

        /// <summary>
        ///  Проверяет отстутствие ошибок при обращении к "45 Возврат материалов".
        /// </summary>
        [Test]
        public void Test_VM()
        {
            decimal vm = this.ActStrTotal.VM;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "46 Возврат материалов по справочникам".
        /// </summary>
        [Test]
        public void Test_VMts()
        {
            decimal vmts = this.ActStrTotal.VMts;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "47 Возврат оборудования".
        /// </summary>
        [Test]
        public void Test_VO()
        {
            decimal vo = this.ActStrTotal.VO;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "48 Возврат оборудования по справочникам".
        /// </summary>
        [Test]
        public void Test_VOts()
        {
            decimal vots = this.ActStrTotal.VOts;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "56 Зимнее удорожание".
        /// </summary>
        [Test]
        public void Test_WinterRise()
        {
            decimal winterRise = this.ActStrTotal.WinterRise;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "4 в т.ч. зарплата машинистов".
        /// </summary>
        [Test]
        public void Test_ZPM()
        {
            decimal zpm = this.ActStrTotal.ZPM;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "63 Сторонняя ЗПМ".
        /// </summary>
        [Test]
        public void Test_ZPMOutside()
        {
            decimal zpmOutside = this.ActStrTotal.ZPMOutside;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "15 ЗП машинистов по справочникам".
        /// </summary>
        [Test]
        public void Test_ZPMts()
        {
            decimal zpmts = this.ActStrTotal.ZPMts;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "17 ЗП машинистов ВПТ".
        /// </summary>
        [Test]
        public void Test_ZPMvt()
        {
            decimal vt = this.ActStrTotal.ZPMvt;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "13 Сметная ЗП в НР".
        /// </summary>
        [Test]
        public void Test_ZPnr()
        {
            decimal zPnr = this.ActStrTotal.ZPnr;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "49 Пуск и регулировка".
        /// </summary>
        [Test]
        public void Test_ZPR()
        {
            decimal zpr = this.ActStrTotal.ZPR;
        }

        /// <summary>
        /// Проверяет отстутствие ошибок при обращении к "31 Запчасти".
        /// </summary>
        [Test]
        public void Test_ZTCHob()
        {
            decimal ztcHob = this.ActStrTotal.ZTCHob;
        }
    }
}
