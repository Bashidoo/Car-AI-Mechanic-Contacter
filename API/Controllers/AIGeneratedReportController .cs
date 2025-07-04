using CarDealership.Application.Common.Results;
using CarDealership.Application.Dtos.AIGeneratedReportDtos;
using Microsoft.AspNetCore.Mvc;

using System.Text;
using System.Text.Json;

[ApiController]
[Route("api/[controller]")]
public class AIGeneratedReportController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AIGeneratedReportController> _logger;

    public AIGeneratedReportController(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        ILogger<AIGeneratedReportController> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("generate")]
    public async Task<ActionResult<OperationResult<string>>> Generate([FromBody] AIGeneratedReportRequestDto request)
    {
        var apiKey = _configuration["OneMinAI:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return BadRequest(OperationResult<string>.Fail("API key is missing"));
        }

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("API-KEY", apiKey);

        // Step 1: Create conversation
        var convBody = new
        {
            type = "CHAT_WITH_AI",
            model = "gpt-4o", // Or dynamic
            title = "AI Report"
        };

        var convRes = await _httpClient.PostAsync(
            "https://api.1min.ai/api/conversations",
            new StringContent(JsonSerializer.Serialize(convBody), Encoding.UTF8, "application/json"));

        if (!convRes.IsSuccessStatusCode)
        {
            var error = await convRes.Content.ReadAsStringAsync();
            _logger.LogError("Conversation creation failed: {Error}", error);
            return StatusCode((int)convRes.StatusCode, OperationResult<string>.Fail("Failed to create conversation"));
        }

        var convJson = JsonDocument.Parse(await convRes.Content.ReadAsStringAsync());
        var convId = convJson.RootElement.GetProperty("conversation").GetProperty("uuid").GetString();

        var instruction = "The User is supposed to describe u a car problem. U will generate a proffesional swedish email for a car mechanics to suggest a price class and contact information. User Prompt will be written after double brackets (/). No allowed formating or changing structure // \\n";
        var fullPrompt = $"{instruction} // \n (( {request.Prompt} ))";

        var promptPayload = new
        {
            type = "CHAT_WITH_AI",
            model = "gpt-4o",
            conversationId = convId,
            promptObject = new { prompt = fullPrompt }
        };

        var msgRes = await _httpClient.PostAsync(
            "https://api.1min.ai/api/features",
            new StringContent(JsonSerializer.Serialize(promptPayload), Encoding.UTF8, "application/json"));

        if (!msgRes.IsSuccessStatusCode)
        {
            var error = await msgRes.Content.ReadAsStringAsync();
            _logger.LogError("Prompt sending failed: {Error}", error);
            return StatusCode((int)msgRes.StatusCode, OperationResult<string>.Fail("Failed to get AI response"));
        }

        var replyJson = JsonDocument.Parse(await msgRes.Content.ReadAsStringAsync());
        var result = replyJson.RootElement
            .GetProperty("aiRecord")
            .GetProperty("aiRecordDetail")
            .GetProperty("resultObject")[0]
            .GetString();

        return Ok(OperationResult<string>.Ok(result));
    }
}
