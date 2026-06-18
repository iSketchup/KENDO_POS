using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Serilog;


namespace Main.Models;

public partial class CommentCreateDto
{
    [System.Text.Json.Serialization.JsonPropertyName("CommentText")]
    public string CommentText { get; set; }
}

public class CommentResponseDto
{
    public string CommentText { get; set; }
    public string CommentAuthor { get; set; }
}


public interface ICommentRepository
{
    Task<List<Comment>> GetComments(int userId, int shaderId);
    Task AddComment(int userId, int shaderId, string commentText);
    Task DeleteComment(int userId, int shaderId, int commentId);
}



public class CommentRepositoryRest : ICommentRepository
{
    private HttpClient client;

    public CommentRepositoryRest(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<Comment>> GetComments(int userId, int shaderId)
    {
        var dtos = await client.GetFromJsonAsync<List<CommentResponseDto>>($"{userId}/{shaderId}/comments/");

        List<Comment> result = new();

        if (dtos != null)
        {
            foreach (var dto in dtos)
            {
                result.Add(new Comment(dto.CommentText, dto.CommentAuthor));
            }
        }

        return result;
    }

    public async Task AddComment(int userId, int shaderId, string commentText)
    {
        var dto = new CommentCreateDto { CommentText = commentText };

        var result = await client.PostAsJsonAsync($"{userId}/{shaderId}/comments/", dto);

        var body = await result.Content.ReadAsStringAsync();

        if (!result.IsSuccessStatusCode)
        {
            Log.Logger.Error("POST comment failed {StatusCode}: {Body}", result.StatusCode, body);
        }

        result.EnsureSuccessStatusCode();

    }

    public async Task DeleteComment(int userId, int shaderId, int commentId)
    {
        var result = await client.DeleteAsync($"{userId}/{shaderId}/comments/{commentId}");

        if (!result.IsSuccessStatusCode)
        {
            var body = await result.Content.ReadAsStringAsync();
            Log.Logger.Error("DELETE comment failed {StatusCode}: {Body}", result.StatusCode, body);
        }

        result.EnsureSuccessStatusCode();
    }
}

public class CommentRepositoryFake : ICommentRepository
{
    private List<Comment> comments = new()
    {
        new Comment("love the colors here", "Dino_Fan_42"),
        new Comment("this melted my GPU", "GLSL_Goblin"),
        new Comment("underrated shader", "Dino_Fan_42"),
    };

    public Task<List<Comment>> GetComments(int userId, int shaderId)
    {
        return Task.FromResult(new List<Comment>(comments));
    }

    public Task AddComment(int userId, int shaderId, string commentText)
    {
        comments.Add(new Comment(commentText, "You"));
        return Task.CompletedTask;
    }

    public Task DeleteComment(int userId, int shaderId, int commentId)
    {
        throw new NotSupportedException(
            "DeleteComment can't target a specific fake comment until CommentId is restored on the Comment model.");
    }
}