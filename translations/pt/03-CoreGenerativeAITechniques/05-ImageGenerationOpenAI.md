# Geração de Imagens com Azure OpenAI

Nesta lição, exploraremos como usar o Azure OpenAI para gerar imagens usando o DALL-E em suas aplicações .NET. A geração de imagens permite criar imagens originais baseadas em descrições textuais, abrindo possibilidades criativas para várias aplicações.

---

## Introdução

[![Geração de Imagens com Azure OpenAI](https://img.youtube.com/vi/ru3U8MHbFFI/0.jpg)](https://youtu.be/ru3U8MHbFFI?feature=shared)

_⬆️ Clique na imagem para assistir ao vídeo ⬆️_

A IA de geração de imagens permite criar imagens originais a partir de descrições textuais ou prompts. Usando serviços como o DALL-E através do Azure OpenAI, você pode especificar exatamente o que deseja ver em uma imagem, incluindo estilo, composição, objetos e mais. Isso pode ser útil para criar ilustrações, arte conceitual, mockups de design e outros conteúdos visuais.

## Geração de Imagens com Azure OpenAI

Vamos explorar como gerar imagens usando o Azure OpenAI em uma aplicação .NET:

```csharp
var client = new OpenAIClient(
    new Uri("Seu endpoint Azure OpenAI"), 
    new AzureKeyCredential("Sua chave de API do Azure OpenAI"));

ImageGenerationOptions imageGenerationOptions = new()
{
    DeploymentName = "dalle3", // O nome da sua implantação do modelo DALL-E no Azure OpenAI
    Prompt = "Um gatinho sentado sob o luar, arte digital",
    Size = ImageSize.Size1024x1024,
    Quality = ImageGenerationQuality.Standard,
    Style = ImageGenerationStyle.Natural,
};

Response<ImageGenerations> imageGenerations = await client.GetImageGenerationsAsync(imageGenerationOptions);
Uri imageUri = imageGenerations.Value.Data[0].Url;
```

### Aplicação de Exemplo

No exemplo [ImageGeneration-01](./src/ImageGeneration-01), implementamos uma aplicação de console que gera imagens baseadas em um prompt de texto usando o modelo DALL-E.

## Próximo Passo

👉 [Vamos executar modelos localmente com AI Toolkit, Docker e Foundry Local!](../../../03-CoreGenerativeAITechniques/06-LocalModelRunners.md)

**Aviso Legal**:  
Este documento foi traduzido utilizando serviços de tradução baseados em IA. Embora nos esforcemos para garantir a precisão, esteja ciente de que traduções automatizadas podem conter erros ou imprecisões. O documento original em seu idioma nativo deve ser considerado a fonte oficial. Para informações críticas, recomenda-se a tradução profissional humana. Não nos responsabilizamos por quaisquer mal-entendidos ou interpretações equivocadas decorrentes do uso desta tradução.