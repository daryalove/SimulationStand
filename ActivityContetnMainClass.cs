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
    public class ActivityContetnMainClass : Fragment
    {
        private Button btn_auth_form { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.content_main, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            btn_auth_form = view.FindViewById<Button>(Resource.Id.btn_auth_form);

            btn_auth_form.Click += BtnAuthClick;
        }

        private void BtnAuthClick(object sender, EventArgs e)
        {
            FragmentTransaction fragment = this.FragmentManager.BeginTransaction();
            OrdersClass _orderClass = new OrdersClass();
            fragment.Replace(Resource.Id.fragment, _orderClass);
            fragment.AddToBackStack(null);
            fragment.Commit();
        }

    }
}