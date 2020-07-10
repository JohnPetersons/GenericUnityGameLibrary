using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    button inputs for the first player/controller would be a1, b1, x1, y1, etc.
    */
    public class GameInputState : GameEventListenerState {

        private static readonly string[] BUTTON_INPUTS = {"a", "b", "x", "y", "leftTrigger", "leftBumper", "rightTrigger", "rightBumper", "start", "select"};
        private static readonly string[] AXIS_INPUTS = {"leftStickUpDown", "rightStickUpDown", "leftStickLeftRight", "rightStickLeftRight", "dPadUpDown", "dPadLeftRight"};
        public const string START = "start";
        public const string HELD = "held";
        public const string END = "end";
        private Dictionary<string, string> inputMapping = new Dictionary<string, string>();
        private int playerNumber;

        public GameInputState(string listener, int i): base(listener) {
            playerNumber = i;
        }

        public new void Begin() {
            base.Begin();
        }

        public new void Tick() {
            base.Tick();
            foreach(string str in GameInputState.BUTTON_INPUTS) {
                string temp = str + playerNumber;
                if (Input.GetButtonDown(temp) && inputMapping.ContainsKey(temp)) {
                    new TypedGameEvent<string>(this.GetEventListenerId(), inputMapping[temp], GameInputState.START);
                } else if (Input.GetButton(temp) && inputMapping.ContainsKey(temp)) {
                    new TypedGameEvent<string>(this.GetEventListenerId(), inputMapping[temp], GameInputState.HELD);
                } else if (Input.GetButtonUp(temp) && inputMapping.ContainsKey(temp)) {
                    new TypedGameEvent<string>(this.GetEventListenerId(), inputMapping[temp], GameInputState.END);
                }
            } 
            foreach(string str in GameInputState.AXIS_INPUTS) {
                string temp = str + playerNumber;
                if (Input.GetAxis(temp) != 0 && inputMapping.ContainsKey(temp)) {
                    double val = Input.GetAxis(temp);
                    new TypedGameEvent<double>(this.GetEventListenerId(), inputMapping[temp], Math.Max(-1.0, Math.Min(1.0, val)));
                }
            }
        }

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
