/// <summary>
/// file = "MainWindow.xaml.cs"
/// Grace Kumagai
/// This code is based off of Microsoft's WPFDX Interlop and msdn's combo box tutorial
/// </summary>

namespace Wpf.D3D11Interop
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Media.Imaging;
    using System.Windows.Media.Media3D;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Unit conversion
        private const float DegreesToRadians = (float)Math.PI / 180;

        // State Management
        TimeSpan lastRender;
        bool lastVisible;
        bool lastRendered = false;


        public MainWindow()
        {   ///The following is performed when the program begins
            this.InitializeComponent();
            this.host.Loaded += new RoutedEventHandler(this.Host_Loaded);
            this.host.SizeChanged += new SizeChangedEventHandler(this.Host_SizeChanged);
        }


        private static bool Init()
        {   ///This function initializes the WPF 3D3 Interops and prints a failure message if it is not successful
            
            bool initSucceeded = NativeMethods.InvokeWithDllProtection(() => NativeMethods.Init()) >= 0;
            
            if (!initSucceeded)
            {
                MessageBox.Show("Failed to initialize.", "WPF D3D Interop", MessageBoxButton.OK, MessageBoxImage.Error);
                if (Application.Current != null)
                {
                    Application.Current.Shutdown();
                }
            }
            return initSucceeded;
        }

        ///The following are functions defined in D3DVisualization.cpp (C++)
        private static void Cleanup()
        {
            NativeMethods.InvokeWithDllProtection(NativeMethods.Cleanup);
        }

        private static int Render(IntPtr resourcePointer, bool isNewSurface)
        {
            return NativeMethods.InvokeWithDllProtection(() => NativeMethods.Render(resourcePointer, isNewSurface));      
        }

        private static int SetCameraRadius(float radius)
        {
            return NativeMethods.InvokeWithDllProtection(() => NativeMethods.SetCameraRadius(radius));
        }

        private static int SetCameraTheta(float theta)
        {
            return NativeMethods.InvokeWithDllProtection(() => NativeMethods.SetCameraTheta(theta));    
        }

        private static int SetCameraPhi(float phi)
        {
            return NativeMethods.InvokeWithDllProtection(() => NativeMethods.SetCameraPhi(phi));
        }

        #region Callbacks
        private void Host_Loaded(object sender, RoutedEventArgs e)
        {   ///This function initializes the Native Methods, initializes Rendering (Camera, Image, etc)
            ///and toggles the cursor
            Init();
            this.InitializeRendering();

            CurserToggle.Cursor = System.Windows.Input.Cursors.Arrow;
            host.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        private void Host_SizeChanged(object sender, SizeChangedEventArgs e)
        {   ///This function changes the size of the host dependent on the DPI of the monitor, pixel size of
            ///D3D11Image 
            double dpiScale = 1.0; // default value for 96 dpi

            // determine DPI
            // (as of .NET 4.6.1, this returns the DPI of the primary monitor, if you have several different DPIs)
            var hwndTarget = PresentationSource.FromVisual(this).CompositionTarget as HwndTarget;
            if (hwndTarget != null)
            {
                dpiScale = hwndTarget.TransformToDevice.M11;
            }

            int surfWidth = (int)(host.ActualWidth < 0 ? 0 : Math.Ceiling(host.ActualWidth * dpiScale));
            int surfHeight = (int)(host.ActualHeight < 0 ? 0 : Math.Ceiling(host.ActualHeight * dpiScale));

            // Notify the D3D11Image of the pixel size desired for the DirectX rendering.
            // The D3DRendering component will determine the size of the new surface it is given, at that point.
            InteropImage.SetPixelSize(surfWidth, surfHeight);
                
            // Stop rendering if the D3DImage isn't visible - currently just if width or height is 0
            bool isVisible = (surfWidth != 0 && surfHeight != 0);
            if (lastVisible != isVisible)
            {
                lastVisible = isVisible;
                if (lastVisible)
                {
                    CompositionTarget.Rendering += CompositionTarget_Rendering;
                }
                else
                {
                    CompositionTarget.Rendering -= CompositionTarget_Rendering;
                }
            }
        }



        ///The following functions are called when there are changes in the camera's position in the
        ///spherical coordinate system (radius, theta, phi)
        private void Radius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {   ///This function is called when the 'Radius' slider is changed, indicating a change in the
            ///radial camera position
            SetCameraRadius((float)e.NewValue);
        }

        private void Theta_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {   ///This function is called when the 'Radius' slider is changed, indicating a change in the
            ///camera position along the theta's unit vector
            SetCameraTheta((float)e.NewValue * DegreesToRadians);
        }

        private void Phi_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {   ///This function is called when the 'Radius' slider is changed, indicating a change in the
            ///camera position along the phi's unit vector
            SetCameraPhi((float)e.NewValue * DegreesToRadians);
        }
        #endregion Callbacks

        #region Helpers
        private void InitializeRendering()
        {   ///The following function initializes rendering (Camera, Image, etc)
            InteropImage.WindowOwner = (new System.Windows.Interop.WindowInteropHelper(this)).Handle;
            InteropImage.OnRender = this.DoRender;

            // Set up camera
            SetCameraRadius((float)RadiusSlider.Value);
            SetCameraPhi((float)PhiSlider.Value * DegreesToRadians);
            SetCameraTheta((float)ThetaSlider.Value * DegreesToRadians);

            // Start rendering now!
            InteropImage.RequestRender();
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {   ///It's possible for Rendering to call back twice in the same frame 
            ///so so this function only enables rendering when we haven't 
            ///already rendered in this frame.
            RenderingEventArgs args = (RenderingEventArgs)e;
           
            if (this.lastRender != args.RenderingTime)
            {
                InteropImage.RequestRender();
                this.lastRender = args.RenderingTime;
            }
        }

        private void UninitializeRendering()
        {   ///This function halts the rendering
            Cleanup();
            CompositionTarget.Rendering -= this.CompositionTarget_Rendering;
        }
        #endregion Helpers

        private void DoRender(IntPtr surface, bool isNewSurface)
        {   ///This function renders a surface
            Render(surface, isNewSurface);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {   ///This function is exectued at Window Closure
            this.UninitializeRendering();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {   ///This function is called when the "Render" button is pressed. It is required to stop/start the
            ///rendering of the cube when it is clicked with the cursor 
            ///ALSO, this is the function that is not fully functional as the cube does not begin rendering again
            ///and does not disappear

            if (lastRendered)
            {
                lastRendered = false;
                Cleanup();
            }
            else
            {
                lastRendered = true;
                Init();
            }

        }

        

        private static class NativeMethods
        {   /// This class is used to interlop C# and C++. It tells the runtime to look for a
            /// a function using the calling convention Cdecl in native library D3DVisualization.dll
            /// <summary>
            /// Variable used to track whether the missing dependency dialog has been displayed,
            /// used to prevent multiple notifications of the same failure.
            /// </summary>
            private static bool errorHasDisplayed;

            [DllImport("D3DVisualization.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int Init();

            [DllImport("D3DVisualization.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void Cleanup();

            [DllImport("D3DVisualization.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int Render(IntPtr resourcePointer, bool isNewSurface);

            [DllImport("D3DVisualization.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetCameraRadius(float radius);

            [DllImport("D3DVisualization.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetCameraTheta(float theta);

            [DllImport("D3DVisualization.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int SetCameraPhi(float phi);

            /// <summary>
            /// Method used to invoke an Action that will catch DllNotFoundExceptions and display a warning dialog.
            /// </summary>
            /// <param name="action">The Action to invoke.</param>
            public static void InvokeWithDllProtection(Action action)
            {
                InvokeWithDllProtection(
                    () => 
                    { 
                        action.Invoke();
                        return 0;
                    }); 
            }

            /// <summary>
            /// Method used to invoke A Func that will catch DllNotFoundExceptions and display a warning dialog.
            /// </summary>
            /// <param name="func">The Func to invoke.</param>
            /// <returns>The return value of func, or default(T) if a DllNotFoundException was caught.</returns>
            /// <typeparam name="T">The return type of the func.</typeparam>
            public static T InvokeWithDllProtection<T>(Func<T> func)
            {
                try
                {
                    return func.Invoke();
                }
                catch (DllNotFoundException e)
                {
                    if (!errorHasDisplayed)
                    {
                        MessageBox.Show("This sample requires:\nManual build of the D3DVisualization project, which requires installation of Windows 10 SDK or DirectX SDK.\n" +
                                        "Installation of the DirectX runtime on non-build machines.\n\n"+
                                        "Detailed exception message: " + e.Message, "WPF D3D11 Interop",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                        errorHasDisplayed = true;

                        if (Application.Current != null)
                        {
                            Application.Current.Shutdown();                            
                        }
                    }
                }

                return default(T);
            }
        }

        



    }
}
