/*プレイヤーアニメーション*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // アニメーター
    private Animator _animator;

    // アニメーションを再生するかどうか.
    public bool _run;// 走る.
    public bool _jump;// 飛ぶ.
    public bool _push;// 押す.
    public bool _isDead;// やられる.
    public bool _waveHands;// 手を振る.

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("Run", _run);
        _animator.SetBool("Jump", _jump);
        _animator.SetBool("Push", _push);
        _animator.SetBool("isDead", _isDead);
        _animator.SetBool("WaveHands", _waveHands);
    }
}
