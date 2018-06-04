using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using System;
using System.IO;
using Android.Graphics.Drawables;
using Xamarin.Android;
using Android.Content.Res;
using Android.Graphics;
using System.Linq;
using Android.Views;

namespace SpaceGame
{
    [Activity(Label = "SpaceGame", MainLauncher = true)]
    public class MainActivity : Activity
    {
        PlayerShip ship = new PlayerShip(500, 70, 110, 200, 200, "Enterprise", "akira");
        int turntimer = 0;
        string conflicthail;
        string EnemyAttack;
        Conflict RandomConflict = new Conflict();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            TextView shipname = FindViewById<TextView>(Resource.Id.Shipname);
            TextView combatlog = FindViewById<TextView>(Resource.Id.CombatLog);
            TextView enemyshipname = FindViewById<TextView>(Resource.Id.Enemyshipname);
            TextView auxiliarypowertext = FindViewById<TextView>(Resource.Id.auxiliarypowertext);
            TextView ShipPowertext = FindViewById<TextView>(Resource.Id.ShipPowertext);
            TextView Shipspeedtext = FindViewById<TextView>(Resource.Id.Shipspeedtext);
            TextView Shipshieldtext = FindViewById<TextView>(Resource.Id.ShipShieldtext);
            TextView ConflicDescription = FindViewById<TextView>(Resource.Id.ConflictText);
            ImageView shipview = FindViewById<ImageView>(Resource.Id.shipview);
            ProgressBar auxiliarypower = FindViewById<ProgressBar>(Resource.Id.auxiliarypower);
            ProgressBar Healthbar = FindViewById<ProgressBar>(Resource.Id.HealthBar);
            ProgressBar enemyhealthBar = FindViewById<ProgressBar>(Resource.Id.EnemyHealthBar);
            SeekBar weaponPower = FindViewById<SeekBar>(Resource.Id.powerbar);
            weaponPower.Max = ship.Getmaxweaponpower();
            SeekBar shipspeed = FindViewById<SeekBar>(Resource.Id.shipspeed);
            shipspeed.Max = ship.Getmaxspeed();
            SeekBar shipshield = FindViewById<SeekBar>(Resource.Id.shipshield);
            shipshield.Max = ship.Getmaxshields();
            Shipresourceid srid = new Shipresourceid();
            shipname.Text = ship.GetShipname();
            shipview.SetImageResource(srid.Getshipresourceid(ship));
            Button Engage = FindViewById<Button>(Resource.Id.Engagebutton);
            Button Navigate = FindViewById<Button>(Resource.Id.Navigation);
            Button Disengage = FindViewById<Button>(Resource.Id.DisengageButton);
            Button Hail = FindViewById<Button>(Resource.Id.HailButton);
            Button FireWeapons = FindViewById<Button>(Resource.Id.Fireweapon);
            Button Scan = FindViewById<Button>(Resource.Id.ScanButton);

            auxiliarypower.Max = ship.GetTotalpower();
            auxiliarypower.Progress = auxiliarypower.Max;

            Healthbar.Max = ship.GetmaxHealth();
            Healthbar.Progress = ship.GetHealth();

            weaponPower.Progress = ship.GetweaponPower();
            shipspeed.Progress = ship.Getspeed();
            shipshield.Progress = ship.Getshields();

            Boolean inconflict = false;

            void auxilarypowerchanged()
            {
                auxiliarypowertext.Text = "Unused power:" + ((ship.Getmaxauxiliarypower() - ship.Getauxiliarypower() - ship.GetTotalpower()) * -1);
            }

            Engage.Click += delegate
            {               
                    EngageConflict();
                    inconflict = true;
            };
            Navigate.Click += delegate
            {
                var intent = new Intent(this, typeof(nav));
                intent.PutStringArrayListExtra("phone_numbers", _phoneNumbers);
                StartActivity(intent);
            };
            Disengage.Click += delegate
            {
                DisengageConflict();
                inconflict = false;
            };
            Hail.Click += delegate
            {
                HailConflict();
            };
            FireWeapons.Click += delegate 
            {
                FireConflict();
            };
            Scan.Click += delegate
            {
                ScanConflict();
            };            

            weaponPower.ProgressChanged += (a, b) =>
                {
                    if (weaponPower.Progress >= 0)
                    {
                        ship.SetweaponPower(weaponPower.Progress);
                    }
                    else { ship.SetweaponPower(weaponPower.Progress * -1); }
                    ShipPowertext.Text = "Shippower:" + ship.GetweaponPower();
                    auxiliarypower.Progress = ((ship.Getmaxauxiliarypower() - ship.Getauxiliarypower() - ship.GetTotalpower()) * -1);
                    auxilarypowerchanged();
                };

