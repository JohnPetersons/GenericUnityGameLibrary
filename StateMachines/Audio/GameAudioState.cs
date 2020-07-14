using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameAudioState: GameEventListenerState { 

        private AudioClip clip;

        public GameAudioState(GameEventListenerId listenerId, string clipName): base(listenerId) {
            this.clip = Resources.Load<AudioClip>(GameLoader.AUDIO + clipName);
        }

        public override void Begin() {
            base.Begin();
            new TypedGameEvent<AudioClip>(this.GetEventListenerId() + GameAudioStateMachine.AUDIO_LISTENER_SUFFIX, GameAudioStateMachine.PLAY_AUDIO_CLIP, this.clip);
        }
    }
}
