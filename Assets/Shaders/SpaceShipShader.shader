// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Assignment/SpaceshipShader"
{
    Properties
    {            
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

            // Pass in:
            // Vertices
            // Normals
            // Color
            // uv
			struct Input
			{   
				float4 vertex : POSITION;
                float4 color : COLOR;
			};

			struct VertexToFragment
			{   
                float4 pos : SV_POSITION;
                float4 color : COLOR;
			};

            float4 _Color;

			VertexToFragment vert(Input IN) {
				VertexToFragment OUT;
                OUT.pos = UnityObjectToClipPos(IN.vertex);
                OUT.color = IN.color;
                return OUT;
			}

			float4 frag(VertexToFragment IN) : SV_Target {
                return IN.color;
			}

            ENDCG
        }
    }
}
