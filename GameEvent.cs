using System;
using System.Collections.Generic;

namespace GenericUnityGame {
    public class GameEvent {
        private string tag, name;
        private GameDataGeneric data;

        public GameEvent(string tag, string name, GameDataGeneric data) {
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

        public GameDataGeneric GetGameData() {
            return this.data;
        }
    }
}
