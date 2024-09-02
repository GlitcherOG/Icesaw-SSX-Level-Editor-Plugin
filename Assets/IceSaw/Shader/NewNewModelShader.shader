// Upgrade NOTE: upgraded instancing buffer 'NewNewModelShader' to new syntax.

// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NewNewModelShader"
{
	Properties
	{
		_LightVector1("LightVector1", Vector) = (0.5557215,0.8313487,-0.005775411,0)
		_LightVector2("LightVector2", Vector) = (0.5557215,0.8313487,-0.005775411,0)
		_LightVector3("LightVector3", Vector) = (0.5557215,0.8313487,-0.005775411,0)
		_VectorColour1("VectorColour1", Color) = (1,0.9386792,1,1)
		_VectorColour2("VectorColour2", Color) = (1,0.9386792,1,1)
		_VectorColour3("VectorColour3", Color) = (1,0.9386792,1,1)
		_AmbientColour("AmbientColour", Color) = (0.1411765,0.2470588,0.3019608,0.5019608)
		_MainTexture("Main Texture", 2D) = "white" {}
		_AmbientStrength("Ambient Strength", Range( 0 , 5)) = 2
		[IntRange]_Divider("Divider", Range( 1 , 3)) = 1
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

		uniform sampler2D _MainTexture;
		uniform half _AmbientStrength;
		uniform half _Divider;

		UNITY_INSTANCING_BUFFER_START(NewNewModelShader)
			UNITY_DEFINE_INSTANCED_PROP(half4, _MainTexture_ST)
#define _MainTexture_ST_arr NewNewModelShader
			UNITY_DEFINE_INSTANCED_PROP(half4, _AmbientColour)
#define _AmbientColour_arr NewNewModelShader
			UNITY_DEFINE_INSTANCED_PROP(half4, _VectorColour1)
#define _VectorColour1_arr NewNewModelShader
			UNITY_DEFINE_INSTANCED_PROP(half4, _VectorColour2)
#define _VectorColour2_arr NewNewModelShader
			UNITY_DEFINE_INSTANCED_PROP(half4, _VectorColour3)
#define _VectorColour3_arr NewNewModelShader
			UNITY_DEFINE_INSTANCED_PROP(half3, _LightVector1)
#define _LightVector1_arr NewNewModelShader
			UNITY_DEFINE_INSTANCED_PROP(half3, _LightVector2)
#define _LightVector2_arr NewNewModelShader
			UNITY_DEFINE_INSTANCED_PROP(half3, _LightVector3)
#define _LightVector3_arr NewNewModelShader
		UNITY_INSTANCING_BUFFER_END(NewNewModelShader)

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
			half4 _AmbientColour_Instance = UNITY_ACCESS_INSTANCED_PROP(_AmbientColour_arr, _AmbientColour);
			half3 ase_worldNormal = i.worldNormal;
			half3 WorldNormal54 = ase_worldNormal;
			half3 _LightVector1_Instance = UNITY_ACCESS_INSTANCED_PROP(_LightVector1_arr, _LightVector1);
			half dotResult6 = dot( WorldNormal54 , _LightVector1_Instance );
			half4 _VectorColour1_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorColour1_arr, _VectorColour1);
			half4 clampResult63 = clamp( ( dotResult6 * _VectorColour1_Instance ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			half3 _LightVector2_Instance = UNITY_ACCESS_INSTANCED_PROP(_LightVector2_arr, _LightVector2);
			half dotResult48 = dot( WorldNormal54 , _LightVector2_Instance );
			half4 _VectorColour2_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorColour2_arr, _VectorColour2);
			half4 clampResult62 = clamp( ( dotResult48 * _VectorColour2_Instance ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			half3 _LightVector3_Instance = UNITY_ACCESS_INSTANCED_PROP(_LightVector3_arr, _LightVector3);
			half dotResult52 = dot( WorldNormal54 , _LightVector3_Instance );
			half4 _VectorColour3_Instance = UNITY_ACCESS_INSTANCED_PROP(_VectorColour3_arr, _VectorColour3);
			half4 clampResult60 = clamp( ( dotResult52 * _VectorColour3_Instance ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			half4 Directional_Lighting65 = ( ( clampResult63 + clampResult62 + clampResult60 ) / _Divider );
			half4 Final_Lighting72 = ( ( _AmbientColour_Instance * _AmbientStrength ) + Directional_Lighting65 );
			o.Emission = ( myMainTexture26 * Final_Lighting72 ).rgb;
			half myAlpha29 = tex2DNode17.a;
			o.Alpha = myAlpha29;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19603
Node;AmplifyShaderEditor.CommentaryNode;70;-3888,-400;Inherit;False;2244;1483;Directional Lighting;24;5;54;1;55;56;57;46;50;2;6;48;52;47;51;15;45;49;62;63;60;44;69;67;65;//Directional Lighting;1,1,0.2352941,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;5;-3824,272;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;54;-3632,272;Inherit;False;WorldNormal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;1;-3328,-224;Inherit;False;InstancedProperty;_LightVector1;LightVector1;1;0;Create;True;0;0;0;False;0;False;0.5557215,0.8313487,-0.005775411;0.5557215,0.8313487,-0.005775411;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;55;-3344,-352;Inherit;False;54;WorldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;56;-3296,160;Inherit;False;54;WorldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;57;-3264,608;Inherit;False;54;WorldNormal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;46;-3328,240;Inherit;False;InstancedProperty;_LightVector2;LightVector2;2;0;Create;True;0;0;0;False;0;False;0.5557215,0.8313487,-0.005775411;0.5557215,0.8313487,-0.005775411;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;50;-3296,688;Inherit;False;InstancedProperty;_LightVector3;LightVector3;3;0;Create;True;0;0;0;False;0;False;0.5557215,0.8313487,-0.005775411;0.5557215,0.8313487,-0.005775411;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;2;-3328,-64;Inherit;False;InstancedProperty;_VectorColour1;VectorColour1;4;0;Create;True;0;0;0;False;0;False;1,0.9386792,1,1;0.6318335,0.573073,0.5218945,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.DotProductOpNode;6;-3072,-272;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;48;-3072,192;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;52;-3040,640;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;47;-3328,400;Inherit;False;InstancedProperty;_VectorColour2;VectorColour2;5;0;Create;True;0;0;0;False;0;False;1,0.9386792,1,1;0.6318334,0.5730729,0.5218945,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.ColorNode;51;-3296,848;Inherit;False;InstancedProperty;_VectorColour3;VectorColour3;6;0;Create;True;0;0;0;False;0;False;1,0.9386792,1,1;0.6318334,0.5730729,0.5218945,0;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-2928,-96;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-2928,368;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-2912,752;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;62;-2704,384;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;63;-2640,128;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;60;-2656,704;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-2416,304;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;69;-2400,544;Inherit;False;Property;_Divider;Divider;10;1;[IntRange];Create;True;0;0;0;False;0;False;1;2;1;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;71;-2338,-1074;Inherit;False;859;419;Ambient Lighting;5;16;33;66;20;39;//Ambient Lighting;0.227451,0.7215686,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;67;-2048,368;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;16;-2256,-1024;Inherit;False;InstancedProperty;_AmbientColour;AmbientColour;7;0;Create;True;0;0;0;False;0;False;0.1411765,0.2470588,0.3019608,0.5019608;0.1427576,0.24576,0.3011765,0.5019608;True;True;0;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode;65;-1904,352;Inherit;False;Directional Lighting;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-2288,-816;Inherit;False;Property;_AmbientStrength;Ambient Strength;9;0;Create;True;0;0;0;False;0;False;2;5;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;66;-2000,-768;Inherit;False;65;Directional Lighting;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-1968,-1024;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;17;-2864,-976;Inherit;True;Property;_MainTexture;Main Texture;8;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.SimpleAddOpNode;39;-1712,-1008;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;74;-1010,-626;Inherit;False;507;336;Apply Lighting;3;73;28;42;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;26;-2576,-960;Inherit;False;myMainTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;72;-1344,-928;Inherit;False;Final Lighting;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-2576,-864;Inherit;False;myAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;73;-960,-464;Inherit;False;72;Final Lighting;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;28;-960,-576;Inherit;False;26;myMainTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.DotProductOpNode;11;-336,-1648;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;10;-656,-1840;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;14;-640,-1648;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;13;-144,-1616;Inherit;False;ViewDir;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;-368,-144;Inherit;False;29;myAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-736,-544;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-160,-368;Half;False;True;-1;7;ASEMaterialInspector;0;0;Unlit;NewNewModelShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;0;Custom;0.5;True;False;0;True;TransparentCutout;;Transparent;ForwardOnly;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;False;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;0;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;54;0;5;0
WireConnection;6;0;55;0
WireConnection;6;1;1;0
WireConnection;48;0;56;0
WireConnection;48;1;46;0
WireConnection;52;0;57;0
WireConnection;52;1;50;0
WireConnection;15;0;6;0
WireConnection;15;1;2;0
WireConnection;45;0;48;0
WireConnection;45;1;47;0
WireConnection;49;0;52;0
WireConnection;49;1;51;0
WireConnection;62;0;45;0
WireConnection;63;0;15;0
WireConnection;60;0;49;0
WireConnection;44;0;63;0
WireConnection;44;1;62;0
WireConnection;44;2;60;0
WireConnection;67;0;44;0
WireConnection;67;1;69;0
WireConnection;65;0;67;0
WireConnection;20;0;16;0
WireConnection;20;1;33;0
WireConnection;39;0;20;0
WireConnection;39;1;66;0
WireConnection;26;0;17;0
WireConnection;72;0;39;0
WireConnection;29;0;17;4
WireConnection;11;0;10;0
WireConnection;11;1;14;0
WireConnection;13;0;11;0
WireConnection;42;0;28;0
WireConnection;42;1;73;0
WireConnection;0;2;42;0
WireConnection;0;9;30;0
ASEEND*/
//CHKSM=47608884A68739827BC153F72DD41799A97ACD37