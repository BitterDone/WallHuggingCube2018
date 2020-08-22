using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugWall : MonoBehaviour
{
    public GameObject cube;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
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

}
