using System;
using System.Collections.Generic;

namespace GenericUnityGame {
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
