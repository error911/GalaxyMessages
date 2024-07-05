# GalaxyMessages
Fast messages flow for Unity

### Install from Unity Package Manager.
You can add `https://github.com/error911/GalaxyMessages.git?path=Assets/Plugins/GalaxyMessages` to Package Manager

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

        public sealed class GamePaused : GalaxyMessage<GamePaused> { }
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

    // SEND EMPTY MESSAGE
    Message.Player.GamePaused.Publish();
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

// Message Callback method
private void MessageOnChangeHP(Message.Player.ChangeHP hp)
{
    Debug.Log($"New Player HP: {hp}");
}
```

Working correctly with GC (If you have several messages)
---
```csharp
List<IDisposable> _disposables = new List<IDisposable>();

public void Start()
{
    _disposables.Add(Message.Player.ChangeHP.Subscribe(MessageOnChangeHP));
}

public void Release()
{
    foreach (var disposable in _disposables)
        disposable.Dispose()
}

```


remember to use: `namespace GGTeam.GalaxyMessages`
