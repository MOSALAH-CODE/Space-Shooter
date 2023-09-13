using System.Collections;
using UnityEngine;

public class DestroyAfterClone : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Destroy());
    }
    /// <summary>
    /// To destroy cloning explosion and laser collision animations
    /// </summary>
    /// <returns></returns>
    IEnumerator Destroy()
    {
        yield return new WaitForSecondsRealtime(1);
        Destroy(this.gameObject);
    }
}
