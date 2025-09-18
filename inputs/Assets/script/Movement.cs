using UnityEngine;



public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed;
    private Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body=GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        body.linearVelocity = movement*speed;
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void OnValidate()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }
}
