using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DynamicUIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _uIicon;
    [SerializeField] private GameObject _icon;
    [SerializeField] private GameObject _iconBorder;
    [SerializeField] private GameObject _welcomeMessage;


    public float sensitivity = 0.1f;
    public OVRInput.Controller controller = OVRInput.Controller.RTouch;
    [SerializeField] private Transform RaycastCursorVisual;
    private bool isHovered = false;
    private Vector2 touchStartPosition;
    private bool demoStarted = false;
    void Start()
    {
        SetupWelcomeMessage();
    }

    // Update is called once per frame
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

              //  Debug.Log("UI" + _uIicon.transform.position);

                _uIicon.transform.position = new Vector3(RaycastCursorVisual.position.x, RaycastCursorVisual.position.y - 1.85f, _uIicon.transform.position.z);
            }

        }
    }
    [Button]
    public void DemoStarted()
    {
        GetComponent<BoxCollider>().enabled = false;
        demoStarted = true;
    }

    public void IsHovered()
    {
        isHovered = true;
    }

    public void UnHovered()
    {
        isHovered = false;
    }

    [Button]
    public void IconGazeHovered()
    {
        LeanTween.scale(_iconBorder, new Vector3(0.4f, 0.4f, 0.4f), 0.55f).setEaseOutQuart();
    }
    [Button]
    public void IconGazeUnHovered()
    {
        LeanTween.scale(_iconBorder, new Vector3(0.28f, 0.28f, 0.28f), 0.25f).setEaseOutQuart();
    }
    [Button]
    public void SetupWelcomeMessage()
    {
        _welcomeMessage.GetComponent<CanvasGroup>().alpha = 0.0f;
        _welcomeMessage.SetActive(false);
    }

    [Button]
    public void ActivateWelcomeMessage()
    {
        if (demoStarted)
        {
            _welcomeMessage.SetActive(true);
            LeanTween.alphaCanvas(_welcomeMessage.GetComponent<CanvasGroup>(), 1.0f, 1.0f);
            LeanTween.moveLocalY(_welcomeMessage, 1.75f, 1.0f).setEaseInOutCubic();
        }
        
    }
    [Button]
    public void DeactivateWelcomeMessage()
    {
        LeanTween.alphaCanvas(_welcomeMessage.GetComponent<CanvasGroup>(), 0.0f, 0.5f);
        LeanTween.moveLocalY(_welcomeMessage, 1.8f, 0.5f).setEaseInOutCubic().setOnComplete(SetupWelcomeMessage);
    }
}
