// $Date: 2022-07-13 21:09:52 +0300 (Ср, 13 июл 2022) $
// $Revision: 595 $
// $Author: eloginov $
// Тесты ресурса строки (типа работа) акта

namespace A0Tests.Integrate.Implement.ActString.ActStrResource
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности ресурса строки (типа работа) акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Resource для строки (типа работа) акта",
        Author = "agalkin")]
    class Test_WorkActString_IA0ActResourceBase : Test_IA0ActResourceBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateWorkActString();
            Assert.IsTrue(this.ActString.Resources.Count > 0, "В тестовой строке нет ресурсов");
            this.Resource = this.ActString.Resources.Items[0];
            Assert.NotNull(this.Resource);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Resource);
            this.Resource = null;
            base.TearDown();
        }
    }
}
