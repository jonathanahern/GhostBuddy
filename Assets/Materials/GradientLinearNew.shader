Shader "Unlit/GradientLinearNew"{
	Properties
	{
		_TopColor ("Top Color", Color) = (1,1,1,1)
		_MidColor ("Middle Color", Color) = (0.5,0.5,0.5,1.0)
		_BottomColor ("Bottom Color", Color) = (0,0,0,1)
		_Rotate ("Rotation", Range(0,1)) = 0.0
		_Width("Sharpness",Range(3.,50.)) = 12
		_MaintainWidth("Constant Width", Range(0,1))=1.0
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
			float _Width;
			float _MaintainWidth;
						
			v2f vert (appdata v)
			{
				v2f o;
			//	o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
					o.screenPos = ComputeScreenPos(o.vertex);
				return o;
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
   			 	float rad = fmod(RadUV.x + _Rotate,1.0);// * 6.283185307179586476;

   			 	float width = lerp(_Width,_Width*_Width, RadUV.y*_MaintainWidth);

   			 	float3 col1 = lerp(_BottomColor.rgb,_TopColor.rgb,saturate(width*rad));
   			 	float3 col2 = lerp(col1,_MidColor.rgb,saturate(width*rad-(width/3)));
   			 	float3 col3 = lerp(col2,_BottomColor.rgb,saturate(width*rad-(2*width/3)));
   			 	return fixed4(col3,1.);
				//float3 col = float3(RadUV.y,RadUV.x,1);
			}
			ENDCG
		}
	}
}
