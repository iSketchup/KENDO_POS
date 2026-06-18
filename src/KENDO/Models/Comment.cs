using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Serilog;

namespace Main.Models;

// DTOs matching the FastAPI Pydantic models

public class Comment
{
    public string CommentText { get; set; }
    public string CommentAuthor { get; set; }
   

    public Comment(string commentText, string commentAuthor)
    {
        CommentText = commentText;
        CommentAuthor = commentAuthor;
    }
}
