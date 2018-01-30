using BlockchainExplorerDesktop.BlockchainService;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainExplorerDesktop
{
    public partial class MainForm : Form
    {
        BlockchainServiceClient client = new BlockchainServiceClient();

        public MainForm()
        {
            InitializeComponent();
            blockListBox.DisplayMember = "Hash";
        }

        private async void Main_Load(object sender, EventArgs e)
        {
            await LoadAsync();
        }

        private async Task<bool> LoadAsync()
        {
            return await Task.Run(() =>
            {
                var blocks = client.GetChain();
                foreach (var block in blocks)
                {
                    blockListBox.BeginInvoke((Action)delegate { blockListBox.Items.Add(block); });
                }
                return true;
            });
        }

        private void BlockListBox_DoubleClick(object sender, EventArgs e)
        {

            if (blockListBox.SelectedItem is BlockService item)
            {
                textBox3.Text = item.CreatedOn.ToLocalTime().ToString("dd.MM.yyyy HH.mm.ss");
                textBox4.Text = item.Hash;
                textBox5.Text = item.PreviousHash;
                textBox6.Text = item.User;
                textBox7.Text = item.Data;
            }
        }

        private async void AddBlockButton_Click(object sender, EventArgs e)
        {
            var block = await client.AddDataAsync(inputDataBlockTextBox.Text);
            blockListBox.Items.Add(block);
            inputDataBlockTextBox.Text = "";
        }

        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }
    }
}
