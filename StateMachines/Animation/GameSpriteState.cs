using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameSpriteState: GameEventListenerState
    { 
        public const string TIMED_ANIMATION_CHANGE = "timedAnimationChange";
        private Sprite sprite;
        private double startTimer, timer, sequenceTimer;
        private List<Sprite> sequence;
        private List<double> sequenceTimers;
        private int sequenceIndex;

        public GameSpriteState(GameEventListenerId listenerId): base(listenerId) {
            
        }

        public GameSpriteState(GameEventListenerId listenerId, string spriteName): base(listenerId) {
            this.sprite = Resources.Load<Sprite>(GameLoader.SPRITES + spriteName);
            this.startTimer = 0.0;
            this.timer = 0.0;
            this.sequenceIndex = -1;
        }

        public GameSpriteState(GameEventListenerId listenerId, string spriteName, double firstSequenceLength): base(listenerId) {
            this.sprite = Resources.Load<Sprite>(GameLoader.SPRITES + spriteName);
            this.startTimer = 0.0;
            this.timer = 0.0;
            this.sequence = new List<Sprite>();
            this.sequence.Add(this.sprite);
            this.sequenceTimers = new List<double>();
            this.sequenceTimers.Add(firstSequenceLength);
            this.sequenceTimer = 0;
            this.sequenceIndex = 0;
        }

        public override void Begin() {
            base.Begin();
            new TypedGameEvent<Sprite>(this.GetListenerId() + GameSpriteStateMachine.SPRITE_LISTENER_SUFFIX, GameSpriteStateMachine.SPRITE_CHANGE, this.sprite);
        }

        public override void Tick() {
            base.Tick();
            this.SequenceAnimation();
            if (this.startTimer > 0) {
                this.timer -= GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
                if (this.timer <= 0) {
                    new TypedGameEvent<bool>(this.GetListenerId() + GameSpriteStateMachine.SPRITE_LISTENER_SUFFIX, GameSpriteState.TIMED_ANIMATION_CHANGE, true);
                }
            }
        }

        public void SequenceAnimation() {
            if (this.sequenceIndex >= 0) {
                this.sequenceTimer += GameSystem.GetDeltaTime(GameSystem.GAMEPLAY, Time.deltaTime);
                if (this.sequenceTimer > this.sequenceTimers[this.sequenceIndex]) {
                    this.sequenceTimer = 0;
                    this.sequenceIndex += 1;
                    if (this.sequenceIndex >= this.sequence.Count) {
                        this.sequenceIndex = 0;
                    }
                    new TypedGameEvent<Sprite>(this.GetListenerId() + GameSpriteStateMachine.SPRITE_LISTENER_SUFFIX, GameSpriteStateMachine.SPRITE_CHANGE, this.sequence[this.sequenceIndex]);
                }
            }
        }

        public void AddSequencedSprite(string spriteName, double timer) {
            this.sequence.Add(Resources.Load<Sprite>(GameLoader.SPRITES + spriteName));
            this.sequenceTimers.Add(timer);
        }

        public void SetTimedSpriteStateChange(GameSpriteState spriteState, double startTimer) {
            this.startTimer = startTimer;
            this.timer = startTimer;
            this.AddStateChange(GameSpriteState.TIMED_ANIMATION_CHANGE, spriteState);
        }

        public override GameState GetNextState(GameEvent gameEvent) {
            GameState result = base.GetNextState(gameEvent);
            if (result != this && this.startTimer > 0){
                this.timer = this.startTimer;
            }
            if (result != this && this.sequenceIndex > 0) {
                this.sequenceIndex = 0;
            }
            return result;
        }
    }
}
