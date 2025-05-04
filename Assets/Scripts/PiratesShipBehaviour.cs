using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PiratesShipBehaviour : MonoBehaviour
{
    [SerializeField] private float _startMovingSpeed = 3;
    public bool IsMovingFromEndScreen = true;
    private Vector3 _targetMovingPosition;
    private Vector3 _targetHidingPosition;

    [SerializeField] private float _amplitude = 3f;  
    [SerializeField] private float _frequency = 1f;
    private Vector3 _movingBasePosition;

    public int Health;

    private bool _isPlayerDestroyPirate = false;

    void Start()
    {
        _targetMovingPosition = new Vector3(5.5f, 0, 0);
        _targetHidingPosition = new Vector3(10f, 0, 0);
        Health = 5;
    }

    void Update()
    {
        MovingFromEndScreen();
        if (_isPlayerDestroyPirate)
        {
            HidePiratesShip();
        }
        else if (!IsMovingFromEndScreen)
        {
            MovingUpAndDown();
        }
    }

    private void MovingFromEndScreen()
    {
        if (IsMovingFromEndScreen)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetMovingPosition,
                _startMovingSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, _targetMovingPosition) < 0.01f)
            {
                Debug.Log("IsMovingFromEndScreen = false;");
                IsMovingFromEndScreen = false;
                _movingBasePosition = transform.localPosition;
            }
        }
    }

    private void MovingUpAndDown()
    {
        //Debug.Log("The ship is moving Up and Down.");
        float yOffset = Mathf.Sin(Time.time * _frequency) * _amplitude;

        transform.localPosition = new Vector3(_movingBasePosition.x, _movingBasePosition.y + yOffset,
            _movingBasePosition.z);
    }

    private void HidePiratesShip()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetHidingPosition,
            _startMovingSpeed * 2 * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, _targetHidingPosition) < 0.01f)
        {
            Debug.Log("HIDING");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerTorpedo"))
        {
            Debug.Log("Player fire the ship");
            

            if (Health > 0)
            {
                Health--;
                if (Health == 0)
                {
                    _isPlayerDestroyPirate = true;
                }
            }
        }
    }
}
