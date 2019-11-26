using MSCLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Satzooma
{
    public class Satzooma : Mod
    {
        public override string ID => "me.jfoster.Satzooma"; //Your mod ID (unique)
        public override string Name => "Satzooma"; //You mod name
        public override string Author => "Rave"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => false;

        Dictionary<string, Settings> settings = NewSettings(
            new Settings("AWD", "Enable All Wheel Drive", false),
            new Settings("FifthGear", "Enable Fifth Gear", false),
            new Settings("Auto", "Enable Automatic Gearshifts", false)
        );

        public override void OnNewGame()
        {
            // Called once, when starting a New Game, you can reset your saves here
        }

        public override void OnLoad()
        {
            Drivetrain drivetrain = null;
            try
            {
                drivetrain = GameObject.Find("SATSUMA(557kg, 248)").GetComponent<Drivetrain>();
            }
            catch (Exception ex)
            {
                ModConsole.Error(string.Format("Error: {0}\r\nStacktrace: {1}", ex.Source, ex.StackTrace));
            }

            if (drivetrain != null)
            {
                if ((bool)settings["AWD"].GetValue())
                {
                    drivetrain.SetTransmission(Drivetrain.Transmissions.AWD);
                }

                if ((bool)settings["Auto"].GetValue())
                {
                    drivetrain.automatic = true;
                }

                if ((bool)settings["FifthGear"].GetValue())
                {
                    List<float> list = new List<float>();
                    list.AddRange(drivetrain.gearRatios);
                    list.Add(0.8f);
                    drivetrain.gearRatios = list.ToArray();
                }
            }
        }

        public override void ModSettings()
        {
            foreach (var item in settings)
            {
                Settings.AddCheckBox(this, item.Value);
            }
        }

        public override void OnSave()
        {
            // Called once, when save and quit
            // Serialize your save file here.
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
        }

        public override void Update()
        {
            // Update is called once per frame
        }

        public static Dictionary<string, Settings> NewSettings(params Settings[] list)
        {
            var dict = new Dictionary<string, Settings>();
            foreach (var item in list)
            {
                dict.Add(item.ID, item);
            }
            return dict;
        }
    }
}
