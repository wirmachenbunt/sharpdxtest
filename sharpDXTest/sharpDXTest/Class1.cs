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
            properties.PixelSize = new SharpDX.Size2(1, 1);
            properties.PresentOptions = PresentOptions.None;

            RenderTarget2D = new WindowRenderTarget(Factory2D, new RenderTargetProperties(new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Premultiplied)), properties);

            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;


            //SceneColorBrush = new SolidColorBrush(RenderTarget2D, System.Drawing.Color.White);
            //NOT WORKING
        }



        #endregion constructor and init

        //called when data for any output pin is requested
        public void Evaluate(int SpreadMax)
        {
            RenderTarget2D.BeginDraw();

            //not drawing anything yet

            RenderTarget2D.EndDraw();

        }
    }
}