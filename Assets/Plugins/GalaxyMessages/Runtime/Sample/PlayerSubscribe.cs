using System;
using UnityEngine;

namespace GGTeam.GalaxyMessages.Sample
{
    public class PlayerSubscribe : MonoBehaviour
    {
        private IDisposable _messageOnPlayerChangeHP;

        private void Start()
        {
            // SEND MESSAGE (data = hp)
            Message.Player.ChangeHP.Publish(20);
        }

        void OnEnable()
        {
            // SUBSCRIBE
            _messageOnPlayerChangeHP = Message.Player.ChangeHP.Subscribe(MessageOnChangeHP);
        }

        private void OnDisable()
        {
            // UNSUBSCRIBE
            _messageOnPlayerChangeHP?.Dispose();
        }

        private void MessageOnChangeHP(Message.Player.ChangeHP hp)
        {
            Debug.Log($"New Player HP: {hp}");
        }

    }
}