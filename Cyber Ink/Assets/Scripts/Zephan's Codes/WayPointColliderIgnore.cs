using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointColliderIgnore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(0, 9); 
        Physics2D.IgnoreLayerCollision(0, 8);
        Physics2D.IgnoreLayerCollision(0, 6);
        Physics2D.IgnoreLayerCollision(3, 6);
        Physics2D.IgnoreLayerCollision(3, 9);

    }
}
