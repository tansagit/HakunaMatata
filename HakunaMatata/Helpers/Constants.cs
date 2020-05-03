using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HakunaMatata.Helpers
{
    public class Constants
    {
        public static readonly string GOOGLE_MAP_API_KEY = "AIzaSyBY8PE_X6qdTWkZwdBENNpGJTYr1cTjG9I";

        public static readonly string GOOGLE_MAP_MARKER_API =
            "https://maps.googleapis.com/maps/api/js?key=AIzaSyBY8PE_X6qdTWkZwdBENNpGJTYr1cTjG9I&callback=initMap";

        public static readonly string LOCAL_IMG_SERVER = "http://127.0.0.1:8887/";

        public enum PriceRange
        {
            All,
            BelowOne,
            One_Two,
            Two_Three,
            Three_Five,
            Five_Seven,
            Seven_Ten,
            Higher
        }

        public enum AcreageRange
        {
            All,
            BelowTwenty,
            Twenty_Thirty,
            Thirty_Fifty,
            Fifty_Sixty,
            Sixty_Seventy,
            Seventy_Eighty,
            Higher
        }
    }
}
