using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Singleton -> Make Player singleton to access to this object wherever script
    public static Player obj;

    // Attributes for live
    public int lives = 3;

    // Attributes for moving
    public bool isGrounded = false;
    public bool isMooving = false;
    public bool isImmune = false;
    public float speed = 5f;
    public float jumpForce = 3f;
    public float movHor;
    public float immuneTimecnt = 0f;
    public float immuneTime = 0.5f;

    // Attributes to get contact with floor
    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    // Attributes to get references about this gameobject
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _spr;

    // Start before the game start
    void Awake() {
        // Implement the Singleton
        obj = this;
    }

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        movHor = Input.GetAxisRaw("Horizontal");

        isMooving = (movHor != 0f);

        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // Animations
        _anim.SetBool("isMoving", isMooving);
        _anim.SetBool("IsGrounded", isGrounded);

        Flip(movHor);
    }

    // Use to calculate the physics calculations
    void FixedUpdate() {
        _rb.velocity = new Vector2(movHor * speed, _rb.velocity.y);
    }

    // Method to jump the player
    private void Jump() {
        // With this condition if player is jumping the function exit
        if (!isGrounded) return;

        // If not is jumping
        _rb.velocity = Vector2.up * jumpForce;
    }

    // Rotate the scale transform if player move to left or right
    private void Flip(float _value) {
        Vector3 scale = transform.localScale;

        if (_value < 0)
            scale.x = Mathf.Abs(scale.x) * -1;
        else if (_value > 0)
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    // Method to remove lives
    public void GetDamage() {
        lives--;

        if (lives <= 0)
            this.gameObject.SetActive(false);
    }

    public void AddLive() {
        lives++;

        if (lives > Game.obj.maxLives)
            lives = Game.obj.maxLives;
    }

    // Occurs when a Scene or game ends
    void OnDestroy() {
        // Finish the object Singleton
        obj = null;
    }
}
