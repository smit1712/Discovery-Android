using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.IO;
using System;
using Android.Content.Res;
using Android.Content;
using System.Runtime.Remoting.Contexts;
using System.Linq;
using System.Text;
using Android.Runtime;
using Android.Views;
using static Android.InputMethodServices.InputMethodService;

namespace SpaceGame
{

    public class Conflict
    {

        string Currentconflict;
        string HailMessage;
        string fase1;
        string fase2;
        string fase2_startCondition;
        bool fase2_enterd;
        string fase3;
        string fase3_startCondition;
        bool fase3_enterd;
        string fase4;
        string fase4_startCondition;
        bool fase4_enterd;

        bool GotHailed = false;
        bool gotscanned = false;
        PlayerShip enemyship = new PlayerShip(0, 0, 0, 0, 0, "", "");
        List<string> lines = new List<string>();

        public Conflict(string ConflictName)
        {
            using (StreamReader sr = new StreamReader(Application.Context.Assets.Open("Conflicts/" + ConflictName)))
            {
                while (sr.Peek() >= 0)
                {
                    lines.Add(sr.ReadLine());
                }

                Currentconflict = sr.ReadLine();
                enemyship.health = Convert.ToInt32(lines[1]);  
                enemyship.maxhealth = Convert.ToInt32(lines[1]);
                enemyship.weaponPower = Convert.ToInt32(lines[2]);
                enemyship.speed = Convert.ToInt32(lines[3]);
                enemyship.shields = Convert.ToInt32(lines[4]);
                enemyship.totalpower = Convert.ToInt32(lines[5]);
                enemyship.Sethailmassage(lines[6]);
                enemyship.Shipname = lines[7];
                enemyship.shipclass = lines[8];
                fase1 = lines[10];
                fase2_startCondition = lines[20];
                fase2 = lines[21];
                fase3_startCondition = lines[30];
                fase3 = lines[31];
                fase4_startCondition = lines[40];
                fase4 = lines[41];
            }

        }
        public Conflict() { }

        private void PowerAI()
        {
            while (enemyship.EnoughPowercheck() == false)
            {
                enemyship.Changeshields(-1);
                enemyship.Changespeed(-1);
                enemyship.ChangeweaponPower(-1);
            }
            while (enemyship.TomuchPowercheck() == false)
            {
                enemyship.Changeshields(+1);
                enemyship.Changespeed(+1);
                enemyship.ChangeweaponPower(+1);
            }

        }


        public string Hail()
        {
            GotHailed = true;
            return HailMessage;
            
        }
        public string Underfire(int damage)
        {
            string combatlog = enemyship.ChangeHealth(damage);
            NextTurn();
            return combatlog;
        }
        public string Nextattack()
        {
            
            return Convert.ToString(enemyship.GetweaponPower());                              
        }
    
        public string Nexthail()
        {
            
            return enemyship.Gethailmassage();
        }
        public string Nextscan()
        {
            gotscanned = true;
            return "scan doesnt show anything";
            
        }
        public void NextTurn()
        {
            PowerAI();


        }
        public string GetEnemyShipname()
        {
            return enemyship.GetShipname();
        }
        public int GetEnemyHealth()
        {
            return enemyship.GetHealth();
        }
        public int GetEnemyMaxHealth()
        {
            return enemyship.GetmaxHealth();
        }
        private void FaseCheck()
        {
            if (fase2_enterd == false)
            {
                switch (fase2_startCondition)
                {
                    case "half health":
                        if (enemyship.GetmaxHealth() / 2 < enemyship.GetHealth())
                        {
                            fase2_enterd = true;
                        }
                        break;

                    case "no shields":
                        if (enemyship.Getshields() <= 0)
                        {
                            fase2_enterd = true;
                        }
                        break;


                    case "Hailed":
                        if (GotHailed == true)
                        {
                            fase2_enterd = true;
                        }
                        break;

                    case "scanned":
                        if (gotscanned == true)
                        {
                            fase2_enterd = true;
                        }
                        break;

                }
            }
            else
            {
                if (fase3_enterd == false)
                {
                    switch (fase3_startCondition)
                    {
                        case "half health":
                            if (enemyship.GetmaxHealth() / 2 < enemyship.GetHealth())
                            {
                                fase3_enterd = true;
                            }
                            break;

                        case "no shields":
                            if (enemyship.Getshields() <= 0)
                            {
                                fase3_enterd = true;
                            }
                            break;

                        case "Hailed":
                            if (GotHailed == true)
                            {
                                fase3_enterd = true;
                            }
                            break;

                        case "scanned":
                            if (gotscanned == true)
                            {
                                fase3_enterd = true;
                            }
                            break;

                    }
                }
                else
                {                    
                        if (fase4_enterd == false)
                        {
                            switch (fase4_startCondition)
                            {
                                case "half health":
                                    if (enemyship.GetmaxHealth() / 2 < enemyship.GetHealth())
                                    {
                                        fase4_enterd = true;
                                    }
                                    break;

                                case "no shields":
                                    if (enemyship.Getshields() <= 0)
                                    {
                                        fase4_enterd = true;
                                    }
                                    break;

                                case "Hailed":
                                    if (GotHailed == true)
                                    {
                                        fase4_enterd = true;
                                    }
                                    break;

                                case "scanned":
                                    if (gotscanned == true)
                                    {
                                        fase4_enterd = true;
                                    }
                                    break;

                            }
                        }
                }
            }
        }
    }
}