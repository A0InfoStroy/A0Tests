// $Date: 2022-07-13 21:03:35 +0300 (Ср, 13 июл 2022) $
// $Revision: 590 $
// $Author: eloginov $
// Тесты IA0ImplementServices для строк (типа ресурс)

namespace A0Tests.Integrate.Implement.Services.ImplementServices
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности сервисов актов для строк (типа ресурс).
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ImplementServices для (типа ресурс)",
        Author = "agalkin")]
    class ResLSString_IA0ImplementServices : Test_IA0ImplementServicesBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            CreateResLSString();

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
        /// Проверяет работоспособность создания строк работы акта.
        /// </summary>
        [Test(Description = "Создание строк работы"), Timeout(60000)]
        public void Test_CreateResStr()
        {
            // Количество создаваемых строк.
            int stringCount = 100;

            IA0Act act = this.CreateActFromLS(10.0);

            // Узел для добавления строк.
            IA0TreeNode node = this.GetLastNode(act.Tree.Head);

            // Создание строк (типа ресурс).
            for (var i = 1; i <= stringCount; i++)
            {
                IA0ActString actString = act.CreateResString(aNSIID: 7, aFolderID: 907, aResID: 6197091, aNodeID: node.ID);
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

            // Создание текстовых (типа ресурс).
            for (var i = 1; i <= stringCount; i++)
            {
                IA0ActString actString = act.CreateResString(aNSIID: 7, aFolderID: 907, aResID: 6197091, aNodeID: node.ID);
                Assert.NotNull(actString);
                actString.Name = $"Текстовая строка {i + 100}";
                actString.TotalVolume = i * 10;
                actString.MUnit = "м2";
            }

            this.DeleteAct(act.ID.GUID);
        }
    }
}
