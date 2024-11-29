using System.Collections.ObjectModel;
using System.Threading.Channels;

namespace WpfAppChannels;

public class Consumer
{
    private readonly ChannelReader<int> _reader;
    public ObservableCollection<string> ProcessedData { get; } = new();
    
    private readonly string _consumerName;

    public Consumer(ChannelService<int> channelService, string consumerName)
    {
        _reader = channelService.UnboundedChannel.Reader;
        _consumerName = consumerName;
    }

    public async Task ConsumeDataAsync()
    {
        while (await _reader.WaitToReadAsync())
        {
            while (_reader.TryRead(out int data))
            {
                // Process data
                var processed = $"{_consumerName} processed {data}";
                
                // Update collection
                ProcessedData.Add(processed); 
                
                // Simulate processing delay
                await Task.Delay(300);
            }
        }
    }
}