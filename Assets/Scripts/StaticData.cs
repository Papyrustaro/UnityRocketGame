using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの名前(ゲーム開始時に記載)
    /// </summary>
    public static string playerName = "test";

    /// <summary>
    /// 現在遊んでいるステージのscene名(ランキングデータ保存に使用)
    /// </summary>
    public static string playingStageSceneName;
}
