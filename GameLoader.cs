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
        
        private List<GameObject> loaded;
        public static string FILES = "Loadfiles/";
        public static string PREFABS = "Prefabs/";
        public static string SPRITES = "Sprites/";
        public static string AUDIO = "Audio/";

        public GameLoader() {
            this.loaded = new List<GameObject>();
        }

        public void LoadFile(string file) {
            TextAsset fileAsset = Resources.Load<TextAsset>(GameLoader.FILES + file);
            if (fileAsset == null) {
                return;
            }
            string fileText = fileAsset.text;
            if (fileText.Length == 0) {
                return;
            }
            this.Load(fileText);
        }
        public void Load(string text) {
            while(text.Length > 0) {
                int index = text.IndexOf('\n');
                if (text.Length > 1 && text.Substring(0, 2).Equals("//")) {
                    text = text.Substring(index + 1);
                    continue;
                }
                if (index <= 0) {
                    this.LoadResource(text.Substring(0));
                    break;
                } else {
                    this.LoadResource(text.Substring(0, index));
                }
                text = text.Substring(index + 1);
            }
        }

        private void LoadResource(string resourceText) {
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
            this.loaded.Add(go);
        }

        public void RemoveLoaded() {
            foreach(GameObject go in this.loaded) {
                GameObject.Destroy(go);
            }
        }
    }
}
