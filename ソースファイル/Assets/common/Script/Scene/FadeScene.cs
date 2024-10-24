// 画面のフェードインアウトの処理

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{
    public Color _color;
    // フェードインアウトの真偽.
    public bool _isFadeIn;
    public bool _isFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        _isFadeIn = true;
        _isFadeOut = false;
        _color = GetComponent<Image>().color;
        _color.r = 0.0f;
        _color.g = 0.0f;
        _color.b = 0.0f;
        _color.a = 1.0f;
        gameObject.GetComponent<Image>().color = _color;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        FadeUpdate();
    }

    // フェード処理.
    private void FadeUpdate()
    {
        if(_color.a >= 1.0f)
        {
            _isFadeIn = true;
        }

        // 透明度を固定化.
        if (_color.a <= 0.0f)
        {
            _color.a = 0.0f;
            _isFadeIn = false;
        }

        // フェードイン.
        if (_isFadeIn)
        {
            _color.a -= 0.01f;
            gameObject.GetComponent<Image>().color = _color;
        }
        // フェードアウト.
        else if (_isFadeOut)
        {
            _color.a += 0.01f;
            gameObject.GetComponent<Image>().color = _color;
        }
    }

    public float GetAlphColor() { return _color.a; }
}
