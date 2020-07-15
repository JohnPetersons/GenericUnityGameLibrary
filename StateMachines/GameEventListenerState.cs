using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameEventListenerState: GameState
    { 
        private GameEventListenerId listenerId;
        protected GameObject gameObject;
        public GameEventListenerState(GameEventListenerId listenerId): base() {
            this.listenerId = listenerId;
            this.gameObject = GameSystem.GetGameData<GameObject>(listenerId.GetListenerId());
        }

        public override void Begin() {
            base.Begin();
        }

        public override void Tick() {
            base.Tick();
        }

        public string GetListenerId() {
            return this.listenerId.GetListenerId();
        }
    }
}
