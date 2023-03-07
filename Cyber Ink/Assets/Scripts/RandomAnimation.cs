using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        var state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, 0, Random.Range(0f, 1f));
    }
}
