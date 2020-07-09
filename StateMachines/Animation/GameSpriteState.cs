using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameSpriteState: GameEventListenerState
    { 
        public const string TIMED_ANIMATION_CHANGE = "timedAnimationChange";
        private Sprite sprite;
        private double startTimer, timer;
        public GameSpriteState(string listenerId, string spriteName): base(listenerId) {
            this.sprite = Resources.Load<Sprite>(GameLoader.SPRITES + spriteName);
            this.startTimer = 0.0;
            this.timer = 0.0;
        }

        public new void Begin() {
            base.Begin();
            new TypedGameEvent<Sprite>(this.GetEventListenerId(), GameSpriteStateMachine.SPRITE_CHANGE, this.sprite);
        }

        public new void Tick() {
            base.Tick();
            if (this.startTimer >= 0) {
                this.timer -= GameSystem.GetDeltaTime(Time.deltaTime);
                if (this.timer <= 0) {
                    new TypedGameEvent<bool>(this.GetEventListenerId(), GameSpriteState.TIMED_ANIMATION_CHANGE, true);
                }
            }
        }

        public void SetTimedSpriteStateChange(GameSpriteState spriteState, double startTimer) {
            this.startTimer = startTimer;
            this.timer = startTimer;
            this.AddStateChange(GameSpriteState.TIMED_ANIMATION_CHANGE, spriteState);
        }

        public new GameState GetNextState(GameEvent gameEvent) {
            GameState result = base.GetNextState(gameEvent);
            if (result != this && this.startTimer > 0){
                this.timer = this.startTimer;
            }
            return result;
        }
    }
}
