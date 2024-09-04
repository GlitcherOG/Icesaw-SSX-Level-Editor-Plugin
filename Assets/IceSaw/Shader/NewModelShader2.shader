// Upgrade NOTE: upgraded instancing buffer 'NewModelShader2' to new syntax.

// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NewModelShader2"
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
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Unlit keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
		};

		uniform sampler2D _MainTexture;
		uniform float4 _AmbientColour;
		uniform float4 _VectorColour1;
		uniform float4 _VectorColour2;
		uniform float4 _VectorColour3;

		UNITY_INSTANCING_BUFFER_START(NewModelShader2)
			UNITY_DEFINE_INSTANCED_PROP(float4, _MainTexture_ST)
#define _MainTexture_ST_arr NewModelShader2
			UNITY_DEFINE_INSTANCED_PROP(float3, _VectorDir1)
#define _VectorDir1_arr NewModelShader2
			UNITY_DEFINE_INSTANCED_PROP(float3, _VectorDir2)
#define _VectorDir2_arr NewModelShader2
			UNITY_DEFINE_INSTANCED_PROP(float3, _VectorDir3)
#define _VectorDir3_arr NewModelShader2
		UNITY_INSTANCING_BUFFER_END(NewModelShader2)

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
			float3 ase_worldNormal = i.worldNormal;
			float3 _VectorDir1_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorDir1_arr, _VectorDir1);
			float dotResult6 = dot( ase_worldNormal , _VectorDir1_Instance );
			float3 _VectorDir2_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorDir2_arr, _VectorDir2);
			float dotResult48 = dot( ase_worldNormal , _VectorDir2_Instance );
			float3 _VectorDir3_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorDir3_arr, _VectorDir3);
			float dotResult53 = dot( ase_worldNormal , _VectorDir3_Instance );
			o.Emission = ( myMainTexture26 * ( ( _AmbientColour * 3.0 ) + ( ( dotResult6 * _VectorColour1 ) + ( dotResult48 * _VectorColour2 ) + ( dotResult53 * _VectorColour3 ) ) ) ).rgb;
			float myAlpha29 = tex2DNode17.a;
			o.Alpha = myAlpha29;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19603
Node;AmplifyShaderEditor.WorldNormalVector;49;-3008,-576;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;50;-3008,-416;Inherit;False;InstancedProperty;_VectorDir2;VectorDir2;2;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;5;-3008,-1120;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;55;-2992,128;Inherit;False;InstancedProperty;_VectorDir3;VectorDir3;3;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;1;-3008,-960;Inherit;False;InstancedProperty;_VectorDir1;VectorDir1;1;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;54;-2992,-32;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DotProductOpNode;48;-2768,-480;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;51;-3008,-256;Inherit;False;Property;_VectorColour2;VectorColour2;5;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.DotProductOpNode;6;-2768,-1024;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;56;-2992,288;Inherit;False;Property;_VectorColour3;VectorColour3;6;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;2;-3008,-800;Inherit;False;Property;_VectorColour1;VectorColour1;4;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.DotProductOpNode;53;-2752,64;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;17;-1344,-1152;Inherit;True;Property;_MainTexture;Main Texture;8;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-2496,-272;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-2496,-816;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-2480,272;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;16;-2160,-752;Inherit;False;Property;_AmbientColour;AmbientColour;7;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RangedFloatNode;88;-2096,-512;Inherit;False;Constant;_Float0;Float 0;9;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;26;-1024,-1152;Inherit;False;myMainTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;57;-2016,-352;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;-1888,-576;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-1024,-1040;Inherit;False;myAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;84;-1696,-384;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;46;-1568,-688;Inherit;False;26;myMainTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;-992,-128;Inherit;False;29;myAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-1344,-480;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-768,-592;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;NewModelShader2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;False;0;True;TransparentCutout;;Transparent;ForwardOnly;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;False;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;48;0;49;0
WireConnection;48;1;50;0
WireConnection;6;0;5;0
WireConnection;6;1;1;0
WireConnection;53;0;54;0
WireConnection;53;1;55;0
WireConnection;47;0;48;0
WireConnection;47;1;51;0
WireConnection;15;0;6;0
WireConnection;15;1;2;0
WireConnection;52;0;53;0
WireConnection;52;1;56;0
WireConnection;26;0;17;0
WireConnection;57;0;15;0
WireConnection;57;1;47;0
WireConnection;57;2;52;0
WireConnection;86;0;16;0
WireConnection;86;1;88;0
WireConnection;29;0;17;4
WireConnection;84;0;86;0
WireConnection;84;1;57;0
WireConnection;85;0;46;0
WireConnection;85;1;84;0
WireConnection;0;2;85;0
WireConnection;0;9;30;0
ASEEND*/
//CHKSM=3F4A0E8CFB6C94D3AEDE202CFCBF829B6747632B