using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointers : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        //block edge z
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -2.5f);
        
        try
        {
            //rotate x and y to target
            Vector3 targetPos = target.transform.position;
            Vector3 myPos = transform.position;
            Vector3 direction = targetPos - myPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        catch
        {
            Destroy(gameObject);
        }
    }
}
