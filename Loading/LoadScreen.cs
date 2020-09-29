using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericUnityGame;

namespace GenericUnityGame {
    public class LoadScreen : GameEventListener {
        private float opacity;
        private SpriteRenderer spriteRenderer;
        public const string TAG = "loadScreen";
        private bool fadeInGame;
        private string loadTarget;
        // Start is called before the first frame update
        public override void Begin() {
            base.Begin();
            this.SetListenerId(LoadScreen.TAG);
            this.opacity = 1.5f;
            this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            this.fadeInGame = true;
            this.loadTarget = "";
            GameSystem.SetTimeMultiplier(LoadScreen.TAG, 2.0);
        }

        // Update is called once per frame
        public override void Tick() {
            base.Tick();
            if (this.fadeInGame && this.opacity > 0) {
                this.opacity -= (float)GameSystem.GetDeltaTime(LoadScreen.TAG, Time.deltaTime);
                this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, Math.Max(0.0f, Math.Min(1.0f, this.opacity)));
                if (this.opacity <= 0.0f) {
                    new TypedGameEvent<bool>(GameMaster.TAG, "loaded", true);
                }
            } else if (!this.fadeInGame && this.opacity < 1.6f) {
                this.opacity += (float)GameSystem.GetDeltaTime(LoadScreen.TAG, Time.deltaTime);
                this.spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, Math.Max(0.0f, Math.Min(1.0f, this.opacity)));
                if (this.opacity >= 1.0f) {
                    this.opacity = 1.5f;
                    this.fadeInGame = true;
                    new TypedGameEvent<string>(GameMaster.TAG, "load", this.loadTarget);
                }
            }
        }

        public override void HandleGameEvent(GameEvent gameEvent) {
            base.Tick();
            if (gameEvent.GetName().Equals("fadeInGame")) {
                this.fadeInGame = true;
            } else if (gameEvent.GetName().Equals("fadeOutGame")) {
                this.fadeInGame = false;
                this.loadTarget = gameEvent.GetGameData<string>();
            }
        }
    }
}
