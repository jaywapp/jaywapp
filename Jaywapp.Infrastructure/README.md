# Jaywapp.Infrastructure

Jaywapp 프로젝트에서 공통으로 사용되는 유틸리티, 헬퍼, 도메인 독립적 재사용 컴포넌트를 제공하는 라이브러리입니다.

## 프로젝트 구성

| 프로젝트 | 타겟 | 설명 |
|---------|------|------|
| **Jaywapp.Infrastructure** | netstandard2.0 | 필터링 시스템, 헬퍼 (Collection, DataTable, Enum, Enumerable, File, HashSet, Random, XML) |
| **Jaywapp.Common** | netstandard2.0 | 도메인 독립 확장 메서드, 가드, 모델 |

## Jaywapp.Infrastructure

### 헬퍼

- **CollectionHelper** - `ICollection<T>.AddRange()`
- **DataTableHelper** - `DataColumnCollection.ToList()`, `DataRowCollection.ToList()`
- **EnumHelper** - `GetValues<T>()`, `TryGetDescription()`, `GetValueFromDescription<T>()`
- **EnumerableHelper** - `IsNullOrEmpty()`, `IgnoreNull()`, `ToObservableCollection()`, `ChainPairing()`
- **FileHelper** - `ReadLines()`, `Decompress()`, `Compress()`, `ClearDirectory()`, `GetFiles()`
- **HashSetHelper** - `AddRange()`, `RemoveRange()`
- **RandomHelper** - `NextBoolean()`, `NextString()`, `NextCharacter()`, `Next<TEnum>()`, `NextColor()`
- **XmlHelper** - `GetAttributeValue()`, `TryGetAttributeValue()`, `GetAttributeValueOrEmpty()`

### 필터링 시스템

- `IFilter` 인터페이스 (`AND`/`OR` 논리 연산자 지원)
- `Filter` - 단일 프로퍼티 필터 (Equal, NotEqual, LessThan, Contains, Regex 등)
- `FilterGroup` - 중첩 필터 로직을 위한 Composite 패턴

## Jaywapp.Common

### 확장 메서드

| 분류 | 메서드 |
|------|--------|
| **StringExtensions** | `HasValue()`, `SafeTruncate()`, `ToSha256()`, `NormalizeLineEndings()` |
| **CollectionExtensions** | `IsEmpty()`, `None()`, `SafeForEach()`, `Batch()`, `DistinctBy()` |
| **TaskExtensions** | `SafeFireAndForget()`, `WithTimeout()`, `WithRetry()` |
| **DateTimeExtensions** | `StartOfDay()`, `EndOfDay()`, `IsInRange()`, `ToUtcSafe()`, `ToLocalSafe()` |
| **EnumExtensions** | `GetDescription()`, `SafeParse()`, `TryParseEnum()`, `GetValues()` |

### 가드

- `Guard.NotNull()`, `NotNullOrEmpty()`, `NotNullOrWhiteSpace()`, `InRange()`, `NotEmpty()`, `Requires()`

### 모델

| 모델 | 설명 |
|------|------|
| **Result / Result\<T\>** | 예외 없이 성공/실패를 표현하는 패턴 |
| **Error** | 코드, 메시지, 예외, 메타데이터를 포함하는 오류 모델 |
| **PageRequest / PageResult\<T\>** | 페이지네이션 요청 및 응답 |
| **SortDefinition** | 정렬 필드 및 방향 |
| **Range\<T\>** | 범위 포함/겹침 검사를 지원하는 범용 범위 모델 |
| **DatePeriod** | 포함/배제 경계를 지원하는 날짜 범위 |
| **TreeNode\<T\>** | DFS/BFS 탐색을 지원하는 범용 트리 |
| **ChangeSet\<T\>** | 추가/수정/삭제 변경 추적 |
| **Trackable\<T\>** | 원본/현재 값 추적 (수락/거부 지원) |

## 설치

```
PM> Install-Package Jaywapp.Infrastructure
```

## 빌드 및 테스트

```bash
dotnet build Jaywapp.Infrastructure.sln
dotnet test Jaywapp.Infrastructure.sln
```

**테스트 요약**: 168개 테스트 (Infrastructure: 50개, Common: 118개) - 전체 통과

## 솔루션 구조

```
Jaywapp.Infrastructure.sln
├── Jaywapp.Infrastructure/           (netstandard2.0)
├── Jaywapp.Common/                   (netstandard2.0)
├── UnitTest/
│   ├── Jaywapp.Infrastructure.Tests/ (net8.0, NUnit)
│   └── Jaywapp.Common.Tests/        (net8.0, NUnit)
└── docs/
    └── refactoring-design.md
```

## 참고 사항

- WPF 전용 타입 (ColorHelper, Converters)은 netstandard2.0에서 제외
- XML 문서 주석이 패키지에 포함됨
- Jaywapp.Common은 외부 의존성 없음

## 라이선스

[MIT](LICENSE)
