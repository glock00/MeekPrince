using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincePixel : MonoBehaviour
{
    public AudioClip jumpClip;
    public AudioClip dieClip;
    public AudioClip winClip;

    private GameController controller;
    private Animator animator;
    private bool stationTouched = false;
    private GameObject unloadingStation;
    private bool enterPressed;

    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        enterPressed = Input.GetKey(KeyCode.Return);
    }

    void FixedUpdate()
    {
        if (enterPressed && stationTouched)
        {
            animator.SetBool("Unloading", true);
        }
        else if (!enterPressed)
        {
            animator.SetBool("Unloading", false);
        }
    }

    void PlayJumpSound(AudioClip clip)
    {
        controller.audioSource.PlayOneShot(clip);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            controller.Score(1);
        }
        else if (other.gameObject.CompareTag("Diamond"))
        {
            other.gameObject.SetActive(false);
            controller.Score(3);
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {
            controller.WinGame();
            controller.audioSource.PlayOneShot(winClip);
        }
        else if (other.gameObject.CompareTag("Fire"))
        {
            controller.audioSource.PlayOneShot(dieClip);
            controller.LoseGame();
        }
    }
}
