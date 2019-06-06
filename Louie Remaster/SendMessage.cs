using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord.WebSocket;

namespace Louie_Remaster
{
    public partial class SendMessage : Form
    {
        private List<SocketGuildChannel> channelList;
        private DiscordSocketClient _client;
        private ulong _guildId;

        public SendMessage(List<SocketGuildChannel> channelList, DiscordSocketClient _client, ulong _guildId)
        {
            this.channelList = channelList;
            this._client = _client;
            this._guildId = _guildId;
            InitializeComponent();
        }

        private void SendMessage_Load(object sender, EventArgs e)
        {
            foreach (SocketGuildChannel channel in channelList)
            {
                cbSelectChannel.Items.Add(channel.Name.ToString());
            }
        }

        private void cbSelectChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblChannelID.Text = channelList[cbSelectChannel.SelectedIndex].Id.ToString();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _client.GetGuild(_guildId).GetTextChannel(ulong.Parse(lblChannelID.Text)).SendMessageAsync(txtMessage.Text);
            txtMessage.Text = "";
        }
    }
}
