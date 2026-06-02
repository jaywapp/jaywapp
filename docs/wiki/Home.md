# Jaywapp.Infrastructure Wiki

Jaywapp 프로젝트에서 공통으로 사용되는 유틸리티, 헬퍼, 도메인 독립적 재사용 컴포넌트를 제공하는 라이브러리입니다.

## 목차

- [[Architecture]] - 전체 아키텍처 및 설계
- [[Jaywapp.Common]] - 도메인 독립 Common 모듈
- [[Jaywapp.Infrastructure-Helpers]] - Infrastructure 헬퍼 가이드
- [[Filtering-System]] - 필터링 시스템
- [[Refactoring-Log]] - 리팩터링 이력

## 프로젝트 구성

| 프로젝트 | 타겟 | 설명 |
|---------|------|------|
| Jaywapp.Infrastructure | netstandard2.0 | 필터링 시스템, 헬퍼 유틸리티 |
| Jaywapp.Common | netstandard2.0 | 도메인 독립 확장 메서드, 가드, 모델 |
| Jaywapp.Infrastructure.Tests | net8.0 | Infrastructure NUnit 테스트 |
| Jaywapp.Common.Tests | net8.0 | Common NUnit 테스트 |

## 빌드 및 테스트

```bash
dotnet build Jaywapp.Infrastructure.sln
dotnet test Jaywapp.Infrastructure.sln
```

## 라이선스

MIT License
