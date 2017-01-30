Shader "Unlit/GradientSin"{
	Properties
	{
		_TopColor ("Top Color", Color) = (1,1,1,1)
		_MidColor ("Middle Color", Color) = (0.5,0.5,0.5,1.0)
		_BottomColor ("Bottom Color", Color) = (0,0,0,1)
		_Rotate ("Rotation", Range(0,1)) = 0.0
	}
	SubShader
	{
	    Tags {"Queue"="Background"  "IgnoreProjector"="True"}

		LOD 100

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
				float4 screenPos: TEXCOORD1;

			};

			float4 _TopColor;
			float4 _MidColor;
			float4 _BottomColor;
			float _Rotate;
						
			v2f vert (appdata v)
			{
				v2f o;
			//	o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				return o;
			}

			float3 evaluateBezierPosition( float3 v1, float3 v2, float3 v3, float t )
			{
			    float3 p;
			    float OneMinusT = 1.0 - t;
			    float b0 = OneMinusT*OneMinusT*OneMinusT;
			    float b1 = 3.0*t*OneMinusT*OneMinusT;
			    float b2 = 3.0*t*t*OneMinusT;
			    float b3 = t*t*t;
			    return b0*v1 + b1*v2 + b2*v2 + b3*v3;
			}
 

			fixed4 frag (v2f i) : SV_Target
			{
				
				//get the screen coordinate
				float2 screenUV = i.screenPos.xy / i.screenPos.w;

				screenUV = (screenUV*1.414213562373095)-float2(0.7071067811865475244,0.7071067811865475244);

				//convert to radial coordinates
				float2 RadUV = float2(0.,0.);    
				RadUV.x = ((atan2(screenUV.x,screenUV.y) * 0.31830988618379067153776752674503) * 0.5) + .5; //angle
   			 	RadUV.y = length(screenUV); //distance to center

   			 	//each color should take up 60° of the wheel
   			 	float rad = fmod(RadUV.x + _Rotate,1.0) * 6.283185307179586476;
   			 	float col1Amt = 0.5*(sin(rad+ 2.094395102393195492)+1.);
   			 	float col2Amt = 0.5*(sin(rad+4.18879020478639098461)+1.);
   			 	float col3Amt = 0.5*(sin(rad)+1.);



   			 	float3 col = col1Amt*_TopColor.rgb+col2Amt*_MidColor.rgb+col3Amt*_BottomColor.rgb;
   			 	return fixed4(col,1.);
				//float3 col = float3(RadUV.y,RadUV.x,1);
			}
			ENDCG
		}
	}
}
