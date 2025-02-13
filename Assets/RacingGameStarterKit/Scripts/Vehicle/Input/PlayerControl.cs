//PlayerControl.cs handles user input

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RGSK
{
    public class PlayerControl : MonoBehaviour
    {
        public enum InputTypes { Desktop, Mobile, Automatic }
        public enum MobileSteerType { TiltToSteer, TouchSteer }

        [Header("Input Platform")]
        public InputTypes inputType = InputTypes.Automatic;

        private Car_Controller car_controller;
        private Motorbike_Controller bike_controller;

        [Header("Mobile Controls")]
        public MobileSteerType mobileSteerType;

        [Header("Other")]
        public bool autoAcceleration;

        void Awake()
        {
            if (GetComponent<Car_Controller>())
                car_controller = GetComponent<Car_Controller>();

            if (GetComponent<Motorbike_Controller>())
                bike_controller = GetComponent<Motorbike_Controller>();
        }

        void Start()
        {

            if (!InputManager.instance)
            {
                Debug.LogError("No Input Manager Found! Please Create an Input Manager by going to Window/RacingGameStarterKit/RaceComponents");
                enabled = false;
                return;
            }

            autoAcceleration = (PlayerPrefs.GetString("AutoAcceleration") == "True");

            if (inputType == InputTypes.Mobile || inputType == InputTypes.Automatic)
            {
                if (MobileControlManager.instance)
                {
                    MobileControlManager.instance.UpdateControls(this);
                }
            }
        }

        void Update()
        {
            switch (inputType)
            {
                case InputTypes.Desktop:
                    DesktopControl();
                    break;

                case InputTypes.Mobile:
                    MobileControl();
                    break;

                case InputTypes.Automatic:
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL
                    DesktopControl();
#else
					MobileControl();
#endif
                    break;
            }
        }

        void DesktopControl()
        {

            switch (InputManager.instance.inputDevice)
            {
                case InputManager.InputDevice.Keyboard:

                    //Get input values
                    float accel = (!autoAcceleration) ? Mathf.Clamp01(Input.GetAxis(InputManager.instance.keyboardInput.verticalAxis)) : 1.0f;
                    float brake = Mathf.Clamp01(-Input.GetAxis(InputManager.instance.keyboardInput.verticalAxis));
                    float handbrake = Mathf.Clamp01(Input.GetAxis(InputManager.instance.keyboardInput.handbrakeAxis));
                    float steer = Mathf.Clamp(Input.GetAxis(InputManager.instance.keyboardInput.horizontalAxis), -1, 1);
                    bool nitro = Input.GetKey(InputManager.instance.keyboardInput.nitroKey);

                    //Send inputs
                    SendInputs(accel, brake, steer, handbrake, nitro);

                    //Pause
                    if (Input.GetKeyDown(InputManager.instance.keyboardInput.pauseKey))
                    {
                        if (RaceManager.instance)
                            RaceManager.instance.PauseRace();
                    }

                    //Respawn
                    if (Input.GetKeyDown(InputManager.instance.keyboardInput.respawnKey))
                    {
                        Respawn();
                    }

                    break;

                case InputManager.InputDevice.XboxController:
                    // for single player
                    //float pluto_accel = AppData.plutoData.input == 0 ? 1 : 0;

                    //float pluto_brake = AppData.plutoData.input;
                    //for multiplayer
                    // float pluto_accel = AppData.plutoData.torq >= 0 ? Mathf.Clamp01(AppData.plutoData.torq)*2f : 0;

                    // float pluto_brake = AppData.plutoData.torq < 0 ? Mathf.Clamp01(-AppData.plutoData.torq)*2f : 0;
                    bool pluto_nitro = false;// AppData.plutoData.input == 0 ? true : false;


                    float minMovement = 0;
                    //float pluto_steer = Mathf.Abs(AppData.plutoData.angle) > minMovement ? Mathf.Clamp((AppData.plutoData.angle - Mathf.Sign(AppData.plutoData.angle) * minMovement) / 40, -1, 1) : 0;
                    float pluto_handbrake = Input.GetButton(InputManager.instance.xboxControllerInput.handbrakeButton) ? 0 : 0;


                    //Send inputs
                    //SendInputs(pluto_accel, pluto_brake, pluto_steer, pluto_handbrake, pluto_nitro);

                    //Pause
                    if (Input.GetButtonDown(InputManager.instance.xboxControllerInput.pauseButton))
                    {
                        if (RaceManager.instance)
                            RaceManager.instance.PauseRace();
                    }

                    //Respawn
                    if (Input.GetButton(InputManager.instance.xboxControllerInput.respawnButton))
                    {
                        Respawn();
                    }

                    break;
                case InputManager.InputDevice.PLUTO:

                    ////Get input values
                    //float pluto_accel = (!autoAcceleration) ? Mathf.Clamp01(AppData.plutoData.input) : 1.0f;
                    //float pluto_brake = Mathf.Clamp01(Mathf.Clamp01(AppData.plutoData.input));
                    //float pluto_steer = Mathf.Clamp(AppData.plutoData.angle/90, -1, 1);
                    //float pluto_handbrake = Input.GetButton(InputManager.instance.xboxControllerInput.handbrakeButton) ? 0 : 0;
                    //bool pluto_nitro = Input.GetButton(InputManager.instance.xboxControllerInput.nitroButton);

                    ////Send inputs
                    //SendInputs(pluto_accel, pluto_brake, pluto_steer, pluto_handbrake, pluto_nitro);

                    ////Pause
                    //if (Input.GetButtonDown(InputManager.instance.xboxControllerInput.pauseButton))
                    //{
                    //    if (RaceManager.instance)
                    //        RaceManager.instance.PauseRace();
                    //}

                    ////Respawn
                    //if (Input.GetButton(InputManager.instance.xboxControllerInput.respawnButton))
                    //{
                    //    Respawn();
                    //}

                    break;
            }
        }

        void MobileControl()
        {

            float steer = 0;

            if (mobileSteerType == MobileSteerType.TiltToSteer)
            {
                //steer according to the device tilt amount
                steer = Input.acceleration.x;
            }
            else
            {
                //steer with the on-screen ui buttons
                if (InputManager.instance.mobileInput.steerRight != null && InputManager.instance.mobileInput.steerLeft != null)
                {
                    steer = InputManager.instance.mobileInput.steerRight.inputValue + (-InputManager.instance.mobileInput.steerLeft.inputValue);
                }
            }

            //send inputs
            float _accel = (!autoAcceleration) ? InputManager.instance.mobileInput.accelerate.inputValue : 1.0f;
            float _brake = InputManager.instance.mobileInput.brake.inputValue;
            float _handbrake = (InputManager.instance.mobileInput.handBrake) ? InputManager.instance.mobileInput.handBrake.inputValue : 0;
            bool _nitro = (InputManager.instance.mobileInput.nitro) ? InputManager.instance.mobileInput.nitro.buttonPressed : false;

            SendInputs(_accel, _brake, steer, _handbrake, _nitro);
        }

        void SendInputs(float accel, float brake, float steer, float handbrake, bool nitro)
        {
            if (car_controller)
            {
                car_controller.motorInput = (brake <= 0) ? accel : 0;
                car_controller.brakeInput = brake;
                car_controller.steerInput = steer;
                car_controller.handbrakeInput = handbrake;
                car_controller.usingNitro = nitro;
            }

            if (bike_controller)
            {
                bike_controller.motorInput = (brake <= 0) ? accel : 0;
                bike_controller.brakeInput = brake;
                bike_controller.steerInput = steer;
                bike_controller.usingNitro = nitro;
            }
        }

        public void Respawn()
        {
            if (RaceManager.instance)
                RaceManager.instance.RespawnRacer(transform, GetComponent<Statistics>().lastPassedNode, 3.0f);
        }
    }
}
