using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public void LoadThisScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}