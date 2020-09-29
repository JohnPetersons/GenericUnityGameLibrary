using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GenericUnityGame {
    /*
    Mostly just to call GameSystem.Update() and be attached to an empty GameObject.
    Also does some basic testing to make sure I didn't break things

    TODO: Move some of the game specific stuff to a child class
    */
   public class GameMaster : GameEventListener {
        // Start is called before the first frame update
        public const string TAG = "gameMaster";

        private GameLoader loader;
        private bool loaded;

        public override void Begin() {
            base.Begin();
            this.SetListenerId(GameMaster.TAG);
            GameSystem.SetTimeMultiplier("default", 1.0);
            this.loader = new GameLoader();
            GameSystem.SetGameData<GameLoader>("GameLoader", this.loader);
            GameSystem.SetGameData<GameLoader>("MenuLoader", new GameLoader());
            this.loader.LoadFile("MainMenu");
            this.loaded = false;
        }

        // Update is called once per frame
        public override void Tick() {
            GameSystem.Update();
        }

        public override void HandleGameEvent(GameEvent gameEvent) {
            base.HandleGameEvent(gameEvent);
            if (this.loaded) {
                if (gameEvent.GetName().Equals("startMatch")) {
                    this.loader.RemoveLoaded();
                    this.loader.LoadFile("Game");
                    this.loaded = false;
                } else if (gameEvent.GetName().Equals("mainMenu")) {
                    this.loader.RemoveLoaded();
                    this.loader.LoadFile("MainMenu");
                    this.loaded = false;
                }
            } else if (gameEvent.GetName().Equals("loaded")) {
                this.loaded = true;
            }
        }
   }
}
