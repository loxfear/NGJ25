
#if !defined(SHADERGRAPH_PREVIEW)
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
#endif
void ViewNormals_half (float2 UV, out float3 Out)
{
  #if !defined(SHADERGRAPH_PREVIEW)
  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
  #endif
    // Preview redefines _Time in a way that will throw a compile error on this import.
    #if !defined(SHADERGRAPH_PREVIEW)
    Out = SampleSceneNormals(UV);
    #else
    Out = float3(0, 0, 0);
    #endif
}
void ViewNormals_float (float2 UV, out float3 Out)
{
    #if !defined(SHADERGRAPH_PREVIEW)
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
    #endif
    // Preview redefines _Time in a way that will throw a compile error on this import.
    #if !defined(SHADERGRAPH_PREVIEW)
    Out = SampleSceneNormals(UV);
    #else
    Out = float3(0, 0, 0);
    #endif
}