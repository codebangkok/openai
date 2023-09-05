# Azure OpenAI Service

### Website
* [Azure OpenAI Service](https://azure.microsoft.com/en-us/products/cognitive-services/openai-service)
* [Azure OpenAI Service Documentation](https://learn.microsoft.com/en-us/azure/cognitive-services/openai)
* [Azure OpenAI Service REST API reference](https://learn.microsoft.com/en-us/azure/cognitive-services/openai/reference)
* [Semantic Kernel Documentation](https://learn.microsoft.com/en-us/semantic-kernel)
* [Semantic Kernel GitHub](https://github.com/microsoft/semantic-kernel)

### Request Access
* [Azure OpenAI Service](https://aka.ms/oai/access)
* [Azure OpenAI GPT-4 Public Preview](https://aka.ms/oai/get-gpt4)

### .NET Package
* [Azure.AI.OpenAI ](https://www.nuget.org/packages/Azure.AI.OpenAI)
* [Microsoft.SemanticKernel](https://www.nuget.org/packages/Microsoft.SemanticKernel)
* [Microsoft.SemanticKernel.Skills.Web](https://www.nuget.org/packages/Microsoft.SemanticKernel.Skills.Web)

### REST API
Completions
```http
POST https://{your-resource-name}.openai.azure.com/openai/deployments/{deployment-id}/completions?api-version={api-version}
content-type: application/json
api-key: <API-KEY>

{
  "prompt": ""
}
```

#### Chat completions
```http
POST https://{your-resource-name}.openai.azure.com/openai/deployments/{deployment-id}/chat/completions?api-version={api-version}
content-type: application/json
api-key: <API-KEY>

{
    "messages": [
        {
            "role": "user", 
            "content": ""
        }
    ]
}
```