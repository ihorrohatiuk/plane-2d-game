using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Border"))
        {
            Destroy(this.gameObject);
        }

        else if (collision.tag.Equals("Player"))
        {
            Debug.Log("Collision with player!");
            Destroy(_player.gameObject);
        }
    }
}
