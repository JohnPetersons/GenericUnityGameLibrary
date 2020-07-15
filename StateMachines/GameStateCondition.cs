using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public abstract class GameStateCondition {

        protected GameEventListenerId sendTo;
        private string eventOnSuccess;
        protected GameObject gameObject;

        private List<string> listensFor;
        public GameStateCondition(GameEventListenerId sendTo, string eventOnSuccess) {
            this.gameObject = GameSystem.GetGameData<GameObject>(sendTo.GetListenerId());
            this.sendTo = sendTo;
            this.eventOnSuccess = eventOnSuccess;
            this.listensFor = new List<string>();
        }

        public void AddEventToListenFor(string tag) {
            this.listensFor.Add(tag);
        }

        public List<string> GetListensFor() {
            return this.listensFor;
        }

        public void Success() {
            new TypedGameEvent<bool>(sendTo.GetListenerId(), eventOnSuccess, true);
        }
        public abstract bool CheckCond(GameEvent gameEvent);
    }
}
