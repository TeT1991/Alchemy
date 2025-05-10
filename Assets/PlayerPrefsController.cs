using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsController : MonoBehaviour
{
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
