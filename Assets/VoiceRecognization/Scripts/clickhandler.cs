using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Collider))]


public class clickhandler : MonoBehaviour
{
    public UnityEvent upEvent;
    public UnityEvent DownEvent;


    void OnMouseDown()
    {
        Debug.Log("Down");
        DownEvent.Invoke();
        
    }

    void OnMouseUp()
    {
        upEvent.Invoke();
        Debug.Log("Up");
    }

}
