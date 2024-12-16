Shader "Sprites/Pixel Snap/URP-Alpha-Blended"
{
    Properties
    {
        // 텍스처 속성 정의
        _MainTex ("Main Texture", 2D) = "white" {} // 스프라이트 텍스처
    }

    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent" // 투명 렌더링 태그
            "Queue"="Transparent"     // 투명 큐에 배치
        }

        Blend SrcAlpha OneMinusSrcAlpha // 알파 블렌딩 설정
        ZWrite Off                     // Z 버퍼 쓰기 비활성화

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert             // Vertex 함수
            #pragma fragment frag           // Fragment 함수
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // 텍스처 샘플러 선언
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            // 텍스처의 텍셀 크기를 나타내는 속성 (자동 설정됨)
            half4 _MainTex_TexelSize;

            // 정점 데이터 구조체 (Vertex 셰이더 입력)
            struct Attributes
            {
                float4 positionOS : POSITION; // 객체 공간에서의 정점 위치
                float2 uv : TEXCOORD0;       // 기본 UV 좌표
            };

            // Vertex 셰이더의 출력 및 Fragment 셰이더의 입력 구조체
            struct Varyings
            {
                float4 positionCS : SV_POSITION; // 클립 공간 정점 위치
                float2 uv : TEXCOORD0;          // 텍스처 UV 좌표
            };

            // Vertex 셰이더: 입력 데이터를 클립 공간으로 변환
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                // 객체 공간 정점 위치를 클립 공간으로 변환
                OUT.positionCS = TransformObjectToHClip(IN.positionOS);

                // 텍스처 UV 좌표를 그대로 전달
                OUT.uv = IN.uv;

                return OUT;
            }

            // Fragment 셰이더: 텍스처 색상 샘플링 및 출력
            half4 frag(Varyings IN) : SV_Target
            {
                // 텍셀 크기 (텍스처 해상도에 따라 자동 계산됨)
                float2 texelSize = _MainTex_TexelSize.xy;

                // 텍셀 중심으로 UV 보정
                float2 correctedUV = IN.uv + texelSize * 0.5;

                // 텍스처 샘플링
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, correctedUV);

                // 최종 색상을 반환
                return color;
            }
            ENDHLSL
        }
    }
}
