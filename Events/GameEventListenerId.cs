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
        private List<string> suffixes;
        private Dictionary<string, List<GameEventListener>> suffixesToListeners;

        // Start is called before the first frame update
        void Start() {
            this.Begin();
        }

        public virtual void Begin() {
            this.SetListenerId();
            this.suffixes = new List<string>();
            this.suffixesToListeners = new Dictionary<string, List<GameEventListener>>();
        }

        // Update is called once per frame
        void Update() {
            
        }

        // In extended classes call base.Destroy()
        public void Destroy() {
            GameSystem.SetGameData<GameObject>(this.listenerId, null);
        }

        public void AddEventListenerToSuffix(string suffix, GameEventListener gel) {
            if (!this.suffixes.Contains(suffix)) {
                this.suffixes.Add(suffix);
                this.suffixesToListeners.Add(suffix, new List<GameEventListener>());
            }
            if (!this.suffixesToListeners[suffix].Contains(gel)) {
                this.suffixesToListeners[suffix].Add(gel);
            }
        }

        public void RemoveEventListenerIdFromSuffix(string suffix, GameEventListener gel) {
            if (this.suffixes.Contains(suffix) && this.suffixesToListeners[suffix].Contains(gel)) {
                this.suffixesToListeners[suffix].Remove(gel);
                if (this.suffixesToListeners[suffix].Count <= 0) {
                    this.suffixesToListeners.Remove(suffix);
                    this.suffixes.Remove(suffix);
                }
            }
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
                foreach(string suffix in this.suffixes) {
                    foreach(GameEventListener gel in this.suffixesToListeners[suffix]) {
                        gel.StopListeningTo(this.listenerId + suffix);
                    }
                }
            }
            this.listenerId = str;
            GameSystem.SetGameData<GameObject>(this.listenerId, this.gameObject);
            foreach(GameEventListener gel in listeners) {
                gel.ListenTo(this.listenerId);
            }
            foreach(string suffix in this.suffixes) {
                foreach(GameEventListener gel in this.suffixesToListeners[suffix]) {
                    gel.ListenTo(this.listenerId + suffix);
                }
            }
        }

        public string GetListenerId() {
            return this.listenerId;
        }
    }
}
