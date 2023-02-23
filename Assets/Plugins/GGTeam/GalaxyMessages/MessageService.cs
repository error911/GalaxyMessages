using GGTeam.GalaxyMessages.Runtime;
using System;

namespace GGTeam.GalaxyMessages
{
    [Serializable]
    public class MessageService : IMessageService
    {
        public MessageService()
        {
            _instance = this;
            OnRun();
        }

        public static MessageService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MessageService();
                    _instance.OnRun();
                }

                return _instance;
            }
        }

        private static MessageService _instance;

        private IDisposable _messageBroker;
        private IMessagePublisher _messagePublisher;
        private IMessageReceiver _messageReceiver;

        protected void OnRun()
        {
            var messageBroker = new MessageBroker();

            _messageBroker = messageBroker;
            _messagePublisher = messageBroker;
            _messageReceiver = messageBroker;
        }
        
        protected void OnRelease()
        {
            _messageBroker.Dispose();
        }

        #region IMessagePublisher

        bool IMessagePublisher.Publish<T>(T message)
        {
            return _messagePublisher.Publish(message);
        }

        #endregion

        #region IMessageReceiver

        IObservable<T> IMessageReceiver.Receive<T>()
        {
            return _messageReceiver.Receive<T>();
        }

        #endregion

    }
}