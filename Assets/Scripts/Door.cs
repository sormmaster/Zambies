using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string keyName = "default";
    Vector3 closed;
    Vector3 openUp;
    public void Start()
    {
        closed = transform.position;
        openUp = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
    }
    public void open(string keyPickedUp)
    {
        if(keyName == keyPickedUp)
        {
            StartCoroutine(opening());
        }
    }

    IEnumerator opening()
    {
        
        while(transform.position != openUp)
        {
            transform.position = Vector3.Lerp(transform.position, openUp, Time.deltaTime);
            yield return Time.deltaTime;
        }
    }

}
