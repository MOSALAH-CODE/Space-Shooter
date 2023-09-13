using UnityEngine;

public class Rotate : MonoBehaviour
{
    /// <summary>
    /// Allows 3D meteor objects to spin over.
    /// </summary>
    public float speed = 45;
    void Update()
    {
        if (!MoveForwardManager.Instance.stopMoving)
            transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
    }
}
