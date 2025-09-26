# Generación de imágenes con Azure OpenAI

En esta lección, exploraremos cómo usar Azure OpenAI para generar imágenes con DALL-E en sus aplicaciones .NET. La generación de imágenes le permite crear imágenes originales basadas en descripciones textuales, abriendo posibilidades creativas para diversas aplicaciones.

---

## Introducción

[![Generación de imágenes con Azure OpenAI](https://img.youtube.com/vi/ru3U8MHbFFI/0.jpg)](https://youtu.be/ru3U8MHbFFI?feature=shared)

_⬆️ Haga clic en la imagen para ver el video ⬆️_

La IA de generación de imágenes le permite crear imágenes originales a partir de descripciones textuales o prompts. Utilizando servicios como DALL-E a través de Azure OpenAI, puede especificar exactamente lo que desea ver en una imagen, incluyendo estilo, composición, objetos y más. Esto puede ser útil para crear ilustraciones, arte conceptual, maquetas de diseño y otro contenido visual.

## Generación de imágenes con Azure OpenAI

Veamos cómo generar imágenes utilizando Azure OpenAI en una aplicación .NET:

```csharp
var client = new OpenAIClient(
    new Uri("Su punto final de Azure OpenAI"), 
    new AzureKeyCredential("Su clave API de Azure OpenAI"));

ImageGenerationOptions imageGenerationOptions = new()
{
    DeploymentName = "dalle3", // El nombre de su implementación del modelo DALL-E en Azure OpenAI
    Prompt = "Un gatito sentado bajo la luz de la luna, arte digital",
    Size = ImageSize.Size1024x1024,
    Quality = ImageGenerationQuality.Standard,
    Style = ImageGenerationStyle.Natural,
};

Response<ImageGenerations> imageGenerations = await client.GetImageGenerationsAsync(imageGenerationOptions);
Uri imageUri = imageGenerations.Value.Data[0].Url;
```

### Aplicación de ejemplo

En el ejemplo [ImageGeneration-01](./src/ImageGeneration-01) hemos implementado una aplicación de consola que genera imágenes basadas en un prompt de texto utilizando el modelo DALL-E.

## Siguiente paso

👉 [¡Ejecutemos modelos localmente con AI Toolkit, Docker y Foundry Local!](../../../03-CoreGenerativeAITechniques/06-LocalModelRunners.md)

**Descargo de responsabilidad**:  
Este documento ha sido traducido utilizando servicios de traducción automática basados en inteligencia artificial. Si bien nos esforzamos por garantizar la precisión, tenga en cuenta que las traducciones automáticas pueden contener errores o imprecisiones. El documento original en su idioma nativo debe considerarse como la fuente autorizada. Para información crítica, se recomienda una traducción profesional realizada por humanos. No nos hacemos responsables de malentendidos o interpretaciones erróneas que puedan surgir del uso de esta traducción.