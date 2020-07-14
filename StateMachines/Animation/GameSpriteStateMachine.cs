using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameSpriteStateMachine: GameStateMachine
    { 
        public const string SPRITE_CHANGE = "spriteChange";
        public const string SPRITE_LISTENER_SUFFIX = "Sprite";
        private SpriteRenderer spriteRenderer;

        public override void Begin() {
            base.Begin();
            this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            this.listenerId.AddEventListenerToSuffix(GameSpriteStateMachine.SPRITE_LISTENER_SUFFIX, this);
        }

        public override void Tick() {
            base.Tick();
        }

        public override void HandleGameEvent(GameEvent gameEvent) {
            if (gameEvent.GetName().Equals(GameSpriteStateMachine.SPRITE_CHANGE)) {
                spriteRenderer.sprite = gameEvent.GetGameData<Sprite>();
            }
            base.HandleGameEvent(gameEvent);
        }
    }
}
