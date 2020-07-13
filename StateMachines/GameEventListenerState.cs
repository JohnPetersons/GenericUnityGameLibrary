using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameEventListenerState: GameState
    { 
        private string listenerId;
        public GameEventListenerState(string listenerId): base() {
            this.listenerId = listenerId;
        }

        public override void Begin() {
            base.Begin();
        }

        public override void Tick() {
            base.Tick();
        }

        public string GetEventListenerId() {
            return this.listenerId;
        }
    }
}
