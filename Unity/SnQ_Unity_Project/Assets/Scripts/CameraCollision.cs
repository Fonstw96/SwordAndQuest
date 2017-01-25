using UnityEngine;
using System.Collections;

public class CameraCollision : MonoBehaviour
{

    public bool canScroll = true; //whether or not you can zoom in and out
    public Transform focusPoint; //used as the focal rotation point, and raycast point | must be centered on the player(x and z)
    public float detectionRadius = 0.15f; //radius detection | used to prevent the camera from peering through when standing up against a wall
    public float zoomDistance = 1; //the distance the camera will zoom per scroll
    public int maxZoomOut = 5; //used to limit distance you can zoom out, away from your character
    private int maxZoomIn = 3; //used to limit distance you can zoom in, towards your character
    private int zoom = 0; //used to limit distance you can zoom in and out
    private RaycastHit hit; //used to detect objects in front of camera
    private GameObject camFollow; //monitors camera's position
    private GameObject camSpot; //camera's destination | used for zooming camera in and out

    void Start()
    {
        //set clipping planes to 0.01f
        GetComponent<Camera>().nearClipPlane = 0.01f;

        //set focusPoint
        if (focusPoint == null)
        {
            focusPoint = transform.parent.transform;
        }

        //create camSpot
        camSpot = new GameObject();
        camSpot.transform.name = "CameraSpot";
        camSpot.transform.parent = transform.parent;
        camSpot.transform.position = transform.position;

        //create camFollow
        camFollow = new GameObject();
        camFollow.transform.name = "CameraFollow";
        camFollow.transform.parent = transform.parent;
        camFollow.transform.position = focusPoint.position;
        //make sure the camFollow is looking at the camera
        camFollow.transform.LookAt(transform);
    }
    void Update()
    {
        //If player mouse-scrolls foward
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (canScroll == true)
            {
                //can only zoom in four intervals from camSpot's starting pos
                if (zoom < maxZoomIn)
                {
                    //zoom camSpot in
                    camSpot.transform.position = camSpot.transform.position + 1 * -camFollow.transform.forward;
                    maxZoomOut += 1; maxZoomIn -= 1;
                }
            }
        }
        //If player mouse-scrolls backward
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (canScroll == true)
            {
                //can only zoom out four intervals from camSpot's starting pos
                if (zoom > -maxZoomOut)
                {
                    //zoom camSpot out
                    camSpot.transform.position = camSpot.transform.position - 1 * -camFollow.transform.forward;
                    maxZoomOut -= 1; maxZoomIn += 1;
                }
            }
        }

        //distance between camFollow and camSpot
        float distFromCamSpot = Vector3.Distance(camFollow.transform.position, camSpot.transform.position);
        //distance between camFollow and camera
        float distFromCamera = Vector3.Distance(camFollow.transform.position, transform.position);

        //ShereCast from camFollow to camSpot
        if (Physics.SphereCast(camFollow.transform.position, detectionRadius, camFollow.transform.forward, out hit, distFromCamSpot))
        {
            float distFromHit = Vector3.Distance(camFollow.transform.position, hit.point);
            if (distFromHit < distFromCamera)
            { 
                if (distFromCamera > 1)
                {
                    transform.position = hit.point + 1 * -camFollow.transform.forward;
                }
                else
                {
                    transform.position = camFollow.transform.position;
                }
            }
            else
            {
               
                if (distFromCamera > 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hit.point + 1 * -camFollow.transform.forward, 5 * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, camFollow.transform.position, 5 * Time.deltaTime);
                }
            }
        }
        else
        {
            //ease camera back to camSpot
            transform.position = Vector3.MoveTowards(transform.position, camSpot.transform.position, 5 * Time.deltaTime);
        }
    }
}