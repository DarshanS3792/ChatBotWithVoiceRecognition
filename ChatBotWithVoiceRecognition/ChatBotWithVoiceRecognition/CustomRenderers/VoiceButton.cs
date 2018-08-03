using System;
using Xamarin.Forms;

namespace ChatBotWithVoiceRecognition.CustomRenderers
{
    public class VoiceButton : Button
    {
        public Action<string> OnTextChanged { get; set; }
    }
}
