using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private bool StopMoveing = false;

    public float Speed;
    /// <summary>
    /// Used to move the meteors, missiles, bombs and power-ups
    /// </summary>
    void Update()
    {
        // Used to decrease or increase the speed of some objects or to release them at the same speed
        if (gameObject.CompareTag(Tags.Stones1))
            Speed = MoveForwardManager.Instance.speed - 0.5f;
        else if (Features.CheckTags(  gameObject, Tags.Bomb, Tags.Missile, Tags.SmallStones))
            Speed = MoveForwardManager.Instance.speed + 1;
        else
            Speed = MoveForwardManager.Instance.speed;
        
        StopMoveing = MoveForwardManager.Instance.stopMoving;

        if (GameSceneManager.Check2D && !StopMoveing)
            transform.position += new Vector3(0, -Time.deltaTime * Speed, 0);

        else if (GameSceneManager.Check3D && !StopMoveing)
        {
            if (Features.CheckTags(gameObject, Tags.ShieldPowerUp, Tags.X2PowerUp, Tags.TripleShuotPowerUp))
                transform.Translate(Vector3.down * Time.deltaTime * Speed);
            else
                transform.Translate(Vector3.back * Time.deltaTime * Speed);

            if (gameObject.CompareTag(Tags.SmallStones) && transform.position.z < -7)
                Destroy(gameObject);
        }
    }
}
