using UnityEngine;

public class Features : MonoBehaviour
{
    /// <summary>
    /// Variables for LookAt2D method
    /// </summary>
    private static float offset = 90f;
    private static Vector2 direction;
    private static float angle;

    /// <summary>
    /// compare given tags with a tag
    /// </summary>
    /// <param name="gameObject">The gameObject of the tag we want to compare</param>
    /// <param name="tags">What tags to compare</param>
    /// <returns>return true if any tag is the same as the tag we want to compare</returns>
    static public bool CheckTags(GameObject gameObject, params string[] tags)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (gameObject.CompareTag(tags[i]))
                return true;
        }
        return false;
    }
    /// <summary>
    /// compare given strings with a string
    /// </summary>
    /// <param name="stringToCompare">The gameObject of the string we want to compare</param>
    /// <param name="strings">What strings to compare</param>
    /// <returns>return true if any string is the same as the string we want to compare</returns>
    public static bool CheckStrings(string stringToCompare, params string[] strings)
    {
        for (int i = 0; i < strings.Length; i++)
        {
            if (stringToCompare == strings[i])
                return true;
        }
        return false;
    }
    /// <summary>
    /// Deactivete all gameObjects thats given in parameter
    /// </summary>
    /// <param name="objs">gameObjects that will be deacktivated</param>
    public static void DeactiveteAll(params GameObject[] objs)
    {
        foreach (GameObject obj in objs)
            obj.SetActive(false);
    }
    /// <summary>
    /// Activete all gameObjects thats given in parameter
    /// </summary>
    /// <param name="objs">gameObjects that will be acktivated</param>
    public static void ActiveteAll(params GameObject[] objs)
    {
        foreach (GameObject obj in objs)
            obj.SetActive(true);
    }
    /// <summary>
    /// Rotate the gameObject to the target position in 2D
    /// </summary>
    /// <param name="transform">the gameObject's transform that we want to rotate</param>
    /// <param name="targetPos">The position the we want the object look at</param>
    public static void LookAt2D(Transform transform, Vector3 targetPos)
    {
        direction = targetPos - transform.position;
        direction.Normalize();
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
    /// <summary>
    /// set the given object position at random position between specified range
    /// </summary>
    /// <param name="obj">The object reference that we want to set its new position</param>
    /// <param name="y">if it's a 2D object just change at y-axis</param>
    /// <param name="z">if it's a 3D object just change at z-axis</param>
    /// <param name="spawnRangeX">The x-axis range of the random position</param>
    public static void SetRandomPosition(ref GameObject obj, float y = 1, float z = 0, float spawnRangeX = 5.0f)
    {
        obj.transform.position = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), y, z);
    }
}
