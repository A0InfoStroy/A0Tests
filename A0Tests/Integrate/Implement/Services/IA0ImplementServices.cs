// $Date: 2020-08-06 13:44:47 +0300 (Чт, 06 авг 2020) $
// $Revision: 347 $
// $Author: agalkin $
// Базовые тесты IA0ImplementServices

namespace A0Tests.Integrate.Implement
{
    using System;
    using System.Collections.Generic;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит настройки для создания акта.
    /// </summary>
    public class TNImplementActCreatorOptions : IA0ImplementActCreatorOptions,
        IA0ImplementStringSelector,
        IA0ImplementStringChange
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="TNImplementActCreatorOptions"./>
        /// </summary>
        /// <param name="mode">Режим создания акта.</param>
        /// <param name="executor">Исполнитель.</param>
        /// <param name="contract">Договор.</param>
        /// <param name="busOpID">Бизнес этап.</param>
        /// <param name="kS2InternalSplitKind">Расширенный тип разреза КС-2.</param>
        public TNImplementActCreatorOptions(
            EA0ActCreationMode mode,
            int executor,
            int contract,
            int busOpID,
            EKS2InternalSplitKind kS2InternalSplitKind)
        {
            this.Mode = mode;
            this.Executor = executor;
            this.Contract = contract;
            this.BusOpID = busOpID;
            this.KS2InternalSplitKind = kS2InternalSplitKind;
        }

        /// <summary>
        /// Получает или устанавливает режим создания актов.
        /// </summary>
        public EA0ActCreationMode Mode { get; private set; }

        /// <summary>
        /// Получает или устанавливает исполнителя.
        /// </summary>
        public int Executor { get; private set; }

        /// <summary>
        /// Получает или устанавливает договор.
        /// </summary>
        public int Contract { get; private set; }

        /// <summary>
        /// Получает или устанавливает бизнес этап.
        /// </summary>
        public int BusOpID { get; private set; }

        /// <summary>
        /// Получает или устанавливает режим выбора строк.
        /// </summary>
        public IA0ImplementStringSelector Selector => this;

        /// <summary>
        /// Получает или устанавливает режим изменения строк.
        /// </summary>
        public IA0ImplementStringChange Change => this;

        /// <summary>
        /// Получает или устанавливает расширенный тип разреза КС-2.
        /// </summary>
        public EKS2InternalSplitKind KS2InternalSplitKind { get; private set; }

        /// <summary>
        /// Проверяет соответствие исполнителя, договора и тип учета строки.
        /// </summary>
        /// <param name="LSString">Строка ЛС.</param>
        /// <returns>Признак выполнения условий.</returns>
        public bool Select(IA0LSString LSString)
        {
            return (LSString.IncludeKind != EIncludeKind.ikIgnore)
                && (LSString.ExecutorID == this.Executor)
                && (LSString.ContractID == this.Contract);
        }

