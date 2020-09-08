using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace TestApp
{
    public class BoxStateAdapter : BaseAdapter<int>
    {
        Context context;
        MQTTService _service;
        Android.App.FragmentTransaction manager;

        private string topic_1 = "mashtgbr/esp/led";
        private string topic_2 = "mashtgbr/esp/led2";

        public BoxStateAdapter(Context context, FragmentManager manager, MQTTService service)
        {
            this._service = service;
            this.context = context;
            this.manager = manager.BeginTransaction();
        }
        public override int this[int position] => position;

        public override int Count => 2;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = LayoutInflater.From(context).Inflate(Resource.Layout.activity_holder, null);

            var NumberContainer = view.FindViewById<TextView>(Resource.Id.NumberContainer);
            var img_box_state = view.FindViewById<ImageView>(Resource.Id.ImageContainer);
            var Btn_Container_Fold = view.FindViewById<Button>(Resource.Id.ContainerFold);
            var Btn_Container_Unfold = view.FindViewById<Button>(Resource.Id.ContainerUnfold);
            var Btn_Stop = view.FindViewById<Button>(Resource.Id.StopButton);

            var number = (position + 1).ToString();
            NumberContainer.Text = number;

            string origin_topic = (number == "1") ? topic_1 : topic_2;
            //сложить
            Btn_Container_Fold.Click += delegate (object select, EventArgs eventArgs)
            {
                img_box_state.SetImageResource(Resource.Drawable.containerFolded);
                SetBtnBackground(select,eventArgs, "fold", ref Btn_Container_Fold, ref Btn_Container_Unfold);
                _service.PublishBoxState(origin_topic, "2");
            };
            //остановка
            Btn_Stop.Click += delegate (object select, EventArgs eventArgs)
            {
                SetBtnBackground(select, eventArgs, "off", ref Btn_Container_Fold, ref Btn_Container_Unfold);
                _service.PublishBoxState(origin_topic, "0");
            };
            //разложить
            Btn_Container_Unfold.Click += delegate (object select, EventArgs eventArgs)
            {
                img_box_state.SetImageResource(Resource.Drawable.containerUnfolded);
                SetBtnBackground(select, eventArgs, "unfold", ref Btn_Container_Fold, ref Btn_Container_Unfold);
                _service.PublishBoxState(origin_topic, "1");
            };

            return view;

        }

        private void SetBtnBackground(object select, EventArgs eventArgs, string v, 
            ref Button btn_Container_Fold, ref Button btn_Container_Unfold)
        {
            switch(v)
            {
                case "fold":
                    btn_Container_Fold.SetBackgroundResource(Resource.Drawable.Left_Button_Press);
                    btn_Container_Unfold.SetBackgroundResource(Resource.Drawable.Right_Button_NoPress);
                    break;
                case "unfold":
                    btn_Container_Fold.SetBackgroundResource(Resource.Drawable.Left_Button_NoPress);
                    btn_Container_Unfold.SetBackgroundResource(Resource.Drawable.Right_Button_Press);
                    break;
                case "off":
                    btn_Container_Fold.SetBackgroundResource(Resource.Drawable.Left_Button_NoPress);
                    btn_Container_Unfold.SetBackgroundResource(Resource.Drawable.Right_Button_NoPress);
                    break;
            }
        }
    }
}