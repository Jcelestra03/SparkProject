using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomControl : MonoBehaviour
{
    public float ZoomChange;
    public float SmoothChange;
    public float MinSize, MaxSize;

    private Camera cam;

    private float scale;


    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        scale = 0.2f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.name.Contains("Camera"))
        {
            if (Input.GetMouseButton(1))
            {
                cam.transform.position -= transform.right * Input.GetAxis("Mouse X") * scale;
                cam.transform.position += transform.up * -Input.GetAxis("Mouse Y") * scale;
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                cam.orthographicSize -= ZoomChange * Time.deltaTime * SmoothChange;
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                cam.orthographicSize += ZoomChange * Time.deltaTime * SmoothChange;
            }
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinSize, MaxSize);
        }
        else if (this.gameObject.name.Contains("dropper"))

            {
                Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(cursorPos.x, cursorPos.y);
            }    
    }
 
}
