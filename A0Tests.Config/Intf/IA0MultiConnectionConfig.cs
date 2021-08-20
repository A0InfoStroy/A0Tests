// $Date: 2020-11-18 14:44:24 +0300 (Ср, 18 ноя 2020) $
// $Revision: 429 $
// $Author: agalkin $
// Интерфейсы пользовательских настроек

namespace A0Tests.Config
{
    /// <summary>
    /// Предоставляет настройки для тестов параллельных соединений с БД А0.
    /// </summary>
    public interface IA0MultiConnectionConfig
    {
        /// <summary>
        /// Получает количество одновременно устанавливаемых соединений.
        /// </summary>
        int MultiConnectCount { get; }

        /// <summary>
        /// Получает время ожидания установки соединения для параллельных соединений в мс.
        /// </summary>
        int MultiConnectTimeoutMS { get; }
    }
}