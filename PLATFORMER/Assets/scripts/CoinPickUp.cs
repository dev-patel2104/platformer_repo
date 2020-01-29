using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickSFX;
    [SerializeField] int CoinValue = 100;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(CoinValue);
        AudioSource.PlayClipAtPoint(coinPickSFX, Camera.main.transform.position);
        Destroy(this.gameObject);
    }
}
