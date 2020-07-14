using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameEventListenerState: GameState
    { 
        private GameEventListenerId listenerId;
        public GameEventListenerState(GameEventListenerId listenerId): base() {
            this.listenerId = listenerId;
        }

        public override void Begin() {
            base.Begin();
        }

        public override void Tick() {
            base.Tick();
        }

        public string GetEventListenerId() {
            return this.listenerId.GetListenerId();
        }
    }
}
