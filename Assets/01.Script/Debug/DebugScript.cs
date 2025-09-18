using UnityEngine;
using UnityEngine.UIElements;

public class DebugScript : MonoBehaviour
{
    [SerializeField] private Sprite _magicIcon;

    private bool _isShow = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(_isShow == false)
            {
                UIManager.Instance.ShowSelectPanel();
                _isShow = true;
            }
            else
            {
                UIManager.Instance.CloseSelectPanel();
                _isShow = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            UIManager.Instance.XpApply(1);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            UIManager.Instance.GetMagic(_magicIcon);
        }    
    }
}
