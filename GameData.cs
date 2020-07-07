using System;
using System.Collections.Generic;

namespace GenericUnityGame {
    /*
    Exists so that I can put any type into the data Dictionary in GameSystem 
    */
    public class GameData<T>: GameDataGeneric {
        
        private T gameData;

        public GameData(T gameData) {
            this.gameData = gameData;
        }

        public Type GetDataType() {
            return typeof(T);
        }

        public T GetData() {
            return gameData;
        }

        public void SetData(T gameData) {
            this.gameData = gameData;
        }
    }
}
