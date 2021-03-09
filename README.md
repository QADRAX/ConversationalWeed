# Creating a Discord Bot 

(taken from [official Discord.Net documentation](https://discord.foxbot.me/stable/guides/getting_started/first-bot.html))

Before running this bot, it is necessary to create a bot account via the Discord Applications Portal first.

- Visit the [Discord Applications Portal](https://discord.com/developers/applications/).
- Create a new application.
- Give the application a name (this will be the bot's initial username).
- On the left-hand side, under Settings, click Bot.
- Click on Add Bot.
- Confirm the popup.
- (Optional) If this bot will be public, tick Public Bot.
- Copy the token and insert it into the Discord Token key in ConversationalWeed.Host/Appsettings.json.

# Adding your bot to a server

- On the left-hand side, under Settings, click OAuth2.
- Scroll down to OAuth2 URL Generator and under Scopes tick bot.
- Scroll down further to Bot Permissions and select the permissions that you wish to assign your bot with.
- Open the generated authorization URL in your browser.
- Select a server.
- Click on Authorize.