using AirportCEOTweaks.StandCreation;
using HarmonyLib;
using PlacementStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirportCEOTweaks
{
    class CustomBuildingController
    {
        public static void spawnItem(GameObject item)
        {
            try
            {
                // Get template
                ObjectPlacementController placementController = Singleton<BuildingController>.Instance.GetComponent<ObjectPlacementController>();

                if (item != null && placementController != null)
                {
                    if (!item.activeSelf)
                    {
                        item.SetActive(true);
                    }

                    //VariationsHandler.currentVariationIndex;
                    //AirportCEOTweaks.Log("2 is " + ImageManagerTEMP.CompareTexture(item.transform.GetChild(0).GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite.texture, ImageManagerTEMP.MediumSmallStandSprite().texture));

                    GameObject obj = GameObject.Instantiate(StandManager.STP_MediumSmallStand);
                    obj.SetActive(true);
                    obj.transform.Find("Sprite").Find("FoundationSprite").GetComponent<SpriteRenderer>().sprite = ImageManagerTEMP.MediumSmallStandSprite();

                    placementController.SetObject(StandManager.STP_MediumSmallStand, 0);

                }
            }
            catch (Exception ex)
            {
                AirportCEOTweaks.Log("error occured in here " + ex.Message);
            }
        }
    }

    [HarmonyPatch(typeof(PlacementUtils), "InstantiateObject")]
    public static class Patch_SetCurrentStand
    {
        public static void Postfix(PlacementUtils __instance, PlaceableObject __result)
        {
            if (!__result.TryGetComponent(out StandComponent standComponent))
            {
                return;
            }

            if (!__result is StandModel)
            {
                AirportCEOTweaks.Log("huh");
                return;
            }

            StandModel standModel = __result as StandModel;

            AirportCEOTweaks.Log("hi " + standModel);
            StandDevelopmentTools.CurrentStandModel = standModel;
            AirportCEOTweaks.Log(StandDevelopmentTools.CurrentStandModel.ToString());
        }
    }

    [HarmonyPatch(typeof(PlaceObjectNonDrag), "PlaceObject")]
    public static class Patch_Testing
    {
        public static void Prefix(PlaceObjectNonDrag __instance, GameObject currentObject, PlaceableObject placeableObject)
        {
            AirportCEOTweaks.Log("1111111");
            currentObject.transform.Find("Sprite").Find("FoundationSprite").GetComponent<SpriteRenderer>().sprite = ImageManagerTEMP.MediumSmallStandSprite();
        }
    }
}
