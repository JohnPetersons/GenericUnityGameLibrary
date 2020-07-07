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
            Debug.Log("tests");
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

            GameLoader.LoadFile("TestLoader");
            GameLoader.Load("TestEventListener,1,1,0");
            new TypedGameEvent<string>("listener0", "test", "test1");
            new TypedGameEvent<string>("listener1", "test", "test2");
        }
   }
}
