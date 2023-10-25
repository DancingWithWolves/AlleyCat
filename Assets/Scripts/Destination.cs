using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public GameManager game_manager;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.GetComponentInChildren<PlayerController>()) {
            game_manager.WinGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
