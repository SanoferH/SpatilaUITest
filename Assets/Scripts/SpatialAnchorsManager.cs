using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAnchorsManager : MonoBehaviour
{

    [SerializeField] private GameObject UIAnchor;

    private OVRInput.Controller controller = OVRInput.Controller.RTouch;

    private Transform RightController;
    private void Update()
    {
        bool trigger1Pressed = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
        bool trigger2Pressed = OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger);

        //if the user has pressed the index trigger on one of the two controllers, generate an object in that position
        if (trigger1Pressed)
        {
            Debug.Log("trigger1Pressed");
            // GenerateObject(true);
            GenerateObject1();
        }
            

        if (trigger2Pressed)
        {
            Debug.Log("trigger2Pressed");
            //  GenerateObject(false);
            GenerateObject1();
        }
            
    }

    /// <summary>
    /// Generates an object with the same pose of the controller
    /// </summary>
    /// <param name="isLeft">If the controller to take as reference is the left or right one</param>
    private void GenerateObject(bool isLeft)
    {
        //get the pose of the controller in local tracking coordinates
        OVRPose objectPose = new OVRPose()
        {
            position = OVRInput.GetLocalControllerPosition(isLeft ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch),
            orientation = OVRInput.GetLocalControllerRotation(isLeft ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch)
        };

        //Convert it to world coordinates
        OVRPose worldObjectPose = OVRExtensions.ToWorldSpacePose(objectPose);

        //generate an object depending on the controller onto which the trigger was pressed, and assign it the pose of the controller
        GameObject.Instantiate(UIAnchor,
            objectPose.position,Quaternion.identity);
    }

    private void GenerateObject1()
    {
        RightController.transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        GameObject.Instantiate(UIAnchor,
            RightController.position, Quaternion.identity);
    }
}
