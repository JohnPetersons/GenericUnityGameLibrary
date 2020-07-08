using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameInputReader : GameEventListener {

        private static readonly string[] buttonInputs = {"a", "b", "x", "y", "leftTrigger", "leftBumper", "rightTrigger", "rightBumper", "start", "select"};
        private static readonly string[] axisInputs = {"leftStickUpDown", "rightStickUpDown", "leftStickLeftRight", "rightStickLeftRight", "dPadUpDown", "dPadLeftRight"};
        private Dictionary<string, string> inputMapping = new Dictionary<string, string>();
        private string playerNumber;
        public new void Begin() {
            base.Begin();
        }

        public new void Tick() {
            base.Tick();
            foreach(string str in GameInputReader.buttonInputs) {
                string temp = str + playerNumber;
                string tempInput = "input" + playerNumber;
                if (Input.GetButtonDown(temp) && inputMapping.ContainsKey(temp)) {
                    new TypedGameEvent<string>(tempInput, inputMapping[temp], "start");
                } else if (Input.GetButton(temp) && inputMapping.ContainsKey(temp)) {
                    new TypedGameEvent<string>(tempInput, inputMapping[temp], "held");
                } else if (Input.GetButtonUp(temp) && inputMapping.ContainsKey(temp)) {
                    new TypedGameEvent<string>(tempInput, inputMapping[temp], "end");
                }
            }
        }

        public void SetPlayerNumber(int i) {
            playerNumber = "" + i;
        }

        public void SetInputMapping(string input, string mapping) {
            if (inputMapping.ContainsKey(input)) {
                inputMapping[input] = mapping;
            } else {
                inputMapping.Add(input, mapping);
            }
        }
    }
}
