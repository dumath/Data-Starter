using System;

[Serializable]
public class PlayerData
{
    public string Nickname;
    public int Result;

    public static bool operator< (PlayerData lhs, PlayerData rhs)
    {
        if (lhs.Result < rhs.Result)
            return true;
        else if (lhs.Result > rhs.Result)
            return false;
        else
            return false;
    }

    public static bool operator> (PlayerData lhs, PlayerData rhs)
    {
        if (lhs.Result > rhs.Result)
            return true;
        else if (lhs.Result < rhs.Result)
            return false;
        else
            return false;
    }
}
