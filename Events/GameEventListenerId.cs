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

        public virtual void Begin() {
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
            GameEventListener[] listeners = gameObject.GetComponents<GameEventListener>();
            if (this.listenerId != null && this.listenerId.Length > 0) {
                GameSystem.SetGameData<GameObject>(this.listenerId, null);
                foreach(GameEventListener gel in listeners) {
                    gel.StopListeningTo(this.listenerId);
                }
            }
            this.listenerId = str;
            GameSystem.SetGameData<GameObject>(this.listenerId, this.gameObject);
            foreach(GameEventListener gel in listeners) {
                gel.ListenTo(this.listenerId);
            }
        }

        public string GetListenerId() {
            return this.listenerId;
        }
    }
}
