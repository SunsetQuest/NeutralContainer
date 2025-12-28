## Working name

**Neutral Container** (or “Container” internally)

## Product summary

A web platform where creators embed **unlisted YouTube videos** (with YouTube comments disabled) and receive **consent-based, presence-first feedback**. Each post includes a structured “Response Agreement” (what the creator wants/doesn’t want, and visibility rules). Comments pass through automated moderation designed to reduce disrespect, unsolicited advice, and labeling/diagnosing. The platform is not therapy/coaching; it is an informal container for nuanced sharing without pressure to resolve.

---

## Core principles (the “why” translated into product rules)

1. **Consent-based responses:** The creator chooses what types of responses are welcome.
2. **Presence before interpretation:** Encourage clarifying questions and reflection before advice.
3. **Ambiguity is allowed:** No forced narrative (no “fixing,” no “wrap-up” requirement).
4. **Private is first-class:** Many creators will prefer private feedback by default.
5. **Behavior-based moderation:** The same comment can be acceptable or not depending on the creator’s chosen container rules.

---

## User roles

* **Creator/Author**

  * Creates posts (YouTube embed + response agreement)
  * Reviews comments (private + public + held)
  * Can hide/approve/highlight; can block/report users
* **Commenter**

  * Watches video, reads rules, writes comment
  * Optionally runs “AI check” before submitting
  * Selects comment visibility if allowed
* **Moderator/Admin**

  * Handles held/flagged queues, reports, appeals
  * User management (suspensions, spam controls)
  * Adjusts system-wide moderation thresholds and policy text

---

## Key user flows

### Flow A: Creator creates a post

1. Creator uploads video to YouTube → sets **Unlisted** → **disables YouTube comments**.
2. Creator logs in → “Create Post”
3. Pastes YouTube URL (platform extracts/stores **VideoId**).
4. Creator sets:

   * Title (optional)
   * Context prompt (optional; short; avoids forcing a narrative)
   * **Response Agreement** (structured options + optional custom text)
   * Comment visibility policy:

     * **Private only**
     * **Public only**
     * **Commenter chooses**
   * Moderation strictness: **Standard / High**
5. Post is published with a shareable platform URL.

### Flow B: Commenter leaves feedback

1. Commenter opens post page → sees embedded video and Response Agreement.
2. Commenter writes comment.
3. Optional: clicks **“Check my comment”** (AI self-check + alignment with creator rules).
4. Selects Private/Public (only if creator allows commenter choice).
5. Submit → moderation pipeline returns:

   * **Approved** (posted)
   * **Held** (pending review)
   * **Rejected with rewrite guidance** (edit required)

### Flow C: Creator reviews comments

Creator dashboard shows inbox by status:

* Approved (Private/Public)
* Held (Pending moderation)
* Flagged (Violation detected)
  Actions:
* Approve / Hide
* Highlight (optional “promote”)
* Block user / Report
* Reply (optional; can be Phase 2)

**Privacy best practice:** Do not allow converting private → public unless commenter explicitly consented (recommended: store consent at comment creation).

---

## Response Agreement (structured options)

Avoid relying only on free-form text. Give creators selectable preferences that map cleanly to moderation.

### “What I’m looking for” (choose 1+)

* **Presence-only** (witnessing; “I hear you”)
* **Reflective listening** (summarize what you heard)
* **Clarifying questions** (gentle questions; no fixing)
* **Share your perspective** (non-prescriptive; “in my experience…”)
* **Suggestions/advice allowed** (explicit opt-in; default OFF)
* **Resources allowed** (explicit opt-in; default OFF)

### “Please avoid”

* Diagnosing or labeling (clinical or armchair)
* Moralizing/shaming
* Prescriptive language (“you should…”) if advice not allowed
* Minimizing/dismissing
* Pushing toward resolution if creator chose presence-only

### Visibility settings

* Private only / Public only / Commenter chooses

### Sensitivity toggles (optional but useful)

* “Extra gentle container” (stricter tone thresholds)
* “No mental health labels”
* “No relationship advice”
* “No medical advice”

---

## Page layout (commenter view)

Post page structure (YouTube-like familiarity, but container-first):

1. Embedded YouTube player (top)
2. Creator header (name, title optional, date)
3. **Response Agreement card** (bulleted, scannable; above comment box)
4. Comment composer:

   * Text area
   * Visibility selector (if allowed)
   * Buttons:

     * “Check my comment”
     * “Submit”
5. Public comments list (only if public comments enabled)
6. Report/Flag on each comment

---

## Moderation design (two-layer approach)

### Layer 1: Commenter-facing “AI Check Tools” (coaching, not writing-for-them)

Two tools (can be combined in UI, but conceptually distinct):

1. **Boundary check**

   * Flags likely advice/diagnosis/harshness/shaming
   * Offers rewrite *patterns* (advice → reflection; label → observation + question)
