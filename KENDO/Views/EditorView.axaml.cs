using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;
using AvaloniaEdit.TextMate;
using Main.ViewModels;
using TextMateSharp.Grammars;
using TextMateSharp.Internal.Grammars.Reader;
using TextMateSharp.Internal.Types;
using TextMateSharp.Registry;
using TextMateSharp.Themes;

namespace Main.Views;

public partial class EditorView : UserControl
{
    public EditorView()
    {
        InitializeComponent();
        
        // Base implementationn von TextMate
        var _textEditor = this.FindControl<TextEditor>("Editor");

        var _registryOptions = new RegistryOptions(ThemeName.Abbys);

        var _textMateInstallation = _textEditor.InstallTextMate(_registryOptions);

        _textMateInstallation.SetGrammar(
            _registryOptions.GetScopeByLanguageId(_registryOptions.GetLanguageByExtension(".c").Id));
    }


} 

