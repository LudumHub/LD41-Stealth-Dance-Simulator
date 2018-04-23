    Shader "Custom/OverlayBlend"
    {
        Properties
        {
            _MainTex ("Texture", 2D) = "" {}
			_Edge ("Edge", Range (0,1.5)) = 0.5
        }
       
        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "RenderType" = "Transparent"
            }
           
            Lighting Off
			Cull Off
            Blend SrcAlpha OneMinusSrcAlpha
           
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
               
                #include "UnityCG.cginc"
               
                struct appdata_custom
                {
                    float4 vertex : POSITION;
                    fixed2 uv : TEXCOORD0;
                    float4 color : COLOR;
                };
               
                struct v2f
                {
                    float4 vertex : POSITION;
                    fixed2 uv : TEXCOORD0;
                    float4 color : COLOR;
                };
               
                sampler2D _MainTex;
                fixed4 _MainTex_ST;
				fixed _Edge;

                v2f vert (appdata_custom v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv,_MainTex);
                    o.color = v.color;
                    return o;
                }          
               
                fixed4 frag (v2f i) : COLOR
                {
                    fixed4 diffuse = tex2D(_MainTex, i.uv);
                    //fixed luminance =  diffuse.r * diffuse.g * diffuse.b;
                    //fixed oldAlpha = diffuse.a;
                   
                    //if (luminance > _Edge)
                    //    diffuse *= i.color;
                    //else
                    //    diffuse += i.color * 0.2f;
                   
                    //diffuse.a  = oldAlpha * i.color.a;
                    return diffuse;
                }
                ENDCG
            }
        }
        Fallback off
    }