2. **Alignment check**

   * Compares the comment to the creator’s Response Agreement
   * Example: “Your comment reads as advice; creator requested presence-only.”

### Layer 2: Server-side enforcement (decisions)

Pipeline (recommended order):

1. Spam/profanity/rate-limit heuristics
2. Policy classification:

   * Disrespect/harassment
   * Labeling/diagnosing
   * Unsolicited advice / prescriptive language
   * Moralizing/shaming/minimization
3. Decision outcomes:

   * **Allow** (publish)
   * **Hold** (creator/mod review; safest default when uncertain)
   * **Reject with rewrite guidance** (clear violation)
   * (Optional) **Allow but warn** (use sparingly; can create confusion—best for Phase 2)

**Container-based enforcement:** If advice is enabled for a post, advice detection is informational; if advice is disabled, it triggers hold/reject depending on confidence.

---

## MVP scope (build first)

### Must-have (MVP)

* Auth: register/login/logout (ASP.NET Identity)
* Create Post: YouTube URL + Response Agreement + visibility + moderation strictness
* View Post: embed + rules + comment composer + public comments
* Commenting:

  * Private/public storage and display rules
  * Creator inbox (see all comments)
  * Statuses: Approved / Held / Rejected / Flagged
* Moderation v1:

  * Basic automated checks with reason codes
  * “Hold + explain” default when uncertain
* Reporting:

  * Flag comment flow
* Admin:

  * Minimal moderation queue (held/flagged)
  * Approve/reject + user suspension

### Phase 2 (should-have)

* Creator highlight/promote comments
* Notifications (email/in-app): new comments, held items
* Comment replies/threads (optional)
* Stronger AI check UX (better rewrite guidance, examples)
* Per-creator blocklist / keyword filters
* Post discoverability controls:

  * public directory vs “unlisted on our platform”

### Phase 3 (nice-to-have)

* Structured comment templates (Reflection / Question / Resonance fields)
* Small “circles” / groups with stronger norms
* Reputation signals for aligned commenters

---

## Data model (conceptual, minimal)

* **User**: Id, DisplayName, Email, Role, Status, CreatedAt
* **Post**: Id, CreatorUserId, YouTubeVideoId, Title, ContextText, CreatedAt, Status
* **ResponseAgreement** (1:1 with Post):

  * AllowedFeedbackModes (enum list/bitmask)
  * DisallowedModes (enum list)
  * VisibilityPolicy (PrivateOnly/PublicOnly/CommenterChoice)
  * ModerationLevel (Standard/High)
  * SensitivityFlags (optional)
  * CustomRulesText (optional)
* **Comment**:

  * Id, PostId, CommenterUserId
  * Body
  * Visibility (Private/Public)
  * ModerationStatus (Approved/Held/Rejected/Flagged)
  * ModerationReasons (JSON)
  * CommenterPublicConsent (bool) if “commenter chooses”
  * CreatedAt
* **Report**: Id, ReporterUserId, CommentId, Reason, CreatedAt, Status
* **ModerationLog**: Id, EntityType, EntityId, Action, Actor, Reasons, Timestamp

**Privacy definition to specify in copy and code:**
Private comments are visible to **creator + the commenter + moderators/admin** (recommended). Public comments are visible to all visitors to the post (or all logged-in users, depending on product choice).

---

## Technical approach (.NET / Blazor)

* **Frontend:** Blazor Server for MVP speed (WASM later if desired)
* **Backend:** ASP.NET Core
* **Auth:** ASP.NET Core Identity (+ optional Google OAuth later)
* **DB:** SQL Server 2025 Developer
* **Hosting:** Windows Server 2022
* **YouTube embed:** store VideoId; iframe embed; validate youtu.be and youtube.com formats

**YouTube note to surface in UX:** Unlisted videos can still be shared by link; the platform link effectively shares access.

---

## Engineering milestones (implementation plan)

1. **Scaffold**

   * Blazor + Identity + EF Core + base layout
2. **Posts**

   * Create Post, Post view, Response Agreement storage
3. **Comments**

   * Submit + visibility + public list + creator inbox
4. **Moderation v1**

   * Heuristics + reason codes + hold/reject flows
5. **AI Check (optional in MVP, or Phase 2)**

   * Boundary/alignment checks + UI integration
6. **Admin & Reporting**

   * Flag queue, approvals, suspensions, audit logs
7. **Hardening**

   * Rate limiting, spam controls, abuse prevention, security review

---

## Key risks to flag early

* **Moderation calibration:** aim for “Hold + explain” rather than hard reject when uncertain.
* **Privacy expectations:** define clearly what private means and who can see it.
* **Abuse/spam:** rate limits + account throttles are not optional.
* **Scope creep:** keep MVP to posts + comments + moderation + creator review.
