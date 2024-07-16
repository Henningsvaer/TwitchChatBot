using TwitchLib.Client.Events;

namespace ChatBot.Interfaces.Client
{
    public interface IBot
    {
        abstract void OnLog(object sender, OnLogArgs e);
        abstract void OnConnected(object sender, OnConnectedArgs e);
        abstract void OnJoinedChannel(object sender, OnJoinedChannelArgs e);
        abstract void OnMessageReceived(object sender, OnMessageReceivedArgs e);
        abstract void OnWhisperReceived(object sender, OnWhisperReceivedArgs e);
        abstract void OnNewSubscriber(object sender, OnNewSubscriberArgs e);
    }
}