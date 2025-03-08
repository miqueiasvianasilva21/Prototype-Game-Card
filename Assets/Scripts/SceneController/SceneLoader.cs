using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    public void LoadDeckScene()
    {
        SceneManager.LoadScene("DeckScene"); 
    }

    
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene"); 
    }

   
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene"); 
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void LoadVictoryScene()
    {
        SceneManager.LoadScene("Victory");
    }
    public void LoadHelpScene()
    {
        SceneManager.LoadScene("HelpScene");
    }
}
