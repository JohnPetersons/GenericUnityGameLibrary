using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameLoader {
        
        private static string fileFolder = "Loadfiles/";
        private static string prefabsFolder = "Prefabs/";
        public static void LoadFile(string file) {
            TextAsset fileAsset = Resources.Load<TextAsset>(GameLoader.fileFolder + file);
            if (fileAsset == null) {
                return;
            }
            string fileText = fileAsset.text;
            if (fileText.Length == 0) {
                return;
            }
            GameLoader.Load(fileText);
        }
        public static void Load(string text) {
            while(text.Length > 0) {
                int index = text.IndexOf('\n');
                if (index <= 0) {
                    GameLoader.LoadResource(text.Substring(0));
                    break;
                } else {
                    GameLoader.LoadResource(text.Substring(0, index));
                }
                text = text.Substring(index + 1);
            }
        }

        private static void LoadResource(string resourceText) {
            if (resourceText.Length == 0) {
                return;
            }
            int index = resourceText.IndexOf(",");
            GameObject go;
            if (index <= 0) {
                go = GameObject.Instantiate(Resources.Load(GameLoader.prefabsFolder + resourceText.Substring(0)) as GameObject);
            } else {
                go = GameObject.Instantiate(Resources.Load(GameLoader.prefabsFolder + resourceText.Substring(0, index)) as GameObject);
                resourceText = resourceText.Substring(index + 1);
                float x = float.Parse(resourceText.Substring(0, resourceText.IndexOf(",")));
                resourceText = resourceText.Substring(resourceText.IndexOf(",") + 1);
                float y = float.Parse(resourceText.Substring(0, resourceText.IndexOf(",")));
                resourceText = resourceText.Substring(resourceText.IndexOf(",") + 1);
                float z = float.Parse(resourceText.Substring(0));
                go.transform.position = new Vector3(x, y, z);
            }
        }
    }
}
