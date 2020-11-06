// $Date: 2020-11-06 14:27:26 +0300 (Пт, 06 ноя 2020) $
// $Revision: 414 $
// $Author: agalkin $
// Тесты событий протокола.

namespace A0Tests.Integrate
{
    using A0Service;
    using NUnit.Framework;
    using System.Collections.Generic;

    /// <summary>
    /// Обеспечивает взаимодействие с событиями протокола A0.
    /// </summary>
    public class A0LogEvent : IA0LogEvent
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.<seealso cref="A0LogEvent"./>
        /// </summary>
        public A0LogEvent()
        {
            this.Messages = new List<Message>();
        }

        /// <summary>
        /// Предоставляет данные записи в протоколе A0.
        /// </summary>
        public struct Message
        {
            /// <summary>
            /// Инициализирует новый экземпляр структуры.<seealso cref="Message"./>
            /// </summary>
            /// <param name="type">Тип сообщения.</param>
            /// <param name="text">Текст сообщения.</param>
            public Message(EAppLogMessageType type, string text)
            {
                this.MessageType = type;
                this.MessageText = text;
            }

            /// <summary>
            /// Получает тип сообщения.
            /// </summary>
            public EAppLogMessageType MessageType { get; }

            /// <summary>
            /// Получает текст сообщения.
            /// </summary>
            public string MessageText { get; }
        }

        /// <summary>
        /// Получает список записей в протоколе.
        /// </summary>
        public List<Message> Messages { get; }

        /// <summary>
        /// Добавляет запись в список при возникновении событий в протоколе А0.
        /// </summary>
        /// <param name="aType">Тип сообщения.</param>
        /// <param name="aStr">Текст сообщения.</param>
        public void OnLog(EAppLogMessageType aType, string aStr)
        {
            this.Messages.Add(new Message(aType, aStr));
        }
    }

    [TestFixture(
        Category = "Integrate",
        Description = "Тесты проверки работоспособности IA0LogEvent",
        Author = "agalkin")]
    public class Test_IA0LogEvent : Test_Base
    {
        /// <summary>
        /// Получает или устанавливает протокол А0.
        /// </summary>
        protected IA0LogDomain LogDomain { get; private set; }

        /// <summary>
        /// Осуществляет операции проводимые перед тестированием.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();
            this.LogDomain = this.A0.App.Log;
            Assert.NotNull(this.LogDomain);
        }

        /// <summary>
        /// Осуществляет операции проводимые по завершению тестирования.
        /// </summary>
        public override void TearDown()
        {
            Assert.NotNull(this.LogDomain);
            this.LogDomain = null;
            base.TearDown();
        }

        /// <summary>
        /// Проверяет работоспособность обработчика событий в протоколе А0.
        /// </summary>
        [Test]
        public void Test_Log()
        {
            string error = "Error";
            string exceptLog = "ExceptLog";
            string message = "Message";
            string trace = "Trace";
            string warning = "Warning";

            A0LogEvent logEvent = new A0LogEvent();
            this.LogDomain.Event = logEvent;

            // Добавление в протокол записей, которые вызывают обработчик событий OnLog.
            this.LogDomain.Log.Error(error);
            this.LogDomain.Log.ExceptLog(exceptLog);
            this.LogDomain.Log.Message(message);
            this.LogDomain.Log.Trace(trace);
            this.LogDomain.Log.Warning("Warning");

            // Проверка списка записей A0LogEvent после обработки событий.
            Assert.NotZero(logEvent.Messages.Count);
            Assert.True(logEvent.Messages.Contains(new A0LogEvent.Message(EAppLogMessageType.lmtError, error)));
            Assert.True(logEvent.Messages.Contains(new A0LogEvent.Message(EAppLogMessageType.lmtExceptLog, exceptLog)));
            Assert.True(logEvent.Messages.Contains(new A0LogEvent.Message(EAppLogMessageType.lmtMessage, message)));
            Assert.True(logEvent.Messages.Contains(new A0LogEvent.Message(EAppLogMessageType.lmtTrace, trace)));
            Assert.True(logEvent.Messages.Contains(new A0LogEvent.Message(EAppLogMessageType.lmtWarning, warning)));
        }

        /// <summary>
        /// Проверяет работоспособность обработчика событий в протоколе А0 с выключенной трассировкой.
        /// </summary>
        [Test]
        public void Test_Trace()
        {
            // Отключение трассировки
            this.LogDomain.TraceMessages = false;
            A0LogEvent logEvent = new A0LogEvent();

            this.LogDomain.Event = logEvent;
            this.LogDomain.Log.Trace("Trace");

            // При выключенной трассировке метод Trace не должен добавлять запись в протокол.
            Assert.Zero(logEvent.Messages.Count);
        }
    }
}