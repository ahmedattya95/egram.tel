﻿using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Tel.Egram.Components.Messenger.Explorer.Messages;

namespace Tel.Egram.Gui.Views.Messenger.Explorer.Messages.Shared
{
    public class MessageControl : ContentControl
    {
        public MessageControl()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}