        /// <summary>
        /// Пересчитывает объем ресурсов строки акта.
        /// </summary>
        /// <param name="actString">Строка акта.</param>
        /// <param name="act">Акт.</param>
        /// <param name="ls">Локальная смета.</param>
        /// <param name="lsString">Строка ЛС.</param>
        void IA0ImplementStringChange.Change(IA0ActString actString, IA0Act act, IA0LS ls, IA0LSString lsString)
        {
            // Расчитаем новый объем для строки акта
            actString.Formula = (lsString.TotalVolume / 10.0).ToString();

            // Ресурсы
            for (int i = 0; i < actString.Resources.Count; i++)
            {
                IA0Resource res = actString.Resources.Items[i];
                res.Volume_Fact = res.Volume;
                if (actString.StringKind != EA0StringKind.skWork)
                {
                    res.Volume_Fact = actString.Volume;
                }
            }
        }
    }

    /// <summary>
    /// Содержит тесты проверки работоспособности сервисов акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ImplementServices",
        Author = "agalkin")]
    public class Test_IA0ImplementServices : NewLSString
    {
        /// <summary>
        /// Получает или устанавливает сервис акта.
        /// </summary>
        protected IA0ImplementServices Services { get; private set; }

        /// <summary>
        /// Получает Id исполнителя из БД.
        /// </summary>
        private int ExecutorID => 85;

        /// <summary>
        /// Получает Id договора из БД.
        /// </summary>
        private int ContractID => 410;

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            this.Services = this.A0.Implement.Services;
            Assert.NotNull(this.Services);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Services);
            this.Services = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность метода создания акта.
        /// </summary>
        [Test(Description = "Создание акта")]
        public void Test_Create()
        {
            // На основе этой ЛС создаем акт
            IA0LS ls = this.A0.Estimate.Repo.LS.Load2(this.LS.ID.GUID);
            Assert.NotNull(ls);

            IA0Act act = this.Services.CreateAct(ls.Strings, this.ExecutorID, this.ContractID);
            Assert.NotNull(act);
            this.A0.Implement.Repo.Act.UnLock(act.ID.GUID);

            // Удаляем акт
            this.A0.Implement.Repo.Act.Delete(act.ID.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода создания акта по ЛС с учётом опций.
        /// </summary>
        [Test(Description = "Создание акта")]
        public void Test_FromLS()
        {
            // На основе этой ЛС создаем акт
            IA0LS ls = this.A0.Estimate.Repo.LS.Load2(this.LS.ID.GUID);
            Assert.NotNull(ls);
            Assert.Greater(ls.Strings.Count, 0, "В ЛС должны быть строки");

            // Для этой строки ЛС должна быть строка акта.
            IA0LSString parentStr = ls.Strings.Items[0];
            int executorID = parentStr.ExecutorID;
            var contractID = parentStr.ContractID;
            double heelsPercent = 10.0;

            // Настройки для создания акта как в системе А0.
            IA0ImplementActCreatorOptions options = this.Services.GetDefaultActCreateOptions(
                EA0ActCreationMode.acmHeels,
                executorID,
                contractID,
                heelsPercent);

            IA0Act act = this.Services.FromLS(ls, options);
            Assert.NotNull(act);
            Assert.Greater(act.Strings.Count, 0, "В акте должны быть строки");

            // Проверяем созданные строки в акте
            IA0ActString actString = null;

            for (int i = 0; i < act.Strings.Count; ++i)
            {
                if (act.Strings.Items[i].ParentStrGUID == parentStr.GUID)
                {
                    actString = act.Strings.Items[i];
                    break;
                }
            }

            Assert.NotNull(actString, "Строка акта не создана");
            Assert.AreEqual(actString.ExecutorID, executorID);
            Assert.AreEqual(actString.ContractID, contractID);
            Assert.AreEqual(actString.Volume, parentStr.TotalRemainder / heelsPercent, Math.Pow(10, -act.Title.VolumeScale));
            Assert.AreEqual(actString.ParentTotalVolume, parentStr.AdjustedVolume);
            Assert.AreEqual(actString.LSNumber, parentStr.LSNumber);

            // После создания акт будет заблокирован на редактирование,
            // для снятия блокровки надо использовать UnLock каталога актов.
            this.A0.Implement.Repo.Act.UnLock(act.ID.GUID);

            // Удаляем акт
            this.A0.Implement.Repo.Act.Delete(act.ID.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода создания акта по ЛС с учётом опций.
        /// </summary>
        [Test(Description = "Создание акта Татнефть")]
        public void Test_FromLS_TN()
        {
            // На основе этой ЛС создаем акт
            IA0LS ls = this.A0.Estimate.Repo.LS.Load2(this.LS.ID.GUID);
            Assert.NotNull(ls);

            // Бизнес этап для новго акта
            int busOpID = this.GetBusOpID();

            // Настройки для создания акта
            IA0ImplementActCreatorOptions options = new TNImplementActCreatorOptions(
                EA0ActCreationMode.acmHeels,
                this.ExecutorID,
                this.ContractID,
                busOpID,
                EKS2InternalSplitKind.ks2ikSelling);

            IA0Act act = this.Services.FromLS(ls, options);
            Assert.NotNull(act);
            this.A0.Implement.Repo.Act.UnLock(act.ID.GUID);

            // Удаляем акт
            this.A0.Implement.Repo.Act.Delete(act.ID.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность создания текстовых строк акта.
        /// </summary>
        [Test(Description = "Создание текстовых строк")]
        public void Test_CreateTxtStr()
        {
            // Количество создаваемых строк.
            int stringCount = 100;

            // На основе этой ЛС создаем акт.
            IA0LS ls = this.A0.Estimate.Repo.LS.Load2(this.LS.ID.GUID);
            Assert.NotNull(ls);

            // Настройки для создания акта как в системе А0
            IA0ImplementActCreatorOptions options = this.Services.GetDefaultActCreateOptions(
                EA0ActCreationMode.acmHeels,
                this.ExecutorID,
                this.ContractID,
                10);

            IA0Act act = this.Services.FromLS(ls, options);
            Assert.NotNull(act);

            // Узел для добавления строк.
            IA0TreeNode node = this.GetLastNode(act.Tree.Head);

            // Создание текстовых строк.
            for (var i = 1; i <= stringCount; i++)
            {
                IA0ActString actString = act.CreateTxtString(EA0StringKind.skZt, i.ToString(), node.ID);
                Assert.NotNull(actString);
                actString.Name = string.Format("Текстовая строка {0}", i);
                actString.TotalVolume = i * 10;
                actString.MUnit = "м2";
            }

            this.A0.Implement.Repo.Act.Save(act);
            this.A0.Implement.Repo.Act.UnLock(act.ID.GUID);

            // Грузим акт и добавляем ещё строк
            act = this.A0.Implement.Repo.Act.Load(act.ID.GUID, EAccessKind.akEdit);
            Assert.NotNull(act);

            // Узел для добавления строк
            node = this.GetLastNode(act.Tree.Head);

            // Создание текстовых строк
            for (var i = 1; i <= stringCount; i++)
            {
                IA0ActString actString = act.CreateTxtString(EA0StringKind.skZt, i.ToString(), node.ID);
                Assert.NotNull(actString);
                actString.Name = $"Текстовая строка {i + 100}";
                actString.TotalVolume = i * 10;
                actString.MUnit = "м2";
            }

            this.A0.Implement.Repo.Act.Save(act);
            this.A0.Implement.Repo.Act.UnLock(act.ID.GUID);

            // Удаляем акт
            this.A0.Implement.Repo.Act.Delete(act.ID.GUID);
        }

        /// <summary>
        /// Получает последний узел в дереве.
        /// </summary>
        private IA0TreeNode GetLastNode(IA0TreeNode node)
        {
            return node.Count > 0 ? this.GetLastNode(node.Item[node.Count - 1]) : node;
        }

        /// <summary>
        /// Получает Id бизнес стадии для акта.
        /// </summary>
        /// <returns>Id бизнес стадии.</returns>
        private int GetBusOpID()
        {
            List<IA0BussinessStage> stages = new List<IA0BussinessStage>();

            IA0BussinessStages bo = this.A0.Sys.Repo.BussinnessStages;
            for (int i = 0; i < bo.Count; i++)
            {
                if (bo.Item[i].Kind == EA0ObjectKind.okAct)
                {
                    stages.Add(bo.Item[i]);
                }
            }

            return stages[0].ID;
        }
    }
}