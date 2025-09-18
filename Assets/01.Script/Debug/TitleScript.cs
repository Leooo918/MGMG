using UnityEngine;
using UnityEngine.SceneManagement;

namespace MGMG.Core
{
    public class TitleScript : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void EnterGame()
        {
            SceneManager.LoadScene(_sceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }
}
