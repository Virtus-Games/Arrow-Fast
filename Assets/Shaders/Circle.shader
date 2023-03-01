Shader "Unlit/Circle"
{
    Properties
    {
        _FirstColor("First Color",Color) =(1,1,1,1)
        _SecondColor("Second Color",Color) =(1,1,1,1)
        _RepeatColorChange("Repeat Color Change",Integer) = 1
     }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           

            #include "UnityCG.cginc"

            float4 _FirstColor;
            float4 _SecondColor;
            int _RepeatColorChange;

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
                o.uv = v.uv*_RepeatColorChange ;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
               float V = i.uv.y;

               if(frac(V) < 0.5)
               return fixed4(_FirstColor.rgb,1);

               else
               return fixed4(_SecondColor.rgb,1);
                
              
             }
            ENDCG
        }
    }
}
