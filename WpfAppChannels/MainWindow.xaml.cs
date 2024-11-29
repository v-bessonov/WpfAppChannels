using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppChannels;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Producer _producer;
    private readonly Consumer _consumer1;
    private readonly Consumer _consumer2;

    public ObservableCollection<string> Consumer1Data => _consumer1.ProcessedData;
    public ObservableCollection<string> Consumer2Data => _consumer2.ProcessedData;
    
    public MainWindow()
    {
        InitializeComponent();
        
        InitializeComponent();

        // Shared channel service
        var channelService = new ChannelService<int>();

        // Producer and consumers
        _producer = new Producer(channelService);
        _consumer1 = new Consumer(channelService, "Consumer 1");
        _consumer2 = new Consumer(channelService, "Consumer 2");

        // Bind to UI
        DataContext = this;
    }
    
    private async void StartCommunication_Click(object sender, RoutedEventArgs e)
    {
        // Start producer and consumers
        var producerTask = _producer.ProduceDataAsync();
        var consumer1Task = _consumer1.ConsumeDataAsync();
        var consumer2Task = _consumer2.ConsumeDataAsync();

        // Wait for all tasks to complete
        await Task.WhenAll(producerTask, consumer1Task, consumer2Task);
    }
}