using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestApp
{
    public class OrdersClass : Fragment
    {
        private RadioButton firstOrder { get; set; }
        private RadioButton secondOrder { get; set; }
        private EditText CloudAccessText { get; set; }
        private EditText CommandText { get; set; }
        private Button Btn_Sending_Command { get; set; }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.activity_order_class, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            firstOrder = view.FindViewById<RadioButton>(Resource.Id.RbtnFirstOrder);
            secondOrder = view.FindViewById<RadioButton>(Resource.Id.RbtnSecondOrder);
            CloudAccessText = view.FindViewById<EditText>(Resource.Id.TextCloudAccessOptions);
            CommandText = view.FindViewById<EditText>(Resource.Id.TextCommand);
            Btn_Sending_Command = view.FindViewById<Button>(Resource.Id.ButtonSendCommand);

            Btn_Sending_Command.Click += BtnSendingCommand;
        }

        private void BtnSendingCommand(object sender, EventArgs e)
        {
            
        }
    }
}