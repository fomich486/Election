using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    [SerializeField]
    List<Level> levelBasics;
    private LevelDificulty _currentDificulty;
    
    private float groundLength = 216.2902f;

    Vector3 nextInstantiatePosition = Vector3.zero;
    float nextSpawnTriggerX;
    private void Awake()
    {
        _currentDificulty = LevelDificulty.AClass;
        Spawn();
    }

    private void Update()
    {
        if (GameController.Instance.myHead.PositionX() > nextSpawnTriggerX)
            Spawn(); 

    }

    private void Spawn()
    {
        InstantiateNewLevelDependOnDifficulty();
        CalculateNextSpawnPosition();
        ChangeDifficulty();
    }

    private void InstantiateNewLevelDependOnDifficulty()
    {
        if (_currentDificulty != LevelDificulty.All)
        {
            List<Level> selectedLevels = new List<Level>();
            foreach (Level lvl in levelBasics)
            {
                if (lvl.Dificulty == _currentDificulty)
                    selectedLevels.Add(lvl);
            }
            int number = Random.Range(0, selectedLevels.Count);
            Instantiate(selectedLevels[number].gameObject, nextInstantiatePosition, Quaternion.identity);
        }
        else
        {
            int number = Random.Range(0, levelBasics.Count);
            Instantiate(levelBasics[number].gameObject, nextInstantiatePosition, Quaternion.identity);
        }
    }

    private void CalculateNextSpawnPosition()
    {
        nextInstantiatePosition += Vector3.right * groundLength;
        nextSpawnTriggerX = nextInstantiatePosition.x - groundLength;
    }

    private void ChangeDifficulty()
    {
        switch (_currentDificulty)
        {
            case LevelDificulty.AClass:
                _currentDificulty = LevelDificulty.BClass;
                break;

            case LevelDificulty.BClass:
                _currentDificulty = LevelDificulty.CClass;
                break;

            case LevelDificulty.CClass:
                _currentDificulty = LevelDificulty.All;
                break;
        }
    }
}
