using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
   public class GameMaster : MonoBehaviour {
        // Start is called before the first frame update
        public static const bool TEST = true;

        void Start() {
            if (GameMaster.TEST) {
                this.Tests();
            }
        }

        // Update is called once per frame
        void Update() {
            GameSystem.Update();
        }

        private void Tests() {
            GameSystem.SetGameData<string>("testString", "testString");
            if (!GameSystem.GameDataIsType<string>("testString")) {
                Debug.Log("GameDataIsType Failure: string");
            } 
            if (!GameSystem.GetGameData<string>("testString").Equals("testString")) {
                Debug.Log("GetGameData Failure: string");
            }
            
            GameSystem.SetGameData<bool>("testBool", true);
            if (!GameSystem.GameDataIsType<bool>("testBool")) {
                Debug.Log("GameDataIsType Failure: bool");
            } 
            if (!GameSystem.GetGameData<bool>("testBool").Equals(true)) {
                Debug.Log("GetGameData Failure: bool");
            }

            GameLoader.Load("TestLoader");
            
            new GameEvent("listener0", "test", new GameData<string>("test"));
        }
   }
}
