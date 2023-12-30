using UnityEngine;

public class HightLightController : MonoBehaviour
{
    private GameObject _lastHighlightedObject;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update() 
    {
        var currentHighlightedObject = GetSelectedHighlightableObject();

        var isNewObject = currentHighlightedObject != _lastHighlightedObject;
        if (isNewObject)
        {
            if (_lastHighlightedObject != null)
            {
                var lastInteractable = _lastHighlightedObject.GetComponent<InteractableItem>();
                if (lastInteractable != null)
                {
                    lastInteractable.RemoveFocus();
                }
            }

            if (currentHighlightedObject != null)
            {
                var newInteractable = currentHighlightedObject.GetComponent<InteractableItem>();
                if (newInteractable != null)
                {
                    newInteractable.SetFocus();
                }
            }

            _lastHighlightedObject = currentHighlightedObject;
        }
    }
    

    private GameObject GetSelectedHighlightableObject()
    {
        var transform1 = _camera.transform;
        var ray = new Ray(transform1.position, transform1.forward);

        if (Physics.Raycast(ray, out var hitInfo, 3f))
        {
            return hitInfo.collider.gameObject;
        }

        return null;
    }
}

