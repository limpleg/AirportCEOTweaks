using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirportCEOTweaks.StandCreation
{
    public static class StandManager
    {
        // STP -> Stand Prefabs
        public static GameObject STP_RawMediumStand { get; set; }
        public static GameObject STP_MediumSmallStand { get; set; }
        public static GameObject STP_LargeMediumStand { get; private set; }
        public static GameObject STP_SubLargeStand { get; private set; }

        // Utility variables
        public static bool Initialized { get; private set; }

        // Invoked by other scripts
        public static Action PrefabsSetUp;

        // Invoded by this script
        public static Action StartStandCreation;

        // Instant Set-up
        static StandManager()
        {
            Log("In startup method");
            PrefabsSetUp += OnInitialized;
        }

        public static void OnInitialized()
        {
            Log("In initialized function");
            Initialized = true;
            StartStandCreation.Invoke();
        }

        // Utility Methods
        public static void Log(string message)
        {
            AirportCEOTweaks.Log($"[Stand Manager] {message}");
        }
    }
}
