using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    public float Smooth = 1f;
    
    private Transform player;
    private float xRotation;

    private InputManager input;
    
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance.transform;
        input = Manager.Instance.input;
    }

    // Update is called once per frame
    void Update()
    {
     ///   RotateGun();
    }

    void RotateGun()
    {
        if (input.SwipeUp || input.SwipeDown || input.isSwipping)
        {
            float mouseY = Input.GetAxis("Mouse Y") * Smooth * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -20f, 20f);
        
            transform.localRotation = Quaternion.Euler(xRotation, -180f, 0f);
        }
    }
}
