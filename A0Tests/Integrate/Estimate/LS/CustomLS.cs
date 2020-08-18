// $Date: 2020-07-28 13:37:37 +0300 (Вт, 28 июл 2020) $
// $Revision: 321 $
// $Author: agalkin $

namespace A0Tests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс для тестирования ЛС и частей ЛС.
    /// </summary>
    public abstract class Test_LSCustom : Test_EstimateCustom
    {
        /// <summary>
        /// Получает или устанавливает локальную смету.
        /// </summary>
        protected IA0LS LS { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LS = this.A0.Estimate.Repo.LS.Load2(this.GetLSGUID());
            Assert.NotNull(this.LS);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            this.LS = null;
            base.TearDown();
        }

        /// <summary>
        /// Получает Guid ЛС из БД.
        /// </summary>
        /// <returns>Guid ЛС.</returns>
        protected virtual Guid GetLSGUID() => Guid.Parse("75AB59AA-59DD-4DFA-8CB9-4BFA53A47A0A");
    }

    /// <summary>
    ///  Базовый класс для тестирования строки ЛС.
    /// </summary>
    public abstract class Test_LSStringCustom : Test_LSCustom
    {
    }

    /// <summary>
    ///  Базовый класс для тестирования ресурса строки ЛС.
    /// </summary>
    public abstract class Test_LSResourceCustom : Test_LSCustom
    {
    }
}