using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Triquetra.Input.CustomHandController
{
    // Creating a strippeder hand controller to run custom vr interactables betterer
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

        public override void HapticPulse(float power)
        {
        }

        public override void Vibrate(float power, float time)
        {
        }

        public override bool GetStickPressDown()
        {
            return false;
        }

        public override bool GetStickPressUp()
        {
            return false;
        }

        public override bool GetSecondButtonDown()
        {
            return false;
        }

        public override bool GetSecondButtonUp()
        {
            return false;
        }

        public override bool GetThumbButtonDown()
        {
            return false;
        }

        public override bool GetThumbButtonUp()
        {
            return false;
        }

        public override bool GetTriggerClickDown()
        {
            return false;
        }

        public override bool GetTriggerClickUp()
        {
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