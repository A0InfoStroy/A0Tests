// $Date: 2020-11-18 14:44:24 +0300 (Ср, 18 ноя 2020) $
// $Revision: 429 $
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