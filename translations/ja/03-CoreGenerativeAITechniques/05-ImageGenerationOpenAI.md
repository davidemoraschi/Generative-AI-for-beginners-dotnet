# Azure OpenAIによる画像生成

このレッスンでは、Azure OpenAIとDALL-Eを使用して.NETアプリケーションで画像を生成する方法について学びます。画像生成では、テキストの説明に基づいてオリジナルの画像を作成することができ、さまざまなアプリケーションに創造的な可能性をもたらします。

---

## はじめに

[![Azure OpenAIによる画像生成](https://img.youtube.com/vi/ru3U8MHbFFI/0.jpg)](https://youtu.be/ru3U8MHbFFI?feature=shared)

_⬆️ 動画を見るには画像をクリックしてください ⬆️_

画像生成AIは、テキストの説明やプロンプトから独自の画像を作成することができます。Azure OpenAIを通じてDALL-Eなどのサービスを使用すると、スタイル、構成、オブジェクトなど、画像に表示したいものを正確に指定できます。これは、イラスト、コンセプトアート、デザインモックアップ、その他の視覚的なコンテンツを作成するのに役立ちます。

## Azure OpenAIによる画像生成

Azure OpenAIを使用して.NETアプリケーションで画像を生成する方法を見てみましょう：

```csharp
var client = new OpenAIClient(
    new Uri("Azure OpenAIのエンドポイント"), 
    new AzureKeyCredential("Azure OpenAI APIキー"));

ImageGenerationOptions imageGenerationOptions = new()
{
    DeploymentName = "dalle3", // Azure OpenAIのDALL-Eモデル展開名
    Prompt = "月明かりの下で座っている子猫、デジタルアート",
    Size = ImageSize.Size1024x1024,
    Quality = ImageGenerationQuality.Standard,
    Style = ImageGenerationStyle.Natural,
};

Response<ImageGenerations> imageGenerations = await client.GetImageGenerationsAsync(imageGenerationOptions);
Uri imageUri = imageGenerations.Value.Data[0].Url;
```

### サンプルアプリケーション

[ImageGeneration-01](./src/ImageGeneration-01)のサンプルでは、DALL-Eモデルを使用してテキストプロンプトに基づいた画像を生成するコンソールアプリケーションを実装しています。

## 次のステップ

👉 [AI Toolkit、Docker、Foundry Localを使ってローカルでモデルを実行しましょう！](../../../03-CoreGenerativeAITechniques/06-LocalModelRunners.md)

**免責事項**:  
この文書は、機械ベースのAI翻訳サービスを使用して翻訳されています。正確性を期すよう努めていますが、自動翻訳には誤りや不正確さが含まれる場合があります。元の言語で記載された原文が正式な情報源と見なされるべきです。重要な情報については、専門の人間による翻訳をお勧めします。この翻訳の使用に起因する誤解や解釈の誤りについて、当方は責任を負いません。