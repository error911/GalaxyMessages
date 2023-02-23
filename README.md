# GalaxyMessages
Fast messages flow for Unity

### Install from Unity Package Manager.
You can add `https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask` to Package Manager

![image](https://user-images.githubusercontent.com/46207/79450714-3aadd100-8020-11ea-8aae-b8d87fc4d7be.png)

Usage: Create message class
---
```csharp
public static partial class Message
{
    public static class Player
    {
        public sealed class ChangeHP : GalaxyMessage<ChangeHP, int>
        {
            public int Data => Model;
        }

        public sealed class ChangeMana : GalaxyMessage<ChangeMana, int>
        {
            public int Data => Model;
        }
    }
}
```

Usage: Send message
---
```csharp
private void Damage()
{
    // SEND MESSAGE (data = hp)
    Message.Player.ChangeHP.Publish(20);
}
```

Usage: Subscribe/Unsubscribe
---
```csharp
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
```

remember to use: `namespace GGTeam.GalaxyMessages`
