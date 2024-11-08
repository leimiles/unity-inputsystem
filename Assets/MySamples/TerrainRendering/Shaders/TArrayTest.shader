Shader "MileStudio/TArrayTest"
{
    Properties
    {
        // 纹理数组属性
        _MainTexArray ("Texture Array", 2DArray) = "" { }

        // 用于选择纹理的索引
        _TextureIndex ("Texture Index", Range(0, 9)) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag

            // 使用 URP 的 Shader 包
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // 声明纹理数组
            TEXTURE2D_ARRAY(_MainTexArray);
            SAMPLER(sampler_MainTexArray);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTexArray_ST;
                float _TextureIndex;
            CBUFFER_END

            // 定义输入结构
            struct Attributes
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
            };

            // 定义输出结构
            struct Varyings
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // 顶点着色器
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.position = TransformObjectToHClip(IN.position.xyz);
                OUT.uv = IN.uv * _MainTexArray_ST.xy + _MainTexArray_ST.zw ;
                return OUT;
            }


            // 片段着色器
            half4 frag(Varyings IN) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D_ARRAY(_MainTexArray, sampler_MainTexArray, IN.uv, _TextureIndex);
                return color;
            }
            ENDHLSL
        }
    }

    FallBack "Unlit/Texture"
}
