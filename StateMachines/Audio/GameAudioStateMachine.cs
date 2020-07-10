using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameAudioStateMachine: GameStateMachine
    { 
        public const string PLAY_AUDIO_CLIP = "playAudioClip";
        private AudioSource source;
        public new void Begin() {
            base.Begin();
            this.source = gameObject.GetComponent<AudioSource>();
        }

        public new void Tick() {
            base.Tick();
        }

        public new void HandleGameEvent(GameEvent gameEvent) {
            if (gameEvent.GetName().Equals(GameAudioStateMachine.PLAY_AUDIO_CLIP)) {
                source.Stop();
                source.clip = gameEvent.GetGameData<AudioClip>();
                source.Play();
            }
            base.HandleGameEvent(gameEvent);
        }
    }
}
