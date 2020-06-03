using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPTextManager : MonoBehaviour
{
    [SerializeField] private Text enemyHPText;
    [SerializeField] private ColliderFunctionOfHaveHpObject enemy;
    public Text EnemyHPText => this.enemyHPText;

    public static EnemyHPTextManager Instance { get; set; }

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
        this.enemyHPText.text = "敵HP: " + this.enemy.Hp.ToString();
    }
}
