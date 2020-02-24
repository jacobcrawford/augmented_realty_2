Shader "AR/BasicTextureShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)                
        _Texture("Texture", 2D) = "white"
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag


            fixed4 _Color;

            sampler2D _Texture; 

			struct Input
			{
				float4 position : POSITION;                                
                
                // UV is naming convention for texture coordinates (u,v)
                // The 0 is for having multiple texture coordinates for the same mesh

                float2 uv : TEXCOORD0;
			};

			struct VertexToFragment
			{
				float4 position : SV_POSITION; // interpolated positions                
                float2 uv : TEXCOORD0; // interpolated UV
			};

			VertexToFragment vert(Input IN) {
				VertexToFragment OUT;

                // forwarding position
                // applying MVP matrix (from object to clip space)

				OUT.position = UnityObjectToClipPos(IN.position);

                // forwarding UV
                
                OUT.uv = IN.uv;

				return OUT;
			}

			fixed4 frag(VertexToFragment IN) : SV_Target {

                // multiply sampled texture color with _Color property

				return  _Color; 
			}

            ENDCG
        }
    }
}


