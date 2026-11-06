# GitHub 프로필 전면 개편 — 작업

orchestrator: Codex

모델은 현재 세션과 동일한 Codex 모델을 상속한다. 독립적인 설치 문서와 SVG 작업은 별도 Codex 세션으로 진행하고 통합·게시·검증은 부모가 순서대로 담당한다.

| ID | owner | model | effort | depends_on | parallel_group | files | verification | status |
|---|---|---|---|---|---|---|---|---|
| scope | Codex | inherited | high | — | planning | profile-overhaul-analysis/design/tasks | 원격 README·메타데이터·지침 확인 | complete |
| artwork | Codex | inherited | high | scope | authoring | docs/assets/profile/*.svg | XML 검사, PNG 시각 검토 | complete |
| extension-docs | Codex | inherited | medium | scope | authoring | cc-jsonl-monitor/README.md | 설치 절차와 기존 본문 보존 | complete |
| profile-copy | Codex | inherited | high | scope | authoring | README.md, archive, profile-settings.md | 사실·링크·모바일 가독성 | complete |
| publish | Codex | inherited | high | artwork, profile-copy, extension-docs | sequential | 두 저장소의 승인된 문서 | 기본 브랜치 원격 재조회 | complete |
| visual-qa | Codex | inherited | high | publish | sequential | README 및 자산 | GitHub 공개 렌더링·이미지 로드 | complete |
| account-settings | Codex | inherited | medium | profile-copy | sequential | Bio, pinned repos, repo metadata | 변경 후 공개 페이지 확인 | complete |

| profile-simplify | Codex | inherited | medium | account-settings | sequential | README.md, profile-overhaul-analysis/design/tasks.md | 프로젝트 설명·제품 이미지 부재, 소개·경력 보존, 원격 전체 본문 일치 | complete |

2026-11-06 후속 수정은 같은 README와 관련 문서를 함께 바꾸므로 충돌 방지를 위해 부모 Codex가 순차 처리한다.

## 검증 기록

- 2026-11-06: 독립 검토에서 경력·기능 과장 및 비공개 소스 링크 없음 확인.
- 2026-11-06: CC JSONL Monitor 설치 안내 기본 브랜치 게시 및 전체 원문 read-back 일치. Commit `c346a5241d0b27cab83d0c2233af9a557cdb420d`.
- 원문 백업이 기준 HEAD의 README 바이트와 동일함 확인.
- 2026-11-06: 프로필 11개 변경 파일 기본 브랜치 전체 본문 read-back 일치. Commit `5d3c2d5928a8617c80bb49611d9e8b68968935eb`.
- 공개 GitHub 화면에서 Hero, KeyMan, Marvelog 이미지 3개 실제 로드 확인. Hero/Marvelog에 모바일 전용 SVG와 media source 제공, 350px PNG 렌더 시각 확인. 실제 모바일 브라우저 전체 페이지는 미검증.
- 공개 링크 12개 HTTP 200. LinkedIn은 자동 요청에 HTTP 999를 반환하여 직접 접근 미검증; 기존 공개 프로필 URL 유지.
- 2026-11-06: Bio·Website 저장 완료. 프로필 새로고침 후 반영 및 고정 순서 `key-man → cc-jsonl-monitor → p4-harness → jaywapp-libs` 유지 확인.
- 2026-11-06: `jaywapp`, `cc-jsonl-monitor`, `p4-harness`의 Description·Website·Topics 저장 후 각 About 영역에서 반영 확인.
- 2026-11-06 후속 수정: 메인 README의 프로젝트 기능 설명·제품 이미지·스택 태그·상태 문구 제거를 정적으로 확인. Hero·자기소개·기술 표·경력과 공개 프로젝트 링크 보존. 문서 수정만 포함하므로 실행 테스트 생략.
