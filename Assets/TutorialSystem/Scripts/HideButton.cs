using UnityEngine;
using UnityEngine.UI;

public class HideButton : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(Hide);
    }

    private void Hide()
    {
        button.gameObject.SetActive(false);
        Invoke("Show", 1f);
    }
}