using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Main_AnimationBoss : BaseBehaviour {

	// Use this for initialization
	void Start () {
        var timer = Observable.Timer(System.TimeSpan.FromSeconds(1));
        timer.Subscribe(_ => Destroy(gameObject));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
