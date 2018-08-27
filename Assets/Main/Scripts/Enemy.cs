using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
//public class Enemy : BaseBehaviour {
public sealed class Enemy : BaseBehaviour{

    [SerializeField] GameObject target;
    NavMeshAgent agent;
    Transform targetTransform;
    Transform thisTransform;
    private Vector3 pPos;
    private Vector3 ePos;

    void Start()
    {
        (agent = GetComponent<NavMeshAgent>()).speed = 0f;
        thisTransform = transform;
        targetTransform = target.transform;

        // 当たり判定
        this.OnCollisionEnterAsObservable().Where(col => col.gameObject.tag == "Fire").ThrottleFirst(TimeSpan.FromMilliseconds(500)).First().Subscribe(_ => Destroy(gameObject));
        
        // プレイヤーが近づいたら動きだす
        this.UpdateAsObservable().Select(_ => Vector3.Distance(targetTransform.position, thisTransform.position) < 10).DistinctUntilChanged().Where(_ => _).Subscribe(_ => agent.speed = 2f);
        this.UpdateAsObservable().Subscribe(_ => agent.destination = thisTransform.position);
    }

    //// Use this for initialization
    //void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    agent.speed = 0;
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    // プレイヤーとの距離を算出
    //    pPos = target.transform.position;
    //    ePos = transform.position;
    //    var dist = Vector3.Distance(pPos, ePos);

    //    // 一定距離に近づいたら動き出す
    //    if (dist < 10)
    //    {
    //        agent.speed = 2;
    //    }

    //    // 敵オブジェクトの目的地を指定
    //    agent.destination = pPos;

    //}

    //private void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.tag == "Fire")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
}
