using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject[] earlyStageMasicCircles;
    [SerializeField] private GameObject[] finalStageMasicCircles;
    [SerializeField] private Transform[] masicCirclePositions;
    [SerializeField] private float instantiateMasicCircleInterval;
    [SerializeField] private int changeAttackHp = 50;

    private int earlyStageMasicCirclesCount;
    private int finalStageMasicCirclesCount;
    private int masicCirclePositionsCount;
    private float countTime = 0f;

    private bool isEarlyStage = true;

    private ColliderFunctionOfHaveHpObject haveHpObject;

    private void Awake()
    {
        this.earlyStageMasicCirclesCount = this.earlyStageMasicCircles.Length;
        this.finalStageMasicCirclesCount = this.finalStageMasicCircles.Length;
        this.masicCirclePositionsCount = this.masicCirclePositions.Length;
        this.haveHpObject = GetComponent<ColliderFunctionOfHaveHpObject>();
    }

    private void Update()
    {
        if (StageManager.Instance.IsStop) return;
        this.countTime += Time.deltaTime;
        if(this.countTime >= this.instantiateMasicCircleInterval)
        {
            this.countTime = 0f;
            GenerateMasicCircle();
            if (this.isEarlyStage && haveHpObject.Hp <= this.changeAttackHp)
            {
                this.isEarlyStage = false;
                BGMManager.PlayBGM(BGMManager.bossBGM1);
            }
        }
    }

    public void GenerateMasicCircle()
    {
        if (this.isEarlyStage)
        {
            Instantiate(this.earlyStageMasicCircles[UnityEngine.Random.Range(0, this.earlyStageMasicCirclesCount)], this.masicCirclePositions[UnityEngine.Random.Range(0, this.masicCirclePositionsCount)].position, Quaternion.identity);
        }
        else
        {
            Instantiate(this.finalStageMasicCircles[UnityEngine.Random.Range(0, this.finalStageMasicCirclesCount)], this.masicCirclePositions[UnityEngine.Random.Range(0, this.masicCirclePositionsCount)].position, Quaternion.identity);
        }
    }



}
