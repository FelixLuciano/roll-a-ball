using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float timer = 30;
    public float MovementAmplitude = 1.0f;
    public TextMeshProUGUI LifesText;
    public TextMeshProUGUI CountText;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public TextMeshProUGUI TimerText;

    public AudioSource AwakeAudioSource;
    public AudioSource HitAudioSource;
    public AudioSource PickAudioSource;

    private Rigidbody rb;
    private Vector3 movementVector;
    private Vector3 spawnPosition;
    private int lifes;
    private int count;

    void SetTimerText()
    {
        if (timer >= 0)
        {
            TimerText.text = timer.ToString("#");
        }
        else
        {
            lifes = 0;

            SetLifesText();
        }
    }

    void SetLifesText()
    {
        LifesText.text = lifes.ToString();

        if (lifes <= 0)
        {
            LoseScreen.SetActive(true);
        }
    }

    void SetCountText()
    {
        CountText.text = count.ToString();

        if (count >= 16)
        {
            WinScreen.SetActive(true);
            AwakeAudioSource.Play();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPosition = transform.position;
        lifes = 3;
        count = 0;

        SetTimerText();
        SetLifesText();
        SetCountText();
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        AwakeAudioSource.Play();
    }

    void OnMove(InputValue inputEvent)
    {
        Vector2 movementValue = inputEvent.Get<Vector2>();

        movementVector = new Vector3(movementValue.x, 0.0f, movementValue.y);
    }

    void FixedUpdate()
    {
        if (lifes > 0 && count < 16)
        {
            rb.AddForce(movementVector * MovementAmplitude);

            timer -= Time.deltaTime;
            SetTimerText();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") && lifes > 0 && count < 16)
        {
            count++;
            MovementAmplitude *= 1.25f;
            transform.localScale += Vector3.one * 0.1f;

            other.gameObject.SetActive(false);
            SetCountText();
            PickAudioSource.Play();
        }
        else if (other.gameObject.CompareTag("Void"))
        {
            if (lifes > 0 && count < 16)
            {
                lifes--;
                SetLifesText();
                HitAudioSource.Play();
            }

            transform.position = spawnPosition;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
