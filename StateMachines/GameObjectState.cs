using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameObjectState: GameState
    { 
        private GameObject gameObject;
        public GameObjectState(GameObject gameObject): base() {
            this.gameObject = gameObject;
        }

        public new void Begin() {
            base.Begin();
        }

        public new void Tick() {
            base.Tick();
        }

        public string GetGameObject() {
            return this.gameObject;
        }
    }
}
