using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace AirportCEOTweaks
{
    [HarmonyPatch(typeof(DevelopmentConsoleController), "ProcessConsoleInput")]
    public static class Patch_StandDevelopmentTools
    {
        public static bool Prefix(DevelopmentConsoleController __instance, string consoleInput)
        {
            AirportCEOTweaks.Log(StandDevelopmentTools.CurrentStandModel.ToString());
            if (!consoleInput.SafeSubstring(0, 6).Equals("Tweaks"))
            {
                return true;
            }

            string[] split = consoleInput.Split(' ');

            // Tweaks Set CS string objectName hi
            if (split[1].Equals("Set"))
            {
                if (split.Length < 6)
                {
                    return true;
                }
                if (!split[2].Equals("CS"))
                {
                    __instance.RecordLogEntry("Invalid Tweaks Input", "consoleOutput", UnityEngine.LogType.Log);
                    return false;
                }

                if (StandDevelopmentTools.CurrentStandModel == null)
                {
                    __instance.RecordLogEntry("Current Stand Is Null", "consoleOutput", UnityEngine.LogType.Log);
                    return false;
                }

                if (!split[3].Equals("transform"))
                {
                    StandDevelopmentTools.SetCSVariable(split[3], split[4], split[5]);
                    return false;
                }
                else
                {
                    StandDevelopmentTools.SetCSTransform(split[4], split[5]);
                    return false;
                }

            }
            else if (consoleInput.SafeSubstring(7, 3).Equals("Get"))
            {

            }
            else if (split[1].Equals("Call"))
            {
                if (split.Length < 4)
                {
                    return true;
                }

                if (!split[2].Equals("CS"))
                {
                    __instance.RecordLogEntry("Invalid Tweaks Input", "consoleOutput", UnityEngine.LogType.Log);
                    return false;
                }

                if (StandDevelopmentTools.CurrentStandModel == null)
                {
                    __instance.RecordLogEntry("Current Stand Is Null", "consoleOutput", UnityEngine.LogType.Log);
                    return false;
                }

                if (!StandDevelopmentTools.CallCSFunc(split[3]))
                {
                    __instance.RecordLogEntry("Invalid function", "consoleOutput", UnityEngine.LogType.Log);
                    return false;
                }

                __instance.RecordLogEntry("Call Successful", "consoleOutput", UnityEngine.LogType.Log);
                
            }
            return false;
        }
    }

    public static class StandDevelopmentTools
    {
        public static StandModel CurrentStandModel { get; set; }

        public static void SetStandModel(StandModel standModel)
        {
            CurrentStandModel = standModel;
        }

        public static bool CallCSFunc(string name)
        {
            MethodInfo methodInfo = typeof(StandModel).GetMethod(name);
            if (methodInfo == null)
            {
                return false;
            }
            try
            {
                methodInfo.Invoke(CurrentStandModel, new object[0] { });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void SetCSTransform(string name, string value)
        {
            try
            {
                if (name.Equals("position"))
                {
                    UnityEngine.Vector3 preValue = CurrentStandModel.transform.position;
                    LogVariable($"Changing {name} from {preValue} to {value}");
                    CurrentStandModel.transform.position = Vector3Parser(value);
                }
                else if (name.Equals("scale"))
                {
                    UnityEngine.Vector3 preValue = CurrentStandModel.transform.localScale;
                    LogVariable($"Changing {name} from {preValue} to {value}");
                    CurrentStandModel.transform.localScale = Vector3Parser(value);
                }
                else
                {
                    LogVariable("Invalid Type!");
                }
            }
            catch (Exception ex)
            {
                LogVariable("An exception occured. Error: " + ex.Message);
            }
        }

        public static void SetCSVariable(string type, string name, string value)
        {
            try
            {
                if (type.Equals("int"))
                {
                    int preValue = Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<int>(name).Value;
                    LogVariable($"Changing {name} from {preValue} to {value}");
                    Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<int>(name).Value = int.Parse(value);
                }
                else if (type.Equals("float"))
                {
                    float preValue = Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<float>(name).Value;
                    LogVariable($"Changing {name} from {preValue} to {value}");
                    Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<float>(name).Value = float.Parse(value);
                }
                else if (type.Equals("string"))
                {
                    string preValue = Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<string>(name).Value;
                    LogVariable($"Changing {name} from {preValue} to {value}");
                    Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<string>(name).Value = value;
                }
                else if (type.Equals("bool"))
                {
                    bool preValue = Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<bool>(name).Value;
                    LogVariable($"Changing {name} from {preValue} to {value}");
                    Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<bool>(name).Value = bool.Parse(value);
                }
                else if (type.Equals("Vector2"))
                {
                    Vector2 preValue = Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<Vector2>(name).Value;
                    LogVariable($"Changing {name} from {preValue} to {value}");
                    Traverse.Create(StandDevelopmentTools.CurrentStandModel).Field<Vector2>(name).Value = Vector3Parser(value);
                }
                else
                {
                    LogVariable("Invalid Type!");
                }
            }
            catch (Exception ex)
            {
                LogVariable("An exception occured. Error: " + ex.Message);
            }
        }

        private static Vector3 Vector3Parser(string input)
        {
            string[] split = input.Split(',');
            if (split.Length != 3)
            {
                return Vector3.zero;
            }

            return new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
        }

        private static void LogVariable(string message)
        {
            DevelopmentConsoleController.Instance.RecordLogEntry(message, "consoleOutput", UnityEngine.LogType.Log);
        }
    }
}
