using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireController : MonoBehaviour
{
    public Image image;
    /// <summary>
    /// Having the Fire button wherever it was pressed on the screen.
    /// </summary>
    public void ImagePosition()
    {
        Vector3 mousePos = Input.GetTouch(0).position;
        if (mousePos.x > (Screen.width/2))
        {
            image.transform.position = mousePos;
        }
        
    }
    
}
