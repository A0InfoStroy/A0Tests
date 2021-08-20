// $Date: 2021-01-26 13:18:45 +0300 (Вт, 26 янв 2021) $
// $Revision: 502 $
// $Author: agalkin $

namespace A0Tests.EarlyTests.Integrate.Estimate
{
    using System;
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    ///  Базовый класс для тестирования ЛС и частей ЛС.
    /// </summary>
    public abstract class Test_LSCustom : Test_Estimate
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