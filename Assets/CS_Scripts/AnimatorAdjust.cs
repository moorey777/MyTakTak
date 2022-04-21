using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorAdjust : MonoBehaviour
{
    private Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.speed = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
