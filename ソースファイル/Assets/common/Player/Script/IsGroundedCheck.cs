/*着地判定*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundedCheck : MonoBehaviour
{
    // 足から地面までのRayの長さ.
    //[SerializeField] private float _rayLength = 1.0f;

    [Header("身体にめり込ませるRayの長さ")]
    [SerializeField] private float _rayOffset;

    [Header("円のRayの長さ")]
    [SerializeField] private float _raySphereLength = 0.1f;

    [Header("円のy座標調整")]
    [SerializeField] private float _SphereCastRegulationY = 0;

    // Rayの判定に用いるLayer.
    //[SerializeField] private LayerMask _layerMask = default;

    // SphereCastの中心座標
    private Vector3 _SphereCastCenterPosition = Vector3.zero;

    // 地面に接地しているかどうか.
    public bool _isGround;

    // レイ
    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _SphereCastCenterPosition = new Vector3(transform.position.x, transform.position.y + _SphereCastRegulationY, transform.position.z);

        _isGround = CheckGrounded();
    }

    private bool CheckGrounded()
    {
        // Rayの初期位置と姿勢.
        Ray ray = new(origin: transform.position + Vector3.up * _rayOffset, direction: Vector3.down);

        // 円キャスト.
        Physics.SphereCast(_SphereCastCenterPosition, _raySphereLength, -transform.up, out hit);

        // 接地距離によってtrue.
        if (hit.distance <= 0.2f && hit.distance != 0.0f)
        {
            return true;
        }

        // Rayが接地するかどうか.
        // 円
        return false;
        // 線
        //return Physics.Raycast(ray, _rayLength, _layerMask);

        //return Physics.SphereCast(transform.position, 0.1f, -transform.up, out hit);
    }

    private void OnDrawGizmos()
    {
        // デバッグ表示.
        // 接地.
        // true 緑.
        // false 赤.
        Gizmos.color = _isGround ? Color.green : Color.red;
        //Gizmos.DrawRay(transform.position + Vector3.up * _rayOffset, Vector3.down * _rayLength);
        //Gizmos.DrawWireSphere(transform.position, 0.1f);
        Gizmos.DrawWireSphere(_SphereCastCenterPosition + -transform.up * (hit.distance), _raySphereLength);
    }
}
