using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using vatSysServer.Models;

namespace OLDITester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string Address = "YYYYZQZX";

        private readonly Dictionary<string, ushort> _seqNums = new Dictionary<string, ushort>();
        public MainWindow()
        {
            InitializeComponent();
            msgTypeBox.ItemsSource = Enum.GetValues<CoordinationMessageType>();
            msgTypeBox.SelectedValue = CoordinationMessageType.ACT;
        }

        private string GetSequenceNumber(string address)
        {
            ushort seqNum = 0;
            if (!_seqNums.TryGetValue(address, out seqNum))
            {
                _seqNums.Add(address, seqNum);
            }
            else
                _seqNums[address] = ++seqNum;

            return seqNum.ToString("000");
        }

        private void genButton_Click(object sender, RoutedEventArgs e)
        {
            var msg = new CoordinationMessageDto((CoordinationMessageType)msgTypeBox.SelectedValue, acidBox.Text,
                    new CoordinationMessageFieldStructDto(CoordinationMessageFieldType.REFDATA,
                        new CoordinationMessageFieldStructDto(CoordinationMessageFieldType.SENDER,
                            new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.FAC, Address)),
                        new CoordinationMessageFieldStructDto(CoordinationMessageFieldType.RECVR,
                            new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.FAC, addrBox.Text)),
                        new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.SEQNUM, GetSequenceNumber(addrBox.Text))),
                    new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.SSRCODE, assrBox.Text),
                    new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.ADEP, adepBox.Text),
                    new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.ATOT, atdBox.Text),
                    new CoordinationMessageFieldStructDto(CoordinationMessageFieldType.COORDATA,
                            new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.PTID, ptidBox.Text),
                            new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.STO, stoBox.Text),
                            new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.TFL, tflBox.Text)),
                    new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.ADES, adesBox.Text),
                    new CoordinationMessageFieldBasicDto(CoordinationMessageFieldType.ARCTYPE, atypBox.Text));

            outputBlock.Text = msg.ToString();
            Clipboard.SetText(outputBlock.Text);
        }
    }
}
