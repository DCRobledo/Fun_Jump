using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public static Camera instance;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        this.animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomIn() { animator.SetTrigger("zoom_in"); }
    public void ZoomOut() { animator.SetTrigger("zoom_out"); }
}
