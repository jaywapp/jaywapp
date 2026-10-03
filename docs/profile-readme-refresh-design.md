# 프로필 README 개편 — 설계 (초안)

status: draft (사용자 확인 대기)

## 제약

- GitHub README는 Markdown + 제한된 HTML(`<p align>`, `<picture>`, `<img>`, `<table>`, `<details>`)만 허용. CSS·JS 불가.
- 라이트/다크 대응은 `<picture><source media="(prefers-color-scheme: dark)">` 또는 테마 무관 배지로 처리.
- 외부 이미지 서비스는 장애 시 깨지므로 최소화(shields.io만 기본 사용).

## 정보 구조 (공통)

1. 헤더: 이름 · 한 줄 소개 · 연락 배지
2. About: 현재 하는 일 / 관심사 3~4줄
3. Featured Projects: 4~6개 카드(이름·설명·스택), 카테고리별 묶음(AI·Agent 도구 / Dev Productivity / Apps / Libraries)
4. Tech Stack: 주력·보조 구분
5. Experience: 기간·회사·역할
6. Footer: 전체 프로젝트·블로그 링크

## 콘셉트 3종 (`docs/ux-concepts/profile-readme-refresh/concept-0N/README.md`)

| | concept-01 Minimal Editorial | concept-02 Card Grid | concept-03 Terminal / Dev |
|---|---|---|---|
| 레이아웃 | 단일 컬럼, 텍스트 중심, 여백 | 2열 HTML 테이블 카드 그리드 | 코드블록·트리 구조 |
| 타이포 | 헤딩 위계 + 짧은 문장 | 배지·아이콘 중심 | 모노스페이스 |
| 시각 요소 | 배지 최소 | 스택 아이콘·카테고리 배지 | 없음(텍스트 아트) |
| 동적 요소 | 없음 | 선택 시 통계 카드 | 선택 시 Actions 자동 갱신 |

## 대안과 트레이드오프

- 동적 통계 카드: 시각적이지만 외부 서비스 가용성 의존 → 선택 사항.
- Actions 자동 갱신: 최신성 유지되나 워크플로·권한 추가 → 사용자 승인 시에만.

## 검증 전략

- 링크 전수 HTTP 상태 확인 스크립트
- Playwright로 GitHub 유사 렌더(라이트/다크, 390px/1280px) 스크린샷 확인
- 데이터는 GitHub API 응답과 대조
