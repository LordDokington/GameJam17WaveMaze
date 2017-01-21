Shader "Custom/DarknessEffectShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_Radius ("Radius", Range(0, 1)) = 0.2
		_Penumbra ("Penumbra", Range(0, 1)) = 0.3

		_DarknessColor("DarknessColor", Color) = (0, 0, 0, 1)

		_ShakeX ("ShakeX", Float) = 0.0
		_ShakeY ("ShakeY", Float) = 0.0

		_PlayerPos ("PlayerPos", Vector) = (0.5, 0.5, 0, 0)
		
		_EnemyPos1 ("EnemyPos1", Vector) = (0.5, 0.5, 0, 0)
		_EnemyRadius ("EnemyRadius", Range(0, 1)) = 0.0
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
			
			half _EnemyRadius;

			half _ShakeX;
			half _ShakeY;
			
			sampler2D _MainTex;
			sampler2D _LightMapTex;

			fixed4 _DarknessColor;

			fixed4 _PlayerPos;
			fixed4 _EnemyPos1;
			
			float LightRing(float radius, float penumbra, fixed2 position, fixed2 uv)
			{
				float aspect = 16.0 / 9.0;
				// relative center of object (with quad with uv textures going from 0 to 1 this is the center)
				float2 center = float2(position.x - (1 - aspect) / 2, position.y);
				
				// distance to center is used to check if we are on circle
				float2 transformedUV = float2( uv.x * aspect, uv.y );
				
				float r = length( transformedUV - center );

				// relative position on "smoothed-out borders" - think of smoothed edges as a small ring area
				float endRelative = (r - radius) / (penumbra);
				// use smoothstep to smooth edge
				float tEnd = smoothstep( 0, 1, endRelative );
				
				return tEnd;
			}

			fixed4 frag (v2f i) : SV_Target
			{		
				float tPlayer = LightRing( _Radius, _Penumbra, _PlayerPos + float2(_ShakeX, 0), i.uv );
				float tEnemy = ( _EnemyRadius == 0.0 ) ? 0.0 : LightRing( _EnemyRadius, 0.1, _EnemyPos1, i.uv );
				
				//float t = 1 - (1- tPlayer) * (1- tEnemy);
				float t =  tPlayer * tEnemy;
				
				fixed4 col = tex2D(_MainTex, i.uv);
				//if( r > 0.05f && col.g < 0.4 ) //return float4(1, 0, 0, 1);
				{
					//col = lerp(col, _DarknessColor, _DarknessColor.a);
				}
				// sample the texture
				//else 
				{	
					col = lerp(col, _DarknessColor, t * _DarknessColor.a);
				}

				return col;
			}
			ENDCG
		}
	}
}
