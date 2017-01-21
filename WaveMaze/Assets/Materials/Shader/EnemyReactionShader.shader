Shader "Custom/EnemyReactionShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		
		_Radius ("Radius", Range(0, 1)) = 0.425
		_Thickness ("Thickness", Range(0, 0.5)) = 0.05
		_SmoothingOffset ("Smoothing", Range(0, 0.1)) = 0.06
		
		_Position ("SeedPosition", Vector) = (0.5, 0.5, 0, 0)
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
			half _Thickness;
			half _SmoothingOffset;
			float4 _Position;

			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				// borders o circle, defined by radius and thickness
				float start = _Radius - _Thickness * 0.5; 
				float end =   _Radius + _Thickness * 0.5;

				// offset that is used for smoothing the circle edges
				float offset = _SmoothingOffset;
				// relative center of object (with quad with uv textures going from 0 to 1 this is the center)
				float2 center = float2(_Position.x, _Position.y);

				// distance to center is used to check if we are on circle
				float r = length( i.uv - center );

				// relative position on "s moothed-out borders" - think of smoothed edges as a small ring area
				float startRelative = (r - (start - offset)) / (2 * offset);
				float endRelative = ((end + offset) - r) / (2 * offset);
				// use smoothstep to smooth edges
				float tStart = smoothstep( 0, 1, startRelative );
				float tEnd = smoothstep( 0, 1, endRelative );

				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col = lerp(col, fixed4(1, 0, 0, 1), tStart * tEnd);

				return col;
			}
			ENDCG
		}
	}
}
