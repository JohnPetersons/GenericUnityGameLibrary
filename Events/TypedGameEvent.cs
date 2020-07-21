using System;
using System.Collections.Generic;

namespace GenericUnityGame {
    /*
    is used when creating a GameEvent so that you don't have to do new GameEvent(tag, name, new GameData<T>(data))
    instead you use TypedGameEvent<T>(tag, name. data) which is cleaner to me
    */
    public class TypedGameEvent<T>: GameEvent {

        public TypedGameEvent(string tag, string name, T data): base(tag, name, new TypedGameData<T>(data), false) {

        }

        public TypedGameEvent(string tag, string name, T data, bool priority): base(tag, name, new TypedGameData<T>(data), priority) {

        }
    }
}
