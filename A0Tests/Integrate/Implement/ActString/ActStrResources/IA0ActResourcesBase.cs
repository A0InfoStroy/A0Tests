// $Date: 2022-07-13 21:11:02 +0300 (Ср, 13 июл 2022) $
// $Revision: 596 $
// $Author: eloginov $
// Абстрактный класс тестов ресурсов строки акта

namespace A0Tests.Integrate.Implement.ActString.ActStrResources
{
    using A0Service;
    using NUnit.Framework;

    /// <summary>
    /// Абстрактная тестовая группа (класс) тестов проверки работоспособности списка ресурсов акта.
    /// </summary>
    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0Resources",
        Author = "agalkin")]
    public abstract class Test_IA0ActResourcesBase : NewActStringBase
    {
        /// <summary>
        /// Получает или устанавливает список ресурсов.
        /// </summary>
        protected IA0Resources Resources { get; set; }

        /// <summary>
        /// Проверяет наличие элементов в списке ресурсов.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Count()
        {
            Assert.Greater(this.Resources.Count, 0);
        }

        /// <summary>
        /// Проверяет работоспособность чтения элементов в списке ресурсов.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_Items()
        {
            for (var i = 0; i < this.Resources.Count; ++i)
            {
                IA0Resource resource = this.Resources.Items[i];
                Assert.NotNull(resource);
            }
        }

        /// <summary>
        /// Проверка работоспособности метода создания ресурса по обоснованию.
        /// </summary>
        [Test, Timeout(20000)]
        public virtual void Test_CreateByBasing()
        {
            string basing = "ССЦ01-101-1865";
            string nsiBase = "ТЕР-01";
            int baseID = this.A0.Sys.NSI.Services.ByMark(nsiBase);

            IA0Resource resource = this.Resources.CreateByBasing(basing, baseID);
            Assert.NotNull(resource);
        }
    }
}