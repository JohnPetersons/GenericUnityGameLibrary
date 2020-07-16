using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenericUnityGame {
    public class GamePolygonColliderSpriteState : GameSpriteState {
        
        private Vector2[] colliderPoints;
        private PolygonCollider2D collider;

        public GamePolygonColliderSpriteState(GameEventListenerId listenerId, string spriteName): base(listenerId, spriteName) {
            this.colliderPoints = new Vector2[] {new Vector2(0.5f, 0.5f),
                new Vector2(0.5f, -0.5f),
                new Vector2(-0.5f, -0.5f),
                new Vector2(-0.5f, 0.5f)}; 
            this.collider = this.gameObject.GetComponent<PolygonCollider2D>();
        }

        public GamePolygonColliderSpriteState(GameEventListenerId listenerId, string spriteName, Vector2[] colliderPoints): base(listenerId, spriteName) {
            this.colliderPoints = colliderPoints; 
            this.collider = this.gameObject.GetComponent<PolygonCollider2D>();
        }

        public override void Begin() {
            base.Begin();
            this.collider.SetPath(0, this.colliderPoints);
        }
    }
}
