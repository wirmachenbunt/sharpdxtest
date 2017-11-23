#region usings
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using System.Collections.Generic;
//using System.Drawing;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;

using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX;

using VVVV.Core.Logging;
using SharpDX.Mathematics.Interop;
#endregion usings

namespace VVVV.Nodes
{
    #region PluginInfo
    [PluginInfo(Name = "sharpDXTest",
                Category = "GUI",
                Help = "Template with some gui elements",
                Tags = "c#",
                AutoEvaluate = true)]
    #endregion PluginInfo

    public class GUITemplateNode : UserControl, IPluginEvaluate
    {
        #region fields & pins

        [Import()]
        public ILogger FLogger;

        [Input("Play")]
        public IDiffSpread<bool> FPlay;

        [Output("Clips")]
        public ISpread<bool> ClipsOut;

        public SharpDX.Direct2D1.Factory Factory2D { get; private set; }
        public SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }
        public WindowRenderTarget RenderTarget2D { get; private set; }
        public SolidColorBrush SceneColorBrush { get; private set; }


        #endregion fields & pins

        #region constructor and init

        public GUITemplateNode()
        {
            //setup the gui
            InitializeComponent();
        }

        void InitializeComponent()
        {
            



            Factory2D = new SharpDX.Direct2D1.Factory();
            FactoryDWrite = new SharpDX.DirectWrite.Factory();
            HwndRenderTargetProperties properties = new HwndRenderTargetProperties();
            properties.Hwnd = this.Handle;
            properties.PixelSize = new SharpDX.Size2(this.ClientSize.Width, this.ClientSize.Height);
            properties.PresentOptions = PresentOptions.RetainContents;
            
           

            RenderTarget2D = new WindowRenderTarget(Factory2D, new RenderTargetProperties(new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied)), properties);

            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;

            
            SceneColorBrush = new SolidColorBrush(RenderTarget2D, new RawColor4(1.0f, 0.0f, 0.0f, 1.0f));
            //NOT WORKING
        }



        #endregion constructor and init

        //called when data for any output pin is requested
        public void Evaluate(int SpreadMax)
        {
            RenderTarget2D.BeginDraw();

            ClipsOut[0] = FPlay[0];


            RenderTarget2D.Clear(new RawColor4(1.0f, 0.0f, 0.0f, 1.0f));

            try { RenderTarget2D.EndDraw(); }
            catch { }
            

        }
    }
}