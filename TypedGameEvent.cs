using System;
using System.Collections.Generic;

namespace GenericUnityGame {
    public class TypedGameEvent<T>: GameEvent {
        private string tag, name;
        private GameDataGeneric data;

        public TypedGameEvent(string tag, string name, T data): base(tag, name, new GameData<T>(data)) {

        }
    }
}
