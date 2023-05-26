using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_BattleCollider : MonoBehaviour
{
    [SerializeField] List<GameObject> PossibleEnemies;
    [SerializeField] int MaxEnemiesInEncounter = 2;
    [SerializeField] float BattleProbability = 1f;
    float CoolDownTime = 1f;

    bool fightTriggered = false;
    private CharacterController playerRef = null;

    private void Start()
    {
        GameManager._GAME_MANAGER.ReturningToWorld.AddListener(BattleCooldownTimerStart);
    }

    public void BattleCooldownTimerStart()
    {
        StartCoroutine(CooldownTimer());
    }

    IEnumerator CooldownTimer()
    {
        float time = 0;
        float timer = CoolDownTime;

        while (time < timer)
        {
            time += Time.deltaTime;
            yield return null;
        }
        fightTriggered = false;
        StopCoroutine(CooldownTimer());
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.layer == 3)
        {
            if(other.gameObject.TryGetComponent<CharacterController>(out CharacterController chara))
            {
                playerRef = chara;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!fightTriggered)
        {
            if (other.gameObject == playerRef.gameObject)
            {
                if (playerRef.velocity != Vector3.zero)
                {
                    Debug.Log("Can Trigger Battle");
                    CalculateIfBattle();
                }

            }
        }

    }


    private bool CalculateBattleProbability()
    {
        bool doBattle = false;
        float battleProb = Random.Range(0f, 100f);

        if (battleProb < BattleProbability)
        {
            doBattle = true;
            fightTriggered = true;
        }

        return doBattle;
    }
    private void CalculateIfBattle()
    {
        if (CalculateBattleProbability())
        {
            int encounterNum = Random.Range(0, MaxEnemiesInEncounter + 1);

            List<GameObject> BattleEnemies = new List<GameObject>();

            for (int i = 0; i < MaxEnemiesInEncounter; i++)
            {
                int index = Random.Range(0, PossibleEnemies.Count);
                BattleEnemies.Add(PossibleEnemies[index]);

            }
            
            GameManager._GAME_MANAGER.BeginLoadBattleScene(BattleEnemies);
            
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == playerRef.gameObject)
        {
            playerRef = null;
        }
    }


}