// Upgrade NOTE: upgraded instancing buffer 'NewModelShader' to new syntax.

// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NewModelShader"
{
	Properties
	{
		_VectorDir1("VectorDir1", Vector) = (0,0,0,0)
		_VectorColour1("VectorColour1", Color) = (1,1,1,1)
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
		#pragma target 5.0
		#pragma multi_compile_instancing
		#pragma surface surf Unlit keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			half3 worldNormal;
		};

		uniform half4 _AmbientColour;
		uniform sampler2D _MainTexture;
		uniform half4 _VectorColour1;

		UNITY_INSTANCING_BUFFER_START(NewModelShader)
			UNITY_DEFINE_INSTANCED_PROP(half4, _MainTexture_ST)
#define _MainTexture_ST_arr NewModelShader
			UNITY_DEFINE_INSTANCED_PROP(half3, _VectorDir1)
#define _VectorDir1_arr NewModelShader
		UNITY_INSTANCING_BUFFER_END(NewModelShader)

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			half4 _MainTexture_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_MainTexture_ST_arr, _MainTexture_ST);
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST_Instance.xy + _MainTexture_ST_Instance.zw;
			half4 tex2DNode17 = tex2D( _MainTexture, uv_MainTexture );
			half4 myMainTexture26 = tex2DNode17;
			half3 ase_worldNormal = i.worldNormal;
			half3 _VectorDir1_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorDir1_arr, _VectorDir1);
			half dotResult6 = dot( ase_worldNormal , _VectorDir1_Instance );
			o.Emission = ( ( ( _AmbientColour * myMainTexture26 * 1 ) + ( myMainTexture26 * ( dotResult6 * _VectorColour1 ) ) ) / 2 ).rgb;
			half myAlpha29 = tex2DNode17.a;
			o.Alpha = myAlpha29;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19603
Node;AmplifyShaderEditor.SamplerNode;17;-2864,-976;Inherit;True;Property;_MainTexture;Main Texture;4;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.Vector3Node;1;-2544,-336;Inherit;False;InstancedProperty;_VectorDir1;VectorDir1;1;0;Create;True;0;0;0;False;0;False;0,0,0;0.5557211,0.8313488,-0.005775969;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;5;-2544,-496;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;2;-2576,-176;Inherit;False;Property;_VectorColour1;VectorColour1;2;0;Create;True;0;0;0;False;0;False;1,1,1,1;0.6318334,0.5730729,0.5218944,1;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode;26;-2352,-960;Inherit;False;myMainTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DotProductOpNode;6;-2272,-416;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1792,-176;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;28;-1920,-512;Inherit;False;26;myMainTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;16;-2080,-1040;Inherit;False;Property;_AmbientColour;AmbientColour;3;0;Create;True;0;0;0;False;0;False;1,1,1,1;0.1427576,0.2457599,0.3011764,1.003922;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.GetLocalVarNode;27;-2080,-832;Inherit;False;26;myMainTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.IntNode;25;-1904,-720;Inherit;False;Constant;_Int1;Int 1;5;0;Create;True;0;0;0;False;0;False;1;0;False;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1600,-352;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-1584,-880;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-2336,-848;Inherit;False;myAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;24;-1216,-272;Inherit;False;Constant;_Int0;Int 0;5;0;Create;True;0;0;0;False;0;False;2;0;False;0;1;INT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-1168,-496;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DotProductOpNode;11;-336,-1648;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;13;-144,-1616;Inherit;False;ViewDir;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;10;-656,-1840;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;14;-640,-1648;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleDivideOpNode;23;-1008,-432;Inherit;False;2;0;COLOR;0,0,0,0;False;1;INT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;-1184,-48;Inherit;False;29;myAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-816,-368;Half;False;True;-1;7;ASEMaterialInspector;0;0;Unlit;NewModelShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;False;0;True;TransparentCutout;;Transparent;ForwardOnly;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;False;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;26;0;17;0
WireConnection;6;0;5;0
WireConnection;6;1;1;0
WireConnection;15;0;6;0
WireConnection;15;1;2;0
WireConnection;21;0;28;0
WireConnection;21;1;15;0
WireConnection;20;0;16;0
WireConnection;20;1;27;0
WireConnection;20;2;25;0
WireConnection;29;0;17;4
WireConnection;22;0;20;0
WireConnection;22;1;21;0
WireConnection;11;0;10;0
WireConnection;11;1;14;0
WireConnection;13;0;11;0
WireConnection;23;0;22;0
WireConnection;23;1;24;0
WireConnection;0;2;23;0
WireConnection;0;9;30;0
ASEEND*/
//CHKSM=3837A8098814FE29B903D30E84314EC69237B98B