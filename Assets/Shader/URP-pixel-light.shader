Shader "Sprites/Pixel Snap/URP-2D-Lit-Custom"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {} // 텍스처
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

            // URP Core 포함 (Lighting2D.hlsl 제외)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // 텍스처 속성
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            half4 _MainTex_TexelSize; // 텍스처 크기와 텍셀 크기

            // 정점 구조체
            struct Attributes
            {
                float4 positionOS : POSITION; // 오브젝트 공간 좌표
                float2 uv : TEXCOORD0;       // UV 좌표
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION; // 클립 공간 좌표
                float2 uv : TEXCOORD0;          // UV 좌표
            };

            // 정점 셰이더
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                // 클립 공간으로 변환
                OUT.positionCS = TransformObjectToHClip(IN.positionOS);

                // UV 좌표 전달
                OUT.uv = IN.uv;

                return OUT;
            }

            // 프래그먼트 셰이더
            half4 frag(Varyings IN) : SV_Target
            {
                // 텍셀 크기 계산
                float2 texelSize = _MainTex_TexelSize;

                // UV 좌표 보정 (텍셀 중심으로 이동)
                float2 correctedUV = IN.uv + texelSize * 0.5;

                // 텍스처 샘플링
                half4 baseColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, correctedUV);

                // 2D 조명 계산은 URP가 자동 처리하므로 텍스처 색상만 반환
                return baseColor;
            }
            ENDHLSL
        }
    }
}
