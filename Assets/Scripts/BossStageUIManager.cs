using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStageUIManager : MonoBehaviour
{
    [SerializeField] private Text bossHPText;

    [SerializeField] private ColliderFunctionOfHaveHpObject boss;

    public Text BossHPText => this.bossHPText;

    public static BossStageUIManager Instance { get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
    }

    private void Start()
    {
        this.bossHPText.text = "敵HP: " + boss.Hp.ToString();
        if (BGMManager.playingBGM == E_BGM.boss1) BGMManager.PlayBGM(BGMManager.bossBGM0);
    }
}
