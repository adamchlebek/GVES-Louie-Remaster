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

            //Add Events
            _client.GuildMemberUpdated += _client_GuildMemberUpdated;
            _client.MessageDeleted += _client_MessageDeleted;
            _client.UserJoined += _client_UserJoined;
            _client.UserLeft += _client_UserLeft;

            Task.Delay(-1);

            return Task.CompletedTask;
        }

        private Task _client_UserLeft(SocketGuildUser arg)
        {
            _sql.Execute($"EXEC removeUser @id='{arg.Id}'");
            _client.GetGuild(_guildID).GetTextChannel(502913561094389770).SendMessageAsync($"**{arg.Username}** has left the discord at {DateTime.Now}.");

            return Task.CompletedTask;
        }

        private Task _client_UserJoined(SocketGuildUser arg)
        {
            string msg = $"Hello <@{arg.Id}>! Welcome to the discord! Check out <#463470298524811274> to become an Official Laker in the discord and get yourself verified!";
            string nickname;
            DateTime join = DateTime.Parse(arg.JoinedAt.ToString());
            DateTime create = DateTime.Parse(arg.CreatedAt.ToString());

            _client.GetGuild(_guildID).GetTextChannel(335284078796603392).SendMessageAsync(msg);

            string joinString = $"{join.Year}-{join.Month}-{join.Day} {join.Hour}:{join.Minute}:{join.Second}";
            string createString = $"{create.Year}-{create.Month}-{create.Day} {create.Hour}:{create.Minute}:{create.Second}";

            try
            {
                nickname = arg.Nickname;
            }
            catch
            {
                nickname = arg.Username;
            }

            _sql.Execute($"EXEC addUser @id='{arg.Id}', @username='{RemoveQuotes(arg.Username)}', @nickname= '{RemoveQuotes(arg.Username)}', @discriminator='{arg.Discriminator}', @joinedAt='{joinString}', @createdAt='{createString}'");

            _client.GetGuild(_guildID).GetTextChannel(502913561094389770).SendMessageAsync($"**{arg.Username}** has joined the discord at {DateTime.Now}.");

            return Task.CompletedTask;
        }

        private Task _client_MessageDeleted(Cacheable<IMessage, ulong> arg1, ISocketMessageChannel arg2)
        {
            int msgCount = int.Parse(_sql.GetSingleValue("SELECT messageCount FROM stats"));

            _sql.Execute($"UPDATE stats SET messageCount = {msgCount - 1}");
            
            return Task.CompletedTask;
        }

        private Task _client_GuildMemberUpdated(SocketGuildUser arg1, SocketGuildUser arg2)
        {
            if (arg1.Status == UserStatus.Online || arg1.Status == UserStatus.Idle && arg2.Status == UserStatus.Offline)
            {
                DateTime now = DateTime.Now;

                _sql.Execute($"EXEC addOfflineTime @time='{now}', @id='{arg1.Id}'");
            }
            else if(arg1.Status == UserStatus.Offline && arg2.Status == UserStatus.Online)
            {
                _sql.Execute($"EXEC addOfflineTime @time=NULL, @id='{arg1.Id}'");
            }

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

            //All Checks
            
            int argPos = 0;

            string curdatetime = DateTime.Now.ToString("HH:mm:ss");

            //Delete Messages
            if (arg.Author.Username.Contains("Dyno#3861"))
            {
                //See What User Send The Message
                Embed msg = arg.Embeds.FirstOrDefault();
                string description = msg.Description;

                if (description.Contains(("sent by")))
                {
                    string id = description.Substring(description.IndexOf("<") + 2, 
                        description.IndexOf(">") - description.IndexOf("<") - 2);
                    int firstIndex = description.IndexOf("<") + 1;
                    int secondIndex = description.IndexOf(">") + 1;
                    string chanId = description.Substring(description.IndexOf("<", firstIndex) + 2,
                        (description.IndexOf(">", secondIndex) - 2) - (description.IndexOf("<", firstIndex) + 2) + 2);

                    try
                    {
                        int curMessageCount =
                            int.Parse(_sql.GetSingleValue($"SELECT msgCount FROM allUsers WHERE id='{id}'"));
                        _sql.Execute($"EXEC addMessage @id='{id}', @msgCount = '{curMessageCount - 1}'");
                    }
                    catch (Exception ex)
                    {
                        await Log(ex.Message);
                    }


                    try
                    {
                        int curMessageCount = int.Parse(_sql.GetSingleValue($"SELECT msgCount FROM channelCount WHERE chanID LIKE '{chanId}'"));
                        _sql.Execute($"UPDATE channelCount SET msgCount = '{curMessageCount - 1}' WHERE chanID LIKE '{chanId}'");
                    }
                    catch (Exception ex)
                    {
                        await Log(ex.Message);
                    }
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

            //Add Message Here
            int curCount = int.Parse(_sql.GetSingleValue("SELECT messageCount FROM stats"));
            _sql.Execute($"UPDATE stats SET messageCount = {curCount + 1}");

            int userMessageCount = int.Parse(_sql.GetSingleValue($"SELECT msgCount FROM allUsers WHERE id='{arg.Id}'"));
            _sql.Execute($"EXEC addMessage @id='{arg.Id}', @msgCount = '{userMessageCount + 1}'");

            int channelMessageCount = int.Parse(_sql.GetSingleValue($"SELECT msgCount FROM channelCount WHERE chanID LIKE '{arg.Channel.Id}'"));
            _sql.Execute($"UPDATE channelCount SET msgCount = '{channelMessageCount + 1}' WHERE chanID LIKE '{arg.Channel.Id}'");
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

        public List<SocketGuildChannel> GetChannelList()
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
            StringBuilder str = new StringBuilder();
            DataTable data = _sql.GetDataTable("SELECT role FROM roleList");

            str.Append("**Welcome to Set Game! Here you can set your current roles without having to contact an admin!**" + Environment.NewLine);
            str.Append(Environment.NewLine + "`" + Environment.NewLine);
            str.Append("To edit your role, enter the command !set ROLE. If you have the role already, it will be removed. If you don't have it, you will be added to the role.");
            str.Append(Environment.NewLine + "`" + Environment.NewLine + Environment.NewLine);
            str.Append(":arrow_right: __**Here is a list of roles you can join**__ :arrow_left:");
            str.Append("```");

            foreach(DataRow row in data.Rows)
            {
                str.Append(row["role"].ToString() + Environment.NewLine);
            }

            str.Append("```");

            _client.GetGuild(_guildID).GetTextChannel(537454782110236682).SendMessageAsync(str.ToString());
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            SendMessage send = new SendMessage(GetChannelList(), _client, _guildID);
            send.Show();
        }
    }
}
