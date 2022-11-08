using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intel : MonoBehaviour
{
    public string nextLevel;

    public void SetNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
