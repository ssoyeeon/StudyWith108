// Portal 시스템의 외곽선을 그리기 위한 URP 쉐이더
Shader "Portals/Outline"
{
   Properties
   {
       // 외곽선 색상을 설정하는 프로퍼티, Inspector에서 조절 가능
       _OutlineColour("Outline Colour", Color) = (1, 1, 1, 1)
       
       // 스텐실 버퍼에서 사용할 마스크 ID
       _MaskID("Mask ID", Int) = 1
   }
   
   SubShader
   {
       // 렌더링 설정
       Tags 
       { 
           "RenderType" = "Opaque"                    // 불투명 렌더링
           "Queue" = "Geometry+2"                     // 일반 지오메트리 후에 렌더링
           "RenderPipeline" = "UniversalPipeline"     // URP 파이프라인 사용
       }
       
       // URP 필수 include
       HLSLINCLUDE
           #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
       ENDHLSL
       
       // 스텐실 버퍼 설정 - 포털 내부와 외곽선이 겹치지 않도록 함
       Stencil
       {
           Ref 0        // 참조값
           Comp equal   // 같을 때만 렌더링
       }
       
       Pass
       {
           HLSLPROGRAM
           #pragma vertex vert
           #pragma fragment frag
           
           // 버텍스 쉐이더 입력 데이터 구조체
           struct appdata
           {
               float4 vertex : POSITION;    // 정점 위치
           };

           // 버텍스→프래그먼트 쉐이더 전달 데이터 구조체
           struct v2f
           {
               float4 vertex : SV_POSITION;    // 클립 공간의 정점 위치
           };

           // 외곽선 색상
           uniform float4 _OutlineColour;

           // 버텍스 쉐이더: 객체 공간 → 클립 공간 변환
           v2f vert (appdata v)
           {
               v2f o;
               o.vertex = TransformObjectToHClip(v.vertex.xyz);
               return o;
           }

           // 프래그먼트 쉐이더: 단순히 지정된 외곽선 색상 반환
           float4 frag (v2f i) : SV_Target
           {
               return _OutlineColour;
           }
           ENDHLSL
       }
   }
}