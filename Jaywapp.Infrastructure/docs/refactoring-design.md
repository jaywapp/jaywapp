# Jaywapp.Infrastructure 리팩터링 설계 문서

## 1. 기존 구조 분석

### 1.1 프로젝트 개요
- **타겟 프레임워크**: netstandard2.0
- **버전**: 0.1.0
- **구성**: Helper 확장 메서드, 필터링 시스템, XML/파일/컬렉션 유틸리티

### 1.2 기존 파일 구조
```
Jaywapp.Infrastructure/
├── Enums.cs                   (3개 enum을 하나의 파일에 정의)
├── Attributes/
│   └── FilterableAttribute.cs (Attribute + Extension을 같은 파일에 정의)
├── Helpers/
│   ├── CollectionHelper.cs
│   ├── ColorHelper.cs         (WPF 전용)
│   ├── DataTableHelper.cs
│   ├── EnumHelper.cs
│   ├── EnumerableHelper.cs
│   ├── FileHelper.cs
│   ├── HashSetHelper.cs
│   ├── RandomHelper.cs
│   └── XmlHelper.cs
├── Interfaces/
│   ├── IBlock.cs
│   ├── IFilter.cs             (Interface + Extension을 같은 파일에 정의)
│   └── IFilterPropertySelector.cs
└── Models/
    ├── Filter.cs
    └── FilterGroup.cs
```

### 1.3 식별된 문제점

#### 구조적 문제
| 문제 | 위치 | 심각도 |
|------|------|--------|
| 다중 타입 정의 (파일 당 1개 원칙 위반) | `Enums.cs`, `FilterableAttribute.cs`, `IFilter.cs` | 중 |
| 도메인 종속적 코드와 범용 코드 미분리 | 전체 | 중 |
| 도메인 독립적 재사용 모듈 부재 | 전체 | 고 |

#### 버그
| 버그 | 위치 | 설명 |
|------|------|------|
| `RandomHelper.NextBoolean` 항상 false 반환 | `RandomHelper.cs:16` | `Random.Next(0,1)`은 항상 0 반환 (상한 배타적) |
| `FilterExt.IsFilterd` OR 조건 오류 | `IFilter.cs:41` | `first.Logical` 대신 `filter.Logical`을 사용해야 함 |
| `Filter.Check` 타입 검사 순서 오류 | `Filter.cs:82` | `string`과 `Enum`이 `IComparable`을 구현하므로 항상 `CheckNumber`로 분기 |
| 메서드명 오타 | `IFilter.cs:29` | `IsFilterd` → `IsFiltered` |

#### 코드 품질
- 일부 공개 타입/멤버에 XML 문서 주석 누락
- XML 주석 내 잘못된 형식 (`EnumerableHelper.cs`, `RandomHelper.cs`)
- 테스트 프로젝트에 불필요한 WPF 의존성 (`net6.0-windows7.0`)
- 플레이스홀더 테스트 파일 존재 (`UnitTest1.cs`)
- 필터링 시스템에 대한 단위 테스트 부재

---

## 2. 리팩터링 목표 및 원칙

### 목표
1. **파일 당 하나의 타입** 원칙 적용
2. **버그 수정**: 식별된 4개 버그 모두 수정
3. **도메인 독립적 Common 모듈** 신설
4. **테스트 커버리지 강화**: 필터링 시스템 및 Common 모듈
5. **XML 문서 주석 정규화**

### 원칙
- 점진적, 안전한 리팩터링
- 기존 API 계약 보존 (하위 호환성)
- 테스트 주도 검증
- 최소 변경으로 최대 효과

---

## 3. 리팩터링 결정사항

### 3.1 파일 분리
| 원본 | 분리 결과 |
|------|----------|
| `Enums.cs` | `eLogicalOperator.cs`, `eFilteringOperator.cs`, `eFilteringType.cs` |
| `Attributes/FilterableAttribute.cs` | `FilterableAttribute.cs` + `FilterableAttributeExtensions.cs` |
| `Interfaces/IFilter.cs` | `IFilter.cs` + `FilterExtensions.cs` |

### 3.2 버그 수정
1. **RandomHelper.NextBoolean**: `random.Next(0, 1)` → `random.Next(0, 2)`
2. **FilterExtensions.IsFiltered**: `first.Logical` → `filter.Logical`, 메서드명 `IsFilterd` → `IsFiltered`
3. **Filter.Check**: 타입 검사 순서를 Enum → string → IComparable 순으로 변경

