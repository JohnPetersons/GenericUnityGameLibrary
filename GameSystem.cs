using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    Sends events to GameEventListeners
    Stores data that any class can reference by a tag string
    Has a Dictionary explicitly for time multipliers
        - Only here so that deltaTime and each multiplier can be multiplied by the default time multiplier before being returned
        - To figure out how fast time is moving for gameplay for example you would call:
            GameSystem.SetTimeMultiplier("gameplay", 1.0);
            double deltaTime = GameSystem.GetTimeMultiplier("gameplay", Time.deltaTime);
    */
    public class GameSystem {

        public const string GAMEPLAY = "gameplay";
        public const string DEFAULT = "default";

        private static Dictionary<string, List<GameEventListener>> eventListeners = new Dictionary<string, List<GameEventListener>>();
        private static List<GameEvent> events = new List<GameEvent>();
        private static Dictionary<string, GameData> data = new Dictionary<string, GameData>();
        private static Dictionary<string, double> timeMultipliers = new Dictionary<string, double>();

        public static void Refresh() {
            GameSystem.eventListeners = new Dictionary<string, List<GameEventListener>>();
            GameSystem.events = new List<GameEvent>();
            GameSystem.data = new Dictionary<string, GameData>();
            GameSystem.timeMultipliers = new Dictionary<string, double>();
            GameSystem.timeMultipliers.Add(GameSystem.DEFAULT, 1.0);
        }

        public static void AddGameEvent(GameEvent gameEvent) {
            if (!events.Contains(gameEvent)) {
                events.Add(gameEvent);
            }
        }

        public static void AddGameEventListener(string tag, GameEventListener listener) {
            if (!eventListeners.ContainsKey(tag)) {
                eventListeners.Add(tag, new List<GameEventListener>());
            }
            if (!eventListeners[tag].Contains(listener)) {
                eventListeners[tag].Add(listener);
            }
        }

        public static void RemoveGameEventListener(List<string> tags, GameEventListener eventListener) {
            foreach(string tag in tags) {
                eventListeners[tag].Remove(eventListener);
            }
        }

        public static void RemoveGameEventListener(string tag, GameEventListener eventListener) {
            eventListeners[tag].Remove(eventListener);
        }

        public static bool GameDataIsType<T>(string tag) {
            if (data.ContainsKey(tag) && data[tag].GetDataType().Equals(typeof(T))) {
                return true;
            }
            return false;
        }

        public static void SetGameData<T>(string tag, T gameData) {
            if (!data.ContainsKey(tag)) {
                data.Add(tag, new TypedGameData<T>(gameData));
            } else {
                ((TypedGameData<T>)data[tag]).SetData(gameData);
            }
        }

        public static T GetGameData<T>(string tag) {
            if (data.ContainsKey(tag)) {
                if (data[tag] == null) {
                    data.Remove(tag);
                } else if (data[tag].GetDataType().Equals(typeof(T))){
                    return ((TypedGameData<T>)data[tag]).GetData();
                }
            }
            return default(T);
        }

        public static void SetTimeMultiplier(string tag, double timeMultiplier) {
            if (!timeMultipliers.ContainsKey(tag)) {
                timeMultipliers.Add(tag, timeMultiplier);
            } else {
                timeMultipliers[tag] = timeMultiplier;
            }
        }

        public static double GetTimeMultiplier(string tag) {
            if (timeMultipliers.ContainsKey(tag)) {
                return timeMultipliers[tag];
            }
            return 0.0;
        }

        public static double GetDeltaTime(double deltaTime) {
            return deltaTime * timeMultipliers[GameSystem.DEFAULT];
        }

        public static double GetDeltaTime(string tag, double deltaTime) {
            double result = GameSystem.GetDeltaTime(deltaTime);
            if (timeMultipliers.ContainsKey(tag)) {
                result *= timeMultipliers[tag];
            }
            return result;
        }

        public static double GetDeltaTime(List<string> tags, double deltaTime) {
            double result = GameSystem.GetDeltaTime(deltaTime);
            foreach(string tag in tags) {
                if (timeMultipliers.ContainsKey(tag)) {
                    result *= timeMultipliers[tag];
                }
            }
            return result;
        }

        public static void Update() {
            DateTime startTime = DateTime.Now;
            while (events.Count > 0) {
                List<GameEvent> currentEvents = events;
                events = new List<GameEvent>();
                foreach(GameEvent ge in currentEvents) {
                    if (eventListeners.ContainsKey(ge.GetTag())) {
                        foreach(GameEventListener listener in eventListeners[ge.GetTag()]) {
                            listener.HandleGameEvent(ge);
                        }
                    }
                }
                // In case of a loop that takes longer than 1 second assume it is an infinite loop and kill it
                // prevents having to close unity if an infinite loop is created with events
                if(DateTime.Now.Subtract(startTime).TotalMilliseconds > 999) {
                    events = new List<GameEvent>();
                    Debug.Log("Infinite Loop broken");
                    break;
                }
            }
        }
    }
}

