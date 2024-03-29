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
    public bool hit = false;
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
        // If game is paused the player wont move
        if (Game.obj.isGamePaused) {
            movHor = 0f;
            return;
        }

        movHor = Input.GetAxisRaw("Horizontal");

        isMooving = (movHor != 0f);

        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (hit)
            Rebound();

        // Implements the inmunity (when player touchs an enemy he will inmune with a special animations)
        if (isImmune) {
            _spr.enabled = !_spr.enabled;

            // Decrement the counter 
            immuneTimecnt -= Time.deltaTime;

            if (immuneTimecnt <= 0) {
                isImmune = false;
                _spr.enabled = true;
            }
        }

        // Animations
        _anim.SetBool("IsMoving", isMooving);
        _anim.SetBool("IsGrounded", isGrounded);

        Flip(movHor);
    }

    // Use to calculate the physics calculations
    void FixedUpdate() {
        _rb.velocity = new Vector2(movHor * speed, _rb.velocity.y);
    }

    void GoImmune() {
        isImmune = true;
        // Reset the counter
        immuneTimecnt = immuneTime;
    }

    // Method to jump the player
    private void Jump() {
        // With this condition if player is jumping the function exit
        if (!isGrounded) return;

        // If not is jumping
        _rb.velocity = Vector2.up * jumpForce;

        // Play audio clip
        AudioManager.obj.PlayJump();
    }

    // Rebound if player hit an enemy
    private void Rebound() {
        _rb.velocity = Vector2.up * jumpForce;
        hit = false;
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

        // Play audio clip
        AudioManager.obj.PlayHit();

        // Update the lives text
        UIManager.obj.UpdateLives();

        // Convert to immune
        GoImmune();

        if (lives <= 0) {
            this.gameObject.SetActive(false);
            Game.obj.GameOver();
        }
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
