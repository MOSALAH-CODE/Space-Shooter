using UnityEngine;

public partial class PlayerController
{
    /// <summary>
    /// Define the area where the player can move
    /// </summary>
    private float xRange = 5.0f;
    private float yzRange = 4.0f;

    private float horizontalInput;

    public Joystick joystick;

    public float speed = 10.0f;
    /// <summary>
    /// To hide the player when the player is killed
    /// </summary>
    public GameObject toHidePlayer;
    private void Update()
    {
#if UNITY_EDITOR
        horizontalInput = Input.GetAxis("Horizontal");
        transform.position += transform.right * Time.deltaTime * speed * horizontalInput;
#endif
    }
    /// <summary>
    /// To move the player using the joystick
    /// </summary>
    void FixedUpdate()
    {
        if (MoveForwardManager.Instance.stopMoving)
            toHidePlayer.SetActive(false);
        else if (GameSceneManager.Check2D)
        {
            toHidePlayer.SetActive(true);
            // Move Right
            if (joystick.Horizontal >= .2f)
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            // Move Left
            else if (joystick.Horizontal <= -.2f)
                transform.Translate(Vector2.right * Time.deltaTime * -speed);
            // Move Up
            if (joystick.Vertical >= .2f)
                transform.Translate(Vector2.up * Time.deltaTime * speed);
            // Move down
            else if (joystick.Vertical <= -.2f)
                transform.Translate(Vector2.up * Time.deltaTime * -speed);
            // range of y-axis that the player can move
            if (transform.position.y > yzRange)
                transform.position = new Vector2(transform.position.x, yzRange);
            else if (transform.position.y < -yzRange)
                transform.position = new Vector2(transform.position.x, -yzRange);

        }
        else if (GameSceneManager.Check3D && !MoveForwardManager.Instance.stopMoving)
        {
            // Move Right
            if (joystick.Horizontal >= .2f)
                HorizontalMove(speed, right: true);
            // Move Left
            else if (joystick.Horizontal <= -.2f)
                HorizontalMove( -speed, left: true);
            else
                HorizontalMove( 0);
            // Move Up
            if (joystick.Vertical >= .2f)
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            // Move Down
            else if (joystick.Vertical <= -.2f)
                transform.Translate(Vector3.forward * Time.deltaTime * -speed);
            
            // range of y-axis that the player can move
            if (transform.position.z > yzRange)
                transform.position = new Vector3(transform.position.x, transform.position.y, yzRange);
            else if (transform.position.z < -yzRange)
                transform.position = new Vector3(transform.position.x, transform.position.y, -yzRange);
        }
        // range of x-axis that the player can move
        if (transform.position.x > xRange)
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        else if (transform.position.x < -xRange)
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Move player horizontally with tilt animation
    /// </summary>
    /// <param name="Speed">Movement speed</param>
    /// <param name="right">to start right tilt animation</param>
    /// <param name="left">to start left tilt animation</param>
    void HorizontalMove(float Speed, bool right = false, bool left = false)
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
        collisionAnim.SetBool(Tags.rightAnimation, right);
        collisionAnim.SetBool(Tags.leftAnimation, left);
    }
}
