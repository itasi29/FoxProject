using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : MonoBehaviour
{
    // �A�j���[�V����.
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"{name}");
    }

    private void FixedUpdate()
    {
        // ����������I�u�W�F�N�g������.
        if (transform.position.y < -20.0f || transform.position.z < -50.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.name != "WindSpace") return;

        // ���ɓ�����ƃA�j���[�V�������I��.
        _animator.enabled = false;
    }
}
