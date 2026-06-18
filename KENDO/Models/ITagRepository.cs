using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Serilog;

namespace Main.Models;

public class TagCreateDto
{
    [System.Text.Json.Serialization.JsonPropertyName("TagName")]
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
    Task DeleteTagById(int tagId);
    Task DeleteTagByName(string tagName, int userId, int shaderId);
    Task CreateAndAssignTag(string tagName, int shaderId, int userId);
}

public class TagRepositoryRest : ITagRepository
{
    private HttpClient client;
    
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

    public async Task DeleteTagById(int tagId)
    {
        var result = await client.DeleteAsync($"tags/{tagId}");

        if (!result.IsSuccessStatusCode)
        {
            var body = await result.Content.ReadAsStringAsync();
            Log.Logger.Error("DELETE tag failed {StatusCode}: {Body}", result.StatusCode, body);
        }

        result.EnsureSuccessStatusCode();
    }
    public async Task DeleteTagByName(string tagName, int userId, int shaderId)
    {
        var result = await client.DeleteAsync($"{userId}/shaders/shadertag/{shaderId}/{tagName}");

        if (!result.IsSuccessStatusCode)
        {
            var body = await result.Content.ReadAsStringAsync();
            Log.Logger.Error("DELETE shadertag failed {StatusCode}: {Body}", result.StatusCode, body);
        }

        result.EnsureSuccessStatusCode();
    }
    
    //AI:  How could i create and assign a tag with one methode

    public async Task CreateAndAssignTag(string tagName, int shaderId, int userId)
    {
        Tag? tag;

        var createResult = await client.PostAsJsonAsync("tags/", new TagCreateDto { TagName = tagName });
        var body = await createResult.Content.ReadAsStringAsync();

        if (!createResult.IsSuccessStatusCode)
        {
            Log.Logger.Error("POST tag failed {StatusCode}: {Body}", createResult.StatusCode, body);
            createResult.EnsureSuccessStatusCode();
        }

        var created = await createResult.Content.ReadFromJsonAsync<TagResponseDto>();
        tag = new Tag(created.TagId, created.TagName);
        
        var linkResult = await client.PostAsync(
            $"{userId}/shaders/shadertag/{shaderId}/{tag.TagId}",
            null);

        if (!linkResult.IsSuccessStatusCode)
        {
            body = await linkResult.Content.ReadAsStringAsync();
            Log.Logger.Error("POST shadertag failed {StatusCode}: {Body}", linkResult.StatusCode, body);
            linkResult.EnsureSuccessStatusCode();
        }
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

    public Task DeleteTagById(int tagId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTagByName(string tagName, int userId, int shaderId)
    {
        throw new NotImplementedException();
    }
    
    public Task  CreateAndAssignTag(string tagName, int shaderId, int userId)
    {
        throw new NotImplementedException();
    }
}