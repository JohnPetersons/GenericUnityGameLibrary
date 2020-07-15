using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    button inputs for the first player/controller would be a1, b1, x1, y1, etc.
    */
    public class GameInputState : GameEventListenerState {

        public const string A = "a";
        public const string B = "b";
        public const string X = "x";
        public const string Y = "y";
        public const string LEFT_BUMPER = "leftBumper";
        public const string RIGHT_BUMPER = "rightBumper";
        public const string SELECT = "select";
        public const string START = "start";
        public const string LEFT_STICK_UP_DOWN = "leftStickUpDown";
        public const string LEFT_STICK_LEFT_RIGHT = "leftStickLeftRight";
        private static readonly string[] BUTTON_INPUTS = {A, B, X, Y, LEFT_BUMPER, RIGHT_BUMPER, SELECT, START};
        private static readonly string[] AXIS_INPUTS = {LEFT_STICK_UP_DOWN, LEFT_STICK_LEFT_RIGHT}; 
        // The below strings are for axis I am not currently implementing
        // "leftTrigger", "rightTrigger", "rightStickUpDown", "rightStickLeftRight", "dPadUpDown", "dPadLeftRight"
        public const string KEY_DOWN = "down";
        public const string KEY_HELD = "held";
        public const string KEY_UP = "up";
        private Dictionary<string, string> inputMapping = new Dictionary<string, string>();
        private int playerNumber;

        public GameInputState(GameEventListenerId listener, int i): base(listener) {
            playerNumber = i;
        }

        public override void Begin() {
            base.Begin();
        }

        public override void Tick() {
            base.Tick();
            foreach(string str in GameInputState.BUTTON_INPUTS) {
                string temp = str + playerNumber;
                if (Input.GetButtonDown(temp) && inputMapping.ContainsKey(str)) {
                    new TypedGameEvent<string>(this.GetListenerId(), inputMapping[str], GameInputState.KEY_DOWN);
                } else if (Input.GetButton(temp) && inputMapping.ContainsKey(str)) {
                    new TypedGameEvent<string>(this.GetListenerId(), inputMapping[str], GameInputState.KEY_HELD);
                } else if (Input.GetButtonUp(temp) && inputMapping.ContainsKey(str)) {
                    new TypedGameEvent<string>(this.GetListenerId(), inputMapping[str], GameInputState.KEY_UP);
                }
            } 
            foreach(string str in GameInputState.AXIS_INPUTS) {
                string temp = str + playerNumber;
                if (inputMapping.ContainsKey(str)) {
                    double val = Input.GetAxis(temp);
                    new TypedGameEvent<double>(this.GetListenerId(), inputMapping[str], Math.Max(-1.0, Math.Min(1.0, val)));
                }
            }
        }

        public void SetPlayerNumber(int i) {
            this.playerNumber = i;
        }

        /*
        In order for an input event to be created an input mapping for that input must be created.
        Example: SetInputMapping(GameInputState.LEFT_STICK_UP_DOWN, "leftStick");
            This example will create an event that people listening to listener tag given to this class
            using "leftStick" as the event name.  For axis input types the data will be a double representing
            the position on the axis, in this case up or down.  For button input types the data will be a string
            of either KEY_DOWN, KEY_HELD, or KEY_UP.
        */
        public void SetInputMapping(string input, string mapping) {
            if (inputMapping.ContainsKey(input)) {
                if (mapping == null || mapping.Length <= 0) {
                    inputMapping.Remove(input);
                } else {
                    inputMapping[input] = mapping;
                }
            } else {
                inputMapping.Add(input, mapping);
            }
        }
    }
}
