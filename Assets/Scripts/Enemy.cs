using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Attributes to get references about this gameobject
    private Rigidbody2D _rb;
    private RaycastHit2D _hit;

    // Attributes to move the player
    public float movHor = 1f;
    public float speed = 3f;
    public bool isGroundFloor = true;
    public bool isGroundFront = false;

    // Attributes to get contact floor
    public LayerMask groundLayer;
    public float frontGrndRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    // Attributes for scoring
    public int scoreGive = 50;

    // Attributes for living
    public float livintTime = 0.5f;

    // Start is called before the first frame update
    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        // Avoid to fall
        isGroundFloor = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z), new Vector3(movHor, 0, 0), frontGrndRayDist, groundLayer);

        if (isGroundFloor)
            movHor = movHor * -1;

        // Avoid to hit with wall
        if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer))
            movHor = movHor * -1;

        // Avoid to hit with other enemy
        _hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor * frontCheck, transform.position.y, transform.position.z), new Vector3(movHor, 0, 0), frontDist);
        
        if (_hit.transform != null)
            if (_hit.transform.CompareTag("Enemy"))
                movHor = movHor * -1;
    }

    // Use to calculate the physics calculations
    void FixedUpdate() {
        _rb.velocity = new Vector2(movHor * speed, _rb.velocity.y);
    }

    // Check if the collider hit collision
    void OnCollisionEnter2D(Collision2D other) {
        // Damage to player
        if (other.gameObject.CompareTag("Player")) {
            // Thaks to singleton we can obtaint the class withou instances
            Player.obj.GetDamage();
        }
    }

    // Check if the collider enter in other collider
    void OnTriggerEnter2D(Collider2D other) {
        // Destroy the enemy
        if (other.gameObject.CompareTag("Player")) {
            // Play audio clip
            AudioManager.obj.PlayEnemyHit();
            // Jump player
            Player.obj.hit = true;
            // Destroy game object
            GetKilled();
        }
    }

    // Desactive the gameobject if enemy is dead
    void GetKilled() {
        // Show effect
        FXManager.obj.ShowPop(transform.position);
        // Non active object
        gameObject.SetActive(false);
        // Destroy object
        Destroy(gameObject, livintTime);
    }
}
