using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    Extend this class instead of MonoBehaviour in order to be able to listen to events given to GameSystem
    */
    public class GameEventListener : MonoBehaviour {

        private List<string> listeningTo;
        protected GameEventListenerId listenerId;

        void Start() {
            this.Begin();
        }

        // Start is called before the first frame update
        // In extended classes call base.Begin()
        public virtual void Begin()  {
            this.listenerId = this.gameObject.GetComponent<GameEventListenerId>();
            this.listenerId.SetListenerId();
            if (this.listeningTo == null) {
                this.listeningTo = new List<string>();
            }
            this.ListenTo(this);
        }

        // Update is called once per frame
        void Update() {
            this.Tick();
        }

        public virtual void Tick() {

        }

        public void SetListenerId(string str) {
            this.listenerId.SetListenerId(str);
        }

        public string GetListenerId() {
            return this.listenerId.GetListenerId();;
        }

        public void ListenTo(string tag) {
            if (this.listeningTo == null) {
                this.listeningTo = new List<string>();
            }
            if (!this.listeningTo.Contains(tag)) {
                this.listeningTo.Add(tag);
                GameSystem.AddGameEventListener(tag, this);
            }
        }

        public void ListenTo(GameEventListener gel) {
            if (this.listeningTo == null) {
                this.listeningTo = new List<string>();
            }
            if (!this.listeningTo.Contains(gel.GetListenerId())) {
                this.listeningTo.Add(gel.GetListenerId());
                GameSystem.AddGameEventListener(gel.GetListenerId(), this);
            }
        }

        public void ListenTo(GameObject go) {
            if (this.listeningTo == null) {
                this.listeningTo = new List<string>();
            }
            GameEventListenerId gelID = go.GetComponent<GameEventListenerId>();
            if (gelID != null && !this.listeningTo.Contains(gelID.GetListenerId())) {
                this.listeningTo.Add(gelID.GetListenerId());
                GameSystem.AddGameEventListener(gelID.GetListenerId(), this);
            }
        }

        public void StopListeningTo(string tag) {
            if (this.listeningTo == null) {
                return;
            }
            if (this.listeningTo.Contains(tag)) {
                this.listeningTo.Remove(tag);
                GameSystem.RemoveGameEventListener(tag, this);
            }
        }

        public void StopListeningTo(GameEventListener gel) {
            if (this.listeningTo == null) {
                return;
            }
            if (this.listeningTo.Contains(gel.GetListenerId())) {
                this.listeningTo.Remove(gel.GetListenerId());
                GameSystem.RemoveGameEventListener(gel.GetListenerId(), this);
            }
        }

        public void StopListeningTo(GameObject go) {
            if (this.listeningTo == null) {
                return;
            }
            GameEventListenerId gelID = go.GetComponent<GameEventListenerId>();
            if (gelID != null && this.listeningTo.Contains(gelID.GetListenerId())) {
                this.listeningTo.Remove(gelID.GetListenerId());
                GameSystem.RemoveGameEventListener(gelID.GetListenerId(), this);
            }
        }

        void Destroy() {
            this.OnDestroy();
        }

        public virtual void OnDestroy() {
            GameSystem.RemoveGameEventListener(this.listeningTo, this);
        }

        public virtual void HandleGameEvent(GameEvent gameEvent) {
            
        }
    }
}

