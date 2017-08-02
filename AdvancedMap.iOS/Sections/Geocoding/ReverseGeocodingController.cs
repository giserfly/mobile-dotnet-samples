﻿
using System;
using Carto.Geocoding;
using Shared;

namespace AdvancedMap.iOS
{
    public class ReverseGeocodingController : BaseGeocodingController
    {
        public PackageManagerReverseGeocodingService Service { get; set; }

        public ReverseGeocodingEventListener Listener { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            ContentView = new ReverseGeocodingView();
			View = ContentView;

			Listener = new ReverseGeocodingEventListener(ContentView.Projection);
			Listener.Service = new PackageManagerReverseGeocodingService(Geocoding.Manager);

            Geocoding.Projection = ContentView.Projection;

            Title = "REVERSE GEOCODING";
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ContentView.MapView.MapEventListener = Listener;
            Listener.ResultFound += OnFoundResult;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

			ContentView.MapView.MapEventListener = null;
            Listener.ResultFound -= OnFoundResult;
        }

        void OnFoundResult(object sender, EventArgs e)
        {
            GeocodingResult result = (GeocodingResult)sender;

            if (result == null)
            {
                Alert("Couldn't find any addresses. Are you sure you have downloaded the region you're trying to reverse geocode?");
            }

            string title = "";
            string description = result.ToString();
            bool goToPosition = false;

            ContentView.ObjectSource.ShowResult(ContentView.MapView, result, title, description, goToPosition);
        }
    }
}
