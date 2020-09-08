using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace TestApp
{
    public class BoxStateClass : Fragment
    {
        private ListView listView;

        private RadioButton firstBox { get; set; }
        private RadioButton secondBox { get; set; }
        private EditText CloudAccessText { get; set; }
        private EditText CommandText { get; set; }
        private Button Btn_Sending_Command { get; set; }

        private const string mqttServer = "farmer.cloudmqtt.com";
        private const int mqttPort = 10613; //Your port number
        private const string mqttUserName = "mashtgbr";
        private const string mqttPassword = "S9czqATnDuvL";
        private string topic = "mashtgbr/esp/led";

        MQTTService mQTT;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.activity_order_list, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            listView = view.FindViewById<ListView>(Resource.Id.ListViewOrders);

            var client_data = new MQTTClient()
            {
                ClientId = "ESP32AndroidReleaseClient",
                Password = mqttPassword,
                Port = mqttPort,
                Server = mqttServer,
                UserName = mqttUserName
            };

            mQTT = new MQTTService(client_data, Activity);

            BoxStateAdapter adapter = new BoxStateAdapter(Activity, this.FragmentManager, mQTT);
            listView.Adapter = adapter;

            //firstBox = view.FindViewById<RadioButton>(Resource.Id.RbtnFirstOrder);
            //secondBox = view.FindViewById<RadioButton>(Resource.Id.RbtnSecondOrder);
            //CloudAccessText = view.FindViewById<EditText>(Resource.Id.TextCloudAccessOptions);
            //CommandText = view.FindViewById<EditText>(Resource.Id.TextCommand);
            //Btn_Sending_Command = view.FindViewById<Button>(Resource.Id.ButtonSendCommand);

            //CloudAccessText.Text = topic;
            //CommandText.Text = "0";

            //firstBox.Click += delegate
            //{
            //    topic = "mashtgbr/esp/led";
            //    CloudAccessText.Text = topic;
            //    CommandText.Text = "0";
            //};

            //secondBox.Click += delegate
            //{
            //    topic = "mashtgbr/esp/led2";
            //    CloudAccessText.Text = topic;
            //    CommandText.Text = "0";
            //};

            //Btn_Sending_Command.Click += BtnSendingCommand;

            //AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
            //alert.SetTitle("Статус соединения с облаком");
            //alert.SetMessage(CloudConnectionResult.Message);
            //alert.SetNeutralButton("Закрыть", (senderAlert, args) =>
            //{

            //});
            //Dialog dialog = alert.Create();
            //dialog.Show();
        }

        private void BtnSendingCommand(object sender, EventArgs e)
        {
            mQTT.PublishBoxState(topic, CommandText.Text);
        }

        public override void OnDestroy()
        {
            mQTT.Stop();
            base.OnDestroy();
        }
    }
}