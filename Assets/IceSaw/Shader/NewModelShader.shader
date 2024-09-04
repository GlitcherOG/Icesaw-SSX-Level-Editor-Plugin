// Upgrade NOTE: upgraded instancing buffer 'NewModelShader' to new syntax.

// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NewModelShader"
{
	Properties
	{
		_VectorDir1("VectorDir1", Vector) = (0,0,0,0)
		_VectorDir2("VectorDir2", Vector) = (0,0,0,0)
		_VectorDir3("VectorDir3", Vector) = (0,0,0,0)
		_VectorColour1("VectorColour1", Color) = (0,0,0,1)
		_VectorColour2("VectorColour2", Color) = (0,0,0,1)
		_VectorColour3("VectorColour3", Color) = (0,0,0,1)
		_AmbientColour("AmbientColour", Color) = (1,1,1,1)
		_MainTexture("Main Texture", 2D) = "white" {}
		_LightMode("LightMode", Int) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma target 5.0
		#pragma multi_compile_instancing
		#pragma surface surf Unlit keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
		};

		uniform int _LightMode;
		uniform float4 _AmbientColour;
		uniform sampler2D _MainTexture;
		uniform float4 _VectorColour1;
		uniform float4 _VectorColour2;
		uniform float4 _VectorColour3;

		UNITY_INSTANCING_BUFFER_START(NewModelShader)
			UNITY_DEFINE_INSTANCED_PROP(float4, _MainTexture_ST)
#define _MainTexture_ST_arr NewModelShader
			UNITY_DEFINE_INSTANCED_PROP(float3, _VectorDir1)
#define _VectorDir1_arr NewModelShader
			UNITY_DEFINE_INSTANCED_PROP(float3, _VectorDir2)
#define _VectorDir2_arr NewModelShader
			UNITY_DEFINE_INSTANCED_PROP(float3, _VectorDir3)
#define _VectorDir3_arr NewModelShader
		UNITY_INSTANCING_BUFFER_END(NewModelShader)

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 _MainTexture_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_MainTexture_ST_arr, _MainTexture_ST);
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST_Instance.xy + _MainTexture_ST_Instance.zw;
			float4 tex2DNode17 = tex2D( _MainTexture, uv_MainTexture );
			float4 myMainTexture26 = tex2DNode17;
			float4 temp_output_20_0 = ( _AmbientColour * myMainTexture26 );
			float3 ase_worldNormal = i.worldNormal;
			float3 _VectorDir1_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorDir1_arr, _VectorDir1);
			float dotResult6 = dot( ase_worldNormal , _VectorDir1_Instance );
			float3 _VectorDir2_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorDir2_arr, _VectorDir2);
			float dotResult48 = dot( ase_worldNormal , _VectorDir2_Instance );
			float3 _VectorDir3_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorDir3_arr, _VectorDir3);
			float dotResult53 = dot( ase_worldNormal , _VectorDir3_Instance );
			float4 temp_output_21_0 = ( myMainTexture26 * ( ( dotResult6 * _VectorColour1 ) + ( dotResult48 * _VectorColour2 ) + ( dotResult53 * _VectorColour3 ) ) );
			o.Emission = ( (float)_LightMode > 0.0 ? ( temp_output_20_0 > temp_output_21_0 ? temp_output_20_0 : temp_output_21_0 ) : myMainTexture26 ).rgb;
			float myAlpha29 = tex2DNode17.a;
			o.Alpha = myAlpha29;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19603
Node;AmplifyShaderEditor.WorldNormalVector;49;-2288,16;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;50;-2288,176;Inherit;False;InstancedProperty;_VectorDir2;VectorDir2;2;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;5;-2288,-528;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;54;-2272,560;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;55;-2272,720;Inherit;False;InstancedProperty;_VectorDir3;VectorDir3;3;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;1;-2288,-368;Inherit;False;InstancedProperty;_VectorDir1;VectorDir1;1;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;17;-1344,-1152;Inherit;True;Property;_MainTexture;Main Texture;8;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.DotProductOpNode;48;-2048,112;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;51;-2288,336;Inherit;False;Property;_VectorColour2;VectorColour2;5;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.DotProductOpNode;6;-2048,-432;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;53;-2032,656;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;56;-2272,880;Inherit;False;Property;_VectorColour3;VectorColour3;6;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;2;-2288,-208;Inherit;False;Property;_VectorColour1;VectorColour1;4;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode;26;-832,-1136;Inherit;False;myMainTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-1776,320;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1776,-224;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-1760,864;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;16;-1696,-896;Inherit;False;Property;_AmbientColour;AmbientColour;7;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode;27;-1696,-688;Inherit;False;26;myMainTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;28;-1712,-576;Inherit;False;26;myMainTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;57;-1520,0;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-1408,-800;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1408,-432;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-816,-1024;Inherit;False;myAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;46;-1168,-384;Inherit;False;26;myMainTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.Compare;41;-1136,-544;Inherit;False;2;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.IntNode;43;-1120,-752;Inherit;False;Property;_LightMode;LightMode;9;0;Create;True;0;0;0;False;0;False;0;1;False;0;1;INT;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;-976,-240;Inherit;False;29;myAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Compare;45;-928,-544;Inherit;False;2;4;0;INT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-768,-592;Float;False;True;-1;7;ASEMaterialInspector;0;0;Unlit;NewModelShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;False;0;True;TransparentCutout;;Transparent;ForwardOnly;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;False;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;48;0;49;0
WireConnection;48;1;50;0
WireConnection;6;0;5;0
WireConnection;6;1;1;0
WireConnection;53;0;54;0
WireConnection;53;1;55;0
WireConnection;26;0;17;0
WireConnection;47;0;48;0
WireConnection;47;1;51;0
WireConnection;15;0;6;0
WireConnection;15;1;2;0
WireConnection;52;0;53;0
WireConnection;52;1;56;0
WireConnection;57;0;15;0
WireConnection;57;1;47;0
WireConnection;57;2;52;0
WireConnection;20;0;16;0
WireConnection;20;1;27;0
WireConnection;21;0;28;0
WireConnection;21;1;57;0
WireConnection;29;0;17;4
WireConnection;41;0;20;0
WireConnection;41;1;21;0
WireConnection;41;2;20;0
WireConnection;41;3;21;0
WireConnection;45;0;43;0
WireConnection;45;2;41;0
WireConnection;45;3;46;0
WireConnection;0;2;45;0
WireConnection;0;9;30;0
ASEEND*/
//CHKSM=3523346EE9130A26FB39509D034B3846D9A673BE