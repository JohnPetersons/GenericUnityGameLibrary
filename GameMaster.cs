using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    Mostly just to call GameSystem.Update() and be attached to an empty GameObject.
    Also does some basic testing to make sure I didn't break things
    */
   public class GameMaster : MonoBehaviour {
        // Start is called before the first frame update
        public const bool TEST = true;

        private GameLoader loader;

        void Start() {
            this.loader = new GameLoader();
            if (GameMaster.TEST) {
                this.Tests();
            }
            loader.LoadFile("MainMenu");
        }

        // Update is called once per frame
        void Update() {
            GameSystem.Update();
            this.loader.RemoveLoaded();
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

            // These tests require you have a file called TestLoader.txt in Resources/LoadFiles/ that 
            // contains the string: "TestEventListener" and that you have a prefab in Resources/Prefabs/ 
            // named TestEventListener
            this.loader.LoadFile("TestLoader");
            this.loader.Load("TestEventListener,1,1,0");
            new TypedGameEvent<string>("listener0", "test", "Load file test success");
            new TypedGameEvent<string>("listener1", "test", "Load string test success");
        }
   }
}
