using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    private float scrollSpeed = 1f;
    private int level;
    private int[,] levelBoundaries = new int[,] { { -7, -7, -147, -114 }, { -40, 0, -26, 43 } };

    void Start()
    {
        Application.targetFrameRate = 60;
        level = SceneManager.GetActiveScene().name == "Level 1" ? 1 : 2;
    }

    void FixedUpdate()
    {
        var mousePos = Input.mousePosition; //We need to get the new position every frame

        //if mouse is 50 pixels and less from the left side of the
        //screen, we move the camera in that direction at scrollSpeed
        if (mousePos.x < 50 && gameObject.transform.position.x > levelBoundaries[level - 1, 0])
            gameObject.transform.Translate(-scrollSpeed, 0, 0);

        //if 50px or less from the right side, move right at scrollSpeeed
        if (mousePos.x > Screen.width - 50 && gameObject.transform.position.x < levelBoundaries[level - 1, 1])
            gameObject.transform.Translate(scrollSpeed, 0, 0);

        //move down
        if (mousePos.y < 50 && gameObject.transform.position.z > levelBoundaries[level - 1, 2])
            gameObject.transform.Translate(0, -scrollSpeed * Mathf.Cos(30 * Mathf.Deg2Rad), -scrollSpeed * Mathf.Cos(60 * Mathf.Deg2Rad));

        //move up
        if (mousePos.y > Screen.height - 50 && gameObject.transform.position.z < levelBoundaries[level - 1, 3])
            gameObject.transform.Translate(0, scrollSpeed * Mathf.Cos(30 * Mathf.Deg2Rad), scrollSpeed * Mathf.Cos(60 * Mathf.Deg2Rad));
    }
}
