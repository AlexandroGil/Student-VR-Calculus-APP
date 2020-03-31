using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class planos : MonoBehaviour
{


    public int valueP;
    // Start is called before the first frame update
    void Start()
    {
        valueP = Controls.planoE;
        Debug.Log(valueP);
    }

    // Update is called once per frame
    void Update()
    {
        if(valueP == 1) {
            if (Input.GetKey(KeyCode.J))
            {
                Vector3 position = this.transform.position;
                Debug.Log(position);
                position.x = position.x - 0.2f;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.K))
            {
                Vector3 position = this.transform.position;
                Debug.Log(position);
                position.x = position.x + 0.2f;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.N))
            {
                this.transform.Rotate(2, 0, 0, Space.Self);
            }
            if (Input.GetKey(KeyCode.M))
            {
                this.transform.Rotate(-2, 0, 0, Space.Self);
            }
        }

        if(valueP == 2) {
            if (Input.GetKey(KeyCode.J))
            {
                Vector3 position = this.transform.position;
                Debug.Log(position);
                position.y = position.y - 0.2f;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.K))
            {
                Vector3 position = this.transform.position;
                Debug.Log(position);
                position.y = position.y + 0.2f;
                this.transform.position = position;
            }
        }
        if(valueP == 3) {
            if (Input.GetKey(KeyCode.J))
            {
                Vector3 position = this.transform.position;
                Debug.Log(position);
                position.z = position.z - 0.2f;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.K))
            {
                Vector3 position = this.transform.position;
                Debug.Log(position);
                position.z = position.z + 0.2f;
                this.transform.position = position;
            }
        }
    }
}
