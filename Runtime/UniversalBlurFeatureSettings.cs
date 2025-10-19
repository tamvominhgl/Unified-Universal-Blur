using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Unified.UniversalBlur.Runtime
{
    public class UniversalBlurFeatureSettings : ScriptableObject
    {
        public static UniversalBlurFeatureSettings Default { get; private set; }

        [SerializeField] bool isDefault = false;

        [Header("Blur Settings")]
        [Range(1, 12)] [SerializeField] public int iterations = 4;
        [Range(1f, 10f)] [SerializeField] public float downsample = 2.0f;
        
        [Tooltip("Enable mipmaps for more efficient blur")]
        [SerializeField] public bool enableMipMaps = true;
        // [Range(0f, 10f)] 
        [SerializeField] public float scale = 1f;
        // [Range(0f, 10f)] 
        [SerializeField] public float offset = 1f;
        
        [Space]
        
        [Header("Advanced Settings")]
        [SerializeField] public ScaleBlurWith scaleBlurWith = ScaleBlurWith.ScreenHeight;
        [SerializeField] public float scaleReferenceSize = 1080f;
        
        [Space]
        
        // [SerializeField, ShowAsPass(nameof(_material))] public int shaderPass;
        [SerializeField] public BlurType blurType;

        [Tooltip("For Overlay Canvas: AfterRenderingPostProcessing" +
                 "\n\nOther: BeforeRenderingTransparents (will hide transparents)")]
        [SerializeField] public RenderPassEvent injectionPoint = RenderPassEvent.AfterRenderingPostProcessing;

        [NonSerialized] public RenderTexture SourceRT;
        [NonSerialized] public RenderTexture DestinationRT;
        [NonSerialized] public int RTWidth;
        [NonSerialized] public int RTHeight;

        void OnEnable()
        {
            if (isDefault)
            {
                Default = this;
            }
        }

        void OnDestroy()
        {
            if (Default == this)
            {
                Default = null;
            }
        }

        public void SetGlobalTexture()
        {
            Shader.SetGlobalTexture(Constants.GlobalFullScreenBlurTextureId, DestinationRT);
        }
    }
}