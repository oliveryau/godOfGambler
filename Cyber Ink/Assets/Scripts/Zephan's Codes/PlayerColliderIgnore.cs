using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderIgnore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 6);
        Physics2D.IgnoreLayerCollision(0, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
