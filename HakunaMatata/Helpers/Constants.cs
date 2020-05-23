using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Helpers
{
    public class Constants
    {
        // neu ko tim thay gia tri lat/lng thi default la dia chi cua truong
        public const decimal DEFAULT_LATITUDE = 16.0738013M;
        public const decimal DEFAULT_LONGTITUDE = 108.1477255M;

        public static readonly string GOOGLE_MAP_API_KEY = "AIzaSyBY8PE_X6qdTWkZwdBENNpGJTYr1cTjG9I";

        public static readonly string GOOGLE_MAP_MARKER_API =
            "https://maps.googleapis.com/maps/api/js?key=AIzaSyBY8PE_X6qdTWkZwdBENNpGJTYr1cTjG9I&callback=initMap";

        public static readonly string LOCAL_IMG_SERVER = "http://127.0.0.1:8887/";


    }
}
