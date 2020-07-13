using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    This exists so that all GameEventListeners/GameStateMachines listen to the same tag when they are attached to the same GameObject
    */
    public class GameEventListenerId : MonoBehaviour {

        
        private string listenerId;
        private static int nextListenerId = 0;

        // Start is called before the first frame update
        void Start() {
            this.Begin();
        }

        public void Begin() {
            this.SetListenerId();
        }

        // Update is called once per frame
        void Update() {
            
        }

        // In extended classes call base.Destroy()
        public void Destroy() {
            GameSystem.SetGameData<GameObject>(this.listenerId, null);
        }

        public void SetListenerId() {
            if (this.listenerId == null || this.listenerId.Length <= 0) {
                this.listenerId = "listener" + GameEventListenerId.nextListenerId;
                GameEventListenerId.nextListenerId++;
                GameSystem.SetGameData<GameObject>(this.listenerId, this.gameObject);
            }
        }

        public void SetListenerId(string str) {
            if (this.listenerId == null || this.listenerId.Length <= 0) {
                this.listenerId = str;
                GameSystem.SetGameData<GameObject>(this.listenerId, this.gameObject);
            }
        }

        public string GetListenerId() {
            return this.listenerId;
        }
    }
}
