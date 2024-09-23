using Fantasy.Async;
using Fantasy.Network.Interface;
using Fantasy.Network.Route;
using MongoDB.Bson;

namespace Fantasy;

public sealed class C2M_TestNotifyAddressableMessageHandler : Addressable<Unit, C2M_TestNotifyAddressableMessage>
{
    protected override async FTask Run(Unit entity, C2M_TestNotifyAddressableMessage message)
    {
        Log.Debug($"C2M_TestNotifyAddressableMessageHandler = {message.ToJson()}");


        var addressableRouteComponent = entity.GetComponent<AddressableMessageComponent>();

        // Route能否直接发消息给客户端？？ 模拟服务器主动向客户端发送消息

        //await entity.Scene.NetworkMessagingComponent.SendAddressable(addressableRouteComponent.AddressableId, new M2C_TestNotifyAddressableMessage()
        //{
        //    Msg = "M2C_TestNotifyAddressableMessage From Server"
        //});

        //}); 
        //var sceneConfig = SceneConfigData.Instance.GetSceneBySceneType(SceneType.Map)[0];

        //Session session = entity.Scene.GetSession(entity.RunTimeId);

        //session.Send(new M2C_TestNotifyAddressableMessage()
        //{
        //    Msg = "M2C_TestNotifyAddressableMessage From Server"
        //});

        //var addressableRouteComponent = session?.GetComponent<AddressableRouteComponent>();
        //if(addressableRouteComponent != null )
        //{
        //    await entity.Scene.NetworkMessagingComponent.SendAddressable(addressableRouteComponent.AddressableId, new M2C_TestNotifyAddressableMessage()
        //    {
        //        Msg = "M2C_TestNotifyAddressableMessage From Server"
        //    });
        //}
        //else
        //{
        //    Log.Error("M2C_TestNotifyAddressableMessage no session found");
        //}

        await FTask.CompletedTask;
    }
}