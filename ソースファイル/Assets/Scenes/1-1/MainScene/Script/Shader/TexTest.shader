Shader "Custom/TexTest"
{
    Properties
    {
        _Tex("Tex", 2D) = ""{}
    }

    CGINCLUDE
    sampler2D _Tex;

    float4 paint(float2 uv)
    {
        return tex2D(_Tex, uv);
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

            float4 frag(fin IN) : SV_TARGET// �\����fin���g�p��������
            {
                return paint(IN.texcoord.xy);
            }

            ENDCG
        }
    }
}
