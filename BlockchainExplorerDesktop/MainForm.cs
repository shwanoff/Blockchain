using BlockchainExplorerDesktop.BlockchainService;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockchainExplorerDesktop
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Клиент службы.
        /// </summary>
        BlockchainServiceClient client = new BlockchainServiceClient();

        /// <summary>
        /// Конструктор основной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            blockListBox.DisplayMember = "Hash";
        }

        /// <summary>
        /// Обработчик события загрузки формы.
        /// </summary>
        private async void Main_Load(object sender, EventArgs e)
        {
            await LoadAsync();
        }

        /// <summary>
        /// Обработчик события двойного клика на элементе списка.
        /// </summary>
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

        /// <summary>
        /// Обработчик события нажатия кнопки добавления блока.
        /// </summary>
        private async void AddBlockButton_Click(object sender, EventArgs e)
        {
            var block = await client.AddDataAsync(inputDataBlockTextBox.Text);
            blockListBox.Items.Add(block);
            inputDataBlockTextBox.Text = "";
        }

        /// <summary>
        /// Обработчик события нажатия на ссылку помощи.
        /// </summary>
        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }

        /// <summary>
        /// Загрузка всех блоков из локального хранилища в асинхронном режиме.
        /// </summary>
        /// <returns> Успешность выполнения загрузки блоков. </returns>
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
    }
}
