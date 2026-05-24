using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Main.ViewModels;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Main.Views;

public class GLView : OpenGlControlBase
{
    private bool _glInitialzed;
    private bool _needsReload;
    public static readonly StyledProperty<string> FragmentShaderInProperty =
        AvaloniaProperty.Register<GLView, string>(nameof(FragmentShaderIn));

    public string FragmentShaderIn
    {
        get => GetValue(FragmentShaderInProperty);
        set => SetValue(FragmentShaderInProperty, value);
    }
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == FragmentShaderInProperty)
        {
            queNewReload();
        }
    }
    
    private Stopwatch _timer = new Stopwatch();
    
    // unioforms:
    private int uTime;
    
    float[] vertices = {
        0.5f,  0.5f, 0.0f,  // top right
        0.5f, -0.5f, 0.0f,  // bottom right
        -0.5f, -0.5f, 0.0f,  // bottom left
        -0.5f,  0.5f, 0.0f   // top left
    };

    private uint[] indicies =
    {
        0, 1, 2,
        0, 2, 3,
    };

    private int vertexBufferObject;
    private int vertexArrayObject;
    private int elementBufferObject;

    private string vertexShaderSource = """
                                #version 330 core
                                layout (location = 0) in vec3 aPosition;
                                out vec2 pos;
                                void main()
                                {
                                    pos = aPosition.xy;
                                    gl_Position = vec4(aPosition, 1.0);
                                }
                                """;
    
    private int shaderProgramm;
    
    protected override void OnOpenGlInit(GlInterface gl)
    {

        

        GL.LoadBindings(new AvaloniaBindingsContext(gl));
        
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        
        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);
        
        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject); 
        GL.BufferData(
            BufferTarget.ArrayBuffer, 
            vertices.Length * sizeof(float), 
            vertices, 
            BufferUsageHint.StaticDraw); 
        // ToDo: dynamic draw for hot reloads later

        
        elementBufferObject =  GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject); 
        GL.BufferData(
            BufferTarget.ElementArrayBuffer, 
            indicies.Length * sizeof(uint), 
            indicies, 
            BufferUsageHint.StaticDraw); 
        // ToDo: dynamic draw for hot reloads later

      
        
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        


        _glInitialzed = true;

        Reload();
        

        Console.WriteLine("OpenGL initialized");
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        
        GL.DeleteBuffer(vertexBufferObject);
        GL.DeleteVertexArray(vertexArrayObject);
        GL.DeleteProgram(shaderProgramm);

        Console.WriteLine("OpenGL destroyed");
        
       
    }

    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        
        if (_needsReload)
        {
            _needsReload = false;
            Reload();
        }
        
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, fb);
        
        GL.Viewport(0, 0, (int)Bounds.Width, (int)Bounds.Height);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.UseProgram(shaderProgramm);
        
        float timeValue = (float)_timer.Elapsed.TotalSeconds;
        GL.Uniform1(uTime, timeValue);
        
        GL.BindVertexArray(vertexArrayObject);

        GL.DrawElements(PrimitiveType.Triangles, indicies.Length, DrawElementsType.UnsignedInt, 0);
        
        RequestNextFrameRendering();
        
    }

    private void Reload()
    {
        if (!_glInitialzed) return;

        string fragmentShaderSource = FragmentShaderIn;
        
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        
        GL.CompileShader(vertexShader);

        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int vsuccess);
        if (vsuccess == 0)
        {
            string infoLog = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine(infoLog);
        }

        GL.CompileShader(fragmentShader);

        GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int fsuccess);
        if (fsuccess == 0)
        {
            string infoLog = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(infoLog);
        }
        

        int newProgram = GL.CreateProgram();
        
        GL.AttachShader(newProgram, vertexShader);
        GL.AttachShader(newProgram, fragmentShader);
        
        GL.LinkProgram(newProgram);

        GL.GetProgram(newProgram, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            GL.DeleteProgram(newProgram); 
            return;                       
        }

        if (shaderProgramm != 0)
            GL.DeleteProgram(shaderProgramm);
    
        
        
        shaderProgramm = newProgram;

        GL.DetachShader(newProgram, vertexShader);
        GL.DetachShader(newProgram, fragmentShader);
        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
        
        uTime = GL.GetUniformLocation(shaderProgramm, "uTime");
        _timer.Restart();
    }

    private void queNewReload()
    {
        
        if (!_glInitialzed) return;
        _needsReload = true;
        RequestNextFrameRendering();
    }
}


public class AvaloniaBindingsContext : IBindingsContext
{
    private readonly GlInterface _gl;

    public AvaloniaBindingsContext(GlInterface gl)
    {
        _gl = gl;
    }

    public nint GetProcAddress(string procName)
    {
        return _gl.GetProcAddress(procName);
    }
}