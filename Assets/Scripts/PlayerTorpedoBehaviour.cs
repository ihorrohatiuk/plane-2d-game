using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerTorpedoBehaviour : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }

        else if (collision.CompareTag("PiratesShip"))
        {
            Debug.Log("Boom!!!");
            _audioSource.Play();
            _animator.SetTrigger("Boom");
            StartCoroutine(DisableAfterDelay(0.9f));
        }
    }
    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _animator.SetTrigger("NoBoom");
        gameObject.SetActive(false);
    }
}

