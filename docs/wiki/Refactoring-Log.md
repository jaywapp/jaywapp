# 리팩터링 이력

## v0.1.0 리팩터링 (2026-02)

### 버그 수정

#### 1. RandomHelper.NextBoolean - 항상 false 반환
- **원인**: `Random.Next(0, 1)`은 상한이 배타적이므로 항상 0 반환
- **수정**: `Random.Next(0, 2)`로 변경하여 0 또는 1 반환

#### 2. FilterExtensions.IsFiltered - OR 연산 오류
- **원인**: OR 조건에서 `first.Logical` (첫 번째 필터의 논리 연산자)을 검사하여, 현재 필터의 OR 조건이 무시됨
- **수정**: `filter.Logical`로 변경하여 각 필터의 논리 연산자를 올바르게 적용
- **추가**: 메서드명 오타 수정 (`IsFilterd` → `IsFiltered`)

#### 3. Filter.Check - 타입 검사 순서 오류
- **원인**: `string`과 `Enum`이 모두 `IComparable`을 구현하므로, `IComparable` 검사가 먼저 수행되어 `CheckString`과 `CheckEnum`에 도달 불가
- **수정**: 검사 순서를 `Enum → string → IComparable`로 변경 (구체적 타입 우선)

### 구조 개선

#### 파일 분리 (1파일 1타입 원칙)
| 원본 | 분리 결과 |
|------|----------|
| `Enums.cs` (3 enums) | `eLogicalOperator.cs`, `eFilteringOperator.cs`, `eFilteringType.cs` |
| `FilterableAttribute.cs` (class + ext) | `FilterableAttribute.cs`, `FilterableAttributeExtensions.cs` |
| `IFilter.cs` (interface + ext) | `IFilter.cs`, `FilterExtensions.cs` |

#### 테스트 프로젝트 현대화
- `net6.0-windows7.0` → `net8.0`
- WPF 의존성 제거 (테스트에 불필요)
- 불필요한 `using System.Windows.Media` 제거
- 플레이스홀더 테스트 (`UnitTest1.cs`) 삭제

### 신규 모듈

#### Jaywapp.Common (도메인 독립 모듈)
- **Extensions**: String, Collection, Task, DateTime, Enum (5개 클래스)
- **Guards**: Guard 클래스 (6개 메서드)
- **Models**: Result/Error, Page, Range, DatePeriod, TreeNode, ChangeSet, Trackable (13개 파일)

### 테스트 추가

| 카테고리 | 추가된 테스트 |
|---------|-------------|
| Infrastructure - EnumHelper | 8 tests |
| Infrastructure - Filter | 6 tests |
| Infrastructure - FilterGroup | 4 tests |
| Common - StringExtensions | 13 tests |
| Common - CollectionExtensions | 14 tests |
| Common - TaskExtensions | 9 tests |
| Common - DateTimeExtensions | 9 tests |
| Common - EnumExtensions | 9 tests |
| Common - Guard | 14 tests |
| Common - Result/Error | 10 tests |
| Common - Page | 9 tests |
| Common - Range/DatePeriod | 11 tests |
| Common - TreeNode | 8 tests |
| Common - ChangeTracking | 8 tests |
| **합계** | **132 tests 추가** |

### 최종 테스트 결과
- **전체**: 168 tests, 전체 통과
- **Infrastructure**: 50 tests
- **Common**: 118 tests
