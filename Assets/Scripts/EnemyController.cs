using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float x_speed = 0f;
    public GameManager game_manager;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(x_speed * Time.fixedDeltaTime, 0);
    }

    void OnBecameInvisible()
    {
        Object.Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.GetComponentInChildren<PlayerController>()) {
            game_manager.EndGame();
        }
    }
}
