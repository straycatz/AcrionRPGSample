using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;
using System;

public class Main_Master : BaseBehaviour
{

    [SerializeField] GameObject preAnimetion;
    [SerializeField] GameObject preBoss;

    // Use this for initialization
    void Start () {
        this.UpdateAsObservable()
        .Where(_ => GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        .First()
        .Subscribe(_ => ShowBoss());
    }
	
    private void ShowBoss()
    {
        // アニメーション表示
        var timer = Observable.Timer(System.TimeSpan.FromSeconds(1));
        timer.Subscribe(_ => Instantiate(preAnimetion, new Vector3(0f, 0f, 0f), transform.rotation));

        // ボスオブジェクト生成
        timer = Observable.Timer(System.TimeSpan.FromSeconds(2));
        timer.Subscribe(_ => Instantiate(preBoss, new Vector3(0f, -4, 0f), new Quaternion(0f, 180f, 0f, 0f)));
    }
 
    // Update is called once per frame
    void Update () {
        
    }

    private void OnGUI()
    {
        var enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
        GUI.Label(new Rect(0f, 20f, Screen.width, Screen.height),  string.Format("敵残数：{0}",enemyUnits.Length.ToString()));
    }
}
