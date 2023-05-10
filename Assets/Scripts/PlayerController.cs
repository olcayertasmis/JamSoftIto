using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Compenents")]
    private Rigidbody2D _rb;
    private Animator _animator;

    [Header("Değişkenler")]
    [SerializeField] private float speed;
    private float _horizontal, _vertical;

    [Header("Other Objects")]
    [SerializeField] private GameObject sleepPanel;
    [SerializeField] private GameObject nightPanel;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _rb.velocity = new Vector2(_horizontal * (speed * Time.deltaTime), _vertical * (speed * Time.deltaTime));

        _animator.SetFloat("Horizontal", _horizontal);
        _animator.SetFloat("Vertical", _vertical);

        /*if (horizontal > 0) Debug.Log(" sağ ");
        else Debug.Log("sol");

        if (vertical > 0) Debug.Log("yukarı");
        else Debug.Log("aşağı");*/
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bed"))
        {
            Message();
        }
    }

    private void Message()
    {
        sleepPanel.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position = new Vector2(5f, 1.75f);
            StartCoroutine(CloseEye());
        }
    }

    private IEnumerator CloseEye()
    {
        nightPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Dream");
    }
}