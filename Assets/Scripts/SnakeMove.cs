using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camera.GetComponent<Transform>().position = transform.position - new Vector3(0,0,10);
        transform.position += (Input.mousePosition-new Vector3(10,6,0) - transform.position).normalized * Time.deltaTime;
    }
}
