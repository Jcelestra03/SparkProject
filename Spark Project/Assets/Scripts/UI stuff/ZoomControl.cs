using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Animations;
public class ZoomControl : MonoBehaviour
{
    public float ZoomChange;
    public float SmoothChange;
    public float MinSize, MaxSize;

    private Camera cam;
    private GameObject CM;
    private float scale;
    private bool followPlayer;
    private GameObject player;
    private ConstraintSource playerTarget;
    private PositionConstraint camFollow;
    private Vector3 oldPos;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        camFollow = GetComponent<PositionConstraint>();

        scale = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name.Contains("Camera"))
        {
            if (!followPlayer && (GetComponent<GridControl>().gamestart || GetComponent<GridControl>().editing))
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
            }

            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, MinSize, MaxSize);
        }
        else if (gameObject.name.Contains("dropper"))
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }

        if (GetComponent<GridControl>().gamestart && followPlayer == false)
            CamShift();
        else if (!GetComponent<GridControl>().gamestart && followPlayer == true)
            CamBack();
    }

    public void CamShift()
    {
        if (GameObject.Find("player (1)(Clone)") == null) { return; }
        else { player = (GameObject.Find("player (1)(Clone)")); }
        oldPos = cam.transform.position;

        playerTarget.sourceTransform = player.transform;
        playerTarget.weight = 1;

        followPlayer = true;
        camFollow.SetSource(0, playerTarget);
        camFollow.constraintActive = true;
        cam.orthographicSize = 7;
    }

    public void CamBack()
    {
        if(player == null) { return; }
        cam.transform.position = oldPos;

        followPlayer = false;
        camFollow.constraintActive = false;
    }
}
