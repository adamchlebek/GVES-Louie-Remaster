using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Configuration;

namespace Louie_Remaster
{
    public partial class Home : Form
    {
        DiscordSocketClient _client;
        CommandService _commands;
        IServiceProvider _services;
        sqlConnector _sql;
        ulong _guildID = 335284078796603392;

        public Home()
        {
            InitializeComponent();
            Home.CheckForIllegalCrossThreadCalls = false;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            txtOutput.Text += "Starting...";
            RunBotAsync();
            lblVersion.Text = "v" + Application.ProductVersion;
        }

        Task RunBotAsync()
        {
            _sql = new sqlConnector();
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            _sql.Setup();

            var botToken = File.ReadAllText("Token.txt");

            _client.Log += Log;

            RegisterCommandsAsync();
            _client.LoginAsync(TokenType.Bot, botToken);
            _client.StartAsync();

            Task.Delay(-1);

            return Task.CompletedTask;
        }

        Task Log(LogMessage arg)
        {
            string curdatetime = DateTime.Now.ToString("HH:mm:ss");

            txtOutput.AppendText(curdatetime + " " + arg.Message + Environment.NewLine);

            return Task.CompletedTask;
        }

        Task Log(string log)
        {
            string curdatetime = DateTime.Now.ToString("HH:mm:ss");

            txtOutput.AppendText(curdatetime + " " + log + Environment.NewLine);

            return Task.CompletedTask;
        }

        async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null) return;

            int argPos = 0;

            string curdatetime = DateTime.Now.ToString("HH:mm:ss");

            //Thread sqlFunctions = new Thread(() => sqlCalls(arg));
            //sqlFunctions.Start();

            //Delete Messages
            if (arg.Author.Username.Contains("Dyno#3861"))
            {
                var delmsgcount = int.Parse(_sql.GetSingleValue("SELECT msgCount FROM stats"));
                _sql.Execute($"UPDATE stats SET messageCount ={delmsgcount - 1}");
            }

            if (message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);
                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                {
                    await message.DeleteAsync();
                    await context.Channel.SendMessageAsync("Error: " + result.ErrorReason);
                    await Log(curdatetime + " Error       " + result.ErrorReason);
                }
                else
                {
                    await message.DeleteAsync();
                    await Log(curdatetime + " Command     " + message.Content + "     " + message.Author);
                }

            }

            //Delete Messages in Stream Channel
            if (arg.Channel.Name.Contains("streaming") && (arg.Author.IsBot == false))
            {
                await arg.DeleteAsync();
            }

            //React to message if it Contains SmokeyFish
            if (arg.Content.ToLower().Contains("smokeyfish"))
            {
                IUserMessage usermsg = (IUserMessage)arg;
                Emote emote = Emote.Parse("<:smokeyfish:481276964850892821>");
                await usermsg.AddReactionAsync(emote);
            }
        }

        private void btnReloadDatabase_Click(object sender, EventArgs e)
        {
            List<SocketGuildUser> userList = GetUserList();
            List<SocketGuildChannel> channelList = GetChannelList();

            DataTable users = _sql.GetDataTable("SELECT id, username FROM allUsers");
            var channels = _sql.GetDataTable("SELECT * FROM channelCount");

            //User List

            List<String> guildUsers = new List<string>();
            List<String> sqlUsers = new List<string>();
            List<String> addID = new List<string>();

            int userAddedCount = 0;

            foreach (SocketGuildUser user in userList)
            {
                guildUsers.Add(user.Id.ToString());
            }

            foreach (DataRow dataRow in users.Rows)
            {
                sqlUsers.Add(dataRow["id"].ToString());
            }

            if (!guildUsers.Count.Equals(sqlUsers.Count)){
                foreach (string id in guildUsers)
                {
                    if (!sqlUsers.Contains(id))
                    {
                        addID.Add(id);
                    }
                }
            }

            foreach (string id in addID)
            {
                foreach (SocketGuildUser user in userList)
                {
                    if (id.Contains(user.Id.ToString()))
                    {
                        userAddedCount += 1;

                        DateTime join = DateTime.Parse(user.JoinedAt.ToString());
                        DateTime create = DateTime.Parse(user.CreatedAt.ToString());

                        string joinString = $"{join.Year}-{join.Month}-{join.Day} {join.Hour}:{join.Minute}:{join.Second}";
                        string createString = $"{create.Year}-{create.Month}-{create.Day} {create.Hour}:{create.Minute}:{create.Second}";


                        try
                        {
                            _sql.Execute($"EXEC addUser @id='{user.Id}', @username='{RemoveQuotes(user.Username)}', @nickname='{RemoveQuotes(user.Username)}', @discriminator='{user.Discriminator}', @joinedAt='{joinString}', @createdAt='{createString}'");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            _sql.Close();
                        }
                    }
                }
            }

            Log($"{userAddedCount} users were reloaded to the database.");

            //Channel List

            int channelUpdatedCount = 0;

            List<SocketGuildChannel> valid = new List<SocketGuildChannel>();

            foreach (SocketGuildChannel chan in channelList)
            {
                var chanId = chan.Id;
                bool found = false;

                foreach (DataRow row in channels.Rows)
                {
                    if (row["chanID"].ToString().Trim() == chanId.ToString())
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    valid.Add(chan);
                }
            }

            foreach (SocketGuildChannel channel in valid)
            {
                bool changed = false;

                foreach (DataRow row in channels.Rows)
                {
                    if (row["channelName"].ToString() == "New Channel" && row["chanID"].ToString() == channel.Id.ToString() && !changed)
                    {
                        channelUpdatedCount += 1;
                        _sql.Execute($"UPDATE channelCount SET channelName = '{channel.Name}' WHERE chanID LIKE '%{channel.Id}%'");
                        changed = true;
                    }
                }
            }

            Log($"{channelUpdatedCount} channels were updated in the database.");
        }

        private List<SocketGuildUser> GetUserList()
        {
            var _socket = _client.GetGuild(_guildID);
            List<SocketGuildUser> userList = _socket.Users.ToList();
            return userList;
        }

        private List<SocketGuildChannel> GetChannelList()
        {
            var _socket = _client.GetGuild(_guildID);
            List<SocketGuildChannel> channelList = _socket.Channels.ToList();
            return channelList;
        }

        public string RemoveQuotes(string val)
        {
            return val.Replace("'", "");
        }

        private void btnSendSetGame_Click(object sender, EventArgs e)
        {
            string str = "";
            DataTable data = _sql.GetDataTable("SELECT role FROM roleList");
        }
    }
}
