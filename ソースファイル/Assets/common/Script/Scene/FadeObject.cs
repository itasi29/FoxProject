// マテリアルをフェードアウトする処理.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeObject : MonoBehaviour
{
    // マテリアル.
    private Renderer _fadeMaterial;

    private RotateWindmill _rotateWindmill;

    // フェードする速度.
    private float _fadeSpeed = 0.01f;
    // _fadeMaterialの色.
    private float _r, _g, _b, _a;

    // true :フェードアウト開始.
    // false:フェードアウト停止.
    [SerializeField] private bool _isFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        _rotateWindmill = GameObject.Find("windmill:windmill:polySurface132").GetComponent<RotateWindmill>();

        _fadeMaterial = GetComponent<Renderer>();
        _r = _fadeMaterial.material.color.r;
        _g = _fadeMaterial.material.color.g;
        _b = _fadeMaterial.material.color.b;
        _a = _fadeMaterial.material.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //_isFadeOut = RotateWindmill._instance.GetWindActive();

        _isFadeOut = _rotateWindmill.GetWindActive();

        if ( _isFadeOut )
        {
            StartFadeOut();
        }
    }

    private void StartFadeOut()
    {
        // 透明度を下げる.
        _a -= _fadeSpeed;
        SetMaterialColor();
        if(_a <= 0)
        {
            _a = 0;
            // 描画をoff.
            _fadeMaterial.enabled = false;
        }
    }

    private void SetMaterialColor()
    {
        _fadeMaterial.material.color = new Color(_r, _g, _b, _a);
    }
}
