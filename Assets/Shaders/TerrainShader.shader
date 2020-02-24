Shader "Assignment/TerrainShader"
{
    Properties
    {      
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Texture("Texture", 2D) = "white"
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM

            // Vertex function is called "vert"
            #pragma vertex vert
            // Fragment function is called "frag"
            #pragma fragment frag

            fixed4 _Color;
            sampler2D _Texture; 

            // Pass in:
            // Vertices
            // Normals
            // Color
            // uv
			struct Input
			{   
				float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
			};

			struct VertexToFragment
			{   
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD
			};

			VertexToFragment vert(Input IN) { 
				VertexToFragment OUT;
                OUT.pos = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                return OUT;
			}

			float4 frag(VertexToFragment IN) : SV_Target {
                

                float4 textureColor = tex2D(_Texture, IN.uv);
                return textureColor * _Color;
			}

            ENDCG
        }
    }
}
