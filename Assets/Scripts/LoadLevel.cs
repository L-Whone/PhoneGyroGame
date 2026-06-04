using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] string _levelToLoad;
    public void DoLoadLevel()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
}
