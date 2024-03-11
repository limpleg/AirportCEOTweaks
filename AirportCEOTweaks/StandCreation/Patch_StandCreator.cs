using AirportCEOTweaks.StandCreation;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirportCEOTweaks
{
    [HarmonyPatch(typeof(SaveLoadGameDataController))]
    public static class Patch_StandCreator
    {
        [HarmonyPatch("StartNewGame")]
        public static void Prefix(SaveLoadGameDataController __instance)
        {
            return;
            AirportCEOTweaks.Log("initial test");
            StandCreator.GetStandTemplate();
        }

    }
}
