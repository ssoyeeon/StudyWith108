// Portal �ý����� �ܰ����� �׸��� ���� URP ���̴�
Shader "Portals/Outline"
{
   Properties
   {
       // �ܰ��� ������ �����ϴ� ������Ƽ, Inspector���� ���� ����
       _OutlineColour("Outline Colour", Color) = (1, 1, 1, 1)
       
       // ���ٽ� ���ۿ��� ����� ����ũ ID
       _MaskID("Mask ID", Int) = 1
   }
   
   SubShader
   {
       // ������ ����
       Tags 
       { 
           "RenderType" = "Opaque"                    // ������ ������
           "Queue" = "Geometry+2"                     // �Ϲ� ������Ʈ�� �Ŀ� ������
           "RenderPipeline" = "UniversalPipeline"     // URP ���������� ���
       }
       
       // URP �ʼ� include
       HLSLINCLUDE
           #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
       ENDHLSL
       
       // ���ٽ� ���� ���� - ���� ���ο� �ܰ����� ��ġ�� �ʵ��� ��
       Stencil
       {
           Ref 0        // ������
           Comp equal   // ���� ���� ������
       }
       
       Pass
       {
           HLSLPROGRAM
           #pragma vertex vert
           #pragma fragment frag
           
           // ���ؽ� ���̴� �Է� ������ ����ü
           struct appdata
           {
               float4 vertex : POSITION;    // ���� ��ġ
           };

           // ���ؽ��������׸�Ʈ ���̴� ���� ������ ����ü
           struct v2f
           {
               float4 vertex : SV_POSITION;    // Ŭ�� ������ ���� ��ġ
           };

           // �ܰ��� ����
           uniform float4 _OutlineColour;

           // ���ؽ� ���̴�: ��ü ���� �� Ŭ�� ���� ��ȯ
           v2f vert (appdata v)
           {
               v2f o;
               o.vertex = TransformObjectToHClip(v.vertex.xyz);
               return o;
           }

           // �����׸�Ʈ ���̴�: �ܼ��� ������ �ܰ��� ���� ��ȯ
           float4 frag (v2f i) : SV_Target
           {
               return _OutlineColour;
           }
           ENDHLSL
       }
   }
}