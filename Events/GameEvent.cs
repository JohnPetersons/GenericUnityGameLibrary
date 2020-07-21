using System;
using System.Collections.Generic;

namespace GenericUnityGame {
    /*
    Automatically adds itself to GameSystem when created, don't add a new GameEvent yourself
    tag is the tag that GameEventListeners will be listening to
    */
    public class GameEvent {
        private string tag, name;
        private GameData data;

        public GameEvent(string tag, string name, GameData data, bool priority) {
            this.tag = tag;
            this.name = name;
            this.data = data;
            if (priority) {
                GameSystem.AddPriorityGameEvent(this);
            } else {
                GameSystem.AddGameEvent(this);
            }
        }

        public string GetTag() {
            return this.tag;
        }

        public string GetName() {
            return this.name;
        }

        public K GetGameData<K>() {
            return ((TypedGameData<K>)data).GetData();
        }

        public Type GetGameDataType() {
            return data.GetDataType();
        }
    }
}
