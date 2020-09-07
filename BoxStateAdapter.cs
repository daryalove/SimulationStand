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
        Android.App.FragmentTransaction manager;

        public BoxStateAdapter(Context context, FragmentManager manager)
        {
            this.context = context;
            this.manager = manager.BeginTransaction();
        }
        public override int this[int position] => position;

        public override int Count => 5;

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
            var ImageContainer = view.FindViewById<ImageView>(Resource.Id.ImageContainer);
            var Btn_Container_Fold = view.FindViewById<Button>(Resource.Id.ContainerFold);
            var Btn_Container_Unfold = view.FindViewById<Button>(Resource.Id.ContainerUnfold);
            var Btn_Stop = view.FindViewById<Button>(Resource.Id.StopButton);

            NumberContainer.Text = position.ToString();
            ImageContainer.SetImageResource(Resource.Drawable.containerFolded);
            Btn_Container_Fold.Click += delegate (object select, EventArgs eventArgs)
            {
                SetBtnBackground(select,eventArgs, "fold", ref Btn_Container_Fold, ref Btn_Container_Unfold);
            };
            Btn_Container_Unfold.Click += delegate (object select, EventArgs eventArgs)
            {
                SetBtnBackground(select, eventArgs, "unfold", ref Btn_Container_Fold, ref Btn_Container_Unfold);
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
            }
        }
    }
}