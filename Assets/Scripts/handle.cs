using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handle : MonoBehaviour
{

    public Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void handleTrigger()
    {
        animator.SetTrigger("knob");

    }
}
