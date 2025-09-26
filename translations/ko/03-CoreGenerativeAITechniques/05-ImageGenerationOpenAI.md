# Azure OpenAI를 활용한 이미지 생성

이 수업에서는 .NET 애플리케이션에서 Azure OpenAI와 DALL-E를 활용하여 이미지를 생성하는 방법에 대해 알아보겠습니다. 이미지 생성을 통해 텍스트 설명을 기반으로 독창적인 이미지를 만들 수 있으며, 다양한 애플리케이션에 창의적인 가능성을 제공합니다.

---

## 소개

[![Azure OpenAI를 활용한 이미지 생성](https://img.youtube.com/vi/ru3U8MHbFFI/0.jpg)](https://youtu.be/ru3U8MHbFFI?feature=shared)

_⬆️ 비디오를 보려면 이미지를 클릭하세요 ⬆️_

이미지 생성 AI를 사용하면 텍스트 설명이나 프롬프트로부터 독창적인 이미지를 만들 수 있습니다. Azure OpenAI를 통해 DALL-E와 같은 서비스를 사용하면 이미지에서 보고 싶은 스타일, 구성, 객체 등을 정확하게 지정할 수 있습니다. 이는 일러스트레이션, 컨셉 아트, 디자인 목업 및 기타 시각적 콘텐츠를 만드는 데 유용합니다.

## Azure OpenAI를 활용한 이미지 생성

.NET 애플리케이션에서 Azure OpenAI를 사용하여 이미지를 생성하는 방법을 살펴보겠습니다:

```csharp
var client = new OpenAIClient(
    new Uri("Azure OpenAI 엔드포인트"), 
    new AzureKeyCredential("Azure OpenAI API 키"));

ImageGenerationOptions imageGenerationOptions = new()
{
    DeploymentName = "dalle3", // Azure OpenAI에서 DALL-E 모델 배포 이름
    Prompt = "달빛 아래 앉아 있는 고양이, 디지털 아트",
    Size = ImageSize.Size1024x1024,
    Quality = ImageGenerationQuality.Standard,
    Style = ImageGenerationStyle.Natural,
};

Response<ImageGenerations> imageGenerations = await client.GetImageGenerationsAsync(imageGenerationOptions);
Uri imageUri = imageGenerations.Value.Data[0].Url;
```

### 샘플 애플리케이션

[ImageGeneration-01](./src/ImageGeneration-01) 예제에서는 DALL-E 모델을 사용하여 텍스트 프롬프트를 기반으로 이미지를 생성하는 콘솔 애플리케이션을 구현했습니다.

## 다음 단계

👉 [AI Toolkit, Docker, Foundry Local로 로컬에서 모델을 실행해 봅시다!](../../../03-CoreGenerativeAITechniques/06-LocalModelRunners.md)

**면책 조항**:  
이 문서는 기계 기반 AI 번역 서비스를 사용하여 번역되었습니다. 정확성을 위해 최선을 다하고 있지만, 자동 번역에는 오류나 부정확성이 포함될 수 있습니다. 원본 문서를 해당 언어로 작성된 상태에서 신뢰할 수 있는 권위 있는 자료로 간주해야 합니다. 중요한 정보의 경우, 전문적인 인간 번역을 권장합니다. 이 번역 사용으로 인해 발생하는 오해나 오역에 대해 당사는 책임을 지지 않습니다.