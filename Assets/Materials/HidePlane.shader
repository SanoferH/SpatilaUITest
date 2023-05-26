Shader "Custom/HidePlane"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlanePosition ("Plane Position", Vector) = (0,0,0,0)
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Opaque"}

        Pass
        {
            CGPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal@10.4.0/ShaderLibrary/Common.hlsl"
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
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _PlanePosition;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeGrabScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 screenPos = i.screenPos / i.screenPos.w;
                float4 depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenPos.xy));

                if (depth.z > _PlanePosition.z)
                {
                    discard;
                }
                
                fixed4 col = tex2D(_MainTex, i.uv);
                col.a *= depth.a;
                return col;
            }
            ENDCG
        }
    }
}
