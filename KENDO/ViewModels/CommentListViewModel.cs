using System.Collections.Generic;
using System.Collections.ObjectModel;
using Main.Models;

namespace Main.ViewModels;

public class CommentListViewModel : ViewModelBase
{
    public ObservableCollection<Comment> comments { get; set; }

    public CommentListViewModel()
    {
        comments = new ObservableCollection<Comment>()
        {
            new Comment("Test", "Test"),
            new Comment("Test2", "Test2"),
        };
    }
}