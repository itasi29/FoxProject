Shader "Custom/Test"
{
    CGINCLUDE

#define PI 3.14159265

    /// <summary>
    /// �t�����[�֐�
    /// </summary>
    /// p:uv
    /// n:�g�̐�
    /// radius:���a
    /// angle:��]���x
    /// waveAmp:�g�̐[��
    float flower(float2 p, float n, float radius, float angle, float waveAmp)
    {
        float theta = atan2(p.y, p.x);
        float d = length(p) - radius + sin(theta * n + angle) * waveAmp;
        float b = 0.01 / abs(d);
        return b;
    }

    float4 paint(float2 uv) 
    {
        // ���Ԍo�߂ŐF�ω�
        /*float t = _Time.y;
        float3 col = float3 (
            sin(t*2),
            sin(t*1.2+1.3),
            sin(-t*2.1-2));
        col = col * 0.5 + 0.5;
        return float4(col,1);*/

        // UV���W
        //return float4(uv, 0, 1);


        // ���W�̌��_�𒆐S�_�Ɏ����Ă���
        //float2 p = uv * 2. - 1.;
        //// �~�̔��a
        //float d = length(p) - .5;
        //// ��Βl
        //float b = 0.01 / abs(d);
        //return b;

        float rad1 = 0.8;
        float rad2 = 0.5;
        float rad3 = 0.2;


        // �g
        float2 p = uv * 2. - 1.;
        float3 col = 0;
        col += flower(p, 6.0, rad1, _Time.y * 10.5, 0.1) * float3(0.0, 0.5, 1.0);
        col += flower(p, 6.0, rad2, _Time.y * -8.5, 0.05) * float3(0.0, 0.5, 1.0);
        col += flower(p, 6.0, rad3, _Time.y * 5.5, 0.01) * float3(0.0, 0.5, 1.0);
        return float4(col, 1);

    }

    ENDCG

    SubShader
    {
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // �\���̂̒�`
            struct appdata // vert�֐��̓���
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct fin
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            fin vert(appdata v)// �\���̂��g�p�������o��
            {
                fin o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            float4 frag(fin FIN) : SV_TARGET// �\����fin���g�p��������
            {
                return paint(FIN.texcoord.xy);
            }

            ENDCG
        }
    }
}
