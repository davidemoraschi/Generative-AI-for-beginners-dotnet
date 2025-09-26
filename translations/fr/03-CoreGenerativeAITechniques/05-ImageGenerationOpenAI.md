# Génération d'images avec Azure OpenAI

Dans cette leçon, nous allons explorer comment utiliser Azure OpenAI pour générer des images en utilisant DALL-E dans vos applications .NET. La génération d'images vous permet de créer des images originales basées sur des descriptions textuelles, ouvrant des possibilités créatives pour diverses applications.

---

## Introduction

[![Génération d'images avec Azure OpenAI](https://img.youtube.com/vi/ru3U8MHbFFI/0.jpg)](https://youtu.be/ru3U8MHbFFI?feature=shared)

_⬆️ Cliquez sur l'image pour regarder la vidéo ⬆️_

L'IA de génération d'images vous permet de créer des images originales à partir de descriptions textuelles ou prompts. En utilisant des services comme DALL-E via Azure OpenAI, vous pouvez spécifier exactement ce que vous voulez voir dans une image, y compris le style, la composition, les objets, et plus encore. Cela peut être utile pour créer des illustrations, des concepts artistiques, des maquettes de design, et d'autres contenus visuels.

## Génération d'images avec Azure OpenAI

Voyons comment générer des images en utilisant Azure OpenAI dans une application .NET :

```csharp
var client = new OpenAIClient(
    new Uri("Votre point de terminaison Azure OpenAI"), 
    new AzureKeyCredential("Votre clé API Azure OpenAI"));

ImageGenerationOptions imageGenerationOptions = new()
{
    DeploymentName = "dalle3", // Le nom de votre déploiement de modèle DALL-E sur Azure OpenAI
    Prompt = "Un chaton assis sous la lumière de la lune, art numérique",
    Size = ImageSize.Size1024x1024,
    Quality = ImageGenerationQuality.Standard,
    Style = ImageGenerationStyle.Natural,
};

Response<ImageGenerations> imageGenerations = await client.GetImageGenerationsAsync(imageGenerationOptions);
Uri imageUri = imageGenerations.Value.Data[0].Url;
```

### Application exemple

Dans l'exemple [ImageGeneration-01](./src/ImageGeneration-01), nous avons implémenté une application console qui génère des images basées sur un prompt textuel en utilisant le modèle DALL-E.

## Prochaine étape

👉 [Exécutons des modèles localement avec AI Toolkit, Docker et Foundry Local !](../../../03-CoreGenerativeAITechniques/06-LocalModelRunners.md)

**Avertissement** :  
Ce document a été traduit à l'aide de services de traduction automatisée basés sur l'intelligence artificielle. Bien que nous fassions de notre mieux pour garantir l'exactitude, veuillez noter que les traductions automatiques peuvent contenir des erreurs ou des inexactitudes. Le document original dans sa langue d'origine doit être considéré comme la source faisant autorité. Pour des informations critiques, il est recommandé de recourir à une traduction humaine professionnelle. Nous déclinons toute responsabilité en cas de malentendus ou d'interprétations erronées résultant de l'utilisation de cette traduction.