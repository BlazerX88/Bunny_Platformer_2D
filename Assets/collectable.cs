  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectable : MonoBehaviour
{
    bool isCollected = false;

    void HideCoin()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if(sprite != null)
        {
            sprite.enabled = false;
        }
        var collider = GetComponent<CircleCollider2D>();
        if(sprite != null)
        {
            sprite.enabled = false;
        }
    }
    void CollectCoin()
    {
        isCollected = true;
        HideCoin();
        GameManager gm = GameManager.GetInstance();
        gm.CollectCoins();
        print("coins collected = " + gm.GetCollectedCoins());
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            CollectCoin();
        }
    }
}
