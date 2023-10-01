using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeSurvived = 0;
    public int difficulty;
    [SerializeField] GameObject player;
    [SerializeField] GameObject arena;
    bool running = false;
    [SerializeField] Vector2 arenaShiftTimeRange;
    [SerializeField] float arenaShiftTimeVariance;
    [SerializeField] float areanShiftTimer;
    [SerializeField] Vector2 enemySpawnTimeRange;
    [SerializeField] float enemySpawnTimeVariance;
    [SerializeField] float enemySpawnTimer;
    bool reset = true;
    [SerializeField] GameObject grunt;
    [SerializeField] GameObject sharpShooter;
    [SerializeField] GameObject rusher;
    [SerializeField] GameObject defender;
    [SerializeField] LayerMask onDeathMask;
    public Weapon left;
    public Weapon right;
    public SpellTrinket a;
    public SpellTrinket b;
    public Boots boot;
    public Helmet helm;
    public void startGame()
    {
        Inventory inv = player.GetComponent<Inventory>();
        inv.leftWeapon = left;
        inv.rightWeapon = right;
        inv.trinketOne = a;
        inv.trinketTwo = b;
        inv.helmet = helm;
        inv.boots = boot;
        player.SetActive(true);
        player.GetComponent<Rigidbody2D>().position = Vector3.zero;
        arena.SetActive(true);
        running = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<UIControls>().Die();
        }
        if (running)
        {
            areanShiftTimer -= Time.deltaTime;
            enemySpawnTimer -= Time.deltaTime;
            timeSurvived += Time.deltaTime;
            if(areanShiftTimer <= 0)
            {
                areanShiftTimer += arenaShiftTimeRange[0] + (arenaShiftTimeRange[1] - arenaShiftTimeRange[0]) * (difficulty / 4f) + Random.Range(-arenaShiftTimeVariance,arenaShiftTimeVariance);
                if(reset == true)
                {
                    reset = false;
                    arena.GetComponent<ArenaManager>().setState(ArenaManager.ArenaStates.None, difficulty);
                }
                else
                {
                    reset = true;
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            arena.GetComponent<ArenaManager>().setState(ArenaManager.ArenaStates.Sweep, difficulty);
                            break;
                        case 1:
                            arena.GetComponent<ArenaManager>().setState(ArenaManager.ArenaStates.Growth, difficulty);
                            break;
                        case 2:
                            arena.GetComponent<ArenaManager>().setState(ArenaManager.ArenaStates.Bisect, difficulty);
                            break;
                        default:
                            break;
                    }
                }
            }
            if(enemySpawnTimer <= 0)
            {
                enemySpawnTimer += enemySpawnTimeRange[0] + (enemySpawnTimeRange[1] - enemySpawnTimeRange[0]) * (difficulty / 4f) + Random.Range(-enemySpawnTimeVariance, enemySpawnTimeVariance);
                int roll = Random.Range(0, 7);
                Vector2 position = arena.GetComponent<ArenaManager>().getSpawnPos();
                if(roll == 0)
                {
                    GameObject enemy = Instantiate(sharpShooter);
                    enemy.transform.position = position;
                }
                else if (roll == 1)
                {
                    GameObject enemy = Instantiate(rusher);
                    enemy.transform.position = position;
                }
                else if (roll == 2)
                {
                    GameObject enemy = Instantiate(defender);
                    enemy.transform.position = position;
                }
                else
                {
                    GameObject enemy = Instantiate(grunt);
                    enemy.transform.position = position;
                }
            }

        }
    }
    public void onDeath()
    {
        running = false;
        player.SetActive(false);
        arena.SetActive(false);
        Collider2D[] hits = Physics2D.OverlapCircleAll(Vector2.zero, 1000f, onDeathMask);
        for (int i = 0; i < hits.Length; i++)
        {
            Destroy(hits[i].gameObject);
        }
    }

}
