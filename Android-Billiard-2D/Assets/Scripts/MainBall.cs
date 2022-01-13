using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBall : MonoBehaviour
{
    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}
