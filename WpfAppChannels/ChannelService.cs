using System.Threading.Channels;

namespace WpfAppChannels;

public class ChannelService<T>
{
    public Channel<T> UnboundedChannel { get; }

    public ChannelService()
    {
        UnboundedChannel = Channel.CreateUnbounded<T>(); // Unbounded channel for simplicity
    }
}