using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] GameObject[] upperBisectWalls;
    [SerializeField] float[] bisectWallHorzPositions;
    [SerializeField] float[] upperBisectWallPositions;
    [SerializeField] GameObject[] lowerBisectWalls;
    [SerializeField] float[] lowerBisectWallPositions;
    [SerializeField] Vector2 bisectSpeedRange;
    [SerializeField] GameObject[] sweepers;
    [SerializeField] float[] sweeperPositionMultipliers;
    [SerializeField] Vector2 sweeperSpeedRange;
    [SerializeField] GameObject[] growingObsticles;
    [SerializeField] float growingObsticleMax;
    [SerializeField] Vector2 growingObsticleSpeedRange;
    [SerializeField] GameObject[] spawnPositions;

    public enum ArenaStates
    {
        None,
        Growth,
        Bisect,
        Sweep
    }

    public void setState(ArenaStates states, float difficulty)
    {
        switch(states)
        {
            case ArenaStates.None:
                basicState(difficulty);
                break; 
            case ArenaStates.Growth:
                growthState(difficulty);
                break;
            case ArenaStates.Bisect:
                bisectState(difficulty);
                break;
            case ArenaStates.Sweep:
                sweepState(difficulty);
                break;
            default:
                Debug.LogError("Broken Arena");
                break;
        }
    }

    void basicState(float difficulty)
    {
        foreach(GameObject obsticle in upperBisectWalls)
        {
            obsticle.GetComponent<ArenaMove>().ResetObsticle(bisectSpeedRange[0] + (bisectSpeedRange[1] - bisectSpeedRange[0]) * (difficulty / 4f));
        }
        foreach (GameObject obsticle in lowerBisectWalls)
        {
            obsticle.GetComponent<ArenaMove>().ResetObsticle(bisectSpeedRange[0] + (bisectSpeedRange[1] - bisectSpeedRange[0]) * (difficulty / 4f));
        }
        foreach (GameObject obsticle in sweepers)
        {
            obsticle.GetComponent<ArenaMove>().ResetObsticle(sweeperSpeedRange[0] + (sweeperSpeedRange[1] - sweeperSpeedRange[0]) * (difficulty / 4f));
        }
        foreach (GameObject obsticle in growingObsticles)
        {
            obsticle.GetComponent<ArenaMove>().ResetObsticle(growingObsticleSpeedRange[0] + (growingObsticleSpeedRange[1] - growingObsticleSpeedRange[0]) * (difficulty / 4f));
        }
    }
    
    void growthState(float difficulty)
    {
        float speed = growingObsticleSpeedRange[0] + (growingObsticleSpeedRange[1] - growingObsticleSpeedRange[0]) * (difficulty / 4f);
        switch (Random.Range(0,3))
        {
            case 0:
                growingObsticles[0].GetComponent<ArenaMove>().setScale(Vector2.one * Random.Range(growingObsticleMax / 3f, growingObsticleMax), speed);
                break;
            case 1:
                growingObsticles[1].GetComponent<ArenaMove>().setScale(Vector2.one * Random.Range(growingObsticleMax / 3f, growingObsticleMax), speed);
                break;
            case 2:
                growingObsticles[0].GetComponent<ArenaMove>().setScale(Vector2.one * Random.Range(growingObsticleMax / 3f, growingObsticleMax), speed);
                growingObsticles[1].GetComponent<ArenaMove>().setScale(Vector2.one * Random.Range(growingObsticleMax / 3f, growingObsticleMax), speed);
                break;
            default:
                  break;
        }
    }

    void bisectState(float difficulty)
    {
        float speed = bisectSpeedRange[0] + (bisectSpeedRange[1] - bisectSpeedRange[0]) * (difficulty / 4f);
        for (int i = 0; i < 3; i++)
        {
            switch(Random.Range(0,4))
            {
                case 0:
                    Debug.Log("A");
                    break;
                case 1: // low
                    Debug.Log("B");
                    if (Random.Range(0,2) == 0)
                    {
                        lowerBisectWalls[i].GetComponent<ArenaMove>().move(new Vector2(bisectWallHorzPositions[i], lowerBisectWallPositions[2]), speed);
                    }
                    else
                    {
                        upperBisectWalls[i].GetComponent<ArenaMove>().move(new Vector2(bisectWallHorzPositions[i], upperBisectWallPositions[2]), speed);
                    }
                    break;
                case 2: //mid
                    Debug.Log("C");
                    if (Random.Range(0, 2) == 0)
                    {
                        lowerBisectWalls[i].GetComponent<ArenaMove>().move(new Vector2(bisectWallHorzPositions[i], lowerBisectWallPositions[1]), speed);
                    }
                    else
                    {
                        upperBisectWalls[i].GetComponent<ArenaMove>().move(new Vector2(bisectWallHorzPositions[i], upperBisectWallPositions[1]), speed);
                    }
                    break;
                case 3://high
                    Debug.Log("D");
                    lowerBisectWalls[i].GetComponent<ArenaMove>().move(new Vector2(bisectWallHorzPositions[i], lowerBisectWallPositions[1]), speed);
                    upperBisectWalls[i].GetComponent<ArenaMove>().move(new Vector2(bisectWallHorzPositions[i], upperBisectWallPositions[1]), speed);
                    break;
                default:
                    break;
            }
        }
    }
    void sweepState(float difficulty)
    {
        float speed = sweeperSpeedRange[0] + (sweeperSpeedRange[1] - sweeperSpeedRange[0]) * (difficulty / 4f);
        switch (Random.Range(0, 3))
        {
            case 0:
                sweepers[0].GetComponent<ArenaMove>().move(new Vector2(-sweeperPositionMultipliers[Random.Range(1, 3)],0), speed);
                break;
            case 1:
                sweepers[1].GetComponent<ArenaMove>().move(new Vector2(sweeperPositionMultipliers[Random.Range(1, 3)], 0), speed);
                break;
            case 2:
                sweepers[0].GetComponent<ArenaMove>().move(new Vector2(-sweeperPositionMultipliers[Random.Range(0, 2)], 0), speed);
                sweepers[1].GetComponent<ArenaMove>().move(new Vector2(sweeperPositionMultipliers[Random.Range(0, 2)], 0), speed);
                break;
            default:
                break;
        }
    }

    public Vector2 getSpawnPos()
    {
        return ((Vector2)spawnPositions[Random.Range(0, spawnPositions.Length)].transform.position);
    }
}
