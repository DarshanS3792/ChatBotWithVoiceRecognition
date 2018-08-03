using ChatBotWithVoiceRecognition.DependencyServices;
using ChatBotWithVoiceRecognition.Models;
using ChatBotWithVoiceRecognition.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatBotWithVoiceRecognition.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BotPage : ContentPage
    {
        public delegate ContentPage GetEditorInstance(string InitialEditorText);
        static public GetEditorInstance EditorFactory;
        static ISpeechToText speechRecognitionInstance;

        //Initialize a connection with ID and Name
        BotConnectionService connection = new BotConnectionService("Me");

        //ObservableCollection to store the messages to be displayed
        ObservableCollection<Message> messageList = new ObservableCollection<Message>();

        public BotPage()
        {
            InitializeComponent();

            //Bind the ListView to the ObservableCollection
            MessageListView.ItemsSource = messageList;

            //Start listening to messages
            var messageTask = connection.GetMessagesAsync(messageList);

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    iOSLayout.IsVisible = true;
                    this.Content = iOSLayout;
                    speechRecognitionInstance = DependencyService.Get<ISpeechToText>();
                    speechRecognitionInstance.textChanged += OnTextChange;
                    break;

                case Device.Android:
                    androidLayout.IsVisible = true;
                    voiceButton.OnTextChanged += (s) =>
                    {
                        var message = s;

                        //Make object to be placed in ListView
                        var messageListItem = new Message(message, connection.Account.Name);
                        messageList.Add(messageListItem);

                        //Send the message to the bot
                        connection.SendMessage(message);
                    };
                    break;
            }
        }

        public void OnStart(Object sender, EventArgs args)
        {
            speechRecognitionInstance.Start();
            nameButtonStart.IsEnabled = false;
            nameButtonStop.IsEnabled = true;
        }

        public void OnStop(Object sender, EventArgs args)
        {
            speechRecognitionInstance.Stop();
            nameButtonStart.IsEnabled = true;
            nameButtonStop.IsEnabled = false;
        }

        public void OnTextChange(object sender, EventArgsVoiceRecognition e)
        {
            var message = e.Text;

            //Make object to be placed in ListView
            var messageListItem = new Message(message, connection.Account.Name);
            messageList.Add(messageListItem);

            //Send the message to the bot
            connection.SendMessage(message);

            if (e.IsFinal)
            {
                nameButtonStart.IsEnabled = true;
            }
        }

        //Send method for message entry
        //public void Send(object sender, EventArgs args)
        //{
        //    //Get text in entry
        //    var message = ((Entry)sender).Text;

        //    if (message.Length > 0)
        //    {
        //        //Clear entry
        //        ((Entry)sender).Text = "";

        //        //Make object to be placed in ListView
        //        var messageListItem = new Message(message, connection.Account.Name);
        //        messageList.Add(messageListItem);

        //        //Send the message to the bot
        //        connection.SendMessage(message);
        //    }
        //}
    }
}