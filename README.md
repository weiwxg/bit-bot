# bit-bot

Bit-Bot is a DingTalk bot designed to send updates and latest news about the cryptocurrency world. With simple configuration, you can easily deploy Bit-Bot to your DingTalk group and receive real-time cryptocurrency-related information.

## Features

- Sends real-time updates about the cryptocurrency market.
- Supports custom DingTalk bot Webhook and Secret.
- Easy deployment using Docker.

## Quick Start

### 1. Create a DingTalk Bot

Before getting started, you need to create a bot on DingTalk. Refer to the [DingTalk Bot Official Documentation](https://open.dingtalk.com/document/orgapp/robot-overview) to create a bot and obtain the Webhook and Secret.

### 2. Deploy Bit-Bot Using Docker

You can quickly deploy Bit-Bot using Docker. Ensure Docker is installed on your system.

```bash
docker rm -f bit-bot && \
docker rmi weiwxg/bit-bot:1.0 && \
docker run -d \
  --name bit-bot \
  -e ASPNETCORE__ENVIRONMENT=Production \
  -e Dingtalk__Webhook=<Your dingtalk webhook> \
  -e Dingtalk__Secret=<Your dingtalk secret> \
  weiwxg/bit-bot:1.0
```
Replace <Your dingtalk webhook> and <Your dingtalk secret> with the actual values obtained from your DingTalk bot.

### 3. Verify Deployment

Once deployed, Bit-Bot will start running and send the latest cryptocurrency market updates to your configured DingTalk group.

## Contributing

Contributions are welcome! Feel free to submit issues or pull requests to contribute to the project.

