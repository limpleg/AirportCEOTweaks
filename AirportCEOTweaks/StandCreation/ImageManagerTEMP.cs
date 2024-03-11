using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AirportCEOTweaks.StandCreation
{
    public static class ImageManagerTEMP
    {
        public static readonly string MediumSmallStandPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Airport CEO\\uModFramework\\Humoresques Temp Images\\SubMedium-A-1.png";

        public static Sprite MediumSmallStandSprite()
        {
            try
            {
                if (File.Exists(MediumSmallStandPath))
                {
                    AirportCEOTweaks.Log("hi");
                }
                Sprite sprite = Utils.LoadImage(MediumSmallStandPath);
                if (sprite == null)
                {
                    AirportCEOTweaks.Log("hi 2");
                }

                return sprite;
            }
            catch (Exception ex)
            {
                AirportCEOTweaks.Log("Error occured image man. " + ex.Message);
                return null;
            }
        }

         public static bool CompareTexture (Texture2D first, Texture2D second)
        {
            Color[] firstPix = first.GetPixels();
            Color[] secondPix = second.GetPixels();
            if (firstPix.Length!= secondPix.Length)
            {
                return false;
            }
            for (int i= 0;i < firstPix.Length;i++)
            {
                if (firstPix[i] != secondPix[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
