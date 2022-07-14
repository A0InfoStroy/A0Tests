// $Date: 2022-07-14 13:08:20 +0300 (Чт, 14 июл 2022) $
// $Revision: 601 $
// $Author: eloginov $
// Подмена селектора IA0ImplementStringSelector для создания строк акта с переданными парами исполнитель/договор.

namespace A0Tests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;
    using System;

    /// <summary>
    /// Содержит настройки для создания акта.
    /// Принимает опции по умолчанию, полученные от <seealso cref="IA0ImplementServices.GetDefaultActCreateOptions(A0Service.EA0ActCreationMode, int, int, double)"/>
    /// и заменяет селектор <see cref="IA0ImplementStringSelector"/>.
    /// </summary>
    public class ActCreatorOptionsCustom : IA0ImplementActCreatorOptions, IA0ImplementStringSelector
    {
        /// <summary>
        /// Стандартные опции для создания акта, полученные от 
        /// </summary>
        private IA0ImplementActCreatorOptions DefaultOptions { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="ActCreatorOptionsCustom"./>
        /// </summary>
        public ActCreatorOptionsCustom(IA0ImplementActCreatorOptions defaultOptions)
        {
            this.DefaultOptions = defaultOptions;
        }

        /// <summary>
        /// Получает или устанавливает режим создания актов.
        /// </summary>
        public EA0ActCreationMode Mode => DefaultOptions.Mode;

        /// <summary>
        /// Получает или устанавливает исполнителя.
        /// </summary>
        public int Executor => DefaultOptions.Executor;

        /// <summary>
        /// Получает или устанавливает договор.
        /// </summary>
        public int Contract => DefaultOptions.Contract;

        /// <summary>
        /// Получает или устанавливает бизнес этап.
        /// </summary>
        public int BusOpID => DefaultOptions.BusOpID;

        /// <summary>
        /// Получает или устанавливает режим изменения строк.
        /// </summary>
        public IA0ImplementStringChange Change => DefaultOptions.Change;

        /// <summary>
        /// Получает или устанавливает расширенный тип разреза КС-2.
        /// </summary>
        public EKS2InternalSplitKind KS2InternalSplitKind => DefaultOptions.KS2InternalSplitKind;

        /// <summary>
        /// Получает или устанавливает режим выбора строк.
        /// </summary>
        public IA0ImplementStringSelector Selector => this;

        /// <summary>
        /// Селектор всегда возвращает валидность строки на добавление в акт.
        /// </summary>
        bool IA0ImplementStringSelector.Select(IA0LSString LSString)
        {
            return true;
        }
    }

    /// <summary>
    /// Содержит тесты проверки работоспособности модифицированного селектора <seealso cref="IA0ImplementStringSelector"/>.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности модифицированного селектора IA0ImplementStringSelector",
        Author = "eloginov")]
    public class Test_ActCreatorOptionsCustom : NewLSString
    {
        /// <summary>
        /// Получает или устанавливает сервис акта.
        /// </summary>
        protected IA0ImplementServices Services { get; private set; }

        /// <summary>
        /// Исполнитель.
        /// </summary>
        private const int executorID = 84;

        /// <summary>
        /// Договор.
        /// </summary>
        private const int contractID = 312;

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            // Загрузка ЛС со строками с разными парами исполнитель/договор из БД, 
            // должна быть создана заранее в A0.
            this.LS = this.Repo.LS.Load2(new Guid("50BE0F5D-7267-4D5C-9C3F-3CBD2BF5B0ED"));

            // Проверяем наличие строки в загруженной ЛС.
            Assert.Greater(this.LS.Strings.Count, 0, "В ЛС должны быть строки");

            IA0LSString str1 = LS.Strings.Items[0];
            IA0LSString str2 = LS.Strings.Items[1];

            // Проверяем, что строки ЛС содержат разные пары исполнитель/договор
            Assert.AreNotEqual(str1.ExecutorID, str2.ExecutorID, "Строки акта содержат идентичный договор");
            Assert.AreNotEqual(str1.ContractID, str2.ContractID, "Строки акта содержат идентичного исполнителя");

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
        /// Создает акт на основе ЛС.
        /// </summary>
        /// <param name="heelsPercent">Процент от остатка объема строки ЛС.</param>
        /// <returns>Акт, созданный на основе ЛС.</returns>
        private IA0Act CreateActFromLSCustomSelector(double heelsPercent)
        {
            Assert.NotNull(this.LS);

            // В GetDefaultActCreateOptions() передаются необходимые пары договор/исполнитель.
            IA0ImplementActCreatorOptions options = new ActCreatorOptionsCustom(this.Services.GetDefaultActCreateOptions(
                EA0ActCreationMode.acmHeels,
                executorID,
                contractID,
                heelsPercent));

            // Создаем акт со строками на основе ЛС.
            IA0Act act = this.Services.FromLS(this.LS, options);

            return act;
        }

        /// <summary>
        /// Проверяет, что все строки акта содержат переданную пару исполнитель/договор.
        /// </summary>
        [Test(Description = "Проверяет, что все строки акта содержат переданную пару исполнитель/договор"), Timeout(50000)]
        public void Test_CustomStringSelector()
        {
            // Создаем акт на основе ЛС.
            double heelsPercent = 10.0; // Процент от остатка объема строки ЛС
            IA0Act act = this.CreateActFromLSCustomSelector(heelsPercent);

            //Проверка всех строк акта на совпадение переданной паре исполнитель/договор.
            for (int i = 0; i < LS.Strings.Count; i++)
            {
                Assert.AreEqual(executorID, act.Strings.Items[i].ExecutorID, "Исполнитель строки акта не совпадает с переданным");
                Assert.AreEqual(contractID, act.Strings.Items[i].ContractID, "Договор строки акта не совпадает с переданным");
            }

            this.DeleteAct(act.ID.GUID);
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
    }

}
