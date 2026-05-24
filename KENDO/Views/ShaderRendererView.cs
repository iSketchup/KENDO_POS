using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Main.ViewModels;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Main.Control;

public class ShaderRendererView : OpenGlControlBase
{
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
        


        string vertexShaderSource = """
                                    #version 330 core
                                    layout (location = 0) in vec3 aPosition;
                                    out vec2 pos;
                                    void main()
                                    {
                                        pos = aPosition.xy;
                                        gl_Position = vec4(aPosition, 1.0);
                                    }
                                    """;

        string fragmentShaderSource = """
                                      #version 330 core
                                      
                                      in vec2 pos;
                                      
                                      uniform float uTime;
                                      
                                      out vec4 FragColor;
                                      
                                      void main()
                                      {
                                          float pulse = sin(uTime) * 0.5 + 0.5;
                                      
                                          float r = (pos.x + 1.0) * 0.5;
                                          float g = (pos.y + 1.0) * 0.5;
                                      
                                          FragColor = vec4(r * pulse, g * pulse, 0.9 - r * pulse, 1.0);
                                      }
                                      """;
        
        int VertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(VertexShader, vertexShaderSource);
        
        int FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(FragmentShader, fragmentShaderSource);
        
        GL.CompileShader(VertexShader);

        GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int vsuccess);
        if (vsuccess == 0)
        {
            string infoLog = GL.GetShaderInfoLog(VertexShader);
            Console.WriteLine(infoLog);
        }

        GL.CompileShader(FragmentShader);

        GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int fsuccess);
        if (fsuccess == 0)
        {
            string infoLog = GL.GetShaderInfoLog(FragmentShader);
            Console.WriteLine(infoLog);
        }
        
        shaderProgramm = GL.CreateProgram();
        GL.AttachShader(shaderProgramm, VertexShader);
        GL.AttachShader(shaderProgramm, FragmentShader);
        
        GL.LinkProgram(shaderProgramm);
        
        GL.GetProgram(shaderProgramm, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(shaderProgramm);
            Console.WriteLine(infoLog);
        }
        
        GL.DetachShader(shaderProgramm, VertexShader);
        GL.DetachShader(shaderProgramm, FragmentShader);
        GL.DeleteShader(FragmentShader);
        GL.DeleteShader(VertexShader);
        
        
        uTime = GL.GetUniformLocation(shaderProgramm, "uTime");
        
        _timer.Start();

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