using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Qui.GroundDetector;
public class LD2DExampleMob : MonoBehaviour
{
    public LayerMask ground;
    Collider2D col;

    public Text text;

    private void Awake() {
        col = this.GetComponent<Collider2D>();    
    }

    void Update()
    {
        GroundedStatus status = GroundDetector2D.OnGround(col, ground);

        switch (status)
        {
            case GroundedStatus.Grounded:
                text.text = "Grounded";
                break;
            case GroundedStatus.LeftLedge:
                text.text = "LeftLedge";
                break;
            case GroundedStatus.OnAir:
                text.text = "OnAir";
                break;
            case GroundedStatus.OverHole:
                text.text = "OverHole";
                break;
            case GroundedStatus.Peak:
                text.text = "Peak";
                break;
            case GroundedStatus.RightLedge:
                text.text = "RightLedge";
                break;
        }
    }
}
