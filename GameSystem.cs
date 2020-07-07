﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameSystem {
        private static Dictionary<string, List<GameEventListener>> eventListeners = new Dictionary<string, List<GameEventListener>>();
        private static List<GameEvent> events = new List<GameEvent>();
        private static Dictionary<string, GameDataGeneric> data = new Dictionary<string, GameDataGeneric>();
        private static Dictionary<string, double> timeMultipliers = new Dictionary<string, double>();

        public static void Refresh() {
            GameSystem.eventListeners = new Dictionary<string, List<GameEventListener>>();
            GameSystem.events = new List<GameEvent>();
            GameSystem.data = new Dictionary<string, GameDataGeneric>();
            GameSystem.timeMultipliers = new Dictionary<string, double>();
            GameSystem.timeMultipliers.Add("default", 1.0);
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
                data.Add(tag, new GameData<T>(gameData));
            } else {
                ((GameData<T>)data[tag]).SetData(gameData);
            }
        }

        public static T GetGameData<T>(string tag) {
            if (data.ContainsKey(tag)) {
                if (data[tag] == null) {
                    data.Remove(tag);
                } else if (data[tag].GetDataType().Equals(typeof(T))){
                    return ((GameData<T>)data[tag]).GetData();
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
                if(DateTime.Now.Subtract(startTime).TotalMilliseconds > 999) {
                    events = new List<GameEvent>();
                    Debug.Log("Infinite Loop broken");
                    break;
                }
            }
        }
    }
}

