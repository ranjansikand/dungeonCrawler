using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    [Header("Button Type")]

    [Tooltip("Allows a button to be pushed multiple times")]
    [SerializeField] bool toggled;
    [Tooltip("For a button that un-presses after a certain amount of time")]
    [SerializeField] bool timed;

    [Header("Effect")]

    [SerializeField, Range(0, 2f)] float depressAmount;
    [SerializeField] float unlockDelay;
    [Tooltip("Delay before objects are locked again")] 
    [SerializeField] float lockDelay;

    float delay;

    [Header("Attached elements")]

    [SerializeField] GameObject[] objects = new GameObject[1];

    bool pushed = false;


    void Update() {
        if (timed) {
            if (pushed) {
                if (delay <= 0){
                    ResetButton();
                    Invoke(nameof(Unlock), unlockDelay);
                }
                else delay -= Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer > 7 && !pushed)
        {
            pushed = true;
            transform.position -= new Vector3(0, depressAmount, 0);
            Invoke(nameof(Unlock), unlockDelay);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer > 7 && toggled)
        {
            Invoke(nameof(ResetButton), 0.1f);
        }
    }

    void ResetButton()
    {
        pushed = false;
        transform.position += new Vector3(0, depressAmount, 0);
        delay = lockDelay;
    }

    void Unlock()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].GetComponent<InteractiveEnvironment>().Toggle();
        }
    }
}
