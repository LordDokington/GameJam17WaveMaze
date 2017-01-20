Shader "Custom/DarknessEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_Radius ("Radius", Range(0, 1)) = 0.2
		_Penumbra ("Penumbra", Range(0, 1)) = 0.3

		_ShakeX ("ShakeX", Float) = 0.0
		_ShakeY ("ShakeY", Float) = 0.0
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
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			half _Radius;
			half _Penumbra;

			half _ShakeX;
			half _ShakeY;
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				float aspect = 16.0 / 9.0;
				// relative center of object (with quad with uv textures going from 0 to 1 this is the center)
				float2 center = float2(0.5 * aspect + _ShakeX, 0.5 + _ShakeY);

				// distance to center is used to check if we are on circle
				float2 transformedUV = float2( i.uv.x * aspect, i.uv.y );
				
				float r = length( transformedUV - center );

				// relative position on "smoothed-out borders" - think of smoothed edges as a small ring area
				float endRelative = (r - _Radius) / (_Penumbra);
				// use smoothstep to smooth edge
				float tEnd = smoothstep( 0, 1, endRelative );

				// sample the texture
				col = lerp(col, fixed4(0,0,0,1), tEnd);

				return col;

			}
			ENDCG
		}
	}
}
