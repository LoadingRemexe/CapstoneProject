Shader "Custom/CloudShader"
{

	

	
	   Properties {
        _Color("Main Color", Color)= (1,1,1)
        _MainTex ("Diffuse Color", 2D) = "white" {}
        _RimColor("Rim Colour", Color)= (0,0,1)
        _RimPower ("Alpha Amount", Range(0.5,8)) = 3.5
        _AlphaMultiplier( "Alpha Multiplier" , Range( 0,8 )) = 2.0
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Lambert alpha
       
        struct Input {
            float2 uv_MainTex;
            float3 viewDir;
        };
       
        float4 _Color;
        float4 _RimColour;
        sampler2D _MainTex;
        float _RimPower;
        float _AlphaMultiplier;
       
        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * _Color.rgb;
            half VN = saturate(dot (normalize(IN.viewDir), o.Normal));
            half rim = pow (1.0f - VN, _RimPower);
            o.Alpha = o.Albedo.r - (rim * _AlphaMultiplier);
            o.Albedo += rim * _RimColour;
        }
        ENDCG
    }
 
    FallBack "Diffuse"
	
	


	/*
	  Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainAlpha ("Main Alpha", Range(0.0,1.0)) = 1.0
        _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
   
    SubShader {
       
            Tags { "Queue"="Transparent" "RenderType"="Transparent" }
           
            CGPROGRAM
           
            #pragma surface surf Lambert alpha
           
            struct Input {
                float3 _Color;
                float3 viewDir;
            };
           
            float4 _Color;
            float4 _RimColor;
            float _RimPower;
            float _MainAlpha;
           
            void surf (Input IN, inout SurfaceOutput o) {
               
                o.Albedo = _Color;
               
                half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
                o.Emission = _RimColor.rgb * pow (rim, _RimPower);
               
                o.Alpha = _MainAlpha;
            }
           
            ENDCG
           
    }
   
    FallBack "Diffuse"*/


}
