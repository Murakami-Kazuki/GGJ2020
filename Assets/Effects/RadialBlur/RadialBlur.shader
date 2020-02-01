Shader "Custom/RadialBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Samples("Samples", Range(4, 32)) = 16
        _Strength("Strength", float) = 1
        _CenterX("Center X", float) = 0.5
        _CenterY("Center Y", float) = 0.5
        _UnaffectedRadius("Unaffected Radius", float) = 0.1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
             
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
             
            sampler2D _MainTex;
            float _Samples;
            float _Strength;
            float _CenterX;
            float _CenterY;
            float _UnaffectedRadius;
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = fixed4(0,0,0,0);
                float2 dist = i.uv - float2(_CenterX, _CenterY);
                for(int j = 0; j < _Samples; j++) {
                    float scale = 1 - _Strength * (j / _Samples)* (saturate(length(dist) / _UnaffectedRadius));
                    col += tex2D(_MainTex, dist * scale + float2(_CenterX, _CenterY));
                }
                col /= _Samples;
                return col;
            }
            ENDCG
        }
    }
}
