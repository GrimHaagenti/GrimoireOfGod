using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ReticleHandler : MonoBehaviour
{
    

    [SerializeField] GameObject ReticlePrefab;

    private List<GameObject> reticleList = new List<GameObject>();
    private List<Animator> reticleAnimatorList = new List<Animator>();

    private GameObject selectedReticle;
    private int selectedR_index = 0;

    private WeaponReach_Enum displayedReach;
    
    public int GetCurrentEnemyIndex { get { return selectedR_index; } }

    public void InitReticles(List<New_Entity_Script> enemies)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject newRet = Instantiate(ReticlePrefab, transform);
            reticleAnimatorList.Add(newRet.GetComponent<Animator>());
            reticleAnimatorList[i].Play("reticle_idle");
            reticleList.Add(newRet);
            newRet.SetActive(false);
        }
    }

    public void SelectEnemyUI(WeaponReach_Enum reach)
    {

        switch (reach)
        {
            case WeaponReach_Enum.ONE:
                selectedR_index = 0;
                for (int i = 0; i < reticleList.Count; i++)
                {
                   
                        reticleList[i].SetActive(true);
                    if (New_BattleManager._BATTLE_MANAGER.enemies[i].CurrentHP <= 0)
                    {
                        selectedR_index++;
                    }
                }

                selectedReticle = reticleList[selectedR_index];
                reticleAnimatorList[selectedR_index].Play("reticle_selected");
                break;
            case WeaponReach_Enum.ALL:
                for (int i = 0; i < reticleList.Count; i++)
                {
                        reticleList[i].SetActive(true);
                        reticleAnimatorList[i].Play("reticle_selected");
                }
                break;
            case WeaponReach_Enum.NONE_REACH:
                break;
        }
        displayedReach = reach;

    }

    public void ShowReticles(bool show)
    {
        for (int i = 0; i < reticleList.Count; i++)
        {
            reticleList[i].SetActive(show);
        }
    }

    public void HighlightReticle(Directions_Enum dir)
    {
        int newIndex = -1;

        switch (displayedReach)
        {
            case WeaponReach_Enum.ONE:
                switch (dir)
                {
                    case Directions_Enum.UP:
                        break;
                    case Directions_Enum.DOWN:
                        break;
                    case Directions_Enum.LEFT:
                        if (New_BattleManager._BATTLE_MANAGER.enemies[Mathf.Clamp(selectedR_index - 1, 0, reticleList.Count-1)].CurrentHP > 0)
                        {
                            if (Mathf.Clamp(selectedR_index - 1, 0, reticleList.Count - 1) != selectedR_index)
                            {
                                reticleAnimatorList[selectedR_index].Play("reticle_idle");
                            }
                            newIndex = Mathf.Clamp(selectedR_index - 1, 0, reticleList.Count - 1);

                            selectedReticle = reticleList[newIndex];
                            selectedR_index = newIndex;

                            reticleAnimatorList[selectedR_index].Play("reticle_selected");
                        }
                        break;
                    case Directions_Enum.RIGHT:
                        if (New_BattleManager._BATTLE_MANAGER.enemies[Mathf.Clamp(selectedR_index + 1, 0, reticleList.Count - 1)].CurrentHP > 0)
                        {
                            if (Mathf.Clamp(selectedR_index + 1, 0, reticleList.Count - 1) != selectedR_index)
                            {
                                reticleAnimatorList[selectedR_index].Play("reticle_idle");
                            }
                            newIndex = Mathf.Clamp(selectedR_index + 1, 0, reticleList.Count - 1);

                            selectedReticle = reticleList[newIndex];
                            selectedR_index = newIndex;

                            reticleAnimatorList[selectedR_index].Play("reticle_selected");
                        }
                        break;
                    case Directions_Enum.NO_DIR:
                        break;
                }

                break;
            case WeaponReach_Enum.ALL:
                break;
            case WeaponReach_Enum.NONE_REACH:
                break;
        }

        


    }


}
