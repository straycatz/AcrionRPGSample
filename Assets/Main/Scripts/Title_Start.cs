using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Start : BaseBehaviour
{

    [SerializeField] Button Button;

	// Use this for initialization
	void Start () {
        Button.OnClickAsObservable().Subscribe(_ => SceneManager.LoadScene("Main"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
