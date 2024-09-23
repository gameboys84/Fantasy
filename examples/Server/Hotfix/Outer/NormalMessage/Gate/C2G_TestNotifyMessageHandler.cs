using Fantasy.Async;
using Fantasy.Network;
using Fantasy.Network.Interface;

namespace Fantasy.Gate;

public sealed class C2G_TestNotifyMessageHandler : Message<C2G_TestNotifyMessage>
{
    protected override async FTask Run(Session session, C2G_TestNotifyMessage message)
    {
        Log.Debug($"Receive C2G_TestNotifyMessage: {message.Msg}");

        // 如何不通过这个sesssion，而是自己来找到相应session发送消息？模拟服务器主动向客户端发送消息

        session.Send(new G2C_TestNotifyMessage()
        {
            Msg = "C2G_TestNotifyMessageHandler From Server!"
        });
        await FTask.CompletedTask;
    }
}
