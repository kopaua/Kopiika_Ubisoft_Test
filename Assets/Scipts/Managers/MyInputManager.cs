using UnityEngine;
using System;

public class MyInputManager : MonoBehaviour
{  
    public static Action OnClickFire;   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnClickFire?.Invoke();          
        }
    }
}
