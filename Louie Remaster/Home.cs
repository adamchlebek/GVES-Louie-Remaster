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

            var botToken = File.ReadAllText("token.txt");

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
            txtOutput.AppendText(log + Environment.NewLine);

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
    }
}
