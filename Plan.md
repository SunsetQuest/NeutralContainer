# NeutralContainer — Implementation Plan (MVP in this repo)

## Source of truth
The product intent and principles are defined in `readme.md` (the “neutral container” concept: postponed judgment, author-defined intent, neutral language, context optional, presence over policing).
This `Plan.md` describes the concrete MVP implementation that expresses those principles in software.

If there is a conflict between README intent and implementation details here, update this plan to match the README.

---

## Product definition (what we are building)
NeutralContainer is a bounded space where people can share nuanced, unresolved, hard-to-classify experiences without being forced into diagnosis, moral sorting, or premature resolution.

This repo is building an MVP that supports:
- Posts that are text-only OR reference externally hosted media (initially YouTube unlisted videos).
- A visible, author-chosen “Response Agreement” that defines what kinds of replies are welcome.
- Text-only responses that respect the Response Agreement.
- Moderation workflows that prioritize “presence without labeling” while still enabling enforcement against abuse.

---

## Core principles to preserve in the implementation
1. **Postpone judgment**: UI copy and interaction design should bias toward clarification before evaluation.
2. **Author-defined intent**: the author selects a Response Agreement; responders must see it and respond within it.
3. **Neutral language by default**: tone should be matter-of-fact; avoid crisis framing or inspirational framing by default.
4. **Context optional**: authors can post fragments; the system should not force backstory or a “lesson learned.”
5. **Presence over policing**: moderation should target harassment/coercion/unsolicited diagnosis, not disagreement alone.

---

## MVP scope statement
### In scope (MVP)
**Content**
- Post types:
  - Text-only post
  - YouTube-backed post (YouTube URL + optional text)
- Responses: text-only comments/replies.

**Access & roles**
- Login required to view posts.
- Roles:
  - Author/Creator (can create posts; manages responses to their posts)
  - Commenter (can respond to posts)
  - Admin/Moderator (handles moderation queue)

**Response Agreement**
- Authors choose one “agreement” per post (extensible later).
- Responders must explicitly acknowledge it before submitting.
- Agreements are displayed prominently on the post view.

**Moderation**
- Admin queue for review of new responses (and optionally new posts if desired).
- Creator inbox to review responses associated with their posts (especially if moderation is “pre-publication”).
- Moderation outcomes include:
  - Allow / Approve
  - Hold (needs review or clarification)
  - Reject (policy violation / mismatch)
- Clear reasons required for rejects (template-based to reduce tone escalation).

**UX pages (minimum set)**
- Create Post
- Post View (read post + Response Agreement + response composer)
- Creator Inbox (responses awaiting creator review/visibility decisions)
- Admin Queue (global moderation queue)

### Explicitly out of scope (MVP)
- Hosting or uploading video directly to NeutralContainer.
- Video replies.
- DMs / private messaging.
- Public “viral” discovery feed (keep bounded).
- Clinical or diagnostic tooling (no labeling workflows).
- Real-time chat.

---

## YouTube integration (hosting strategy)
### Why YouTube
YouTube is used only as a hosting/streaming platform to avoid building video storage, encoding, and delivery.

### Required creator behavior (product-guided)
When creating a YouTube-backed post, the creator is guided to:
- Upload video to YouTube
- Set visibility to **Unlisted** (recommended default)
- **Disable YouTube comments** (discussion happens in NeutralContainer)
- Paste the YouTube URL into the NeutralContainer “Create Post” form

### What NeutralContainer stores
- The YouTube URL (and derived video ID if needed)
- Post metadata (title, optional text, timestamps, author, status)
- Response Agreement selection
- Comments/responses and moderation events

### What NeutralContainer does not store
- The video file
- YouTube account tokens (MVP)
- YouTube comments

---

## Key user flows

### Flow A — Create Post (text-only)
1. Author clicks “Create Post”
2. Enters title + body (body optional)
3. Selects Response Agreement (required)
4. Publishes post
5. Post becomes viewable to logged-in users

### Flow B — Create Post (YouTube-backed)
1. Author clicks “Create Post”
2. Enters title + optional supporting text
3. Pastes YouTube URL (validated)
4. Selects Response Agreement (required)
5. Publishes post
6. Post view embeds the video (or shows a safe link if embed fails)

### Flow C — Respond to a post
1. Logged-in user opens a post
2. Sees Response Agreement prominently
3. Clicks “Respond”
4. Acknowledges agreement (“I will respond within these boundaries”)
5. Submits text response
6. Response enters moderation pipeline (see below)

### Flow D — Moderation pipeline (recommended default for MVP)
- New response defaults to **Held** (not publicly visible)
- Admin can:
  - Allow → visible (or routed to creator decision if configured)
  - Hold → remains hidden; request edit/clarification
  - Reject → hidden; reason recorded
- Creator inbox:
  - Sees responses tied to their posts
  - Can optionally mark an allowed response as “Featured” later (post-MVP)

This pipeline can be simplified later (e.g., post-moderation) once norms are stable.

---

## Minimal data model (conceptual)
- User
  - Id, Email, Role(s)
- Post
  - Id, AuthorId, Title, Body (optional), MediaType (None|YouTube), YouTubeUrl (optional)
  - ResponseAgreementId
  - Visibility/Status (Draft|Published|Archived)
  - CreatedAt, UpdatedAt
- ResponseAgreement
  - Id, Name, Description, AllowedResponseTypes (enum flags), Examples
- Comment/Response
  - Id, PostId, AuthorId, Body, Status (Held|Allowed|Rejected)
  - CreatedAt, UpdatedAt
- ModerationEvent
  - Id, TargetType (Post|Response), TargetId, Action (Hold|Allow|Reject), ReasonCode, Notes, ActorId, Timestamp

---

## Content policy (MVP-level)
This plan assumes a lightweight but explicit policy:
- Prohibit harassment, coercion, threats, doxxing.
- Prohibit unsolicited diagnosis/labeling as a norm violation (and optionally as a moderation category).
- Encourage clarification questions aligned with the Response Agreement.
- Provide “not crisis support” messaging and standard crisis resources when appropriate.

---

## Technical implementation notes (high-level)
- Platform: .NET 8 web app (Blazor Server is consistent with current repo configuration).
- Auth: ASP.NET Core Identity (Individual Accounts).
- DB: SQL Server (Developer Edition for local development is reasonable) + EF Core.
- Deployment: Windows Server is acceptable; Cloudflare proxy in front is compatible.

(Keep details here minimal; the backlog should drive concrete engineering tasks.)

---

## Definition of done (for MVP)
MVP is “done” when:
- A logged-in author can create a text or YouTube-backed post with a required Response Agreement.
- A logged-in commenter can submit a text response after acknowledging the agreement.
- Admin can Allow/Hold/Reject responses in an admin queue.
- Creator can view responses for their posts in a creator inbox.
- Post view renders agreement + responses according to moderation status.
- Basic abuse reporting exists OR a minimum moderator workflow exists to handle reports.
- Basic audit trail exists for moderation actions (who did what, when, why).

---

## Open questions (tracked in backlog, not blocking the plan)
- Should moderation be pre-publication for all responses, or only for new accounts?
- Should creators have veto power over allowed responses (recommended: yes for MVP if bounded/private)?
- Should posts themselves require moderation in MVP, or only responses?
- What is the initial set of Response Agreements and the exact copy?

These should be resolved by backlog sequencing and user testing, not by expanding this plan.
