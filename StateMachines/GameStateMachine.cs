using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameStateMachine: GameEventListener
    { 
        private List<GameState> currentStates;
        public override void Begin() {
            base.Begin();
            currentStates = new List<GameState>();
        }

        public override void Tick() {
            base.Tick();
            foreach(GameState gs in currentStates) {
                gs.Tick();
            }
        }

        public void AddCurrentState(GameState state) {
            this.currentStates.Add(state);
            state.Begin();
        }

        public override void HandleGameEvent(GameEvent gameEvent) {
            for(int i = 0; i < currentStates.Count; i++) {
                currentStates[i] = currentStates[i].GetNextState(gameEvent);
            }
        }
    }
}
