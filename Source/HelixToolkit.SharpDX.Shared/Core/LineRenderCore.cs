﻿using System;
using SharpDX;
using SharpDX.Direct3D11;
#if !NETFX_CORE
namespace HelixToolkit.Wpf.SharpDX.Core
#else
namespace HelixToolkit.UWP.Core
#endif
{
    public class LineRenderCore : GeometryRenderCore
    {
        /// <summary>
        /// Line parameters, X is Thickness, Y is Smoothness, ZW are unused
        /// </summary>
        public Vector4 LineParams = new Vector4();
        /// <summary>
        /// Final Line Color = LineColor * PerVertexLineColor
        /// </summary>
        public Color4 LineColor = Color.Black;

        protected override void OnUpdateModelStruct(ref ModelStruct model, IRenderMatrices context)
        {
            base.OnUpdateModelStruct(ref model, context);
            model.Color = LineColor;
            model.Params = LineParams;
        }

        protected override void OnRender(IRenderMatrices context)
        {
            UpdateModelConstantBuffer(context.DeviceContext);
            EffectTechnique[0].BindShader(context.DeviceContext);
            EffectTechnique[0].BindStates(context.DeviceContext, StateType.BlendState | StateType.DepthStencilState);
            context.DeviceContext.Rasterizer.State = RasterState;
            OnDraw(context.DeviceContext, InstanceBuffer);
        }
    }
}
