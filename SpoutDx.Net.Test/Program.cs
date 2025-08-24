using System.Numerics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Veldrid;
using Veldrid.SPIRV;
using Veldrid.StartupUtilities;

namespace SpoutDx.Net.Test;

internal class Program
{
    static void Main()
    {
        // Create the window + graphics device
        VeldridStartup.CreateWindowAndGraphicsDevice(
            new WindowCreateInfo(100, 100, 1920, 1080, WindowState.Maximized, "Veldrid Texture Demo"),
            new GraphicsDeviceOptions(true, null, true),
            out var window,
            out var gd
        );

        // Initialize SpoutDX
        var spoutDx = Interop.SpoutDx.SpoutDx_Create(gd.GetD3D11Info().Device);
        Interop.SpoutDx.SpoutDx_SetReceiverName(spoutDx, "VTubeStudioSpout");

        ResourceFactory factory = gd.ResourceFactory;
        var cl = factory.CreateCommandList();

        // // Load image
        // Image<Rgba32> img = Image.Load<Rgba32>("texture.jpg");
        // uint width = (uint)img.Width;
        // uint height = (uint)img.Height;
        // byte[] pixelBytes = new byte[width * height * 4];
        // img.CopyPixelDataTo(pixelBytes);

        // Create texture + view
        // Texture tex = factory.CreateTexture(TextureDescription.Texture2D(width, height, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Sampled));
        // gd.UpdateTexture(tex, pixelBytes, 0, 0, 0, width, height, 1, 0, 0);
        // TextureView texView = factory.CreateTextureView(tex);

        if (!Interop.SpoutDx.SpoutDx_ReceiveTexture(spoutDx))
        {
            Console.WriteLine("Could not receive texture!");

            return;
        }

        var spoutReceivedTexture = Interop.SpoutDx.SpoutDx_GetSenderTexture(spoutDx);

        var format = Interop.SpoutDx.SpoutDx_GetSenderFormat(spoutDx);
        var width = Interop.SpoutDx.SpoutDx_GetSenderWidth(spoutDx);
        var height = Interop.SpoutDx.SpoutDx_GetSenderHeight(spoutDx);

        Console.WriteLine($"Texture info: {format}, {width}x{height}");

        var tex = factory.CreateTexture((ulong) spoutReceivedTexture,
            TextureDescription.Texture2D(
                width,
                height,
                1,
                1,
                PixelFormat.R8_G8_B8_A8_UNorm,
                TextureUsage.Sampled
            )
        );

        Console.WriteLine("Successfully created Veldrid texture");

        // Create sampler
        Sampler sampler = factory.CreateSampler(SamplerDescription.Linear);

        // Create quad vertex/index buffers
        Vertex[] vertices =
        {
            new Vertex(new Vector2(-1,  1), new Vector2(0, 0)),
            new Vertex(new Vector2( 1,  1), new Vector2(1, 0)),
            new Vertex(new Vector2(-1, -1), new Vector2(0, 1)),
            new Vertex(new Vector2( 1, -1), new Vector2(1, 1))
        };
        ushort[] indices = { 0, 1, 2, 3, 2, 1 };

        DeviceBuffer vb = factory.CreateBuffer(new BufferDescription((uint)(vertices.Length * Vertex.SizeInBytes), BufferUsage.VertexBuffer));
        gd.UpdateBuffer(vb, 0, vertices);
        DeviceBuffer ib = factory.CreateBuffer(new BufferDescription((uint)(indices.Length * sizeof(ushort)), BufferUsage.IndexBuffer));
        gd.UpdateBuffer(ib, 0, indices);

        // Resource layout and set
        ResourceLayout layout = factory.CreateResourceLayout(new ResourceLayoutDescription(
            new ResourceLayoutElementDescription("tex", ResourceKind.TextureReadOnly, ShaderStages.Fragment),
            new ResourceLayoutElementDescription("texSampler", ResourceKind.Sampler, ShaderStages.Fragment)
        ));

        // Shaders
        string vertCode = @"
#version 450
layout(location = 0) in vec2 Position;
layout(location = 1) in vec2 TexCoord;
layout(location = 0) out vec2 fsin_TexCoord;
void main()
{
    fsin_TexCoord = TexCoord;
    gl_Position = vec4(Position, 0, 1);
}";
        string fragCode = @"
#version 450
layout(location = 0) in vec2 fsin_TexCoord;
layout(location = 0) out vec4 fsout_Color;
layout(set = 0, binding = 0) uniform texture2D tex;
layout(set = 0, binding = 1) uniform sampler texSampler;
void main()
{
    fsout_Color = texture(sampler2D(tex, texSampler), fsin_TexCoord);
}";

        Shader[] shaders = factory.CreateFromSpirv(
            new ShaderDescription(ShaderStages.Vertex, System.Text.Encoding.UTF8.GetBytes(vertCode), "main"),
            new ShaderDescription(ShaderStages.Fragment, System.Text.Encoding.UTF8.GetBytes(fragCode), "main")
        );

        // Pipeline
        GraphicsPipelineDescription pd = new GraphicsPipelineDescription
        {
            BlendState = BlendStateDescription.SingleOverrideBlend,
            DepthStencilState = DepthStencilStateDescription.Disabled,
            RasterizerState = new RasterizerStateDescription(FaceCullMode.None, PolygonFillMode.Solid, FrontFace.Clockwise, true, false),
            PrimitiveTopology = PrimitiveTopology.TriangleList,
            ResourceLayouts = new[] { layout },
            ShaderSet = new ShaderSetDescription(
                new[] { new VertexLayoutDescription(
                    new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                    new VertexElementDescription("TexCoord", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2)
                )},
                shaders
            ),
            Outputs = gd.SwapchainFramebuffer.OutputDescription
        };

        Pipeline pipeline = factory.CreateGraphicsPipeline(pd);

        // Main loop
        TextureView texView =  factory.CreateTextureView(tex);
        ResourceSet resourceSet = factory.CreateResourceSet(new ResourceSetDescription(layout, texView, sampler));
        while (window.Exists)
        {
            var snapshot = window.PumpEvents();
            if (!window.Exists) break;

            // SpoutDX
            if (!Interop.SpoutDx.SpoutDx_ReceiveTexture(spoutDx))
            {
                Console.WriteLine("Could not receive texture!");

                return;
            }

            if (Interop.SpoutDx.SpoutDx_IsUpdated(spoutDx))
            {
                Console.WriteLine("Texture has been updated!!!! Should recreate veldrid texture there");
            }

            if (Interop.SpoutDx.SpoutDx_IsFrameNew(spoutDx))
            {
                // Console.WriteLine($"Received new frame: {Interop.SpoutDx.SpoutDx_GetSenderFrame(spoutDx)}");

                // resourceSet.Dispose();

                // texView = factory.CreateTextureView(tex);
                // resourceSet = factory.CreateResourceSet(new ResourceSetDescription(layout, texView, sampler));
            }

            // Draw
            cl.Begin();
            cl.SetFramebuffer(gd.SwapchainFramebuffer);
            cl.ClearColorTarget(0, RgbaFloat.Black);
            cl.SetPipeline(pipeline);
            cl.SetVertexBuffer(0, vb);
            cl.SetIndexBuffer(ib, IndexFormat.UInt16);
            cl.SetGraphicsResourceSet(0, resourceSet);
            cl.DrawIndexed((uint)indices.Length, 1, 0, 0, 0);
            cl.End();
            gd.SubmitCommands(cl);
            gd.SwapBuffers();

            resourceSet.Dispose();
        }

        gd.WaitForIdle();

        Interop.SpoutDx.SpoutDx_ReleaseReceiver(spoutDx);
    }

    struct Vertex
    {
        public Vector2 Position;
        public Vector2 TexCoord;
        public Vertex(Vector2 pos, Vector2 tex) { Position = pos; TexCoord = tex; }
        public const uint SizeInBytes = 16;
    }
}