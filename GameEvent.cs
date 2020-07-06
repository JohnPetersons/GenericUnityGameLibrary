using System;
using System.Collections.Generic;

namespace GenericUnityGame {
    public class GameEvent {
        private string tag, name;
        private GameData<object> data;

        public GameEvent(string tag, string name, GameData<object> data) {
            this.tag = tag;
            this.name = name;
            this.data = data;
            GameSystem.AddGameEvent(this);
        }

        public string GetTag() {
            return this.tag;
        }

        public string GetName() {
            return this.name;
        }

        public GameData<object> GetGameData() {
            return this.data;
        }
    }
}
