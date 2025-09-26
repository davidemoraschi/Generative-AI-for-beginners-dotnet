# Bildgenerierung mit Azure OpenAI

In dieser Lektion werden wir lernen, wie man mit Azure OpenAI und DALL-E Bilder erstellen kann.

---

## Einführung

Die Bildgenerierung mit generativer KI hat in den letzten Jahren einen enormen Fortschritt gemacht. Mit Tools wie DALL-E können Sie jetzt Bilder aus Texten generieren. In dieser Lektion werden wir zeigen, wie man mit DALL-E Bilder in einer .NET-Anwendung erstellt.

## Videoanleitung

[![Video zur Bildgenerierung mit DALL-E](https://img.youtube.com/vi/ru3U8MHbFFI/0.jpg)](https://youtu.be/ru3U8MHbFFI?feature=shared)

_⬆️ Klicken Sie auf das Bild, um das Video anzusehen ⬆️_

## Bildgenerierung mit DALL-E

DALL-E ist ein Modell von OpenAI, das Bilder aus Textbeschreibungen erstellen kann. Es ermöglicht Ihnen, visuellen Inhalt auf kreative Weise zu generieren.

### Bildgenerierung mit Azure OpenAI

Sie können das DALL-E-Modell über die Azure OpenAI-API in Ihrer .NET-Anwendung verwenden:

```csharp
var client = new OpenAIClient(
    new Uri("Your Azure OpenAI Endpoint"), 
    new AzureKeyCredential("Your Azure OpenAI API Key"));

ImageGenerationOptions imageGenerationOptions = new()
{
    DeploymentName = "dalle3", // Der Name Ihrer DALL-E-Modelldimension auf Azure OpenAI
    Prompt = "Ein süßes Kätzchen, das im Mondschein sitzt, digitale Kunst",
    Size = ImageSize.Size1024x1024,
    Quality = ImageGenerationQuality.Standard,
    Style = ImageGenerationStyle.Natural,
};

Response<ImageGenerations> imageGenerations = await client.GetImageGenerationsAsync(imageGenerationOptions);
Uri imageUri = imageGenerations.Value.Data[0].Url;
```

### Beispielanwendung

In dem [ImageGeneration-01](./src/ImageGeneration-01)-Beispiel haben wir eine Konsolenanwendung implementiert, die Bilder basierend auf einer Textaufforderung mit dem DALL-E-Modell generiert.

## Als nächstes

👉 [Lasst uns lokale Modelle mit AI Toolkit, Docker und Foundry Local ausführen!](../../../03-CoreGenerativeAITechniques/06-LocalModelRunners.md)

**Haftungsausschluss**:  
Dieses Dokument wurde mit KI-gestützten maschinellen Übersetzungsdiensten übersetzt. Obwohl wir uns um Genauigkeit bemühen, weisen wir darauf hin, dass automatisierte Übersetzungen Fehler oder Ungenauigkeiten enthalten können. Das Originaldokument in seiner ursprünglichen Sprache sollte als maßgebliche Quelle betrachtet werden. Für kritische Informationen wird eine professionelle menschliche Übersetzung empfohlen. Wir übernehmen keine Haftung für Missverständnisse oder Fehlinterpretationen, die sich aus der Nutzung dieser Übersetzung ergeben.