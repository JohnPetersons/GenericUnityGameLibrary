using System;
using System.Collections.Generic;

namespace GenericUnityGame {
    /*
    Exists so that I can put any type into the data Dictionary in GameSystem 
    */
    public interface GameData {
        Type GetDataType();
    }
}
