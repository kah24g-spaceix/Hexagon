Shader "Universal Render Pipeline/2D/Sprite-Lit-Default-Test"
{
    Properties
    {
        // 주 텍스처와 추가적인 텍스처 속성
        _MainTex("Diffuse", 2D) = "white" {}        // 기본 스프라이트 텍스처
        _MaskTex("Mask", 2D) = "white" {}          // 라이트 마스크 텍스처
        _NormalMap("Normal Map", 2D) = "bump" {}   // 노멀 맵 텍스처

        // Unity 레거시 속성
        [HideInInspector] _Color("Tint", Color) = (1,1,1,1)  // 색상 틴트
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1) // 렌더러 색상
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)   // 좌우/상하 플립 설정
        [HideInInspector] _AlphaTex("External Alpha", 2D) = "white" {} // 외부 알파 텍스처
        [HideInInspector] _EnableExternalAlpha("Enable External Alpha", Float) = 0 // 외부 알파 활성화 여부
    }

    SubShader
    {
        Tags 
        {
            "Queue" = "Transparent" // 투명 렌더링
            "RenderType" = "Transparent" // URP에서 투명 렌더링 타입
            "RenderPipeline" = "UniversalPipeline" // Universal Render Pipeline 전용
        }

        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha // 투명도 기반 블렌딩
        Cull Off                     // 백페이스 컬링 비활성화
        ZWrite Off                   // 깊이 버퍼 쓰기 비활성화

        // 조명 계산 Pass
        Pass
        {
            Tags { "LightMode" = "Universal2D" } // URP 2D 조명용 태그

            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // 정점 및 프래그먼트 셰이더 정의
            #pragma vertex CombinedShapeLightVertex
            #pragma fragment CombinedShapeLightFragment

            // 라이트 유형 멀티 컴파일 옵션
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_0 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_1 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_2 __
            #pragma multi_compile USE_SHAPE_LIGHT_TYPE_3 __
            #pragma multi_compile _ DEBUG_DISPLAY

            // 입력 데이터 구조체
            struct Attributes
            {
                float3 positionOS   : POSITION; // 객체 공간 정점 좌표
                float4 color        : COLOR;    // 정점 색상
                float2  uv          : TEXCOORD0; // 텍스처 좌표
                UNITY_VERTEX_INPUT_INSTANCE_ID // 인스턴싱 ID
            };

            // Vertex 출력 및 Fragment 입력 데이터 구조체
            struct Varyings
            {
                float4  positionCS  : SV_POSITION; // 클립 공간 좌표
                half4   color       : COLOR;       // 계산된 정점 색상
                float2  uv          : TEXCOORD0;  // 텍스처 UV 좌표
                half2   lightingUV  : TEXCOORD1;  // 화면 공간 좌표
                #if defined(DEBUG_DISPLAY)
                float3  positionWS  : TEXCOORD2;  // 월드 공간 좌표 (디버깅용)
                #endif
                UNITY_VERTEX_OUTPUT_STEREO
            };

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"

            // 텍스처와 색상 속성
            TEXTURE2D(_MainTex); // 기본 텍스처
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_MaskTex); // 마스크 텍스처
            SAMPLER(sampler_MaskTex);
            half4 _MainTex_ST;   // 텍스처 변환 정보
            float4 _Color;       // 색상 틴트
            half4 _RendererColor; // 렌더러 색상

            // 다양한 라이트 유형 정의
            #if USE_SHAPE_LIGHT_TYPE_0
            SHAPE_LIGHT(0) // 라이트 타입 0
            #endif

            #if USE_SHAPE_LIGHT_TYPE_1
            SHAPE_LIGHT(1) // 라이트 타입 1
            #endif

            #if USE_SHAPE_LIGHT_TYPE_2
            SHAPE_LIGHT(2) // 라이트 타입 2
            #endif

            #if USE_SHAPE_LIGHT_TYPE_3
            SHAPE_LIGHT(3) // 라이트 타입 3
            #endif

            // Vertex 셰이더
            Varyings CombinedShapeLightVertex(Attributes v)
            {
                Varyings o = (Varyings)0;
                UNITY_SETUP_INSTANCE_ID(v); // 인스턴싱 데이터 설정
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); // 스테레오 렌더링 지원

                // 객체 공간 좌표를 클립 공간으로 변환
                o.positionCS = TransformObjectToHClip(v.positionOS);

                #if defined(DEBUG_DISPLAY)
                o.positionWS = TransformObjectToWorld(v.positionOS); // 월드 공간 좌표 계산
                #endif

                // 텍스처 UV 좌표 변환
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // 화면 공간 좌표 계산 (조명 샘플링에 사용)
                o.lightingUV = half2(ComputeScreenPos(o.positionCS / o.positionCS.w).xy);

                // 최종 정점 색상 계산
                o.color = v.color * _Color * _RendererColor;
                return o;
            }

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

            // Fragment 셰이더
            half4 CombinedShapeLightFragment(Varyings i) : SV_Target
            {
                // 텍스처 샘플링 및 색상 조합
                const half4 main = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                const half4 mask = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv);

                SurfaceData2D surfaceData; // 표면 데이터
                InputData2D inputData;    // 입력 데이터

                // 표면 및 입력 데이터 초기화
                InitializeSurfaceData(main.rgb, main.a, mask, surfaceData);
                InitializeInputData(i.uv, i.lightingUV, inputData);

                // 라이트와 텍스처 데이터 결합
                return CombinedShapeLightShared(surfaceData, inputData);
            }
            ENDHLSL
        }

        // 노멀 맵 렌더링 패스 (NormalsRendering)
        Pass
        {
            // Vertex 및 Fragment 셰이더로 노멀 맵 처리
            #pragma vertex NormalsRenderingVertex
            #pragma fragment NormalsRenderingFragment
            // ...

            // 추가 설명 생략 (위 코드와 동일한 구조로 정리)
        }

        // 기본 렌더링 패스 (UniversalForward)
        Pass
        {
            // Vertex 및 Fragment 셰이더로 기본 텍스처 처리
            #pragma vertex UnlitVertex
            #pragma fragment UnlitFragment
            // ...

            // 추가 설명 생략 (위 코드와 동일한 구조로 정리)
        }
    }

    Fallback "Sprites/Default" // 기본 스프라이트 셰이더로 폴백
}