                shipspeed.ProgressChanged += (a, b) =>
                {
                    if (shipspeed.Progress >= 0)
                    {
                        ship.Setspeed(shipspeed.Progress);
                    }
                    else { ship.Setspeed(shipspeed.Progress * -1); }

                    Shipspeedtext.Text = "Shipspeed:" + ship.Getspeed();
                    auxiliarypower.Progress = ((ship.Getmaxauxiliarypower() - ship.Getauxiliarypower() - ship.GetTotalpower()) * -1);
                    auxilarypowerchanged();
                };
                shipshield.ProgressChanged += (a, b) =>
                {
                    if (shipshield.Progress >= 0)
                    {
                        ship.Setshields(shipshield.Progress);
                    }
                    else { ship.Setshields(shipshield.Progress * -1); }


                    Shipshieldtext.Text = "Shipshield:" + ship.Getshields();
                    auxiliarypower.Progress = ((ship.Getmaxauxiliarypower() - ship.Getauxiliarypower() - ship.GetTotalpower()) * -1);
                    auxilarypowerchanged();
                };

                void EngageConflict()
                {
                turntimer = 0;
                    AssetManager assets = Assets;
                    List<string> Conflicts = new List<string>();

                foreach (var conf in assets.List("Conflicts"))
                    {
                        Conflicts.Add(conf);
                    }

                Random rn = new Random();
                int Irnconflict = rn.Next(0, Conflicts.Count());
                string Srnconflict = Conflicts[Irnconflict];
                string content;

                using (StreamReader sr = new StreamReader(assets.Open("Conflicts/"+ Srnconflict)))
                    {
                        content = sr.ReadLine();                        
                    }
                enemyshipname.Visibility = ViewStates.Visible;
                enemyhealthBar.Visibility = ViewStates.Visible;
                ConflicDescription.SetTextColor(Color.Red);
                ConflicDescription.Text = content;
                shipview.Visibility = ViewStates.Gone;                  
                Engage.Visibility = ViewStates.Gone;
                Disengage.Visibility = ViewStates.Visible;
                Hail.Visibility = ViewStates.Visible;
                FireWeapons.Visibility = ViewStates.Visible;
                Scan.Visibility = ViewStates.Visible;

                
                RandomConflict = new Conflict(Srnconflict);

                enemyhealthBar.Max = RandomConflict.GetEnemyMaxHealth();
                enemyhealthBar.Progress = RandomConflict.GetEnemyHealth();
                enemyshipname.Text = RandomConflict.GetEnemyShipname();
            }
                void DisengageConflict()
            {
                ConflicDescription.SetTextColor(Color.Green);
                ConflicDescription.Text = "Green Alert";
                enemyshipname.Visibility = ViewStates.Gone;
                enemyhealthBar.Visibility = ViewStates.Gone;
                shipview.Visibility = ViewStates.Visible;
                Engage.Visibility = ViewStates.Visible;
                Disengage.Visibility = ViewStates.Gone;
                Hail.Visibility = ViewStates.Gone;
                FireWeapons.Visibility = ViewStates.Gone;
                Scan.Visibility = ViewStates.Gone;

            }

            void HailConflict()
            {
                ConflicDescription.Text = RandomConflict.Hail();
               
                NextTurn();
            }
            void FireConflict()
            {                
                combatlog.Text = RandomConflict.Underfire(ship.GetweaponPower());
                NextTurn();
            }
            void ScanConflict()
            {
                ConflicDescription.Text= RandomConflict.Nextscan();
                NextTurn();
            }
            void NextTurn()
            {
                if (RandomConflict.enemy == false)
                {
                    DisengageConflict();
                }
                else
                {
                    if ((ship.Getmaxauxiliarypower() - ship.Getauxiliarypower() - ship.GetTotalpower()) * -1 >= 0)
                    {
                        turntimer++;
                        EnemyAttack = RandomConflict.Nextattack();
                        ship.ChangeHealth(Convert.ToInt32(EnemyAttack));
                        Statchanged();
                        ConflicDescription.Text = "Red Alert || Turn: " + turntimer + " || You took " + EnemyAttack + " Damage";
                    }
                }
            }

            void Statchanged()
            {           
                    Healthbar.Progress = ship.GetHealth();
                    auxilarypowerchanged();
                    enemyhealthBar.Progress = RandomConflict.GetEnemyHealth();
                    if (ship.Healthcheck() == false)
                    {
                        ConflicDescription.Text = "YOU DIED!";
                        ship.SetTotalauxilarypower(0);
                        ship.ChangeClass("Ghost");
                        ship.ChangeShipname("Ghost");
                    }
               
            }




        }





    }
}

