[![Lastest build](https://github.com/QADRAX/ConversationalWeed/actions/workflows/Build.yml/badge.svg)](https://github.com/QADRAX/ConversationalWeed/actions)
# Conversational Weed 

ConversationalWeed is a card game bot for Discord based on the popular card game [Weed!](https://khepergames.com/category/weed-ware/). Developed during the 2020 quarantine, the game spans long and fun gaming sessions for 3-8 players. The game includes features as personal statistics, skin store and more! 

## Instalation

### 1) Download the latest version

First, download the lastest ConversationalWeed version available [here](https://github.com/QADRAX/ConversationalWeed/releases). Different executables are available for all operating systems: Linux, MacOs and Windows. Just unzip the bundle in the folder of your choice. 

We recommend running the bot on a standar windows machine due to the use of the System.Drawing library for the in-game image generation. Currently, there are [many reasons to stop using this library](https://photosauce.net/blog/post/5-reasons-you-should-stop-using-systemdrawing-from-aspnet) and we detect problems running the bot on azure webjobs and linux distributions such as rasbian because this reasons. It is planned to change this soon. 

### 2) Set your MySql connection string

The game requires a connection to a mySQL database. Just setup your mySql database (for example, installing [MySql Workbench](https://www.mysql.com/products/workbench/)) and add the connectionString in **appsettings.json**:

```json
{
    "WeedDatabase": "Server={YourServer}; Port={YourPort}; Database=conversational_weed; Uid={YourUser}; Pwd={YourPassword}; SslMode=Preferred;"
}

```

Database migrations are enabled by default, so you don't have to worry about updating the bot and using the same connection string. 

### 3) Create a Discord Bot

(taken from [official Discord.Net documentation](https://discord.foxbot.me/stable/guides/getting_started/first-bot.html))

Before running this bot, it is necessary to create a bot account via the Discord Applications Portal first.

- Visit the [Discord Applications Portal](https://discord.com/developers/applications/).
- Create a new application.
- Give the application a name (this will be the bot's initial username).
- On the left-hand side, under Settings, click Bot.
- Click on Add Bot.
- Confirm the popup.
- (Optional) If this bot will be public, tick Public Bot.
- **IMPORTANT**: Copy the token and insert it into the DiscordToken key in **appsettings.json**.

```json
{
    "DiscordToken": "{YourToken}"
}

```

### 4) Add your bot to a Discord server

- On the left-hand side, under Settings, click OAuth2.
- Scroll down to OAuth2 URL Generator and under Scopes tick bot.
- Scroll down further to Bot Permissions and select the permissions that you wish to assign your bot with.
- Open the generated authorization URL in your browser.
- Select a server.
- Click on Authorize.

### 5) Run the console app

Once the previous steps have been carried out and you have the DiscordToken and the connection string to MySQL, you can run the executable **ConversationalWeed.Host** and play on Discord with your friends! 
