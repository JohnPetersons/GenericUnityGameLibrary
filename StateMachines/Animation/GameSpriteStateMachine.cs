using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameSpriteStateMachine: GameStateMachine
    { 
        public const string SPRITE_CHANGE = "spriteChange";
        private SpriteRenderer spriteRenderer;
        public new void Begin() {
            base.Begin();
            this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public new void Tick() {
            base.Tick();
        }

        public new void HandleGameEvent(GameEvent gameEvent) {
            if (gameEvent.GetName().Equals(GameSpriteStateMachine.SPRITE_CHANGE)) {
                spriteRenderer.sprite = gameEvent.GetGameData<Sprite>();
            }
            base.HandleGameEvent(gameEvent);
        }
    }
}
