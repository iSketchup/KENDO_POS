namespace Main.ViewModels;

public class EditorViewModel : ViewModelBase
{
    public string Code { get; set; } = """
                                       #version 330

                                       out vec4 outputColor;

                                       in vec2 TexCoord;
                                       
                                       uniform sampler2D texture0;
                                       uniform sampler2D texture1;

                                       void main()
                                       {
                                            vec4 tex0 = texture(texture0, TexCoord);
                                            vec4 tex1 = texture(texture1, TexCoord);
                                            outputColor = mix(tex0,tex1,0.3);
                                       }
                                       """;
}