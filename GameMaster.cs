using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
   public class GameMaster : MonoBehaviour {
        // Start is called before the first frame update
        public const bool TEST = true;

        void Start() {
            if (GameMaster.TEST) {
                this.Tests();
            }
            GameLoader.LoadFile("MainMenu");
        }

        // Update is called once per frame
        void Update() {
            GameSystem.Update();
        }

        private void Tests() {
            GameSystem.SetGameData<string>("testString", "testString");
            if (!GameSystem.GameDataIsType<string>("testString")) {
                Debug.Log("GameDataIsType failure: string");
            }  else {
                Debug.Log("GameDataIsType success: string");
            }
            if (!GameSystem.GetGameData<string>("testString").Equals("testString")) {
                Debug.Log("GetGameData failure: string");
            } else {
                Debug.Log("GetGameData success: string");
            }
            
            GameSystem.SetGameData<bool>("testBool", true);
            if (!GameSystem.GameDataIsType<bool>("testBool")) {
                Debug.Log("GameDataIsType failure: bool");
            } else {
                Debug.Log("GameDataIsType success: bool");
            }
            if (!GameSystem.GetGameData<bool>("testBool").Equals(true)) {
                Debug.Log("GetGameData failure: bool");
            } else {
                Debug.Log("GetGameData success: bool");
            }

            GameLoader.LoadFile("TestLoader");
            GameLoader.Load("TestEventListener,1,1,0");
            new TypedGameEvent<string>("listener0", "test", "Load file test success");
            new TypedGameEvent<string>("listener1", "test", "Load string test success");
        }
   }
}
