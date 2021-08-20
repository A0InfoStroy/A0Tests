// $Date: 2020-12-28 15:10:25 +0300 (Пн, 28 дек 2020) $
// $Revision: 476 $
// $Author: agalkin $
// Тесты Сервисов сметного домена

namespace A0Tests.LateTests.Integrate.Estimate
{
    using System;
    using System.Data.OleDb;
    using ADODB;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности службы домена сметных данных.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0EstimateServices",
        Author = "agalkin")]
    public class Test_IA0EstimateServices : Test_Estimate
    {
        /// <summary>
        /// Получает или устанавливает службу домена сметных данных.
        /// </summary>
        protected dynamic Services { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Services = this.A0.Estimate.Services;
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
        /// Проверяет работоспособность метода назначения бизнес-стадии.
        /// </summary>
        [Test(Description = "Назначение бизнес стадии")]
        public void Test_SetBusinessStage()
        {
            Assert.Greater(this.A0.Sys.Repo.BussinnessStages.Count, 0);

            // Комплекс для тестирования
            dynamic complex = this.A0.Estimate.Repo.Complex.New2(this.HeadComplexGuid, this.HeadNodeID);
            Guid complexGUID = complex.ID.GUID;
            this.A0.Estimate.Repo.Complex.Save(complex);

            // Проект для тестирования
            dynamic project = this.A0.Estimate.Repo.Proj.New();
            Guid projGUID = project.ID.GUID;
            this.A0.Estimate.Repo.Proj.Save(project);

            // ОС для тестирования
            dynamic os = this.A0.Estimate.Repo.OS.New(projGUID, this.HeadNodeID);
            Guid osGUID = os.ID.GUID;
            this.A0.Estimate.Repo.OS.Save(os);

            // ЛС для тестирования
            dynamic ls = this.A0.Estimate.Repo.LS.New(osGUID, this.HeadNodeID);
            Guid lsGUID = ls.ID.GUID;
            this.A0.Estimate.Repo.LS.Save(ls);

            // Акт для тестирования
            dynamic act = this.A0.Implement.Repo.Act.New(lsGUID, 0, 0, 100);
            Guid actGUID = act.ID.GUID;
            this.A0.Implement.Repo.Act.Save(act);
            dynamic bsFirst = null;
            try
            {
                for (int i = 0; i < this.A0.Sys.Repo.BussinnessStages.Count; ++i)
                {
                    dynamic bs = this.A0.Sys.Repo.BussinnessStages.Item[i];
                    switch (bs.Kind)
                    {
                        case 1: // EA0EntityKind.ekComplex
                            if (bsFirst == null)
                            {
                                bsFirst = bs;
                            }

                            this.Services.SetBusinessStage(1, complexGUID, bs.ID);
                            break;
                        case 2: // EA0EntityKind.ekProject
                            this.Services.SetBusinessStage(2, projGUID, bs.ID);
                            break;
                        case 3: // EA0EntityKind.ekOS
                            this.Services.SetBusinessStage(3, osGUID, bs.ID);
                            break;
                        case 4: // EA0EntityKind.ekLS
                            this.Services.SetBusinessStage(4, lsGUID, bs.ID);
                            break;
                        case 5: // EA0EntityKind.okAct
                            this.Services.SetBusinessStage(5, actGUID, bs.ID);
                            break;
                        default:
                            throw new Exception("Тип объекта не соотвествует ожидаемым");
                    }
                }
            }
            finally
            {
                // Возврат на первый бизнес этап, чтобы была разрешена операция удаления.
                this.Services.SetBusinessStage(1, complexGUID, bsFirst.ID);

                this.A0.Estimate.Repo.Complex.Delete(complexGUID);
            }
        }

        /// <summary>
        /// Проверяет работоспособность метода получения Id договора по его номеру в случае отсутствия договора в БД.
        /// </summary>
        [Test(Description = "ИД отсутствующего договора по номеру")]
        public void Test_ContractIDByNumberNone()
        {
            int id = this.Services.ContractIDByNumber("Этого договора нет в БД " + Guid.NewGuid().ToString(), Guid.Empty);
            Assert.AreEqual(0, id);
        }

        /// <summary>
        /// Проверяет работоспособность метода получения Id договора по его номеру.
        /// </summary>
        [Test(Description = "ИД договора по номеру")]
        public void Test_ContractIDByNumber()
        {
            // Получаем из таблицы БД ИД договора, GUID проекта и номер.
            // Отправляем запрос в A0Service.
            // Сравниваем результат.
            // Тест может не проходить, если Номер договора не уникальный в проекте.

            // Соединение с БД из A0Service/
            _Connection mainConnection = this.A0.App.Connection.MainConnection as _Connection;

            // Соединение с БД для получения данных в обход A0Service.
            using (OleDbConnection connection = new OleDbConnection(mainConnection.ConnectionString))
            {
                connection.Open();

                using (OleDbCommand command = new OleDbCommand("SELECT ContractID, ProjGUID, Number FROM Contracts", connection))
                using (var reader = command.ExecuteReader())
                {
                    Assert.True(reader.HasRows, "В системе ожидается наличие договоров");

                    while (reader.Read())
                    {
                        int contractID = reader.GetInt32(0);
                        Guid projGUID = reader.GetGuid(1);
                        string number = reader.GetString(2);

                        // Отправка запроса в A0Service.
                        int id = this.Services.ContractIDByNumber(number, projGUID);
                        Assert.AreEqual(contractID, id);
                    }
                }
            }
        }
    }
}