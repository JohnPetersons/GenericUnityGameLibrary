using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    
    public class GameOnTriggerHandler : GameEventListener {

        private List<GameObject> currentCollisions;

        public new void Begin() {
            base.Begin();
            this.currentCollisions = new List<GameObject>();
        }

        public new void Tick() {
            base.Tick();
            if (this.currentCollisions.Count > 0) {
                foreach(GameObject go in this.currentCollisions) {
                    new TypedGameEvent<GameObject>(this.GetListenerId(), "triggerStay", go);
                }
            }
        }

        private void OnTriggerEnter(Collider other) {
            new TypedGameEvent<GameObject>(this.GetListenerId(), "triggerEnter", other.gameObject);
            this.currentCollisions.Add(other.gameObject);
        } 

        private void OnTriggerExit(Collider other) {
            new TypedGameEvent<GameObject>(this.GetListenerId(), "triggerExit", other.gameObject);
            this.currentCollisions.Remove(other.gameObject);
        }
    }
}
