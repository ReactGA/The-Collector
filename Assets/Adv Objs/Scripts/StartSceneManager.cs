using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public void LoadPub()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
