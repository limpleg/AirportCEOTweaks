using System;
using System.Collections;
using UnityEngine;
using HarmonyLib;
using System.Reflection;

namespace AirportCEOAircraft
{
	[HarmonyPatch(typeof(GameController))]
	static class Patch_GameControllerToAddNewPrefabs
	{
		private static AircraftAdder adder;
		private static int iteration = 0;
		private static IEnumerator item;

		[HarmonyPostfix]
		[HarmonyPatch("Awake")]
		public static void Patch_AddPrefabs(GameController __instance)
		{
			adder = Singleton<AirTrafficController>.Instance.gameObject.AddComponent<AircraftAdder>();
			ModLoaderInteractionHandler.AddCoroutineReference(adder);
			AirportCEOAircraft.TweaksLogger.LogMessage("Added aircraft adder, set up callback with mod loader!");
		}
	}
}