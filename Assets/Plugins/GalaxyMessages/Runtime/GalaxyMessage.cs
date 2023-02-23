/* By Murnik Roman
 * License: Without restrictions for all
 * 2021-2022
 * Version 1.0
 */

using System;
using System.Collections.Generic;
using GGTeam.GalaxyMessages.Runtime;

namespace GGTeam.GalaxyMessages
{
    public abstract class GalaxyMessage<T>
        where T : GalaxyMessage<T>, new()
    {
        private static readonly Stack<T> _messages = new Stack<T>();

        private static IMessageService MessageService => GalaxyMessages.MessageService.Instance;

        public static void Publish()
        {
            var message = _messages.Count > 0
                ? _messages.Pop()
                : new T();

            MessageService.Publish(message);
            _messages.Push(message);
        }

        public static IDisposable Subscribe(Action<T> action, int priority = 0)
        {
            return MessageService
                .Receive<T>()
                .Subscribe(action, priority);
        }
    }

    public abstract class GalaxyMessage<TMessage, TModel>
        where TMessage : GalaxyMessage<TMessage, TModel>, new()
    {
        private static readonly Stack<TMessage> _messages = new Stack<TMessage>();

        private static IMessageService MessageService => GalaxyMessages.MessageService.Instance;

        public TModel Model { get; private set; }

        public static void Publish(in TModel model)
        {
            var message = _messages.Count > 0
                ? _messages.Pop()
                : new TMessage();

            message.Model = model;
            MessageService.Publish(message);
            message.Model = default;
            _messages.Push(message);
        }

        public static IDisposable Subscribe(Action<TMessage> action, int priority = 0)
        {
            return MessageService
                .Receive<TMessage>()
                .Subscribe(action, priority);
        }
    }
}