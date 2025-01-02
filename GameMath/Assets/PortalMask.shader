Shader "Portals/PortalMask"
{
   Properties
   {
       // ���п� ǥ�õ� �⺻ �ؽ�ó
       _MainTex("Main Texture", 2D) = "white" {}
   }
   SubShader
   {
       Tags 
       { 
           "RenderType" = "Opaque"          // ������ ������
           "Queue" = "Geometry"             // �⺻ ������Ʈ�� ť
           "RenderPipeline" = "UniversalPipeline"  // URP ���
       }
       
       // URP �ھ� ���̺귯�� ����
       HLSLINCLUDE
           #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
       ENDHLSL

       Pass
       {
           Name "Mask"
           
           // ���ٽ� ���� ����
           Stencil
           {
               Ref 1           // ������ 1
               Pass replace    // ���ٽǰ� ��ü
           }

           HLSLPROGRAM
               #pragma vertex vert
               #pragma fragment frag

               // ���ؽ� ���̴� �Է� ����ü
               struct appdata
               {
                   float4 vertex : POSITION;    // ���� ��ġ
               };

               // ���ؽ��������׸�Ʈ ���̴� ���� ����ü
               struct v2f
               {
                   float4 vertex : SV_POSITION;     // Ŭ�� ���� ��ġ
                   float4 screenPos : TEXCOORD0;    // ��ũ�� ��ǥ
               };

               // ���ؽ� ���̴�
               v2f vert(appdata v)
               {
                   v2f o;
                   o.vertex = TransformObjectToHClip(v.vertex.xyz);    // ��ü��Ŭ�� ���� ��ȯ
                   o.screenPos = ComputeScreenPos(o.vertex);           // ��ũ�� ��ǥ ���
                   return o;
               }

               // ���� �ؽ�ó
               uniform sampler2D _MainTex;

               // �����׸�Ʈ ���̴�
               float4 frag(v2f i) : SV_Target
               {
                   float2 uv = i.screenPos.xy / i.screenPos.w;    // UV ��ǥ ���
                   float4 col = tex2D(_MainTex, uv);              // �ؽ�ó ���ø�
                   return col;
               }
           ENDHLSL
       }
   }
}