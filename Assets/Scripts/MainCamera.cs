using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera instance;

    private Animator animator;

    void Start()
    {
        instance = this;

        this.animator = this.GetComponent<Animator>();
    }

    public void ZoomIn() { animator.SetTrigger("zoom_in"); }
    public void ZoomOut() { animator.SetTrigger("zoom_out"); }
}
