Shader "Custom/Outline Shader" 
 {
      Properties 
      {
          _MainTex ("Base (RGB)", 2D) = "white" {}
          _OutLineSpreadX ("Outline Spread", Range(0,0.05)) = 0.007
          _OutLineSpreadY ("Outline Spread", Range(0,0.05)) = 0.007
          _Color("Outline Color", Color) = (1.0,1.0,1.0,1.0)
      }
   
      SubShader
   
      {
          Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
          ZWrite On Blend One OneMinusSrcAlpha Cull Off
          LOD 110
   
          CGPROGRAM
          #pragma surface surf Lambert alpha
   
          struct Input 
          {
              float2 uv_MainTex;
              fixed4 color : COLOR;
          };
   
          sampler2D _MainTex;
          float _OutLineSpreadX;
          float _OutLineSpreadY;
          float4 _Color;
   
          void surf(Input IN, inout SurfaceOutput o)
          {
              fixed4 mainColor = (tex2D(_MainTex, IN.uv_MainTex+float2(_OutLineSpreadX,_OutLineSpreadY)) + tex2D(_MainTex, IN.uv_MainTex-float2(_OutLineSpreadX,_OutLineSpreadY))) * _Color.rgba;
              fixed4 addcolor = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
   
              if(addcolor.a > 0.95){
              mainColor = addcolor;}
   
              o.Albedo = mainColor.rgb;
              o.Alpha = mainColor.a;
          }
          ENDCG       
      }
   
      SubShader 
      {
         Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
          ZWrite Off Blend One OneMinusSrcAlpha Cull Off Fog { Mode Off }
          LOD 100
          Pass {
              Tags {"LightMode" = "Vertex"}
              ColorMaterial AmbientAndDiffuse
              Lighting off
              SetTexture [_MainTex] 
              {
                  Combine texture * primary double, texture * primary
              }
          }
      }
      Fallback "Diffuse", 1
  }