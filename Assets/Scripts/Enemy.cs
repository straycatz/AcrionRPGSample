using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseBehaviour {

    public GameObject target;
    NavMeshAgent agent;
    private Vector3 pPos;
    private Vector3 ePos;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 0;
	}
	
	// Update is called once per frame
	void Update () {

        // プレイヤーとの距離を算出
        pPos = target.transform.position;
        ePos = transform.position;
        var dist = Vector3.Distance(pPos, ePos);

        // 一定距離に近づいたら動き出す
        if (dist < 10)
        {
            agent.speed = 2;
        }

        // 敵オブジェクトの目的地を指定
        agent.destination = pPos;

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Fire")
        {
            Destroy(this.gameObject);
        }
    }
}
