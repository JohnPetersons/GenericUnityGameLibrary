using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameAudioStateMachine: GameStateMachine
    { 
        public const string PLAY_AUDIO_CLIP = "playAudioClip";
        public const string AUDIO_LISTENER_SUFFIX = "Audio";
        private AudioSource source;
        
        public override void Begin() {
            base.Begin();
            this.source = gameObject.GetComponent<AudioSource>();
            this.ListenTo(this.GetListenerId() + GameAudioStateMachine.AUDIO_LISTENER_SUFFIX);
        }

        public override void Tick() {
            base.Tick();
        }

        public override void HandleGameEvent(GameEvent gameEvent) {
            if (gameEvent.GetName().Equals(GameAudioStateMachine.PLAY_AUDIO_CLIP)) {
                source.Stop();
                source.clip = gameEvent.GetGameData<AudioClip>();
                source.Play();
            }
            base.HandleGameEvent(gameEvent);
        }
    }
}
