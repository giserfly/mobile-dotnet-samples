﻿
using System;
using Shared;

namespace AdvancedMap.Droid
{
    public class BaseGeocodingActivity : PackageDownloadBaseActivity
    {
        protected const string ApiKey = "mapzen-e2gmwsC";
		
        public Geocoding GeocodingClient { get { return Client as Geocoding; } }

		protected override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			string folder = GetPackageFolder(Geocoding.PackageFolder);
			bool isFullDirectory = true;
			Client = new Geocoding(folder, isFullDirectory);
		}
    }
}
