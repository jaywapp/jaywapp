# 아키텍처

## 솔루션 구조

```
Jaywapp.Infrastructure.sln
├── Jaywapp.Infrastructure/           (netstandard2.0, 외부 의존성 없음)
│   ├── Attributes/                   필터링 어트리뷰트
│   ├── Helpers/                      유틸리티 확장 메서드
│   ├── Interfaces/                   필터링 인터페이스
│   └── Models/                       필터링 모델
│
├── Jaywapp.Common/                   (netstandard2.0, 외부 의존성 없음)
│   ├── Extensions/                   범용 확장 메서드
│   ├── Guards/                       인수 검증
│   └── Models/                       도메인 독립 모델
│
└── UnitTest/
    ├── Jaywapp.Infrastructure.Tests/ (net8.0, NUnit 3.14.0)
    └── Jaywapp.Common.Tests/        (net8.0, NUnit 3.14.0)
```

## 설계 원칙

### 1. 도메인 독립성
- `Jaywapp.Common`은 어떤 비즈니스 로직도 포함하지 않음
- Console, WPF, Web API, Background Service 등 모든 환경에서 재사용 가능
- 외부 NuGet 패키지 의존성 없음

### 2. 단일 책임
- 파일 당 하나의 타입 (클래스, 인터페이스, Enum)
- 각 Helper 클래스는 하나의 도메인에 집중

### 3. 확장 메서드 패턴
- 기존 .NET 타입을 확장하여 Fluent API 제공
- 원본 타입 수정 없이 기능 추가

### 4. 불변성 우선
- Common 모델은 가능한 불변(immutable)으로 설계
- 생성자를 통한 초기화, 읽기 전용 프로퍼티

## 프로젝트 간 의존성

```
Jaywapp.Infrastructure ──(참조 없음)── Jaywapp.Common

Jaywapp.Infrastructure.Tests ──→ Jaywapp.Infrastructure
Jaywapp.Common.Tests ──→ Jaywapp.Common
```

> Infrastructure와 Common은 서로 독립적입니다.
> 각각 별도로 NuGet 패키지로 배포하거나, 독립적으로 참조할 수 있습니다.
