using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    
    public class GameOnTriggerHandler : GameEventListener {

        private List<GameObject> currentCollisions;

        public override void Begin() {
            base.Begin();
            this.currentCollisions = new List<GameObject>();
        }

        public override void Tick() {
            base.Tick();
            if (this.currentCollisions.Count > 0) {
                foreach(GameObject go in this.currentCollisions) {
                    new TypedGameEvent<GameObject>(this.GetListenerId(), "triggerStay", go, true);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            new TypedGameEvent<GameObject>(this.GetListenerId(), "triggerEnter", other.gameObject, true);
            this.currentCollisions.Add(other.gameObject);
        } 

        private void OnTriggerExit2D(Collider2D other) {
            new TypedGameEvent<GameObject>(this.GetListenerId(), "triggerExit", other.gameObject, true);
            this.currentCollisions.Remove(other.gameObject);
        }
    }
}
