// Made with Amplify Shader Editor v1.9.6.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ModelShader"
{
	Properties
	{
		_MainTexture("Main Texture", 2D) = "white" {}
		[Toggle(_NOLIGHTMODE_ON)] _NoLightMode("No Light Mode", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		LOD 1
		Cull Off
		Stencil
		{
			Ref 0
		}
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma target 5.0
		#pragma shader_feature_local _NOLIGHTMODE_ON
		#pragma surface surf Standard keepalpha noshadow exclude_path:deferred nometa noforwardadd 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTexture;
		uniform float4 _MainTexture_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST.xy + _MainTexture_ST.zw;
			float4 tex2DNode2 = tex2D( _MainTexture, uv_MainTexture );
			float4 MainTextureRef44 = tex2DNode2;
			#ifdef _NOLIGHTMODE_ON
				float4 staticSwitch14 = float4( 0,0,0,0 );
			#else
				float4 staticSwitch14 = MainTextureRef44;
			#endif
			o.Albedo = staticSwitch14.rgb;
			#ifdef _NOLIGHTMODE_ON
				float4 staticSwitch11 = ( MainTextureRef44 * 1.0 );
			#else
				float4 staticSwitch11 = float4( 0,0,0,0 );
			#endif
			o.Emission = staticSwitch11.rgb;
			float TextureAlpha51 = tex2DNode2.a;
			o.Alpha = TextureAlpha51;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19603
Node;AmplifyShaderEditor.SamplerNode;2;-1297.35,-99.87978;Inherit;True;Property;_MainTexture;Main Texture;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;6;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT3;5
Node;AmplifyShaderEditor.RegisterLocalVarNode;44;-935.485,-96.56364;Inherit;False;MainTextureRef;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-678.1752,-72.0101;Inherit;False;Constant;_EmissionStrength;EmissionStrength;2;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;48;-720.7031,-201.8769;Inherit;False;44;MainTextureRef;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;51;-910.932,17.91983;Inherit;False;TextureAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-470.112,-156.6892;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;14;-354.5132,-259.4917;Inherit;False;Property;_Keyword0;Keyword 0;1;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Reference;11;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;59;-324.7885,58.25419;Inherit;False;51;TextureAlpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;11;-289.9341,-113.2514;Inherit;False;Property;_NoLightMode;No Light Mode;1;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;131.9174,-157.0838;Float;False;True;-1;7;ASEMaterialInspector;1;0;Standard;ModelShader;False;False;False;False;False;False;False;False;False;False;True;True;False;False;True;False;False;False;False;False;False;Off;0;False;;0;False;;False;0;False;;0;False;;False;4;Custom;0.5;True;False;0;True;TransparentCutout;;Transparent;ForwardOnly;12;all;True;True;True;True;0;False;;True;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;False;2;5;False;;10;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;100;1,0,0,1;VertexScale;False;False;Spherical;False;True;Relative;1;;2;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;44;0;2;0
WireConnection;51;0;2;4
WireConnection;3;0;48;0
WireConnection;3;1;5;0
WireConnection;14;1;48;0
WireConnection;11;0;3;0
WireConnection;0;0;14;0
WireConnection;0;2;11;0
WireConnection;0;9;59;0
ASEEND*/
//CHKSM=2487FC624BBB8A8395A9053CAD27C26A8AB7DE77