using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed = 0.4f;

    void Update()
    {
        var pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;
        if(pos.x < -10.5f)
        {
            Destroy(gameObject);
        }
    }
}
