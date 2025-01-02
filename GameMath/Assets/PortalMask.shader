Shader "Portals/PortalMask"
{
   Properties
   {
       // 포털에 표시될 기본 텍스처
       _MainTex("Main Texture", 2D) = "white" {}
   }
   SubShader
   {
       Tags 
       { 
           "RenderType" = "Opaque"          // 불투명 렌더링
           "Queue" = "Geometry"             // 기본 지오메트리 큐
           "RenderPipeline" = "UniversalPipeline"  // URP 사용
       }
       
       // URP 코어 라이브러리 포함
       HLSLINCLUDE
           #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
       ENDHLSL

       Pass
       {
           Name "Mask"
           
           // 스텐실 버퍼 설정
           Stencil
           {
               Ref 1           // 참조값 1
               Pass replace    // 스텐실값 교체
           }

           HLSLPROGRAM
               #pragma vertex vert
               #pragma fragment frag

               // 버텍스 쉐이더 입력 구조체
               struct appdata
               {
                   float4 vertex : POSITION;    // 정점 위치
               };

               // 버텍스→프래그먼트 쉐이더 전달 구조체
               struct v2f
               {
                   float4 vertex : SV_POSITION;     // 클립 공간 위치
                   float4 screenPos : TEXCOORD0;    // 스크린 좌표
               };

               // 버텍스 쉐이더
               v2f vert(appdata v)
               {
                   v2f o;
                   o.vertex = TransformObjectToHClip(v.vertex.xyz);    // 객체→클립 공간 변환
                   o.screenPos = ComputeScreenPos(o.vertex);           // 스크린 좌표 계산
                   return o;
               }

               // 메인 텍스처
               uniform sampler2D _MainTex;

               // 프래그먼트 쉐이더
               float4 frag(v2f i) : SV_Target
               {
                   float2 uv = i.screenPos.xy / i.screenPos.w;    // UV 좌표 계산
                   float4 col = tex2D(_MainTex, uv);              // 텍스처 샘플링
                   return col;
               }
           ENDHLSL
       }
   }
}