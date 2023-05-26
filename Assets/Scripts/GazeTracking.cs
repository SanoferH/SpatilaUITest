using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTracking : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 1.0f;
    [SerializeField] private DynamicUIBehaviour _dynamicUIBehaviour;
    [SerializeField] private Transform _uIPosition;
    private bool _iconExpanded = false;
    private bool _welcomeMessageShown = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<IconIdentifier>())
            {
                if (!_iconExpanded)
                {
                    _dynamicUIBehaviour.IconGazeHovered();
                    _iconExpanded = true;
                }
            }
        }
        else if (_iconExpanded)
        {
            _dynamicUIBehaviour.IconGazeUnHovered();
            _iconExpanded = false;
        }

        float dist = Vector3.Distance(_uIPosition.position, transform.position);
        if (dist < 2.0f)
        {
            if (!_welcomeMessageShown)
            {
                _dynamicUIBehaviour.ActivateWelcomeMessage();
                _welcomeMessageShown = true;
            }
        }
        else if(_welcomeMessageShown)
        {
            _dynamicUIBehaviour.DeactivateWelcomeMessage();
            _welcomeMessageShown = false;
        }
    }

    private void Update()
    {
        
        
    }
}
