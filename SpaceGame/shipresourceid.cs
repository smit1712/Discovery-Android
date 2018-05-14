using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;


namespace SpaceGame
{
    public class Shipresourceid
    {
        int shipclassid = 0;
        public int Getshipresourceid(PlayerShip ship)
        {
            switch (ship.GetShipclass())
            {
                case "akira":
                    shipclassid = Resource.Drawable.akira;
                    break;
            }
            return shipclassid;
        }
    }
}