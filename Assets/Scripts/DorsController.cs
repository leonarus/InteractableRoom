using System;
using UnityEngine;

public class DorsController : MonoBehaviour
{
    private void Update()
    {
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out var hitInfo, 3f))
        {
            var hitObject = hitInfo.collider.gameObject;

            if (Input.GetKeyDown(KeyCode.E))
            {
                var door = hitObject.GetComponent<Door>();
                
                if (door != null)
                {
                    door.SwitchDoorState();
                }
            }
        }
    }
}
