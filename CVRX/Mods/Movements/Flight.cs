using ABI_RC.Systems.Movement;
using CVRX.UIs;
using CVRX.Wrappers;
using System;
using UnityEngine;

namespace CVRX.Mods.Movements
{
    internal class Flight
    {
        public static bool IsFlying = false;
        public static bool IsFlyBoost = false;
        public static float FlySpeed = 4.2f;
        public static Transform transform;

        public static void Flyatoggle(bool s)
        {
            if (s)
            {
                State(false);
                HudWrappers.TriggerDropText("Flight", "Flight Disabled.");
            }
            else
            {
                State(true);
                HudWrappers.TriggerDropText("Flight", "Flight Enabled.");
            }
        }

        public static void Render()
        {
            UIEx.Collapsible("Flight", () =>
            {
                if (GUILayout.Button($"In-World flight:{(UtilsStuff.FlightForced ? "Forced" : "Unforced")}"))
                {
                    UtilsStuff.ForceFlightStat();
                }
                IsFlying = UIEx.Checkbox(IsFlying, IsFlying ? "Enabled" : "Disabled", (newState) =>
                {
                    Flyatoggle(IsFlying);
                });
                GUILayout.Label($"Flight Speed:{FlySpeed}");
                FlySpeed = (int)GUILayout.HorizontalSlider(FlySpeed, 4.2F, 20);
            });
        }

        public static void State(bool s)
        {
            if (s)
            {
                if (transform == null) transform = Camera.main.transform;
                PlayerWrappers.GetLocalPlayerSetup().GetComponent<BetterBetterCharacterController>().gravityScale = 0f;
                IsFlying = true;
            }
            else
            {
                IsFlying = false;
                PlayerWrappers.GetLocalPlayerSetup().enabled = true;
                PlayerWrappers.GetLocalPlayerSetup().GetComponent<BetterBetterCharacterController>().gravityScale = 1.0f;
            }
        }
        public static void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) & Input.GetKey(KeyCode.LeftControl))
            {
                IsFlying = !IsFlying;
                Flyatoggle(!IsFlying);
            }
            if (IsFlying)
            {
                if (GeneralWrappers.IsInVr())
                {
                    if (Math.Abs(Input.GetAxis("Vertical")) != 0f) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.forward * FlySpeed * Time.deltaTime * Input.GetAxis("Vertical");
                    if (Math.Abs(Input.GetAxis("Horizontal")) != 0f) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.right * FlySpeed * Time.deltaTime * Input.GetAxis("Horizontal");
                    if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0f) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * FlySpeed * Time.deltaTime * Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical");
                    if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0f) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * FlySpeed * Time.deltaTime * Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical");
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        if (!IsFlyBoost)
                        {
                            FlySpeed *= 2f;
                            IsFlyBoost = true;
                        }
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        if (IsFlyBoost)
                        {
                            FlySpeed /= 2f;
                            IsFlyBoost = false;
                        }
                    }
                    if (Input.GetKey(KeyCode.E)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * FlySpeed * Time.deltaTime;
                    else if (Input.GetKey(KeyCode.Space)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * FlySpeed * Time.deltaTime;
                    else if (Input.GetKey(KeyCode.Q)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.up * -1f * FlySpeed * Time.deltaTime;
                    if (Input.GetKey(KeyCode.W)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.forward * FlySpeed * Time.deltaTime;
                    else if (Input.GetKey(KeyCode.S)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.forward * -1f * FlySpeed * Time.deltaTime;
                    if (Input.GetKey(KeyCode.A)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.right * -1f * FlySpeed * Time.deltaTime;
                    else if (Input.GetKey(KeyCode.D)) PlayerWrappers.GetLocalPlayer().transform.position += transform.transform.right * FlySpeed * Time.deltaTime;
                }
            }
        }
    }
}