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

        public override void Begin() {
            base.Begin();
        }

        public override void Tick() {
            base.Tick();
        }

        public GameObject GetGameObject() {
            return this.gameObject;
        }
    }
}
