Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0.0
        _EdgeWidth ("Edge Width", Range(0,0.1)) = 0.02
        _EdgeColor ("Edge Color", Color) = (1,0.5,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NoiseTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _DissolveAmount;
        float _EdgeWidth;
        fixed4 _EdgeColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Sample noise texture for dissolve pattern
            float noise = tex2D(_NoiseTex, IN.uv_NoiseTex).r;
            
            // Calculate dissolve cutoff
            float cutoff = _DissolveAmount;
            
            // Clip pixels based on noise and dissolve amount
            clip(noise - cutoff);
            
            // Calculate edge glow
            float edge = 1 - smoothstep(cutoff, cutoff + _EdgeWidth, noise);
            
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            
            // Mix base color with edge color
            o.Albedo = lerp(c.rgb, _EdgeColor.rgb, edge * _EdgeColor.a);
            
            // Add emission for glowing edge effect
            o.Emission = _EdgeColor.rgb * edge * _EdgeColor.a;
            
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
