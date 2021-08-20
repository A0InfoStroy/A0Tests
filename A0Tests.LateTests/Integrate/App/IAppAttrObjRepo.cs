// $Date: 2020-12-30 17:37:30 +0300 (Ср, 30 дек 2020) $
// $Revision: 483 $
// $Author: agalkin $
// Тесты каталога объектов с атрибутами

namespace A0Tests.LateTests.Integrate.App
{
    using System.Collections.Generic;
    using NUnit.Framework;

    /// <summary>
    ///  Содержит тесты проверки работоспособности IA0AttributeRepo.
    /// </summary>
    [TestFixture(Category = "Integrate",
        Description = "Тесты проверки работоспособности IAppAttrObjRepo",
        Author = "agalkin")]
    public class Test_IAppAttrObjRepo : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает значение ссылки на каталог объектов с аттрибутами.
        /// </summary>
        protected dynamic Repo { get; private set; }

        /// <summary>
        /// Получает или устанавливает значение множества объектов с аттрибутами.
        /// </summary>
        protected HashSet<string> AttrObjs { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.Repo = this.A0.App.Attributes.Repo;
            Assert.NotNull(this.Repo);

            // Объекты с атрибутами, присутствующие в приложении
            this.AttrObjs = new HashSet<string>(new[]
            {
                    "A0LSString",
                    "A0Resource",
                    "A0LSStrBasing",
                    "A0LSStrTotal",
                    "A0LSTree",
                    "A0ActTotal",
                    "A0LSTotal",
                    "A0ProjTotal",
                    "A0OSTotal",
                    "A0ComplexTotal",
                    "A0LSTitle",
                    "A0ActTitle",
                    "A0OSTitle",
                    "A0ProjTitle",
                    "A0ComplexTitle",
                    "A0ActString",
                    "A0ActTree",
                    "A0Contract",
                    "A0SysOrgTreeNode",
                    "A0SysOrgTree",
                    "A0SysExecutor",
                    "A0SysEmployee",
            });
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Repo);
            this.Repo = null;
            base.TearDown();
        }

        /// <summary>
        /// Тестирует наличие аттрибутов в каталоге.
        /// </summary>
        [Test(Description = "Получение объектов с атрибутами")]
        public void Test_Read()
        {
            var it = this.Repo.Read();
            dynamic attrObj = it.Next();

            // Должен быть хотя бы один объект.
            Assert.NotNull(attrObj);

            while (attrObj != null)
            {
                string name = attrObj.Name;
                Assert.True(name.Length > 0);

                // Если в приложении есть объект отсутсвующий в AttrObjs
                Assert.True(this.AttrObjs.Contains(name), "Объект {0} отсутствует в списке", name);
                this.AttrObjs.Remove(name);

                attrObj = it.Next();
            }

            // Если в приложении определены не все объекты из AttrObjs
            Assert.Zero(this.AttrObjs.Count, "Остались предопределенные объекты");
        }

        /// <summary>
        /// Тестирует получение объектов с аттрибутами из каталога.
        /// </summary>
        [Test(Description = "Получение объектов с атрибутами")]
        public void Test_Get()
        {
            int errorsCount = 0;
            foreach (var attrObj in this.AttrObjs)
            {
                try
                {
                    dynamic attr = this.Repo.Get(attrObj);
                    Assert.NotNull(attr);
                }
                catch (System.Exception)
                {
                    errorsCount++;
                    continue;
                }
            }

            Assert.Zero(errorsCount);
        }
    }
}