using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFlag_DefeatEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> mustDefeatEnemys = new List<GameObject>();
    private int mustDefeatEnemyCount = 0;

    public static ClearFlag_DefeatEnemy Instance { get; set; }

    private void Awake()
    {
        this.mustDefeatEnemyCount = this.mustDefeatEnemys.Count;

        if(ClearFlag_DefeatEnemy.Instance == null)
        {
            ClearFlag_DefeatEnemy.Instance = this;
        }
        else
        {
            throw new System.Exception();
        }
    }

    public void CheckDefeatedEnemy(GameObject defeatedEnemy)
    {
        foreach(GameObject mustDefeatEnemy in this.mustDefeatEnemys)
        {
            if(defeatedEnemy.GetInstanceID() == mustDefeatEnemy.GetInstanceID())
            {
                Debug.Log("A");
                this.mustDefeatEnemyCount--;
                if(this.mustDefeatEnemyCount <= 0)
                {
                    StageManager.Instance.StageClear();
                }
                return;
            }
        }
    }
}
