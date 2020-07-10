﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GameAudioState: GameEventListenerState { 

        private AudioClip clip;

        public GameAudioState(string listenerId, string clipName): base(listenerId) {
            this.clip = Resources.Load<AudioClip>(GameLoader.AUDIO_CLIPS + clipName);
        }

        public new void Begin() {
            base.Begin();
            new TypedGameEvent<AudioClip>(this.GetEventListenerId(), GameAudioStateMachine.PLAY_AUDIO_CLIP, this.clip);
        }
    }
}
