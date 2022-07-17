using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public float speed;
    public float deacceleration;
    private Rigidbody2D rb;
    private Vector2 movement;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = Vector2.ClampMagnitude(movement, speed);

        
    }

    private void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + movement * Time.fixedDeltaTime * speed);
    }
}
