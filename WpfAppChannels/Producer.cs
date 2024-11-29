using System.Threading.Channels;

namespace WpfAppChannels;

public class Producer
{
    private readonly ChannelWriter<int> _writer;

    public Producer(ChannelService<int> channelService)
    {
        _writer = channelService.UnboundedChannel.Writer;
    }

    public async Task ProduceDataAsync()
    {
        for (var i = 1; i <= 20; i++)
        {
            // Produce data (e.g., numbers)
            await _writer.WriteAsync(i); 
            
            // Simulate production delay
            await Task.Delay(500); 
        }
        
        // Signal that no more data will be written
        _writer.Complete(); 
    }
}