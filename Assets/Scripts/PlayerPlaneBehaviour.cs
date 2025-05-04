using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlaneBehaviour : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 12;
    private Rigidbody2D _rb;
    private Vector2 _playerDirection;

    [SerializeField] private GameObject _playerTorpedoPrefab;
    [SerializeField] private int _maxTorpedosCount = 5;
    private Queue<GameObject> _torpedoPool;

    [SerializeField] private float _torpedoSpeed = 15;

    private float _nextFireTime = 0f;
    private float _fireRate = 0.5f;

    private bool _isReloading = false;
    [SerializeField] private float _reloadTime = 2f;

    private List<GameObject> _activeTorpedoes;

    [SerializeField] private TextMeshProUGUI _torpedosCountUI;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _torpedoPool = new Queue<GameObject>();
        _activeTorpedoes = new List<GameObject>();

        for (int i = 0; i < _maxTorpedosCount; i++)
        {
            GameObject torpedo = Instantiate(_playerTorpedoPrefab, transform.position + new Vector3(-0.2f, -0.7f, 0), Quaternion.identity, transform);
            torpedo.SetActive(false);
            _torpedoPool.Enqueue(torpedo);
        }
    }

    void Update()
    {
        float directionY = Input.GetAxisRaw("Vertical");
        _playerDirection = new Vector2(0, directionY).normalized;
        
        if (_torpedoPool.Count < 1)
        {
            _torpedosCountUI.text = "Press Fire to reload torpedoes";
        }
        else
        {
            _torpedosCountUI.text = "Torpedoes: " + _torpedoPool.Count;
        }

        if (!_isReloading && Input.GetButton("Fire1") && Time.time > _nextFireTime)
        {
            FireTorpedo();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(0, _playerDirection.y * _playerSpeed);
    }

    private void FireTorpedo()
    {
        if (_torpedoPool.Count > 0)
        {
            GameObject torpedo = _torpedoPool.Dequeue();
            _torpedosCountUI.text = "Torpedoes: " + _torpedoPool.Count;
            torpedo.SetActive(true);
            torpedo.transform.position = transform.position + new Vector3(-0.2f, -0.7f, 0);

            Rigidbody2D torpedoRb = torpedo.GetComponent<Rigidbody2D>();
            if (torpedoRb != null)
            {
                torpedoRb.velocity = new Vector2(_torpedoSpeed, 0);
            }

            _activeTorpedoes.Add(torpedo);
        }
        else
        {
            StartCoroutine(ReloadTorpedoes());
        }
    }

    private IEnumerator ReloadTorpedoes()
    {
        _isReloading = true;
        Debug.Log("Reloading");

        yield return new WaitForSeconds(_reloadTime);

        foreach (var torpedo in _activeTorpedoes)
        {
            torpedo.SetActive(false);
            torpedo.transform.localPosition = new Vector3(-0.2f, -0.7f, 0);
            torpedo.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _torpedoPool.Enqueue(torpedo);
        }

        _activeTorpedoes.Clear();

        Debug.Log("Ready to fire!");
        _torpedosCountUI.text = "Torpedoes: " + _activeTorpedoes.Count;
        _isReloading = false;
    }
}
