using System;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Main.Control;

public class GlView : OpenGlControlBase
{
    private float[] vertices =
    {
        0.0f,  0.5f, 0.0f,
        -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f
    };
    private int vao;
    private int vbo;
    private int shaderProgram;
    
    protected override void OnOpenGlInit(GlInterface gl)
    {
        GL.LoadBindings(new AvaloniaBindingsContext(gl));
        
        
        vao = GL.GenVertexArray();
        vbo = GL.GenBuffer();
        
        
        GL.BindVertexArray(vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
        
        GL.BufferData(
            BufferTarget.ArrayBuffer,
            vertices.Length * sizeof(float),
            vertices,
            BufferUsageHint.StaticDraw);
        
        GL.VertexAttribPointer(
            0,
            3,
            VertexAttribPointerType.Float,
            false,
            3 * sizeof(float),
            0);
        
        GL.EnableVertexAttribArray(0);
        
        string vertexShaderSource =
            """
            #version 330 core

            layout (location = 0) in vec3 aPos;

            void main()
            {
                gl_Position = vec4(aPos, 1.0);
            }
            """;
        
        string fragmentShaderSource =
            """
            #version 330 core

            out vec4 FragColor;

            void main()
            {
                FragColor = vec4(1.0, 0.0, 0.0, 1.0);
            }
            """;
        
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);
        
        // Fehler output
        GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int vStatus);
        if (vStatus == 0)
            Console.WriteLine("Vertex shader error: " + GL.GetShaderInfoLog(vertexShader));
        
        
        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);
        
        // Fehler output
        GL.GetProgram(shaderProgram, GetProgramParameterName.LinkStatus, out int lStatus);
        if (lStatus == 0)
            Console.WriteLine("Link error: " + GL.GetProgramInfoLog(shaderProgram));
        
        shaderProgram = GL.CreateProgram();

        GL.AttachShader(shaderProgram, vertexShader);
        GL.AttachShader(shaderProgram, fragmentShader);

        GL.LinkProgram(shaderProgram);
        
        Console.WriteLine("OpenGL initialized");
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        Console.WriteLine("OpenGL destroyed");
    }

    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        GL.ClearColor(0f, 0f, 0f, 1f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.UseProgram(shaderProgram);

        GL.BindVertexArray(vao);

        GL.DrawArrays(
            PrimitiveType.Triangles,
            0,
            3);
        
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