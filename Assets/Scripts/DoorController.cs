using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, ISwitchable
{
    private Vector3 targetPosition;
    public Transform startPosition;
    public Transform endPosition;
    [SerializeField] float speed;
    private bool opened;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        opened = false;
        moving = false;
        targetPosition = endPosition.position;        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == endPosition.position || transform.position == startPosition.position)
            {
                moving = false;
            }           
        }
    }

    public void on() {
        Debug.Log("ON DOOR");
        targetPosition = endPosition.position;
        moving = true;
        opened = true;
    }

    public void off() {
        targetPosition = startPosition.position;
        moving = true;
        opened = false;
    }

    public bool isOn() {
        return opened;
    }

    public bool isOff() {
        return !isOn();
    }
}
