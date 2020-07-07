using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameState
    { 
        private GameObject gameObject;
        private Dictionary<string, GameState> states;
        private List<GameStateCondition> conds;

        public GameState() {
            states = new Dictionary<string, GameState>();
            conds = new List<GameStateCondition>();
        }

        public void Begin() {
            
        }

        public void Tick() {
            
        }

        public void setGameObject(GameObject gameObject) {
            this.gameObject = gameObject;
        }

        public void AddStateChange(string eventName, GameState endState) {
            if (!states.ContainsKey(eventName)) {
                states.Add(eventName, endState);
            } else {
                states[eventName] = endState;
            }
        }

        public void AddGameStateCondition(GameStateCondition cond) {
            this.conds.Add(cond);
        }

        public GameState GetNextState(GameEvent gameEvent) {
            if (states.ContainsKey(gameEvent.GetName())) {
                states[gameEvent.GetName()].Begin();
                return states[gameEvent.GetName()];
            } else {
                foreach(GameStateCondition c in conds) {
                    if (c.CheckCond(gameEvent)) {
                        c.Success();
                        break;
                    }
                }
            }
            return this;
        }
    }
}
