using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimDirector : MonoBehaviour
{
    // フェードキャンバスの取得.
    [SerializeField] private Fade _fade;

    public bool _isFade = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_isFade)
        {
            _fade.FadeIn(1.0f);
        }
        else
        {
            _fade.FadeOut(1.0f);
        }
    }
}
