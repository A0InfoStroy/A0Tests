namespace A0Tests.Integrate.Estimate
{
    using A0Service;
    using NUnit.Framework;
    using System;

    /// <summary>
    ///  Содержит тесты проверки работоспособности IA0Executions.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Executions",
        Author = "agalkin")]
    public class Test_IA0Executions : Test_LSStringCustom
    {
        /// <summary>
        ///  Получает или устанавливает выполнение.
        /// </summary>
        protected IA0Executions Executions { get; set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            Assert.IsTrue(this.LS.Strings.Count > 0, "В тестовой ЛС нет строк");
            IA0LSString lsString = this.LS.Strings.Items[0];
            Assert.IsNotNull(lsString);
            this.Executions = lsString.Executions;
            Assert.NotNull(this.Executions);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.Executions);
            this.Executions = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет количество выполнений.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Count()
        {
            int count = this.Executions.Count;
            Assert.Greater(count, 0, "В строке должно быть ненулевое выполнение");
        }

        /// <summary>
        /// Проверяет отсутствие ошибок при обращении к полям выполнения.
        /// </summary>
        [Test, Timeout(20000)]
        public void Test_Execution()
        {
            IA0Execution execution = this.Executions.Item[0];
            Assert.NotNull(execution);
            Guid actGUID = execution.ActGUID;
            Guid actStrGUID = execution.ActStrGUID;
            double adjVolume = execution.AdjustedVolume;
            string contract = execution.Contract;
            DateTime date = execution.Date;
            string executor = execution.Executor;
            EIncludeKind includeKind = execution.IncludeKind;
            string mark = execution.Mark;
            double totalVolume = execution.TotalVolume;
            double volume = execution.Volume;
        }
    }
}
