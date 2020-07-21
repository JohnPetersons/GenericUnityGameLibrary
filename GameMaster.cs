using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GenericUnityGame {
    /*
    Mostly just to call GameSystem.Update() and be attached to an empty GameObject.
    Also does some basic testing to make sure I didn't break things
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
            Dictionary<string, float> settingsList = new Dictionary<string, float>();
            if (File.Exists(@"settings.txt")) {
                foreach (string line in File.ReadLines(@"settings.txt")) {
                    settingsList.Add(line.Substring(0, line.IndexOf(",")), float.Parse(line.Substring(line.IndexOf(",") + 1)));
                }
                Settings.charge = settingsList["charge"];
                Settings.gameSpeed = settingsList["gameSpeed"];
                Settings.moveForward = settingsList["moveForward"];
                Settings.moveBackward = settingsList["moveBackward"];
                Settings.quickStepForward = settingsList["quickStepForward"];
                Settings.quickStepForwardTimer1 = settingsList["quickStepForwardTimer1"];
                Settings.quickStepForwardTimer2 = settingsList["quickStepForwardTimer2"];
                Settings.quickStepBackward = settingsList["quickStepBackward"];
                Settings.quickStepBackwardTimer1 = settingsList["quickStepBackwardTimer1"];
                Settings.quickStepBackwardTimer2 = settingsList["quickStepBackwardTimer2"];
                Settings.dashChargeTimer = settingsList["dashChargeTimer"];
                Settings.dash = settingsList["dash"];
                Settings.dashTimer = settingsList["dashTimer"];
                Settings.chargeRecoveryTimer = settingsList["chargeRecoveryTimer"];
                Settings.collisionLoss = settingsList["collisionLoss"];
                Settings.collisionWin = settingsList["collisionWin"];
            }
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
