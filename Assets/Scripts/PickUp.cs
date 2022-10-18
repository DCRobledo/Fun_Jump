using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    private Rigidbody2D rigidBody;



    private void Start() {
        this.rigidBody = this.GetComponent<Rigidbody2D>();

        rigidBody.velocity += Vector2.left * speed; 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            HUD.instance.UpdateExperience(Random.Range(20, 50), true);

            AudioController.Instance.Play("pick_up");

            Destroy(this.gameObject);
        }
            

        else if(other.tag == "Bounds")
            Destroy(this.gameObject);
    }

    public void SwitchDirection() { speed *= -1; }
}
