using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject dead_button;
    public GameObject dead_text;
    public GameObject win_text;
    public float eps = 1e-100f;
    public GameObject player;
    public GameObject enemy_prefab;
    private GameObject enemy;

    public void ReloadGame()
    {
        // Scene scene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(scene.name);
    }
    void Start()
    {
        dead_button.SetActive(false);
        dead_text.SetActive(false);
        win_text.SetActive(false);
    }

    public void EndGame ()
    {
        Time.timeScale = 0;
        dead_button.SetActive(true);
        dead_text.SetActive(true);
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        win_text.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Random.value < eps && !enemy) {
            Vector3 pos = new Vector3(7.89f, -3.17f, 0);
            enemy = Instantiate(enemy_prefab, pos, Quaternion.identity);
            enemy.GetComponent<EnemyController>().x_speed = -7;
            enemy.GetComponent<EnemyController>().game_manager = this;
        }

    }
}
