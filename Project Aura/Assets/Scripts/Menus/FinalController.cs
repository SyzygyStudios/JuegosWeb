using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalController : MonoBehaviour
{
    [SerializeField] private int world;
    private GameMetrics _gameMetrics;

    void Start()
    {
        _gameMetrics = FindObjectOfType<GameMetrics>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _gameMetrics.UnlockPower(world);
            SceneManager.LoadScene("SelectLevel");
        }
    }
}
