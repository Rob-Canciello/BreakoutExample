using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public Camera mainCamera;
    private Vector2 screenBoundaries;

    private float objectWidth;
    private float objectHeight;
    
    private void Awake()
    {
        // Here we get the boundaries of our Sprite, but since we're Clamping from our center position,
        // we only need to know what half of our object's dimension are..
        objectWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        screenBoundaries = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
       Vector3 playerPos = transform.position;
        // We set our desired player position (playerPos) by clamping its x and y values.
        // We then add the halved objectHeight/Width to our minimum value and subtract from our minimum value. 
        playerPos.x = Mathf.Clamp(playerPos.x, screenBoundaries.x * -1 + objectWidth, screenBoundaries.x - objectWidth);
        playerPos.y = Mathf.Clamp(playerPos.y, screenBoundaries.y * -1 + objectHeight, screenBoundaries.y - objectHeight);

        transform.position = playerPos;

    }
}
