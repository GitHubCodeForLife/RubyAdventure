using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(target.position);
        if (target == null) return;
        Vector3 pos = transform.position;
        pos.x = target.position.x;
        pos.y = target.position.y + 3;
        transform.position = pos;
    }
}
