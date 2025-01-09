Shader "Custom/RGBShiftShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Amount ("Shift Amount", Float) = 0.005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Amount;

            fixed4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;
                
                // Apply RGB shift by offsetting each color channel
                float2 redUV = uv + float2(_Amount, _Amount);
                float2 greenUV = uv + float2(-_Amount, _Amount);
                float2 blueUV = uv + float2(_Amount, -_Amount);
                
                float red = tex2D(_MainTex, redUV).r;
                float green = tex2D(_MainTex, greenUV).g;
                float blue = tex2D(_MainTex, blueUV).b;
                
                return fixed4(red, green, blue, 1.0);
            }
            ENDCG
        }
    }
}
