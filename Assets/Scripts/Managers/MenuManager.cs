using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nickname;

    private void Start() => nickname.onEndEdit.AddListener(DataManager.Instance.SetData);

    /// <summary>
    /// 
    /// </summary>
    public void Play()
    {
        if (DataManager.Instance.PlayerData != null)
            if (DataManager.Instance.PlayerData.Nickname != string.Empty)
                SceneManager.LoadScene(1);
    }
}
