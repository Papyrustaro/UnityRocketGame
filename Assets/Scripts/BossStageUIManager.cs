using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStageUIManager : MonoBehaviour
{
    [SerializeField] private Text bossHPText;
    [SerializeField] private Text playerHPText;

    [SerializeField] private ColliderFunctionOfHaveHpObject boss;
    [SerializeField] private ColliderFunctionOfHaveHpObject player;

    public Text BossHPText => this.bossHPText;
    public Text PlayerHPText => this.playerHPText;

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
        this.bossHPText.text = "ボスHP: " + boss.Hp.ToString();
        this.playerHPText.text = "味方HP: " + player.Hp.ToString();
    }
}
