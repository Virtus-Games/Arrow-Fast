Shader "Unlit/PostProcessGrayScale"
{
    Properties
    {
        [HideInInspector]
        _MainTex("MainTex",2D) = "white"{}

       
         _GrayScale("Grayscale",float) = 0
    }
    SubShader
    {
        

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
             

            #include "UnityCG.cginc"


            float _GrayScale;
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

            sampler2D _MainTex;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
               
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                 fixed4 renderTex = tex2D(_MainTex,i.uv);
                 if(_GrayScale == 1)
                 {
                   fixed3 grayscale = (renderTex.r + renderTex.g + renderTex.b)/4;
                    return fixed4( grayscale,1);
                 }
                 return renderTex;
                
            }
            ENDCG
        }
    }
}
