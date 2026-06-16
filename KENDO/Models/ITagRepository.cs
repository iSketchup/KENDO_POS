using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Serilog;

namespace Main.Models;

public class TagCreateDto
{
    public string TagName { get; set; }
}

public class TagResponseDto
{
    public int TagId { get; set; }
    public string TagName { get; set; }
}

public class Tag
{
    public int TagId { get; set; }
    public string TagName { get; set; }

    public Tag(int tagId, string tagName)
    {
        TagId = tagId;
        TagName = tagName;
    }
}

public interface ITagRepository
{
    Task<List<Tag>> GetAllTags();
    Task<Tag> GetTagById(int tagId);
    Task<Tag> CreateTag(string tagName);
    Task DeleteTag(int tagId);
}

public class TagRepositoryRest : ITagRepository
{
    private HttpClient client;

    // Dependency Injection
    public TagRepositoryRest(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<Tag>> GetAllTags()
    {
        var dtos = await client.GetFromJsonAsync<List<TagResponseDto>>("tags/");

        List<Tag> result = new();

        if (dtos != null)
        {
            foreach (var dto in dtos)
            {
                result.Add(new Tag(dto.TagId, dto.TagName));
            }
        }

        return result;
    }

    public async Task<Tag> GetTagById(int tagId)
    {
        var response = await client.GetAsync($"tags/{tagId}");

        if (!response.IsSuccessStatusCode)
        {
            Log.Logger.Error("GET tag failed {StatusCode}", response.StatusCode);
            response.EnsureSuccessStatusCode();
        }

        var dto = await response.Content.ReadFromJsonAsync<TagResponseDto>();

        return dto == null ? null : new Tag(dto.TagId, dto.TagName);
    }

    public async Task<Tag> CreateTag(string tagName)
    {
        var dto = new TagCreateDto { TagName = tagName };

        var result = await client.PostAsJsonAsync("tags/", dto);

        var body = await result.Content.ReadAsStringAsync();

        if (!result.IsSuccessStatusCode)
        {
            Log.Logger.Error("POST tag failed {StatusCode}: {Body}", result.StatusCode, body);
            result.EnsureSuccessStatusCode();
        }

        var created = await result.Content.ReadFromJsonAsync<TagResponseDto>();

        return new Tag(created.TagId, created.TagName);
    }

    public async Task DeleteTag(int tagId)
    {
        var result = await client.DeleteAsync($"tags/{tagId}");

        if (!result.IsSuccessStatusCode)
        {
            var body = await result.Content.ReadAsStringAsync();
            Log.Logger.Error("DELETE tag failed {StatusCode}: {Body}", result.StatusCode, body);
        }

        result.EnsureSuccessStatusCode();
    }
}

public class TagRepositoryFake : ITagRepository
{
    private List<Tag> tags = new()
    {
        new Tag(1, "retro"),
        new Tag(2, "abstract"),
        new Tag(3, "raymarching"),
    };

    public Task<List<Tag>> GetAllTags()
    {
        return Task.FromResult(new List<Tag>(tags));
    }

    public Task<Tag> GetTagById(int tagId)
    {
        var tag = tags.Find(t => t.TagId == tagId);
        return Task.FromResult(tag);
    }

    public Task<Tag> CreateTag(string tagName)
    {
        var newId =  tags[^1].TagId + 1;
        var newTag = new Tag(newId, tagName);
        tags.Add(newTag);
        return Task.FromResult(newTag);
    }

    public Task DeleteTag(int tagId)
    {
        tags.RemoveAll(t => t.TagId == tagId);
        return Task.CompletedTask;
    }
}