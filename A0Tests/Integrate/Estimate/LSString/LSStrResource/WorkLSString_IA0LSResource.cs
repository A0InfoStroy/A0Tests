// $Date: 2022-07-13 20:59:22 +0300 (Ср, 13 июл 2022) $
// $Revision: 588 $
// $Author: eloginov $
// Тесты ресурса строки (типа работа) локальных смет.

namespace A0Tests.Integrate.Estimate.LSString.LSStrResource
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Содержит тесты проверки работоспособности ресурса строки (типа работа) ЛС.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Resource строки (типа работа) ЛС",
        Author = "agalkin")]
    public class Test_WorkLSString_IA0LSResource : Test_IA0LSResourceBase
    {
        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            CreateWorkLSString();
            Assert.IsTrue(this.LSString.Resources.Count > 0, "В тестовой строке нет ресурсов");
            this.Resource = this.LSString.Resources.Items[0];
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
