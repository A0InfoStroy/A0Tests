// $Date: 2021-02-17 12:07:05 +0300 (Ср, 17 фев 2021) $
// $Revision: 524 $
// $Author: agalkin $
// Интеграционные тесты IA0ImplementActServices

namespace A0Tests.Integrate.Implement
{
    using A0Service;
    using NUnit.Framework;

    public class Test_IA0ImplementActServices : NewAct
    {
        /// <summary>
        /// Получает или устанавливает сервис акта.
        /// </summary>
        protected IA0ImplementActServices ActServices { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            this.ActServices = this.A0.Implement.Act.Services;
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ActServices);
            this.ActServices = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность групповых операций над актами.
        /// </summary>
        [Test]
        public void Test_GrOpers()
        {
            string author = "Author";
            string job = "Job";

            // Установка подписей для акта.
            this.Act.Title.Author.Name = author;
            this.Act.Title.Author.Job = job;
            this.ImplRepo.Act.Save(this.Act);

            // Создание дополнительного акта и установка подписей.
            IA0Act secondAct = this.ImplRepo.Act.New(this.LS.ID.GUID, 0, 0, 100);
            secondAct.Title.Author.Name = author;
            secondAct.Title.Author.Job = job;
            this.ImplRepo.Act.Save(secondAct);

            // Добавление идентификаторов актов в список редактируемых актов.
            IA0GUIDsEditable actGuids = new A0GUIDsEditable();
            actGuids.Add(this.Act.ID.GUID);
            actGuids.Add(secondAct.ID.GUID);

            
            for (int i = 0; i < actGuids.Count; i++)
            {
                // Разблокирование актов.
                this.ImplRepo.Act.UnLock(actGuids.Item[i]);            
            }

            // Очистка подписей в актах.
            this.ActServices.GrOpers.Title.SignsClear(actGuids);

            // Проверка выполнения групповой операции.
            for (int i = 0; i < actGuids.Count; i++)
            {
                var act = this.ImplRepo.Act.Load(actGuids.Item[i], EAccessKind.akRead);
                Assert.True(string.IsNullOrEmpty(act.Title.Author.Name));
                Assert.True(string.IsNullOrEmpty(act.Title.Author.Job));
            }

            // Удаление дополнительного акта.
            this.ImplRepo.Act.Delete(secondAct.ID.GUID);         
        }
    }
}