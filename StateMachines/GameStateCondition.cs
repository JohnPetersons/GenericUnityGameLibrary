using System.Collections;
using System.Collections.Generic;

namespace GenericUnityGame {
    public abstract class GameStateCondition {

        private string sendTo, eventOnSuccess;

        private List<string> listensFor;
        public GameStateCondition(string sendTo, string eventOnSuccess) {
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
            new TypedGameEvent<bool>(sendTo, eventOnSuccess, true);
        }
        public abstract bool CheckCond(GameEvent gameEvent);
    }
}
