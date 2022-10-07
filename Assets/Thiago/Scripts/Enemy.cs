using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    public GameObject player;
    [SerializeField] float speed;
    public AnimationClip clip;
    private GameObject enemyHand;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyHand = GameObject.FindWithTag("EnemyHand");
        agent.speed = speed;
        enemyHand.SetActive(false);
        //  player = GameObject.FindWithTag("PPP");
    }

    void Update()
    {
        agent.destination = player.transform.position;
        anim.SetBool("Walk", true);

        if(Vector3.Distance(transform.position, player.transform.position) < 2.5f)
        {
            anim.SetBool("Attack", true);
            agent.speed = 0.0f;
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        enemyHand.SetActive(true);
        yield return new WaitForSeconds(clip.length);
        anim.SetBool("Attack", false);
        
        agent.speed = speed;
    }
}
