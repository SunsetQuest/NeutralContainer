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

## UI plan update: visual direction (step 2)
This direction translates the README principles into a calm, neutral visual system that emphasizes presence, clarity, and low cognitive load.

### Palette
**Light theme**
- Base: soft parchment white (#F7F5F2), pure white surfaces (#FFFFFF).
- Ink: charcoal (#2E2A27) for primary text, muted graphite (#5D5853) for secondary text.
- Accent: muted blue-gray (#5B6B7A) for links and accents; avoid saturated blues.
- Borders: warm gray (#DDD6CF) with subtle contrast.
- Feedback: restrained tones (success #5A7C6E, warning #B08D57, danger #A6615E).

**Dark theme**
- Base: deep espresso (#1B1917), elevated surfaces (#26231F).
- Ink: warm off-white (#E7E0D8) for primary text, muted clay (#B7AEA4) for secondary.
- Accent: desaturated blue-gray (#6C7A88) for links and accents.
- Borders: soft umber (#3A3530).
- Feedback: desaturated equivalents (success #6E8C7F, warning #B49563, danger #A87472).

### Typography
- **Primary**: system sans (e.g., `-apple-system`, `Segoe UI`, `Inter`, `Roboto`, `Helvetica Neue`) for neutrality and clarity.
- **Scale**: modest hierarchy (H1 32–36, H2 24–28, H3 20–22, body 16–17, small 13–14).
- **Weight**: regular (400) for body, medium (500) for headings and buttons; avoid heavy bold by default.
- **Line height**: 1.5–1.65 for body text to support calm reading.

### Spacing & layout rhythm
- 4/8/12/16/24/32/48 spacing scale.
- Generous vertical rhythm; breathe between sections and response blocks.
- Max content width: 720–840px for reading-focused pages; wider only for dashboards.

### Surface hierarchy & elevation
- Flat by default; elevation only for active/interactive elements.
- Cards: subtle border + slight background contrast; avoid heavy shadows.
- Inputs: soft fill with clear focus state (2px outline, low-saturation accent).

### Light/dark theme guidance
- Keep contrast comfortable, not stark (avoid pure black/white pairing).
- Use consistent token names for surface/text/border/intent colors to enable theme switching.
- Use a single accent color per theme for links and primary actions; rely on weight and spacing for emphasis.

### Interaction tone
- Buttons: rounded corners (8–10px), low chroma fills.
- Links: understated, underline on hover/focus.
- Status/mode indicators: small pills with soft background tints and clear text labels.

---

## UI plan update: layout patterns (step 3)
Updated layout patterns for key screens emphasize clarity, warmth, and reduced cognitive load while keeping the “container” feeling bounded.

### Home / feed
- Structure: single-column reading rail with optional narrow sidebar for filters (desktop only).
- Card pattern: title, short excerpt, response agreement chip, and a calm meta row (author, date).
- CTA: “Open post” button is primary; “Respond later” secondary link.
- Empty state: reassure neutrality (“No posts yet. This space stays quiet until someone shares.”).

### Post detail
- Layout: centered content column (max 760–840px), with generous vertical spacing.
- Response Agreement block: always above responses, visually distinct (soft panel with icon + agreement label).
- Media: embedded video in a framed surface with muted controls; fallback to link if embed fails.
- Response list: compact cards with subtle separators; highlight moderation status only when needed.

### Response flow (composer)
- Stepper-lite: “Agreement → Compose → Review” inline, not a modal, to reduce friction.
- Agreement acknowledgment: checkbox + short reminder copy, placed directly above the editor.
- Editor: comfortable line height, low chrome, clear focus ring; helper text for tone guidance.
- Submission: primary action aligned right; secondary “Save draft” if enabled later.

### Login / register
- Calm, minimal form surfaces; avoid heavy branding or sales copy.
- Soft panel with supportive microcopy (“This is a quiet, account-bound space.”).
- Clear error presentation without alarmist color use; keep labels always visible.

### Global layout notes
- Navigation: reduce emphasis on gradients; aim for neutral surfaces and legible contrast.
- Use consistent section headers and spacing for moderation screens (admin queue, creator inbox).
- Prefer inline status tags over banners to avoid over-alerting.

---

## UI plan update: accessibility audit (step 6)
Accessibility checks focused on contrast, focus visibility, and motion comfort across both themes. The adjustments prioritize keyboard clarity and reduced motion without changing the overall visual direction.

### Contrast & legibility
- Maintain readable body text contrast on both surfaces; keep muted text reserved for supporting metadata only.
- Use clear borders on inputs/cards to keep component boundaries visible on low-contrast displays.
- Preserve distinct alert/status text colors paired with soft background tints to keep state recognition clear.

### Focus states & keyboard navigation
- Ensure focus rings are visible for links, buttons, inputs, selects, and navigation elements.
- Add focus-visible styling on intent selection rows to make keyboard selection obvious.
- Keep the focus target on page navigation (h1) visible with an outline rather than removing it.

### Motion & reduced motion
- Avoid hover-driven motion for users who prefer reduced motion (disable transform/transition where possible).
- Retain subtle transitions for standard motion settings to preserve affordance without distraction.

---

## UI plan update: validation against principles + MVP flow (step 7)
The refreshed UI direction and component updates were validated against the README principles and the MVP flow to ensure the visual system reinforces the product intent without adding friction.

### README principles alignment
- **Postpone judgment**: Response Agreement is visually prominent, and supporting copy avoids evaluative language; status indicators are subdued and contextual.
- **Author-defined intent**: Agreement chips, intent selectors, and agreement panels remain primary in the hierarchy across feed, detail, and response flows.
- **Neutral language by default**: UI tone stays matter-of-fact, with muted accents and restrained status color usage to avoid urgency or crisis framing.
- **Context optional**: Layouts prioritize concise titles and optional body text; media and supporting context are framed as optional blocks rather than required fields.
- **Presence over policing**: Moderation and status visuals are present but de-emphasized, favoring calm inline tags instead of alarm banners.

### MVP flow validation
- **Home/feed** emphasizes scanability and a quiet entry point with calm cards and agreement tags.
- **Post detail** keeps the agreement and responses in a single-column, low-noise reading flow.
- **Response flow** keeps acknowledgement and composition in sequence with minimal chrome to reduce cognitive load.
- **Auth screens** remain minimal and supportive, preserving the “bounded space” tone.
- **Moderation screens** use consistent spacing and status tags to avoid punitive visual weight while remaining clear.

### UI notes update (what changed vs. previous UI)
- Navigation and layout visuals are simplified to neutral surfaces; gradients are reduced and replaced by subtle borders and surface contrast.
- Themed tokens now drive all surfaces, borders, and text colors for consistent light/dark behavior.
- Core components (buttons, cards, inputs, tabs, badges, alerts) now share rounded, low-chroma styling aligned with the calm visual system.
- Focus-visible states and reduced-motion overrides are now part of the default UI contract.

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
- Home (recent posts list for logged-in users)
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
