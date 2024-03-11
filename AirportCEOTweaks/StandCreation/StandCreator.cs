using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirportCEOTweaks.StandCreation
{
    public static class StandCreator
    {
        public static void GetStandTemplate()
        {
            if (!StandCreationUtilities.GetStandTemplateCopy(Enums.ThreeStepScale.Medium, out GameObject stand))
            {
                return;
            }

            StandManager.STP_RawMediumStand = stand;

            // List of things to do once stand creation starts
            StandManager.StartStandCreation += CreateMediumSmallStand;

            StandManager.PrefabsSetUp.Invoke();
        }

        static void CreateMediumSmallStand()
        {
            try
            {
                // Basic Set-up
                GameObject stand = GameObject.Instantiate(StandManager.STP_RawMediumStand);
                stand.SetActive(false);

                GameObject foundationSprite = stand.transform.GetChild(0).GetChild(2).gameObject;
                SpriteRenderer foundationSpriteRenderer = foundationSprite.GetComponent<SpriteRenderer>();
                foundationSpriteRenderer.sprite = ImageManagerTEMP.MediumSmallStandSprite();
                stand.AddComponent<StandComponent>();

                stand.SetActive(true);
                StandManager.STP_MediumSmallStand = stand;

                bool  hi = ImageManagerTEMP.CompareTexture(foundationSpriteRenderer.sprite.texture, ImageManagerTEMP.MediumSmallStandSprite().texture);
                AirportCEOTweaks.Log("it is " + hi);
                // UI trigger
                StandUICreator.CreateStandUI(StandManager.STP_MediumSmallStand);
                AirportCEOTweaks.Log("1");
            }
            catch (Exception ex)
            {
                AirportCEOTweaks.Log("error occured. " + ex.Message);
            }
        }
    }
}
