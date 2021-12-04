using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qui.GroundDetector {

    public enum GroundedStatus { 
        Grounded,
        LeftLedge,
        RightLedge,
        Peak,
        OverHole,
        OnAir
    }

    public class GroundDetector2D
    {

        public static GroundedStatus OnGround(Collider2D collider, LayerMask ground) {
            return OnGround(collider, ground, Vector3.down);
        }

        //TODO: Math to support other directions
        public static GroundedStatus OnGround(Collider2D collider, LayerMask ground, Vector3 down) {
            GroundedStatus res;

            Vector2 rightSphere = new Vector2(collider.bounds.max.x, collider.bounds.min.y);
            Vector2 centerSphere = new Vector2(collider.bounds.center.x, collider.bounds.min.y);
            Vector2 leftSphere = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
            Debug.DrawRay(rightSphere, down * 0.1f);
            Debug.DrawRay(centerSphere, down * 0.1f);
            Debug.DrawRay(leftSphere, down * 0.1f);

            bool right = Physics2D.OverlapCircle(rightSphere, 0.1f, ground);
            bool center = Physics2D.OverlapCircle(centerSphere, 0.1f, ground);
            bool left  = Physics2D.OverlapCircle(leftSphere, 0.1f, ground);

            if(right && !left) res = GroundedStatus.LeftLedge;
            else if(!right && left) res = GroundedStatus.RightLedge;
            else if(!center && !right && !left) res = GroundedStatus.OnAir;
            else if(center && !right && !left) res = GroundedStatus.Peak;
            else if(!center && right && left) res = GroundedStatus.OverHole;
            else res = GroundedStatus.Grounded;

            return res;
        }
    }

}