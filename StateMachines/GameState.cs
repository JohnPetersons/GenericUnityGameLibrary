using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameState
    { 
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

        /*
        Two different event groups.
        The events that trigger the first if statement will switch the state.
        The events that are passed to the else statement are for GameStateCondtions that will usually trigger an event
        in the Success function that sends an event that would be caught by the first if statement.
        */
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
