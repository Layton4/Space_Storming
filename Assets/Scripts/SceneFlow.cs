using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    public IEnumerator GoToScene(string sceneName,float timer)
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(sceneName);
    }

    public void GoToNewGame()
    {
        StartCoroutine(GoToScene("NewGame", 1.2f));
    }
    
    public void GoToGame()
    {
        StartCoroutine(GoToScene("Game", 1.2f));
    }


}
