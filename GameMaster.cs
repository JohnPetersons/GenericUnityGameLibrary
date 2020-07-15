using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    Mostly just to call GameSystem.Update() and be attached to an empty GameObject.
    Also does some basic testing to make sure I didn't break things
    */
   public class GameMaster : GameEventListener {
        // Start is called before the first frame update
        public const bool TEST = false;
        public const string TAG = "gameMaster";

        private GameLoader testLoader, loader;

        public override void Begin() {
            base.Begin();
            this.SetListenerId(GameMaster.TAG);
            GameSystem.SetTimeMultiplier("default", 1.0);
            if (GameMaster.TEST) {
                this.testLoader = new GameLoader();
                this.Tests();
            }
            this.loader = new GameLoader();
            this.loader.LoadFile("MainMenu");
        }

        // Update is called once per frame
        public override void Tick() {
            GameSystem.Update();
            if (GameMaster.TEST) {
                this.testLoader.RemoveLoaded();
            }
        }

        public override void HandleGameEvent(GameEvent gameEvent) {
            base.HandleGameEvent(gameEvent);
            if (gameEvent.GetName().Equals("startMatch")) {
                this.loader.RemoveLoaded();
                this.loader.LoadFile("Game");
                Debug.Log(gameEvent.GetGameData<string>());
            } else if (gameEvent.GetName().Equals("mainMenu")) {
                this.loader.RemoveLoaded();
                this.loader.LoadFile("MainMenu");
            }
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
            this.testLoader.LoadFile("TestLoader");
            this.testLoader.Load("TestEventListener,1,1,0");
            new TypedGameEvent<string>("listener0", "test", "Load file test success");
            new TypedGameEvent<string>("listener1", "test", "Load string test success");
        }
   }
}
