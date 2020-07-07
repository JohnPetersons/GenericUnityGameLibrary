using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameLoader {
        
        private static string fileFolder = "Loadfiles/";
        public static void Load(string file) {
            string fileText = Resources.Load<TextAsset>(GameLoader.fileFolder + file).text;
            int index = 0;
            while(index < fileText.Length) {
                int stopIndex = fileText.IndexOf('\n', index);
                if (stopIndex <= 0) {
                    GameLoader.LoadResource(fileText.Substring(index));
                    break;
                } else {
                    GameLoader.LoadResource(fileText.Substring(index, stopIndex));
                }
                index = stopIndex + 1;
            }
        }

        private static void LoadResource(string resourceText) {
            Debug.Log(resourceText);
        }
    }
}
