using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderEnabler : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D collider_;
    // Start is called before the first frame update
    void Start()
    {
        collider_ = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        collider_.enabled = (player.transform.position.y > transform.position.y + collider_.size.y);
    }
}
