﻿using Microsoft.WindowsAzure.MobileServices;

using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

using ToDoPCL.Interfaces;

namespace ToDo.Droid
{    
	[Activity (Label = "ToDo", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

            ToDoPCL.ToDoPCL.Init(this);

			LoadApplication (new ToDoPCL.ToDoPCL ());
		}
    }    
}

