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
        public bool primaryButtonDown;
        public bool primaryButtonUp;
        
        public bool secondaryButtonDown;
        public bool secondaryButtonUp;
        
        public bool triggerButtonDown;
        public bool triggerButtonUp;
        
        public override void Awake()
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
            // Stick
            if (stickPressed)
            {
                stickPressDown = !wasStickPressed;
                wasStickPressed = true;
            }
            else
            {
                stickPressUp = wasStickPressed;
                wasStickPressed = false;
            }
            // Primary
            if (thumbButtonPressed)
            {
                primaryButtonDown = !thumbButtonPressed;
                thumbButtonPressed = true;
            }
            else
            {
                primaryButtonUp = thumbButtonPressed;
                thumbButtonPressed = false;
            }
            // Secondary
            if (secondaryThumbButtonPressed)
            {
                secondaryButtonDown = !secondaryThumbButtonPressed;
                secondaryThumbButtonPressed = true;
            }
            else
            {
                secondaryButtonUp = secondaryThumbButtonPressed;
                secondaryThumbButtonPressed = false;
            }
            // Trigger
            if (triggerClicked)
            {
                triggerButtonDown = !triggerClicked;
                triggerClicked = true;
            }
            else
            {
                triggerButtonUp = triggerClicked;
                triggerClicked = false;
            }
            //HapticPulse(totalVibePower);
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
            __result = _instance.stickPressDown;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetStickPressUp))]
        [HarmonyPrefix]
        public static bool P_GetStickPressUp(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = _instance.stickPressUp;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetSecondButtonDown))]
        [HarmonyPrefix]
        public static bool P_GetSecondButtonDown(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = _instance.secondaryButtonDown;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetSecondButtonUp))]
        [HarmonyPrefix]
        public static bool P_GetSecondButtonUp(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = _instance.secondaryButtonUp;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetThumbButtonDown))]
        [HarmonyPrefix]
        public static bool P_GetThumbButtonDown(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = _instance.primaryButtonDown;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetThumbButtonUp))]
        [HarmonyPrefix]
        public static bool P_GetThumbButtonUp(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = _instance.primaryButtonUp;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetTriggerClickDown))]
        [HarmonyPrefix]
        public static bool P_GetTriggerClickDown(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = _instance.triggerButtonDown;
            return false;
        }

        [HarmonyPatch(typeof(VRHandController), nameof(VRHandController.GetTriggerClickUp))]
        [HarmonyPrefix]
        public static bool P_GetTriggerClickUp(VRHandController __instance, ref bool __result)
        {
            if (__instance is not TriquetraInput_VRHandController _instance)
                return true;
            __result = _instance.triggerButtonUp;
            return false;
        }
        
        // STICK AXIS
        public void StickAxis(Vector2 axis)
        {
            stickAxis = axis;
            
            var eventDelegate = (MulticastDelegate)typeof(VRHandController).GetField(nameof(OnStickAxis), BindingFlags.Instance | BindingFlags.Public)?.GetValue(this);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new object[] { this, axis });
                }
            }
        }

        // THUMB BUTTON
        public void ThumbButtonPressed()
        {
            thumbButtonPressed = true;
            
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
            thumbButtonPressed = false;
            
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
            secondaryThumbButtonPressed = true;
            
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
            secondaryThumbButtonPressed = false;
            
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
            stickPressed = true;
            
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
            stickPressed = false;
            
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
            if (axis < 0.0001f)
                triggerClicked = false;
            if (axis > 0.9999f)
                triggerClicked = true;
            
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