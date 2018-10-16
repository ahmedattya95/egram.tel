using TdLib;
using Tel.Egram.Components.Messenger.Explorer.Messages;
using Tel.Egram.Graphics;
using Tel.Egram.Messaging.Messages;

namespace Tel.Egram.Components.Messenger.Explorer
{
    public class MessageModelFactory : IMessageModelFactory
    {
        private readonly IAvatarLoader _avatarLoader;

        public MessageModelFactory(
            IAvatarLoader avatarLoader
            )
        {
            _avatarLoader = avatarLoader;
        }
        
        public MessageModel CreateMessage(Message message)
        {
            var messageData = message.MessageData;
            var content = messageData.Content;

            switch (content)
            {
                case TdApi.MessageContent.MessageText messageText:
                    return CreateTextMessage(message, messageText);
                default:
                    return CreateUnsupportedMessage(message);
            }
        }

        private MessageModel CreateTextMessage(Message message, TdApi.MessageContent.MessageText messageText)
        {
            var user = message.User;
            var chat = message.Chat;

            var avatar = (user == null)
                ? _avatarLoader.GetAvatar(chat, AvatarSize.Big)
                : _avatarLoader.GetAvatar(user, AvatarSize.Big);

            var authorName = (user == null)
                ? chat.Title
                : $"{user.FirstName} {user.LastName}";
            
            var text = messageText.Text.Text;
            
            return new TextMessageModel
            {
                AuthorName = authorName,
                Avatar = avatar,
                Message = message,
                Text = text
            };
        }

        private MessageModel CreateUnsupportedMessage(Message message)
        {
            return new UnsupportedMessageModel
            {
                Message = message
            };
        }
    }
}