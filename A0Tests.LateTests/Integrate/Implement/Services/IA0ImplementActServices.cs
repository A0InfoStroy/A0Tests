// $Date: 2021-02-19 15:50:23 +0300 (Пт, 19 фев 2021) $
// $Revision: 527 $
// $Author: agalkin $
// Интеграционные тесты IA0ImplementActServices

namespace A0Tests.LateTests.Integrate.Implement
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class Test_IA0ImplementActServices : Test_NewAct
    {
        /// <summary>
        /// Получает или устанавливает сервис акта.
        /// </summary>
        protected dynamic ActServices { get; private set; }

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
            dynamic secondAct = this.ImplRepo.Act.New(this.LS.ID.GUID, 0, 0, 100);
            secondAct.Title.Author.Name = author;
            secondAct.Title.Author.Job = job;
            this.ImplRepo.Act.Save(secondAct);

            // Добавление идентификаторов актов в список редактируемых актов.
            IA0GUIDs actGuids = new A0GUIDs();
            actGuids.Add(this.Act.ID.GUID);
            actGuids.Add(secondAct.ID.GUID);


            for (int i = 0; i < actGuids.Count; i++)
            {
                // Разблокирование актов.
                this.ImplRepo.Act.UnLock(actGuids.get_Item(i));
            }

            // Очистка подписей в актах.
            this.ActServices.GrOpers.Title.SignsClear(actGuids);

            // Проверка выполнения групповой операции.
            for (int i = 0; i < actGuids.Count; i++)
            {
                var act = this.ImplRepo.Act.Load(actGuids.get_Item(i), 0);
                Assert.True(string.IsNullOrEmpty(act.Title.Author.Name));
                Assert.True(string.IsNullOrEmpty(act.Title.Author.Job));
            }

            // Удаление дополнительного акта.
            this.ImplRepo.Act.Delete(secondAct.ID.GUID);
        }
    }

    public interface IA0GUIDs
    {
        Guid get_Item(int Index);
        void Add(Guid GUID);
        void Delete(int Index);
        int Count { get; }
    }

    public class A0GUIDs : IA0GUIDs
    {
        private List<Guid> guids;

        public A0GUIDs()
        {
            this.guids = new List<Guid>();
        }

        public int Count => this.guids.Count;

        public void Add(Guid GUID)
        {
            this.guids.Add(GUID);
        }

        public void Delete(int Index)
        {
            this.guids.RemoveAt(Index);
        }

        public Guid get_Item(int Index) => this.guids[Index];

    }
}