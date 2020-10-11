using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugWall : MonoBehaviour
{
    public GameObject cube;
    public Camera mainCamera;
    Color _red = new Color(1, 0, 0, 1);
    Color _blue = new Color(0, 0, 1, 1);
    int counter = 0;
    private void InteractionManager_InteractionSourcePressed(UnityEngine.XR.WSA.Input.InteractionSourcePressedEventArgs obj)
    {
        var color = cube.GetComponent<Renderer>().material.color;//GetColor("_Color");
        Debug.Log("didi the thing " + color); 
        if (obj.pressType == UnityEngine.XR.WSA.Input.InteractionSourcePressType.Select)
        {
            // GetComponent<Renderer>().material.color = kubus.GetComponent<Renderer>().material.GetColor("_Color");
            // OnSelectInteraction();
            // change cube color
            
            switch (counter) {
                case 0:
                    cube.GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case 1:
                    cube.GetComponent<Renderer>().material.color = Color.red;
                    break;
                default:
                    cube.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
            };

            counter++;
            if (counter > 1)
            {
                counter = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cubeLocation = new Vector3(0, 1, 4);
        RaycastHit hit;
        if (TryGazeHitTest(out hit))
        {
            // OnSelectObjectInteraction(hit.point, hit);
            cubeLocation = hit.point;
        }

        cube.transform.position = cubeLocation;

        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     InteractionManager_InteractionSourcePressed();
        // }
    }

    private bool TryGazeHitTest(out RaycastHit target)
    {
        int layer = 5;
        int layerMask = 1 << layer;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        // Does the ray intersect any objects excluding the player layer
        // if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out target, Mathf.Infinity, layerMask))
        // Camera mainCamera = Camera.main;
        // return Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out target);
        return Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out target, Mathf.Infinity, layerMask);
    }

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.WSA.Input.InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;

        // Debug.Log("Color =  " + cube.GetComponent<Renderer>().material.color);
        // Debug.Log(Color.red == RGBA(1.000, 0.000, 0.000, 1.000));
        // Debug.Log(Color.red == new Color(1, 0, 0, 1)); // true
    }

    void OnDestroy()
    {
        UnityEngine.XR.WSA.Input.InteractionManager.InteractionSourcePressed -= InteractionManager_InteractionSourcePressed;
    }
}
