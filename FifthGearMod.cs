using MSCLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rave.FifthGear
{
    public class FifthGearMod : Mod
    {
        public override string ID => "Rave.FifthGear"; // Your mod ID (unique)
        public override string Name => "FifthGear"; // Your mod name
        public override string Author => "Rave"; // Your Username
        public override string Version => "1.0"; // Version

        public override void OnLoad()
        {
            // Called once, when mod is loading after game is fully loaded
            try
            {
                Drivetrain drivetrain = GameObject.Find("SATSUMA(557kg, 248)").GetComponent<Drivetrain>();
                if (drivetrain != null)
                {
                    List<float> list = new List<float>();
                    list.AddRange(drivetrain.gearRatios);
                    list.Add(0.8f);
                    drivetrain.gearRatios = list.ToArray();
                }
            }
            catch (Exception ex)
            {
                ModConsole.Error(string.Format("Error: {0}\r\nStacktrace: {1}", ex.Source, ex.StackTrace));
            }
        }
    }
}
