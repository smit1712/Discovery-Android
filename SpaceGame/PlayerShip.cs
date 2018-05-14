using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace SpaceGame
{
    public class PlayerShip
    {
       public int health;
        public int weaponPower;
        public int speed;
        public int shields;
        public int maxhealth;
        public int maxshields;
        public int maxweaponpower;
        public int maxspeed;
        public int totalpower;
        public string Shipname;
        public string shipclass;
        public string hailmassage;

        public PlayerShip(int health, int maxweaponPower, int maxspeed,int maxshields, int totalpower, string Shipname, string shipclass)
        {
            this.health = health;
            this.weaponPower = 0;
            this.speed = 0;
            this.shields = 0;
            this.Shipname = Shipname;
            this.shipclass = shipclass;
            this.totalpower = totalpower;

            this.maxhealth = health;
            this.maxshields = maxshields;
            this.maxspeed = maxspeed;
            this.maxweaponpower = maxweaponPower;

        }
        private bool Totalpowercheck(int speed, int shields, int weaponPower)
        {
            if (speed + shields + weaponPower > totalpower)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void Sethailmassage(string message)
        {
            hailmassage = message;
        }
        public void SetTotalauxilarypower(int power)
        {
            totalpower = power;
        }
        public string Gethailmassage()
        {
            return hailmassage;
        }
        public bool Healthcheck()
        {
            if (health >= 0)
            {
                return true;
            }
            else return false;
        }
        public bool EnoughPowercheck()
        {
            if (totalpower - weaponPower - shields - speed >= 0)
            {
                return true;
            }
            else return false;
        }
        public bool TomuchPowercheck()
        {
            if (totalpower - weaponPower - shields - speed <= 3)
            {
                return true;
            }
            else return false;
        }
        public string ChangeHealth(int Damage)
        {
            if (Damage >= shields)
            {
                this.health = this.health + this.shields - Damage;
                shields = 0;
                return "Enemy is taking direct damage";
            }
            else
            {
               
                this.totalpower = this.totalpower - Damage;
                return "Shields Holding";
            }
            
        }
        public void Changespeed(int speed)
        {
            if (Totalpowercheck(speed,this.shields,this.weaponPower) == true)
            {
                this.speed = this.speed + speed;
            }
        }
        public void Changeshields(int shields)
        {
            if (Totalpowercheck(shields, this.speed, this.weaponPower) == true)
            {
                this.shields = this.shields + shields;
            }
        }

        public void ChangeweaponPower(int weaponPower)
        {
            if (Totalpowercheck(weaponPower, this.speed, this.shields) == true)
            {
                this.weaponPower = this.weaponPower + weaponPower;
            }
        }
        public void Setspeed(int speed)
        {
            if (Totalpowercheck(speed, this.shields, this.weaponPower) == true)
            {
                this.speed = speed;
            }
        }
        public void Setshields(int shields)
        {
            if (Totalpowercheck(shields, this.speed, this.weaponPower) == true)
            {
                this.shields =  shields;
            }
        }

        public void SetweaponPower(int weaponPower)
        {
            if (Totalpowercheck(weaponPower, this.speed, this.shields) == true)
            {
                this.weaponPower =  weaponPower;
            }
        }
        public void ChangeShipname(string Shipname)
        {
            this.Shipname = Shipname;
        }

        public void ChangeClass(string shipclass)
        {
            this.shipclass = shipclass;
        }

        public int GetHealth()
        {
            return this.health;
        }
        public int GetmaxHealth()
        {
            return this.maxhealth;
        }
        public int GetweaponPower()
        {
            return this.weaponPower;
        }
        public int Getspeed()
        {
            return this.speed;
        }
        public int Getshields()
        {
            return this.shields;
        }
        public int Getauxiliarypower()
        {
            return Getmaxauxiliarypower() - (this.weaponPower + this.speed + this.shields);
        }
        public int Getmaxshields()
        {
            return this.maxshields;
        }
        public int Getmaxspeed()
        {
            return this.maxspeed;
        }
        public int Getmaxweaponpower()
        {
            return this.maxweaponpower;
        }
        public int Getmaxauxiliarypower()
        {
            return this.maxweaponpower + this.maxspeed + this.maxshields;
        }
        public int GetTotalpower()
        {
            return this.totalpower;
        }
        public string GetShipname()
        {
            return this.Shipname;
        }
        public string GetShipclass()
        {
            return this.shipclass;
        }

    }
}