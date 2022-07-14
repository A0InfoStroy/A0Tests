// $Date: 2020-11-18 15:20:02 +0300 (Ср, 18 ноя 2020) $
// $Revision: 430 $
// $Author: agalkin $
// Интерфейсы пользовательских настроек

namespace A0Tests.Config
{
    /// <summary>
    /// Предоставляет настройки тестов соединения с БД А0.
    /// </summary>
    public interface IA0ConnectionConfig
    {
        /// <summary>
        /// Получает время ожидания установки соединения в мс.
        /// </summary>
        int ConnectTimeoutMS { get; }
    }
}