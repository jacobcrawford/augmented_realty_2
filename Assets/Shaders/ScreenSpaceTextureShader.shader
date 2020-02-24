Shader "AR/ScreenSpaceTextureShader" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Texture ("Texture", 2D) = "white"
    }
    SubShader {
        Tags {"RenderType" = "Opaque"}

        Pass {            

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Color;

            sampler2D _Texture;

            struct Input {
                float4 position : POSITION;                
            };

            struct VertexToFragment {
                float4 position : SV_POSITION;
                float4 uv : TEXCOORD0;
            };

            VertexToFragment vert(Input IN) {
                VertexToFragment OUT;
                
                OUT.position = UnityObjectToClipPos(IN.position);                

                // forwarding clip space coordinates as UV (texture) coordinates

                OUT.uv = UnityObjectToClipPos(IN.position);

                return OUT;
            }

            fixed4 frag(VertexToFragment IN) : SV_Target {

                // divide clip coordinates by w component
                // to normalized device coordinates [-1,1]

                float2 newUV = IN.uv / IN.uv.w;

                // normalize the coordinates 

                newUV = float2 (newUV.x * 0.5 + 0.5,
                                -newUV.y * 0.5 + 0.5);

                // multiply sampled texture color with _Color property

                return tex2D(_Texture, newUV) * _Color;
            }   

            ENDCG
        }
    }
}