//triplanar node

void TextureProjection_float 
(
    in Texture2D Tex,
    in SamplerState SS,
    in float3 Position,
    in float3 Normal,
    in float  Tile,
    in float  Blend,
    in float  Blend,
    in float Speed,
    in float Rotation, 
    out float4 Out
)
{
    float Speed_UV = _Time.y * Speed;
    
    float3 Node_UV = Position * Tile;
    float3 Node_Blend = pow(abs(Normal), Blend);
    Node_Blend /= dot(Node_Blend, 1.0);
    
    float4 Node_X = SAMPLE_TEXTURE2D(Tex, SS, Unity_Rotate_Degrees_float(Node_UV.yz, 0, Rotation) + Speed_UV);
    float4 Node_Y = SAMPLE_TEXTURE2D(Tex, SS, Unity_Rotate_Degrees_float(Node_UV.xz, 0, Rotation) + Speed_UV);
    float4 Node_Z = SAMPLE_TEXTURE2D(Tex, SS, Unity_Rotate_Degrees_float(Node_UV.xy, 0, Rotation) + Speed_UV);
    
    Out = Node_X * Node_Blend.x + Node_Y * Node_Blend.y + Node_Z * Node_Blend.z;
}