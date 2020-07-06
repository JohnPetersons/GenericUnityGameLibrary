using System;
using System.Collections.Generic;

namespace GenericUnityGame {
    public class GameData<T>: GameDataGeneric {
        
        private T data;

        public GameData(T data) {
            this.data = data;
        }

        public Type GetDataType() {
            return data.GetType();
        }

        public T GetData() {
            return data;
        }

        public void SetData(T data) {
            this.data = data;
        }
    }
}
