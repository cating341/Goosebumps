Shader "Custom/glow" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader {
        Tags { "Queue"="Transparent" }
        LOD 200
        ZTest Always
        Cull Off
        Blend One One
 
        CGPROGRAM
        #pragma surface surf Lambert
 
        float4 _Color;
 
        struct Input {
            float3 viewDir;
            float3 worldNormal;
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
            o.Alpha = _Color.a * pow(abs(dot(normalize(IN.viewDir),
                normalize(IN.worldNormal))),4.0);
            o.Emission = _Color.rgb * o.Alpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
