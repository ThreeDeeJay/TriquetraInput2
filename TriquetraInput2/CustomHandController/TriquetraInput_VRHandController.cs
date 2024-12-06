using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace Triquetra.Input.CustomHandController
{
    [HarmonyPatch]
    public class TriquetraInput_VRHandController : VRHandController
    {
        public override void Awake()
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
            HapticPulse(totalVibePower);
        }

        public override void OnEnable()
        {
            if (controllers == null)
            {
                controllers = new List<VRHandController>();
            }
            if (interactionTransform) return;
            interactionTransform = new GameObject("interactionTf").transform;
            interactionTransform.parent = transform;
            interactionTransform.localPosition = Vector3.zero;
        }

        public override void OnDisable()
        {
        }

        public override void OnDestroy()
        {
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.HapticPulse))]
        [HarmonyPrefix]
        public static bool P_HapticPulse(VRHandController __instance)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.Vibrate))]
        [HarmonyPrefix]
        public static bool P_Vibrate(VRHandController __instance)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetStickPressDown))]
        [HarmonyPrefix]
        public static bool P_GetStickPressDown(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = false;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetStickPressUp))]
        [HarmonyPrefix]
        public static bool P_GetStickPressUp(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = false;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetSecondButtonDown))]
        [HarmonyPrefix]
        public static bool P_GetSecondButtonDown(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = false;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetSecondButtonUp))]
        [HarmonyPrefix]
        public static bool P_GetSecondButtonUp(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = false;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetThumbButtonDown))]
        [HarmonyPrefix]
        public static bool P_GetThumbButtonDown(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = false;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetThumbButtonUp))]
        [HarmonyPrefix]
        public static bool P_GetThumbButtonUp(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = false;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetTriggerClickDown))]
        [HarmonyPrefix]
        public static bool P_GetTriggerClickDown(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = false;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetTriggerClickUp))]
        [HarmonyPrefix]
        public static bool P_GetTriggerClickUp(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = false;
            return false;
        }
        // STICK AXIS
        public void StickAxis(Vector2 axis)
        {
            var eventDelegate = (MulticastDelegate)typeof(VRHandController).GetField(nameof(OnStickAxis), BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { this, axis });
                }
            }

            stickAxis = axis;
        }

        // THUMB BUTTON
        public void ThumbButtonPressed()
        {
            var eventDelegate = (MulticastDelegate)typeof(VRHandController).GetField(nameof(OnThumbButtonPressed), BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { });
                }
            }
        }

        public void ThumbButtonReleased()
        {
            
            var eventDelegate = (MulticastDelegate)typeof(VRHandController).GetField(nameof(OnSecondaryThumbButtonReleased), BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { });
                }
            }
        }

        // SECONDARY THUMB BUTTON
        public void SecondaryThumbButtonPressed()
        {
            
            var eventDelegate = (MulticastDelegate)typeof(VRHandController).GetField(nameof(SecondaryThumbButtonPressed), BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { });
                }
            }
        }

        public void SecondaryThumbButtonReleased()
        {
            
            var eventDelegate = (MulticastDelegate)typeof(VRHandController).GetField(nameof(SecondaryThumbButtonReleased), BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { });
                }
            }
        }

        // THUMBSTICK BUTTON
        public void ThumbstickButtonPressed()
        {
            
            var eventDelegate = (MulticastDelegate)typeof(VRHandController).GetField(nameof(OnStickPressed), BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { });
                }
            }
        }

        public void ThumbstickButtonReleased()
        {
            
            var eventDelegate = (MulticastDelegate)typeof(VRHandController).GetField(nameof(OnStickUnpressed), BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { });
                }
            }
        }

        // TRIGGER AXIS
        public void TriggerAxis(float axis)
        {
            
            var eventDelegate = (MulticastDelegate)typeof(VRHandController).GetField(nameof(OnTriggerAxis), BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { this, axis });
                }
            }
        }
    }
}