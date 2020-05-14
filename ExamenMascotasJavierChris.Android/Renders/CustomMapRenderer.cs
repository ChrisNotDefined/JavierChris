using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExamenMascotasJavierChris.Droid.Renders;
using ExamenMascotasJavierChris.Models;
using ExamenMascotasJavierChris.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace ExamenMascotasJavierChris.Droid.Renders
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        PetModel Pet;
        List<PetModel> PetList;

        public CustomMapRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                this.Pet = (e.NewElement as CustomMap).Pet;
                this.PetList = (e.NewElement as CustomMap).PetList;
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            if(PetList != null)
            {
                foreach (var item in PetList)
                {
                    if(item.Nombre == pin.Label &&
                        item.Latitud == pin.Position.Latitude &&
                        item.Longitud == pin.Position.Longitude)
                    {
                        Pet = item;
                    }
                }
            }
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(Pet.Latitud, Pet.Longitud));
            // Using SnippetProperty as the imageURL to pass the data to the Marker
            marker.SetSnippet(Pet.ImageBase64);
            marker.SetTitle(Pet.Nombre);
            return marker;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            

            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;
                view = inflater.Inflate(Resource.Layout.MarkerWindow, null);
                var infoImage = view.FindViewById<ImageView>(Resource.Id.MarkerWindowImage);
                var infoNombre = view.FindViewById<TextView>(Resource.Id.MarkerWindowNombre);

                if (infoImage != null) infoImage.SetImageBitmap(BitmapFactory.DecodeFile(marker.Snippet));
                if (infoNombre != null) infoNombre.Text = marker.Title;

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}