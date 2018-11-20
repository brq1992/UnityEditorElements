Shader "Custom/Customize"
{
    Properties
    {
        [Header(Toggle)]
        [Toggle] _Invert("Invert?", Float) = 0
        [Toggle(ENABLE_FANCY)] _Fancy ("Fancy?", Float) = 0
  
        [Header(KeywordEnum)]
        [KeywordEnum(None, Add, Multiply)] _Overlay ("Overlay mode", Float) = 0
        [Space]
  
        [Header(PowerSlider)]
        [PowerSlider(3.0)] _Shininess ("Shininess", Range (0.01, 1)) = 0.08
        [Space]
  
        [Header(IntRange)]
        [IntRange] _Alpha ("Alpha", Range (0, 255)) = 100
  
        [Header(Cull)]
        [Enum(UnityEngine.Rendering.CullMode)] _CullMode ("Cull Mode", Float) = 0
        [Space]
  
        [Header(Blend)]
        [Enum(UnityEngine.Rendering.BlendMode)] _BlendSrcFactor ("Blend SrcFactor", Float) = 0
        [Enum(UnityEngine.Rendering.BlendMode)] _BlendDstFactor ("Blend DstFactor", Float) = 0
  
        [Header(Stencil)]
        [IntRange] _StencilRef ("Stencil Reference", Range(0, 255)) = 0
        [Enum(CompareFunction)] _StencilComp ("Stencil Compare Function", Float) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilOp ("Stencil Operation", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
  
        Stencil
        {
            Ref [_StencilRef]
            Comp [_StencilComp]
            Pass [_StencilOp]
        }
  
        Pass
        {
            Cull [_CullMode]
            Blend [_BlendSrcFactor] [_BlendDstFactor]
  
            CGPROGRAM
            #pragma shader_feature _INVERT_ON
            #pragma shader_feature ENABLE_FANCY
            #pragma vertex vert
            #pragma fragment frag
              
            #include "UnityCG.cginc"
  
            struct appdata
            {
                float4 vertex : POSITION;
            };
  
            struct v2f
            {
                float4 vertex : SV_POSITION;
            };
  
            float4 _Color;
              
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
  
                return o;
            }
              
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color;
  
                #if _INVERT_ON
                col = 1 - col;
                #endif
  
                #if ENABLE_FANCY
                col.r = 0.5;
                #endif
  
                return col;
            }
            ENDCG
        }
    }
}