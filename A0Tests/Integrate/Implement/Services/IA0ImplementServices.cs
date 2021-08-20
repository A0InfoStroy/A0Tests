// $Date: 2021-06-07 13:29:27 +0300 (Пн, 07 июн 2021) $
// $Revision: 533 $
// $Author: eloginov $
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

            // Для создания актов со строками необходимо прочитать ЛС из БД.
            this.LS = this.Repo.LS.Load2(this.LS.ID.GUID);
            Assert.NotNull(this.LS);

            // Проверяем наличие строки в загруженной ЛС.
            Assert.Greater(this.LS.Strings.Count, 0, "В ЛС должны быть строки");
            this.LSString = this.LS.Strings.Items[0];

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
        [Test(Description = "Создание акта"), Timeout(20000)]
        public void Test_Create()
        {
            IA0Act act = this.Services.CreateAct(this.LS.Strings, this.ExecutorID, this.ContractID);
            Assert.NotNull(act);
            this.DeleteAct(act.ID.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода создания акта по ЛС с учётом опций.
        /// </summary>
        [Test(Description = "Создание акта"), Timeout(20000)]
        public void Test_FromLS()
        {
            // Создаем акт на основе ЛС.
            double heelsPercent = 10.0; // Процент от остатка объема строки ЛС
            IA0Act act = this.CreateActFromLS(heelsPercent);

            // Проверяем строки в акте.
            IA0ActString actString = null;
            for (int i = 0; i < act.Strings.Count; ++i)
            {
                if (act.Strings.Items[i].ParentStrGUID == this.LSString.GUID)
                {
                    actString = act.Strings.Items[i];
                    break;
                }
            }

            Assert.NotNull(actString, "Строка акта не создана");
            Assert.AreEqual(actString.ExecutorID, this.LSString.ExecutorID);
            Assert.AreEqual(actString.ContractID, this.LSString.ContractID);

            // Проверяем полученный объем строки акта на соответствие заданным процентам с учетом погрешности.
            Assert.AreEqual(actString.TotalVolume, this.LSString.TotalRemainder * heelsPercent / 100, Math.Pow(10, -act.Title.VolumeScale));

            // Проверяем номер родительской ЛС.
            Assert.AreEqual(actString.LSNumber, this.LSString.LSNumber);

            this.DeleteAct(act.ID.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность исполнения строки.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_ParentExecutions()
        {
            // Создаем акт на основе ЛС.
            double heelsPercent = 10.0;
            IA0Act act = this.CreateActFromLS(heelsPercent);

            Assert.Greater(act.Strings.Count, 0, "В первом акте должны быть строки");
            IA0ActString actString = act.Strings.Items[0];
            Assert.NotNull(actString, "Не могу найти первую строку акта");
            Assert.AreEqual(this.LSString.GUID, actString.ParentStrGUID, "Не могу найти строку ЛС для строки акта");

            // Создаем  второй акт на основе ЛС.
            double heelsPercent2 = 5.0;
            IA0Act act2 = this.CreateActFromLS(heelsPercent2);

            Assert.Greater(act2.Strings.Count, 0, "Во втором акте должны быть строки");
            IA0ActString actString2 = act.Strings.Items[0];
            Assert.NotNull(actString2, "Не могу найти вторую строку акта");
            Assert.AreEqual(this.LSString.GUID, actString2.ParentStrGUID, "Не могу найти строку ЛС для строки акта");

            // Перечитываем ЛС из БД для получения исполнения.
            this.LS = this.A0.Estimate.Repo.LS.Load2(this.LS.ID.GUID);
            IA0LSString parentString = this.LS.Strings.Items[0];
            Assert.Greater(parentString.Executions.Count, 0, "В строке ЛС должны быть исполнения");

            // Перечитываем акты из БД для получения родительских исполнений.
            act = this.A0.Implement.Repo.Act.Load(act.ID.GUID, EAccessKind.akRead);
            actString = act.Strings.Items[0];
            act2 = this.A0.Implement.Repo.Act.Load(act2.ID.GUID, EAccessKind.akRead);
            actString2 = act2.Strings.Items[0];

            // Исполнение по родительской строке ЛС
            IA0Executions parentExecutions = actString.ParentExecutions;
            Assert.NotNull(parentExecutions);
            Assert.Greater(parentExecutions.Count, 0, "В строке акта должны быть исполнения");
            IA0Executions parentExecutions2 = actString2.ParentExecutions;
            Assert.NotNull(parentExecutions2);
            Assert.Greater(parentExecutions2.Count, 0, "В строке акта должны быть исполнения");

            // В первом акте должно быть исполнение, соответствующее второму акту.
            IA0Execution parentExecution = parentExecutions.Item[0];
            Assert.AreEqual(heelsPercent2, parentExecution.Volume, Math.Pow(10, -act.Title.VolumeScale));

            // В втором акте должно быть исполнение, соответствующее первому акту.
            IA0Execution parentExecution2 = parentExecutions2.Item[0];
            Assert.AreEqual(heelsPercent, parentExecution2.Volume, Math.Pow(10, -act2.Title.VolumeScale));

            // Проверяем исполнения ЛС соответсвующие исполненям акта.
            for (int i = 0; i < parentString.Executions.Count; i++)
            {
                IA0Execution lsExecution = parentString.Executions.Item[i];
                if (lsExecution.ActStrGUID == actString.GUID)
                {
                    Assert.AreEqual(lsExecution.Volume, actString.Volume, Math.Pow(10, -act.Title.VolumeScale));
                    Assert.AreEqual(lsExecution.TotalVolume, actString.TotalVolume, Math.Pow(10, -act.Title.VolumeScale));
                    Assert.AreEqual(lsExecution.AdjustedVolume, actString.AdjustedVolume, Math.Pow(10, -act.Title.VolumeScale));
                }
                else if (lsExecution.ActStrGUID == actString2.GUID)
                {
                    Assert.AreEqual(lsExecution.Volume, actString2.Volume, Math.Pow(10, -act2.Title.VolumeScale));
                    Assert.AreEqual(lsExecution.TotalVolume, actString2.TotalVolume, Math.Pow(10, -act2.Title.VolumeScale));
                    Assert.AreEqual(lsExecution.AdjustedVolume, actString2.AdjustedVolume, Math.Pow(10, -act2.Title.VolumeScale));
                }
                else
                {
                    Assert.True(false, "Не найдены соответствующие исполнения акта");
                }
            }

            this.DeleteAct(act.ID.GUID);
            this.DeleteAct(act2.ID.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода создания акта по ЛС с учётом опций.
        /// </summary>
        [Test(Description = "Создание акта Татнефть"), Timeout(20000)]
        public void Test_FromLS_TN()
        {
            // Бизнес этап для новго акта
            int busOpID = this.GetBusOpID();

            // Настройки для создания акта
            IA0ImplementActCreatorOptions options = new TNImplementActCreatorOptions(
                EA0ActCreationMode.acmHeels,
                this.ExecutorID,
                this.ContractID,
                busOpID,
                EKS2InternalSplitKind.ks2ikSelling);

            IA0Act act = this.Services.FromLS(this.LS, options);
            Assert.NotNull(act);
            this.DeleteAct(act.ID.GUID);
        }

        /// <summary>
        /// Проверяет работоспособность создания текстовых строк акта.
        /// </summary>
        [Test(Description = "Создание текстовых строк"), Timeout(20000)]
        public void Test_CreateTxtStr()
        {
            // Количество создаваемых строк.
            int stringCount = 100;

            IA0Act act = this.CreateActFromLS(10.0);

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

            // Грузим акт и добавляем ещё строк.
            act = this.A0.Implement.Repo.Act.Load(act.ID.GUID, EAccessKind.akEdit);
            Assert.NotNull(act);

            // Узел для добавления строк.
            node = this.GetLastNode(act.Tree.Head);

            // Создание текстовых строк.
            for (var i = 1; i <= stringCount; i++)
            {
                IA0ActString actString = act.CreateTxtString(EA0StringKind.skZt, i.ToString(), node.ID);
                Assert.NotNull(actString);
                actString.Name = $"Текстовая строка {i + 100}";
                actString.TotalVolume = i * 10;
                actString.MUnit = "м2";
            }

            this.DeleteAct(act.ID.GUID);
        }

        /// <summary>
        /// Создает акт на основе ЛС.
        /// </summary>
        /// <param name="heelsPercent">Процент от остатка объема строки ЛС.</param>
        /// <returns>Акт, созданный на основе ЛС.</returns>
        private IA0Act CreateActFromLS(double heelsPercent)
        {
            // Настройки для создания акта как в системе А0.
            IA0ImplementActCreatorOptions options = this.Services.GetDefaultActCreateOptions(
                EA0ActCreationMode.acmHeels,
                this.LSString.ExecutorID,
                this.LSString.ContractID,
                heelsPercent);

            // Создаем акт со строками на основе ЛС.
            IA0Act act = this.Services.FromLS(this.LS, options);
            Assert.NotNull(act);
            Assert.Greater(act.Strings.Count, 0, "В акте должны быть строки");

            return act;
        }

        /// <summary>
        /// Удаляет акт из БД.
        /// </summary>
        /// <param name="actGuid">Guid акта.</param>
        private void DeleteAct(Guid actGuid)
        {
            // После создания акт будет заблокирован на редактирование,
            // для снятия блокировки надо использовать UnLock каталога актов.
            this.A0.Implement.Repo.Act.UnLock(actGuid);

            // Удаляем акт.
            this.A0.Implement.Repo.Act.Delete(actGuid);
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