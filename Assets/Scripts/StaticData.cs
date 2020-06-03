using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの名前(ゲーム開始時に記載)
    /// </summary>
    public static string playerName = "player0";

    /// <summary>
    /// 現在遊んでいるステージのscene名(ランキングデータ保存に使用)
    /// </summary>
    public static string playingStageSceneName;

    /// <summary>
    /// missionFailedのときは、こちらにランキングデータを格納し、次回以降のmissionFailed時はこちらを参照。keyはミッション名(Mission0など)。valueは上位10名のデータ
    /// </summary>
    public static Dictionary<string, ResultDataNameAndTime> highRankResults = new Dictionary<string, ResultDataNameAndTime>();

    /// <summary>
    /// missionFailedのときは、こちらにランキングデータを格納し、次回以降のmissionFailed時はこちらを参照。keyはミッション名(Mission0など)。valueは上位10名のデータ
    /// </summary>
    public static Dictionary<string, ResultDataNameAndDate> recentResults = new Dictionary<string, ResultDataNameAndDate>();

    /// <summary>
    /// プレイヤーの回転速度
    /// </summary>
    public static float rotationSpeed = 300f;
}

public class ResultDataNameAndTime
{
    private string playerNameText;
    private string resultTimeText;

    public string PlayerNameText => this.playerNameText;
    public string ResultTimeText => this.resultTimeText;

    public ResultDataNameAndTime(string playerNameText, string resultTimeText)
    {
        this.playerNameText = playerNameText; this.resultTimeText = resultTimeText;
    }
}

public class ResultDataNameAndDate
{
    private string playerNameText;
    private string clearDateText;

    public string PlayerNameText => this.playerNameText;
    public string ClearDateText => this.clearDateText;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rank">日時の新しさ順位(1位は1)</param>
    /// <param name="playerName">プレイヤー名</param>
    /// <param name="clearDate">クリアした日付う</param>
    public ResultDataNameAndDate(string playerNameText, string clearDateText)
    {
        this.playerNameText = playerNameText; this.clearDateText = clearDateText;
    }
}
