using System.Collections;
using System.Collections.Generic;

namespace GenericUnityGame {
    public abstract class GameStateCondition {
        private string sendTo, eventOnSuccess;
        public GameStateCondition(string sendTo, string eventOnSuccess) {
            this.sendTo = sendTo;
            this.eventOnSuccess = eventOnSuccess;
        }

        public void Success() {
            new TypedGameEvent<bool>(sendTo, eventOnSuccess, true);
        }
        public abstract bool CheckCond(GameEvent gameEvent);
    }
}
