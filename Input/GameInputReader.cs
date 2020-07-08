using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    button inputs for the first player/controller would be a1, b1, x1, y1, etc.
    */
    public class GameInputReader : GameEventListener {

        private static readonly string[] buttonInputs = {"a", "b", "x", "y", "leftTrigger", "leftBumper", "rightTrigger", "rightBumper", "start", "select"};
        private static readonly string[] axisInputs = {"leftStickUpDown", "rightStickUpDown", "leftStickLeftRight", "rightStickLeftRight", "dPadUpDown", "dPadLeftRight"};
        private Dictionary<string, string> inputMapping = new Dictionary<string, string>();
        private string playerNumber;
        private int playerInput;
        public new void Begin() {
            base.Begin();
        }

        public new void Tick() {
            base.Tick();
            foreach(string str in GameInputReader.buttonInputs) {
                string temp = str + playerNumber;
                if (Input.GetButtonDown(temp) && inputMapping.ContainsKey(temp)) {
                    new TypedGameEvent<string>(playerInput, inputMapping[temp], "start");
                } else if (Input.GetButton(temp) && inputMapping.ContainsKey(temp)) {
                    new TypedGameEvent<string>(playerInput, inputMapping[temp], "held");
                } else if (Input.GetButtonUp(temp) && inputMapping.ContainsKey(temp)) {
                    new TypedGameEvent<string>(playerInput, inputMapping[temp], "end");
                }
            }
        }

        public void SetPlayerNumber(int i) {
            playerNumber = i;
            playerInput = "input" + i;
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