### 3.3 테스트 프로젝트 현대화
- 타겟 프레임워크: `net6.0-windows7.0` → `net8.0`
- WPF 의존성 제거 (`UseWPF` 제거)
- 불필요한 import 정리 (`System.Windows.Media`)
- 플레이스홀더 테스트 삭제 (`UnitTest1.cs`)

### 3.4 Common 모듈 설계 결정
- **독립 모듈**: Infrastructure와 Common 간 참조 없음 (중복은 문서화하되 강제 결합 회피)
- **타겟**: netstandard2.0 (최대 호환성)
- **C# 버전**: 7.3 (netstandard2.0 네이티브 지원)
- **외부 의존성**: 없음

---

## 4. 최종 아키텍처

### 4.1 프로젝트 구조
```
Jaywapp.Infrastructure.sln
├── Jaywapp.Infrastructure/              (기존 프로젝트, 리팩터링)
│   ├── netstandard2.0
│   ├── 필터링 시스템 (도메인 특화)
│   └── 범용 Helper (파일, XML, 컬렉션 등)
│
├── Jaywapp.Common/                      (신규, 도메인 독립)
│   ├── netstandard2.0
│   ├── Extensions/
│   │   ├── StringExtensions.cs          (HasValue, SafeTruncate, ToSha256, NormalizeLineEndings)
│   │   ├── CollectionExtensions.cs      (IsEmpty, None, SafeForEach, Batch, DistinctBy)
│   │   ├── TaskExtensions.cs            (SafeFireAndForget, WithTimeout, WithRetry)
│   │   ├── DateTimeExtensions.cs        (StartOfDay, EndOfDay, IsInRange, ToUtcSafe, ToLocalSafe)
│   │   └── EnumExtensions.cs            (GetDescription, SafeParse, TryParseEnum, GetValues)
│   ├── Guards/
│   │   └── Guard.cs                     (NotNull, NotNullOrEmpty, InRange, NotEmpty, Requires)
│   └── Models/
│       ├── Error.cs                     (Code, Message, Exception, Metadata)
│       ├── Result.cs                    (Success/Failure 패턴)
│       ├── ResultOfT.cs                 (제네릭 Result<T>)
│       ├── PageRequest.cs               (Page, PageSize, Sorts, Skip)
│       ├── PageResult.cs                (Items, TotalCount, TotalPages, HasNext/Previous)
│       ├── SortDirection.cs             (Ascending, Descending)
│       ├── SortDefinition.cs            (Field, Direction)
│       ├── Range.cs                     (Min, Max, Contains, Overlaps)
│       ├── DatePeriod.cs                (Start, End, Inclusive/Exclusive, Contains)
│       ├── TraversalOrder.cs            (DepthFirst, BreadthFirst)
│       ├── TreeNode.cs                  (Value, Parent, Children, AddChild, Traverse)
│       ├── ChangeSet.cs                 (Added, Updated, Removed)
│       └── Trackable.cs                (Original, Current, IsDirty, Accept/Reject)
│
├── UnitTest/Jaywapp.Infrastructure.Tests/   (강화)
│   ├── net8.0 (WPF 의존성 제거)
│   ├── 기존 Helper 테스트 유지
│   ├── EnumHelper 테스트 추가
│   ├── Filter 테스트 추가
│   └── FilterGroup 테스트 추가
│
└── UnitTest/Jaywapp.Common.Tests/           (신규)
    ├── net8.0
    ├── Extensions 테스트 (String, Collection, Task, DateTime, Enum)
    ├── Guards 테스트
    └── Models 테스트 (Result, Page, Range, TreeNode, ChangeTracking)
```

### 4.2 테스트 요약
| 프로젝트 | 테스트 수 | 상태 |
|---------|----------|------|
| Jaywapp.Infrastructure.Tests | 50 | 전체 통과 |
| Jaywapp.Common.Tests | 118 | 전체 통과 |
| **합계** | **168** | **전체 통과** |

---

## 5. 가정 사항

1. `Enums.cs` 삭제 후 개별 파일로 분리해도 외부 소비자에 영향 없음 (네임스페이스 동일)
2. `IsFilterd` → `IsFiltered` 메서드명 변경은 breaking change이나, 0.1.0 버전이므로 허용
3. `FilterGroup.IsFiltered`가 기존 `IsFilterd`를 참조하므로 동시 수정 필요
4. Common 모듈은 Infrastructure와 독립적으로 배포/참조 가능하도록 설계
5. 테스트 프로젝트의 net8.0 전환은 프로덕션 코드(netstandard2.0)에 영향 없음
