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
    [SerializeField] private float jumpPower;
    static bool _isEat;
    static float _jumpBuff;
    private bool _isFinish;

    [Header("Other Objects")]
    [SerializeField] private GameObject sleepPanel;
    [SerializeField] private GameObject nightPanel;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject eatPanel;
    [SerializeField] private GameObject mustEatPanel;
    [SerializeField] private GameObject alreadyAtePanel;
    [SerializeField] private GameObject finishPanel;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float Abc = Input.GetAxisRaw("Horizontal");
        float Cbc = Input.GetAxisRaw("Vertical");

        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (activeSceneIndex == 0)
            _rb.velocity = new Vector2(Abc, Cbc);

        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(Vector2.up * (jumpPower + _jumpBuff), ForceMode2D.Impulse);
            }
            else
            {
                _rb.velocity = new Vector2(Abc, 0) * (speed);
            }
        }

        _animator.SetFloat("Horizontal", Abc);
        _animator.SetFloat("Vertical", Cbc);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            finishPanel.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bed")) Message();
        else if (collision.gameObject.CompareTag("Desk")) EatPanel();
        else
        {
            sleepPanel.SetActive(false);
            mustEatPanel.SetActive(false);
            eatPanel.SetActive(false);
            alreadyAtePanel.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            StartCoroutine(OpenHouseScene());
        }
    }

    private void EatPanel()
    {
        if (!_isEat) eatPanel.SetActive(true);
        else alreadyAtePanel.SetActive(true);
    }

    public void EatButton()
    {
        _jumpBuff += 100f;
        _isEat = true;
        eatPanel.SetActive(false);
        alreadyAtePanel.SetActive(false);
    }

    private void Message()
    {
        sleepPanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!_isEat)
            {
                sleepPanel.SetActive(false);
                mustEatPanel.SetActive(true);
            }
            else
            {
                transform.position = new Vector2(5f, 1.75f);
                StartCoroutine(OpenDreamScene());
            }
        }
    }

    private IEnumerator OpenDreamScene()
    {
        nightPanel.SetActive(true);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Dream");
    }

    private IEnumerator OpenHouseScene()
    {
        deathPanel.SetActive(true);
        _isEat = false;

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("House");
    }
}