// $Date: 2022-07-13 21:11:02 +0300 (Ср, 13 июл 2022) $
// $Revision: 596 $
// $Author: eloginov $
// Тесты ресурсов строки (типа работа) акта.

namespace A0Tests.Integrate.Implement.ActString.ActStrResources
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Абстрактная тестовая группа (класс) проверки работоспособности списка ресурсов строки (типа работа) акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Resources строки (типа работа) акта",
        Author = "agalkin")]
    class Test_WorkActString_IA0ActResources : Test_IA0ActResourcesBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateWorkActString();
            this.Resources = this.ActString.Resources;
            Assert.NotNull(this.Resources);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Resources);
            this.Resources = null;
            base.TearDown();
        }
    }
}
