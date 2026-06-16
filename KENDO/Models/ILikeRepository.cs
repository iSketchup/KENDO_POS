using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Serilog;

namespace Main.Models;

// DTOs matching the FastAPI Pydantic models
public class LikeCreateDto
{
    public int user_id { get; set; }
    public int shader_id { get; set; }
}

public class LikeStatusDto
{
    public int amount { get; set; }
    public bool liked_by_u { get; set; }
}

// Domain model used elsewhere in the app
public class LikeStatus
{
    public int Amount { get; set; }
    public bool LikedByUser { get; set; }

    public LikeStatus(int amount, bool likedByUser)
    {
        Amount = amount;
        LikedByUser = likedByUser;
    }
}

public interface ILikeRepository
{
    Task<LikeStatus> GetLikes(int userId, int shaderId);
    Task ToggleLike(int userId, int shaderId);
    Task RemoveLike(int userId, int shaderId);
}

public class LikeRepositoryRest : ILikeRepository
{
    private HttpClient client;

    // Dependency Injection
    public LikeRepositoryRest(HttpClient client)
    {
        this.client = client;
    }

    public async Task<LikeStatus> GetLikes(int userId, int shaderId)
    {
        var dto = await client.GetFromJsonAsync<LikeStatusDto>($"{userId}/{shaderId}/likes/");

        return new LikeStatus(dto.amount, dto.liked_by_u);
    }

    public async Task ToggleLike(int userId, int shaderId)
    {
        var dto = new LikeCreateDto { user_id = userId, shader_id = shaderId };

        var result = await client.PostAsJsonAsync($"{userId}/{shaderId}/likes/", dto);

        var body = await result.Content.ReadAsStringAsync();

        if (!result.IsSuccessStatusCode)
        {
            Log.Logger.Error("POST like failed {StatusCode}: {Body}", result.StatusCode, body);
        }

        result.EnsureSuccessStatusCode();
    }

    public async Task RemoveLike(int userId, int shaderId)
    {
        var dto = new LikeCreateDto { user_id = userId, shader_id = shaderId };

        // HttpClient.DeleteAsync has no body overload, but your endpoint
        // requires a LikesCreate body, so we build the request manually.
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{userId}/{shaderId}/likes/")
        {
            Content = JsonContent.Create(dto)
        };

        var result = await client.SendAsync(request);

        var body = await result.Content.ReadAsStringAsync();

        if (!result.IsSuccessStatusCode)
        {
            Log.Logger.Error("DELETE like failed {StatusCode}: {Body}", result.StatusCode, body);
        }

        result.EnsureSuccessStatusCode();
    }
}

public class LikeRepositoryFake : ILikeRepository
{
    private List<(int UserId, int ShaderId)> likes = new()
    {
        (1, 1),
        (2, 1),
        (1, 2),
    };

    public Task<LikeStatus> GetLikes(int userId, int shaderId)
    {
        var amount = likes.Count(l => l.ShaderId == shaderId);
        var likedByUser = likes.Any(l => l.ShaderId == shaderId && l.UserId == userId);

        return Task.FromResult(new LikeStatus(amount, likedByUser));
    }

    public Task ToggleLike(int userId, int shaderId)
    {
        likes.RemoveAll(l => l.UserId == userId && l.ShaderId == shaderId);
        likes.Add((userId, shaderId));

        return Task.CompletedTask;
    }

    public Task RemoveLike(int userId, int shaderId)
    {
        likes.RemoveAll(l => l.UserId == userId && l.ShaderId == shaderId);

        return Task.CompletedTask;
    }
}