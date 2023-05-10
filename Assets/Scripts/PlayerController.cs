using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Compenents")]
    private Rigidbody2D _rb;

    [Header("Değişkenler")]
    [SerializeField] private float speed;
    private float horizontal, vertical;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Move();    
    }

    void Update()
    {

    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        _rb.velocity = new Vector2(horizontal * (speed * Time.deltaTime), vertical * (speed * Time.deltaTime));

        if (horizontal > 0) Debug.Log(" sağ ");
        else Debug.Log("sol");

        if (vertical > 0) Debug.Log("yukarı");
        else Debug.Log("aşağı");
          
    }
}
