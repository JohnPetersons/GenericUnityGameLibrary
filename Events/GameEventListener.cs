﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    Extend this class instead of MonoBehaviour in order to be able to listen to events given to GameSystem
    */
    public class GameEventListener : MonoBehaviour {

        private List<string> listeningTo;
        private string listenerId, primaryTimeMultiplier;

        void Start() {
            this.Begin();
        }

        // Start is called before the first frame update
        // In extended classes call base.Begin()
        public void Begin()  {
            this.gameObject.GetComponent<GameEventListenerId>().SetListenerId();
            this.listenerId = this.gameObject.GetComponent<GameEventListenerId>().GetListenerId();
            this.listeningTo = new List<string>();
            this.primaryTimeMultiplier = "default";
            this.ListenTo(this.listenerId);
        }

        // Update is called once per frame
        void Update() {
            if (GameSystem.GetTimeMultiplier(this.primaryTimeMultiplier) != 0) {
                this.Tick();
            }
        }

        public void Tick() {

        }

        public string GetListenerId() {
            return this.listenerId;
        }

        public string GetPrimaryTimeMultiplier() {
            return this.primaryTimeMultiplier;
        }

        public void SetPrimaryTimeMultiplier(string tag) {
            this.primaryTimeMultiplier = tag;
        }

        public void ListenTo(string tag) {
            if (!this.listeningTo.Contains(tag)) {
                this.listeningTo.Add(tag);
                GameSystem.AddGameEventListener(tag, this);
            }
        }

        public void StopListeningTo(string tag) {
            if (this.listeningTo.Contains(tag)) {
                this.listeningTo.Remove(tag);
                GameSystem.RemoveGameEventListener(tag, this);
            }
        }

        // In extended classes call base.Destroy()
        public void Destroy() {
            GameSystem.RemoveGameEventListener(this.listeningTo, this);
        }

        public void HandleGameEvent(GameEvent gameEvent) {
            if (GameMaster.TEST && gameEvent.GetName().Equals("test")) {
                Debug.Log(gameEvent.GetGameData<string>());
            }
        }
    }
}

