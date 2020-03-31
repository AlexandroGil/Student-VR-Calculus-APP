using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlStudents : MonoBehaviour
{

    public GameObject student;
    public GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("SpawnerA");
        student = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("C")){
            student.transform.position = GetComponent<TeleportingManager>().GetUpperFunctionPos();
        }
    }
}
