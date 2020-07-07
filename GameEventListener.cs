﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    Extend this class instead of MonoBehaviour in order to be able to listen to events given to GameSystem
    */
    public class GameEventListener : MonoBehaviour {

        private List<string> listeningTo;
        private string listenerId;
        private static int nextListenerId = 0;

        // Start is called before the first frame update
        // In extended classes call base.Start()
        void Start()  {
            this.listeningTo = new List<string>();
            this.listenerId = "listener" + GameEventListener.nextListenerId;
            GameSystem.SetGameData<GameEventListener>(this.listenerId, this);
            this.ListenTo(this.listenerId);
            GameEventListener.nextListenerId++;
        }

        // Update is called once per frame
        void Update() {
            
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
            GameSystem.SetGameData<GameEventListener>(this.listenerId, null);
            GameSystem.RemoveGameEventListener(this.listeningTo, this);
        }

        public void HandleGameEvent(GameEvent gameEvent) {
            if (GameMaster.TEST && gameEvent.GetName().Equals("test")) {
                Debug.Log(gameEvent.GetGameData<string>());
            }
        }
    }
}

