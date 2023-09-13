using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private float HitTime;
    /// <summary>
    /// To hold bullet sound and bullet object.
    /// </summary>
    public AudioSource sfxLaser;

    public GameObject BulletObject;
    /// <summary>
    /// It is used for normal ignition.
    /// </summary>
    public void FireButton()
    {
        sfxLaser.Play();
        Instantiate(BulletObject, PlayerController.Instance.gameObject.transform.position, BulletObject.transform.rotation);
        HitTime = Time.time + 0.15f;
    }
    /// <summary>
    /// It is used for triple ignition.
    /// </summary>
    public void TripleShuot()
    {
        if (GameSceneManager.Check2D)
        {
            TripleShuotSpawn(0f);
        }
        else if (GameSceneManager.Check3D)
        {
            TripleShuotSpawn(90f);
        }

    }
    /// <summary>
    /// Triple firing to spawn the player according to position.
    /// </summary>
    /// <param name="rotationX">Bullet x rotation("0" for 2D, "90" for 3D).</param>
    void TripleShuotSpawn(float rotationX)
    {
        sfxLaser.Play();
        Vector3 R = new(PlayerController.Instance.gameObject.transform.position.x + 0.86f, PlayerController.Instance.gameObject.transform.position.y, PlayerController.Instance.gameObject.transform.position.z);
        Vector3 L = new(PlayerController.Instance.gameObject.transform.position.x - 0.86f, PlayerController.Instance.gameObject.transform.position.y, PlayerController.Instance.gameObject.transform.position.z);
        Quaternion RRotation = Quaternion.identity;
        RRotation.eulerAngles = new Vector3(rotationX, BulletObject.transform.rotation.y, BulletObject.transform.rotation.z - 10f);
        Quaternion LRotation = Quaternion.identity;
        LRotation.eulerAngles = new Vector3(rotationX, BulletObject.transform.rotation.y, BulletObject.transform.rotation.z + 10f);
        Instantiate(BulletObject, R, RRotation);
        Instantiate(BulletObject, PlayerController.Instance.gameObject.transform.position, BulletObject.transform.rotation);
        Instantiate(BulletObject, L, LRotation);
        HitTime = Time.time + 0.15f;
    }
}
