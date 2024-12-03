Shader "Sprites/Pixel Snap/URP-2D-Lit-Custom"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {} // �ؽ�ó
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "LightMode"="Universal2D" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // URP Core ���� (Lighting2D.hlsl ����)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // �ؽ�ó �Ӽ�
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            half4 _MainTex_TexelSize; // �ؽ�ó ũ��� �ؼ� ũ��

            // ���� ����ü
            struct Attributes
            {
                float4 positionOS : POSITION; // ������Ʈ ���� ��ǥ
                float2 uv : TEXCOORD0;       // UV ��ǥ
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION; // Ŭ�� ���� ��ǥ
                float2 uv : TEXCOORD0;          // UV ��ǥ
            };

            // ���� ���̴�
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                // Ŭ�� �������� ��ȯ
                OUT.positionCS = TransformObjectToHClip(IN.positionOS);

                // UV ��ǥ ����
                OUT.uv = IN.uv;

                return OUT;
            }

            // �����׸�Ʈ ���̴�
            half4 frag(Varyings IN) : SV_Target
            {
                // �ؼ� ũ�� ���
                float2 texelSize = _MainTex_TexelSize;

                // UV ��ǥ ���� (�ؼ� �߽����� �̵�)
                float2 correctedUV = IN.uv + texelSize * 0.5;

                // �ؽ�ó ���ø�
                half4 baseColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, correctedUV);

                // 2D ���� ����� URP�� �ڵ� ó���ϹǷ� �ؽ�ó ���� ��ȯ
                return baseColor;
            }
            ENDHLSL
        }
    }
}
