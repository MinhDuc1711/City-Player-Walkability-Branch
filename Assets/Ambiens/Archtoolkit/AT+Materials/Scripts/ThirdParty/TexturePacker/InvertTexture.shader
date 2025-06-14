﻿Shader "Custom/InvertTexture"
{
	Properties{
		_MainTex("Input00", 2D) = "black" {}
	}

		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 100
			
			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 4.6

				#include "UnityCG.cginc"

				struct appdata {
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f {
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;

				fixed4 frag(v2f i) : SV_Target {
					fixed4 col = tex2D(_MainTex, i.uv);
					col.rgb = 1 - col.rgb;
					return col;
				}
				ENDCG
			}
	}
}