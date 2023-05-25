using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    #region Data Acess.
    public static DataManager Instance;

    public PlayerData PlayerData { get; set; }
    #endregion

    #region Initialize.
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //PlayerData = new PlayerData();
        }
    }
    #endregion

    #region Manipulation Data.
    /// <summary>
    /// 
    /// </summary>
    public PlayerData LoadData()
    {
        string path = Application.persistentDataPath + "/saveddata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData bestResult = JsonUtility.FromJson<PlayerData>(json);
            return bestResult;
        }
        else
            return null;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SaveData()
    {
        string json = JsonUtility.ToJson(PlayerData);
        File.WriteAllText(Application.persistentDataPath + "/saveddata.json", json);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nickname"></param>
    public void SetData(string nickname)
    {
        if (PlayerData == null)
            PlayerData = new PlayerData() { Nickname = nickname, Result = 0 };
        else
            PlayerData.Nickname = nickname;
    }
    #endregion
}
