using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] protected Color activeColor; 

    public bool IsActive { get; private set; }
    protected SpriteRenderer sr; 

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        IsActive = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObj = other.gameObject;

        if (!otherObj.CompareTag("Projectile"))
        {
            IsActive = true;
            sr.color = activeColor;
        }
    }
}
