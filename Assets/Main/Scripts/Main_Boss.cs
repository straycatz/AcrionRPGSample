﻿using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class Main_Boss : BaseBehaviour
{

    NavMeshAgent agent;
    Transform targetTransform;
    Transform thisTransform;

    void Start()
    {
        (agent = GetComponent<NavMeshAgent>()).speed = 0f;
        thisTransform = transform;
        targetTransform = GameObject.Find("Player").transform;

        // 当たり判定
        this.OnTriggerStayAsObservable()
        .Where(col => col.gameObject.tag == "Fire")
        .Subscribe(_ => SceneManager.LoadScene("Result"));

        // プレイヤーが近づいたら動きだす
        this.UpdateAsObservable().Select(_ => Vector3.Distance(targetTransform.position, thisTransform.position) < 10).DistinctUntilChanged().Where(_ => _).Subscribe(_ => agent.speed = 2f);
        this.UpdateAsObservable().Subscribe(_ => agent.destination = targetTransform.position);
    }

}