using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseBehaviour
{

    public Vector3 vector;
    public CharacterController chara;
    public GameObject preFire;
    public AudioClip soundFire;
    public AudioClip soundEnemy;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // キーボードでの移動
        vector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * 5;

        // 正面を向く処理
        var forward = Vector3.Slerp(transform.forward, vector, 360 * Time.deltaTime / Vector3.Angle(transform.forward, vector));
        transform.LookAt(transform.position + forward);

        // スペース：ジャンプ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vector.y = 3;
        }
        // F:攻撃
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Fire();
        }

        // ジャンプの場合のために重力分下げる
        vector.y -= 9.8f * Time.deltaTime;

        chara.Move(vector);
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    void Fire()
    {
        var posFire = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f, this.gameObject.transform.position.z);
        Instantiate(preFire, posFire, transform.rotation);
        AudioSource.PlayClipAtPoint(soundFire, transform.position);
    }

    //void OnTriggerEnter(Collider col)
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            StartCoroutine("Damage");
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator Damage()
    {
        // 無敵用のレイヤーに変更
        // 当たり判定を一定間隔で行わせたいため
        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");

        // 判定用に音をならす
        AudioSource.PlayClipAtPoint(soundEnemy, transform.position);

        yield return new WaitForSeconds(0.5f);

        // 元のレイヤーに戻す
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
