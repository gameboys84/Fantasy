namespace Fantasy.Gate;

public sealed class C2G_TestNotifyMessageHandler : Message<C2G_TestNotifyMessage>
{
    protected override async FTask Run(Session session, C2G_TestNotifyMessage message)
    {
        Log.Debug($"Receive C2G_TestNotifyMessage: {message.Msg}");

        session.Send(new G2C_TestNotifyMessage()
        {
            Msg = "C2G_TestNotifyMessageHandler From Server!"
        });
        await FTask.CompletedTask;
    }
}
