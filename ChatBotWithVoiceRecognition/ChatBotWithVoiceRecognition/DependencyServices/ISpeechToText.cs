using System;

namespace ChatBotWithVoiceRecognition.DependencyServices
{
    public interface ISpeechToText
    {
        void Start();
        void Stop();

        event EventHandler<EventArgsVoiceRecognition> textChanged;
    }
}
