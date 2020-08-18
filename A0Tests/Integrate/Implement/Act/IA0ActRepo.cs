// $Date: 2020-08-05 10:48:22 +0300 (Ср, 05 авг 2020) $
// $Revision: 342 $
// $Author: agalkin $
// Тесты каталога Актов

namespace A0Tests.Integrate.Implement
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности каталога актов.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0ActRepo",
        Author = "agalkin")]
    public class Test_IA0ActRepo : NewAct
    {
        /// <summary>
        /// Получает или устанавливает каталог актов.
        /// </summary>
        protected IA0ActRepo ActRepo { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.ActRepo = this.ImplRepo.Act;
            Assert.NotNull(this.ActRepo);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.ActRepo);
            this.ActRepo = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность метода удаления акта.
        /// </summary>
        [Test(Description = "Удаление")]
        public void Test_Delete()
        {
            this.ActRepo.Delete(this.Act.ID.GUID);

            // Попытка загрузить после удаления.
            try
            {
                IA0Act loadedAct = this.ActRepo.Load(this.Act.ID.GUID, EAccessKind.akEdit);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                Assert.AreEqual((uint)ex.HResult, 0x80004005);
            }
        }

        /// <summary>
        /// Проверяет корректность редактирования данных акта.
        /// </summary>
        [Test(Description = "Изменение/Загрузка")]
        public void Test_UpdateRead()
        {
            // Изменение.
            string newName = this.Act.Title.Name + " Изменено";
            this.Act.Title.Name = newName;
            this.ActRepo.Save(this.Act);

            // Проверка.
            this.Act = this.ActRepo.Load(this.Act.ID.GUID, EAccessKind.akEdit);
            Assert.AreEqual(this.Act.Title.Name, newName);
        }

        /// <summary>
        /// Проверяет работоспособность концевика акта.
        /// </summary>
        [Test(Description = "Загрузка акта с концевиком")]
        public void Test_Load()
        {
            Guid actGUID = Guid.Parse("{D88F7747-B361-494D-83B2-247E85F3903A}");
            IA0Act act = this.ActRepo.Load(actGUID, EAccessKind.akRead);
            string name;
            for (var i = 0; i < act.Tree.Head.Prog.Count(); ++i)
            {
                IA0ProgLine pl = act.Tree.Head.Prog.GetItem(i);

                name = pl.Name();
                Assert.NotNull(name);

                if (pl.Kind() == EProgLineKind.plkDiv)
                {
                    // По исторической несправедливости функции
                    // Active(), Printing(), Summary()
                    // возвращают тип HRESULT.
                    // Значение
                    // S_OK    = $00000000;
                    // S_FALSE = $00000001;
                    IA0ProgLineDiv pld = pl as IA0ProgLineDiv;
                    var active = pld.Active();
                    var factor = pld.Factor;
                    var factorCaption = pld.FactorCaption();
                    var factorKind = pld.FactorKind;
                    var number = pld.Number();
                    var printing = pld.Printing();
                    var summary = pld.Summary();
                    var total = pld.Total();
                }
            }

            this.ActRepo.UnLock(actGUID);
        }

        /// <summary>
        /// Проверяет работоспособность метода копирования акта.
        /// </summary>
        [Test(Description = "Копирование")]
        public void Test_Copy()
        {
            IA0Act copyAct = this.ActRepo.Copy(this.Act.ID.GUID, this.LS.ID.GUID);
            Assert.NotNull(copyAct);
            Assert.AreEqual(this.Act.Title.Name, copyAct.Title.Name);
            this.ActRepo.UnLock(copyAct.ID.GUID);
            this.ActRepo.Delete(copyAct.ID.GUID);
        }
    }
}