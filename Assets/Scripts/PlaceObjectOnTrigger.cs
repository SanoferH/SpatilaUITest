using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectOnTrigger : MonoBehaviour
{
    public float sensitivity = 0.1f;
   
    public OVRInput.Controller controller = OVRInput.Controller.RTouch;
    private Vector3 startPosition;
    private Vector2 touchStartPosition;

    [SerializeField] private GameObject IconObject;
    [SerializeField] private Transform PointerPose;


    private bool isHovered = false;

    private bool isIconHovered = false;

    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        if (isHovered)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
            {
                touchStartPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
            }


            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controller))
            {
                float joystickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller).y;
                Vector3 position = transform.position;
                position.z += joystickY * sensitivity;
                transform.position = position;

                IconObject.transform.position = new Vector3(PointerPose.position.x, PointerPose.position.y, IconObject.transform.position.z);
            }
          
        }
       
      

    }

    public void IsHovered()
    {
        isHovered = true;
    }

    public void UnHovered()
    {
        isHovered = false;
    }


}
