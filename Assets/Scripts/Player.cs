using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private Animator animator;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private float jumpForce = 12f;
    [SerializeField] [Range(1, 10)]
    private float fallMultiplier = 2.5f;
    [SerializeField] [Range(1, 10)]
    private float lowJumpMultiplier = 2f;

    [SerializeField] [Range(0f, 2f)]
    private float jumpCoolDown = 0.5f;

    private bool isJumpOnCoolDown = false;
    private bool checkForLanding = false;
    private bool isAirBone = false;

    [SerializeField]
    private GameObject particles;


    private void Start()
    {
        instance = this;

        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.boxCollider = this.GetComponent<BoxCollider2D>();
        this.animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        IsGrounded(true);

        if(checkForLanding) {
            isAirBone = !IsGrounded(false);

            if(!isAirBone)
                Land();
        }
            
        if(Input.GetKeyDown(KeyCode.Space) && !isJumpOnCoolDown && IsGrounded(false))
            Jump();

        if(!Input.GetKey(KeyCode.Space) && rigidBody.velocity.y > 0)
            rigidBody.velocity *= Vector2.down * Physics2D.gravity * (fallMultiplier - 1) * Time.fixedDeltaTime;
    }

    private void FixedUpdate() {
        ApplyGravity(this.fallMultiplier, this.lowJumpMultiplier);
    }


    private void Jump() {
        rigidBody.velocity += Vector2.up * jumpForce;

        animator.SetTrigger("jump");

        SpawnParticles();

        MainCamera.instance.ZoomIn();

        AudioController.Instance.Play("jump");

        HUD.instance.UpdateExperience(Random.Range(5, 36));

        StartCoroutine(JumpCoolDown());
        StartCoroutine(CheckForLandingDelay());
    }

    private void ApplyGravity(float fallMultiplier, float lowJumpMultiplier) {
        // Regular jump gravity
        if(rigidBody.velocity.y < 0) {
            rigidBody.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier) * Time.fixedDeltaTime;
        }

        // Low jump gravity
        else if (rigidBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            rigidBody.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier) * Time.fixedDeltaTime;
    }

    private void Land() {
        checkForLanding = false;

        animator.SetTrigger("land");

        SpawnParticles();

        MainCamera.instance.ZoomOut();

        AudioController.Instance.Play("land");
    }

    private IEnumerator JumpCoolDown() {
        isJumpOnCoolDown = true;

        yield return new WaitForSeconds(jumpCoolDown);

        isJumpOnCoolDown = false;
    }

    private IEnumerator CheckForLandingDelay() {
        yield return new WaitForSeconds(0.2f);

        checkForLanding = true;
    }

    private bool IsGrounded(bool debug) {
        bool isGrounded;

        RaycastHit2D rayCastHit = 
        Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.25f,
            whatIsGround
        );

        isGrounded = rayCastHit.collider != null;

        if (debug) {
            Color rayColor = isGrounded ? Color.green : Color.red;
            Debug.DrawRay(boxCollider.bounds.center, new Vector2(0f, (- boxCollider.bounds.size.y / 2) - 0.25f), rayColor);
        }      

        return isGrounded;
    }

    private void SpawnParticles() {
        Vector3 position = this.transform.position;

        position.y -= boxCollider.bounds.size.y / 2f;

        Instantiate(particles, position, Quaternion.Euler(-90f, 0f, 0f));
    }

    public void LevelUp() {
        jumpForce += 2f;

        HUD.instance.LevelUp();

        AudioController.Instance.Play("level_up");
    }
}
