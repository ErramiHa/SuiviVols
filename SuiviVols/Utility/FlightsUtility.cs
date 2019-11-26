using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuiviVols.Utility
{
    public static class FlightsUtility
    {
                          
        public static double GetDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(Deg2rad(lat1)) * Math.Sin(Deg2rad(lat2)) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Cos(Deg2rad(theta));
                dist = Math.Acos(dist);
                dist = Rad2deg(dist);
                dist = dist * 60 * 1.1515 * 1.609344;

                return (dist);
            }
        }

        public static double ConsommationAvion(double ConsAv,double Distance)
        {
            return (ConsAv * Distance)/100;

        }


        // This function converts decimal degrees to radians             
        private static double Deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }


        // This function converts radians to decimal degrees            
        private static double Rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
