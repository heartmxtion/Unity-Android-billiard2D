using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pockets : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Main ball").Length == 0)
        {
            SceneManager.LoadScene(0);
        }
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
