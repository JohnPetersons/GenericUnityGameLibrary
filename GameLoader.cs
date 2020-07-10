using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    /*
    Loads prefabs by getting their name and spawn locations from either a file or a string
    Format for each line of the file or the string is "PrefabName,x,y,z"
    Files can include comments if the line starts with "//"
    */
    public class GameLoader {
        
        private static List<GameObject> loaded = new List<GameObject>();
        public static string FILES = "Loadfiles/";
        public static string PREFABS = "Prefabs/";
        public static string SPRITES = "Sprites/";
        public static string AUDIO_CLIPS = "AudioClips/";
        public static void LoadFile(string file) {
            TextAsset fileAsset = Resources.Load<TextAsset>(GameLoader.FILES + file);
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
                if (text.Length > 1 && text.Substring(0, 2).Equals("//")) {
                    text = text.Substring(index + 1);
                    continue;
                }
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
                go = GameObject.Instantiate(Resources.Load(GameLoader.PREFABS + resourceText.Substring(0)) as GameObject);
            } else {
                go = GameObject.Instantiate(Resources.Load(GameLoader.PREFABS + resourceText.Substring(0, index)) as GameObject);
                resourceText = resourceText.Substring(index + 1);
                float x = float.Parse(resourceText.Substring(0, resourceText.IndexOf(",")));
                resourceText = resourceText.Substring(resourceText.IndexOf(",") + 1);
                float y = float.Parse(resourceText.Substring(0, resourceText.IndexOf(",")));
                resourceText = resourceText.Substring(resourceText.IndexOf(",") + 1);
                float z = float.Parse(resourceText.Substring(0));
                go.transform.position = new Vector3(x, y, z);
            }
            loaded.Add(go);
        }

        private static void RemoveLoaded() {
            foreach(GameObject go in loaded) {
                GameObject.Destroy(go);
            }
        }
    }
}
