Shader "Unlit/ScoreSlots"
{
    Properties
    {
        _RedChannel("Red Channel",float)= 0
        _GreenChannel("Green Channel",float)= 1
        _AlphaChannel("Alpha Channel",float) = 1
        _CenterOfUV("Center",float)=(1,1,1,1)
        _Radius("Radius",float) =1
        _CircleColor("CircleColor",Color)=(1,1,1,1)
      }
    SubShader
    {
         Tags { "RenderType"="Transparent" "Queue" ="Transparent"}
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
         Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
          

            #include "UnityCG.cginc"

            float _RedChannel;
            float _GreenChannel;
            float _AlphaChannel;
            float4 _CenterOfUV;
            float4 _CircleColor;
            float _Radius;
             struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;    
                float4 objPos:TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

         

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                 o.uv = v.uv;
                 o.objPos = v.vertex;
                 
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv = (i.uv - _CenterOfUV.xy)*2;
                float inCircle = step(length(i.uv),_Radius);
                 fixed3 col = fixed3(_RedChannel,_GreenChannel,0);

                if(i.objPos.z >0.49)
                {
                    
               fixed4 finalCol = inCircle ==0 ? fixed4(col,_AlphaChannel): fixed4(_CircleColor.rgb,_AlphaChannel);
               return finalCol;
                }
               
                return fixed4(col,_AlphaChannel);
            }
            ENDCG
        }
    }
}
