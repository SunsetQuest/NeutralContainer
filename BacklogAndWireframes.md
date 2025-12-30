# Backlog and Wireframes

This file is the executable work queue plus minimal wireframe spec for the NeutralContainer prototype.

**Source of truth:** product intent and principles live in `readme.md`; the concrete MVP shape lives in `Plan.md`. Items moved to `CompletedBacklog.md` are considered done.

## How to process this backlog
- Work **top-down** from the **Active backlog** section.
- Keep scope tight: implement one small vertical slice at a time (UI → domain → persistence).
- Maintain exactly **one** item in **Next** at any time.
- When an item is done:
  1) Move its implementation notes to `CompletedBacklog.md` (what changed + key files/migrations).
  2) Mark the item here as **Moved to:** `CompletedBacklog.md`.

## Backlog conventions (short)
These conventions exist to keep the backlog “agent-friendly” and to prevent drift.

### IDs
- Every backlog item must have a stable ID, used everywhere (PRs, commit messages, CompletedBacklog).
- Format: `NC-E<epic>-<nnn>` (example: `NC-E08-001`).
- If you split an item, keep the parent ID and create children (example: `NC-E08-001a`, `NC-E08-001b`).

### Status (canonical)
Use only these statuses (and keep them consistent):
- **Next** (exactly one item in the entire file)
- **In progress** (0–1 items; should usually match Next)
- **Blocked** (must include a “Blocked by:” note)
- **Not started** (default)
- **Moved to CompletedBacklog.md** (done)

### Item template
Each user story should follow this structure:

- **ID:** `NC-E##-###`
- **User story:** As a <role>, I can <capability> so that <benefit>.
- **Status:** Not started | Next | In progress | Blocked | Moved to CompletedBacklog.md
- **Wireframe reference:** (page + section) OR “N/A”
- **Acceptance criteria**
  - Given / When / Then …
- **Notes / Dependencies** (optional)
  - Blocked by: …
  - Tech notes: …

### “Next” marker rule (work ordering)
- “Top of Active backlog” is the default ordering, but **Next is the single source of truth** for what should be implemented now.
- If you reprioritize, move the Next marker; do not reorder large sections casually.

### Completion logging (CompletedBacklog.md)
When moving an item to CompletedBacklog.md, include:
- The item ID + title
- Summary of behavior change
- Key files touched
- DB migrations (if any)
- Notable decisions (especially auth/privacy/moderation)

---

## Active backlog (top-down)

### Epic 12 — Moderation rejection reasons
**Goal:** Require template reasons when admins reject comments to keep feedback grounded.

1. **ID:** NC-E12-001
* **User story:** As an admin, I must select a rejection reason template before rejecting a comment so moderation feedback stays clear and consistent.
* **Status:** Moved to CompletedBacklog.md
* **Wireframe reference:** Admin Queue (Held/Flagged detail)
* **Acceptance criteria**
  * Given I review a held or flagged comment, when I choose Reject, then I must select a rejection reason template before the action completes.
  * Given I reject a comment with a selected reason, when the action is saved, then the reason is recorded in the moderation log.
  * Given I attempt to reject without a reason, when I submit, then the UI blocks the action with a clear prompt.

### Epic 11 — Safety messaging (not crisis support)
**Goal:** Set clear expectations that NeutralContainer is not a crisis support service.

1. **ID:** NC-E11-001
* **User story:** As a creator or commenter, I see a clear “not crisis support” notice so I understand the platform’s limits.
* **Status:** Moved to CompletedBacklog.md
* **Wireframe reference:** Create Post (header), Post View (comment composer)
* **Acceptance criteria**
  * Given I open Create Post, when the page loads, then I see a brief notice that NeutralContainer is not crisis support with guidance to seek local emergency help if needed.
  * Given I view a post, when I reach the comment composer, then I see the same notice near the composer without blocking submission.
  * Given the notice is displayed, when I scan it, then it uses neutral, non-alarmist language consistent with product principles.

2. **ID:** NC-E11-002
* **User story:** As a member, I can access local crisis resources from the safety notice so I know where to go for urgent help.
* **Status:** Moved to CompletedBacklog.md
* **Wireframe reference:** Create Post (header), Post View (comment composer)
* **Acceptance criteria**
  * Given I see the safety notice, when I look for more help, then I can open a list of crisis resources (or a link to a resources page).
  * Given the resource list is shown, when I read it, then it includes a clear “local emergency services” reminder and a note that resources vary by region.
  * Given I open the resource list, when I return to the page, then the create/comment flow remains intact.

### Epic 10 — Home post list (bounded navigation)
**Goal:** Logged-in users can browse recent posts without a public discovery feed.

1. **ID:** NC-E10-001
* **User story:** As a logged-in member, I can view a recent-posts list on the home page so I can open posts without needing a direct link.
* **Status:** Moved to CompletedBacklog.md
* **Wireframe reference:** Home (new)
* **Acceptance criteria**
  * Given I am logged in, when I open Home, then I see a list of recent posts with title, creator name, post type, and created date, each linking to the post view.
  * Given I am logged out, when I open Home, then I see a prompt to sign in and no post list.
  * Given there are no posts, when I open Home, then I see an empty state with a link to create a post.

### Epic 8 — Support text-only posts (alongside YouTube-backed posts)
**Goal:** Posts can be either text-only or YouTube-backed, while keeping the same Response Agreement + moderation model from `Plan.md`.

1. **ID:** NC-E08-001
* **User story:** As a creator, I can choose a post type (Text-only or YouTube-backed) when creating a post.
* **Status:** Moved to CompletedBacklog.md
* **Acceptance criteria**
  * Given I open Create Post, when the page loads, then I can select **Text-only** or **YouTube-backed**.
  * Given I choose **Text-only**, when I publish, then no YouTube URL is required and the post saves successfully.
  * Given I choose **YouTube-backed**, when I publish, then a valid YouTube URL is required and the VideoId is stored.
  * Given I switch post types, when I publish, then irrelevant fields are ignored (e.g., YouTube URL is not required for Text-only).

2. **ID:** NC-E08-002
* **User story:** As a viewer, I can view a post whether it is Text-only or YouTube-backed.
* **Status:** Moved to CompletedBacklog.md
* **Acceptance criteria**
  * Given a Text-only post, when I open the Post View, then there is no video embed and the post body displays prominently.
  * Given a YouTube-backed post, when I open the Post View, then the video embed displays and the supporting text displays if provided.

---

### Epic 9 — Response Agreement acknowledgement before comment submission
**Goal:** Commenters explicitly acknowledge the creator’s Response Agreement before submitting.

1. **ID:** NC-E09-001
* **User story:** As a commenter, I must acknowledge the Response Agreement before I can submit a comment.
* **Status:** Moved to CompletedBacklog.md
* **Acceptance criteria**
  * Given I am viewing a post, when I open the composer, then I see a short acknowledgement checkbox tied to the Response Agreement (e.g., “I will respond within these boundaries”).
  * Given the acknowledgement is unchecked, when I click Submit, then submission is blocked with a clear validation message.
  * Given the acknowledgement is checked, when I submit, then the comment proceeds through moderation as normal.

---

## Backlog starter (historical; most items have moved to CompletedBacklog.md)
### Epic 1 — Identity, access control, and roles
**Goal:** Only authenticated users can access posts and participate; role-based access for creator vs admin/moderator.
1. **User story:** As a user, I can manage my account basics (display name, password) so my identity is consistent.
* **Moved to:** `CompletedBacklog.md`
---
### Epic 2 — Create YouTube-backed post + Response Agreement (implemented)
**Goal:** Creators can publish a post by providing an unlisted YouTube URL plus structured consent settings (“Response Agreement”).
1. **User story:** As a creator, I can create a post by pasting a YouTube URL so the app stores the VideoId and renders an embed.
* **Moved to:** `CompletedBacklog.md`
2. **User story:** As a creator, I can set post metadata (title optional, context prompt optional) to frame the share without forcing narrative.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given I leave Title blank, when I publish, then the post is created and displays a default title (e.g., “Untitled”).
  * Given I enter Context text, when I publish, then it displays on the post view page.
3. **User story:** As a creator, I can configure a structured Response Agreement (what I’m looking for + what to avoid) so moderation can enforce it.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given the “What I’m looking for” list, when I select one or more options, then they are saved and displayed prominently on the post view page.
  * Given “Suggestions/advice allowed” is not selected, when a comment contains prescriptive advice with high confidence, then the system can Hold or Reject per moderation rules.
  * Given I add optional custom rules text, when I publish, then it is displayed alongside structured options.
4. **User story:** As a creator, I can set visibility policy (Private only / Public only / Commenter chooses) so comment visibility is governed.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given Private only, when a commenter composes a comment, then there is no option to make it public.
  * Given Public only, when a commenter composes a comment, then the comment will be visible publicly upon approval.
  * Given Commenter chooses, when a commenter selects Public, then the system records commenter consent for public display and never makes it public without that consent.
5. **User story:** As a creator, I can choose moderation strictness (Standard/High) so the container can be “extra gentle.”
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given High, when a comment is borderline on tone thresholds, then the system more often Holds rather than Approves (vs Standard).
  * Given I view the post, when I open the Response Agreement, then the chosen strictness is visible (at least to the creator; optionally to everyone).
---
### Epic 3 — Post viewing and comment submission (text-only MVP)
**Goal:** A viewer watches the embed, reads the Response Agreement, and submits text feedback that is routed through moderation.
1. **User story:** As a logged-in commenter, I can view a post (video + rules) so I understand the container before responding.
* **Moved to:** `CompletedBacklog.md`
2. **User story:** As a commenter, I can write and submit a text-only comment that includes a visibility choice if allowed.
* **Moved to:** `CompletedBacklog.md`
3. **User story:** As the system, I route each submitted comment into Approved / Held / Rejected with reason codes to support transparency.
* **Moved to:** `CompletedBacklog.md`
4. **User story (optional MVP / Phase 2-ready):** As a commenter, I can run an “AI check” before submitting to see alignment/boundary issues.
* **Moved to:** `CompletedBacklog.md`
---
### Epic 4 — Moderation v1 (automated checks + decisioning + logs)
**Goal:** Implement “Hold + explain” as the safe default; support reason codes and audit trail.
1. **User story:** As the system, I run baseline heuristics (spam/rate limits/profanity) before deeper classification.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given a user exceeds rate limits, when submitting, then the submission is blocked with a clear error message.
  * Given spam heuristics trigger, when submitting, then comment is Held or Rejected (configurable) with “Spam suspected” reason.
2. **User story:** As the system, I classify likely violations (disrespect/harassment, diagnosing/labeling, unsolicited advice, moralizing/shaming/minimizing).
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given advice is disabled for the post, when a comment contains prescriptive advice with high confidence, then it is Held or Rejected based on configured thresholds.
  * Given advice is enabled, when advice is detected, then it does not cause rejection by itself (though other violations still apply).
  * Given the post has “No mental health labels,” when a comment contains diagnostic language, then it is Held or Rejected and the reason references that setting.
3. **User story:** As the system, I persist moderation reasons and decisions so creators/admin can review and calibrate.
* **Acceptance criteria**
  * Given any moderation decision, when stored, then ModerationReasons JSON includes category + confidence + triggered rule.
  * Given an admin reviews a comment, when they override a decision, then a ModerationLog entry is created with actor + timestamp + action.
---
### Epic 5 — Creator inbox (review, triage, and controls)
**Goal:** Creators can see all inbound feedback (private/public/held/flagged) and act: approve, hide, report, block.
1. **User story:** As a creator, I can view an inbox grouped by status so I can triage quickly.
* **Moved to:** `CompletedBacklog.md`
2. **User story:** As a creator, I can approve or hide comments to manage what is visible and what stays private.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given a comment is Held, when I click Approve, then status becomes Approved and it becomes visible per visibility rules.
  * Given a comment is Approved, when I click Hide, then it is removed from public display (if public) and remains visible in creator inbox with status Hidden (or Approved + Hidden flag).
3. **User story:** As a creator, I can block/report a user or a specific comment to protect my container.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given I click Block user from a comment, when confirmed, then that user can no longer comment on my posts.
  * Given I report a comment, when submitted, then a Report record is created and appears in the Admin Queue.
4. **User story:** As a creator, I can highlight approved public comments so visitors see standout feedback first.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given a comment is approved and public, when I click Highlight, then it appears in a highlighted section on the post view.
  * Given I unhighlight a comment, when the post view loads, then it appears only in the standard public comment list.
---
### Epic 6 — Reporting and Admin Queue (held/flagged/reports/users)
**Goal:** Admin/moderator can resolve held/flagged content, handle reports, and suspend users.
1. **User story:** As an admin, I can see a queue of Held comments and decide Approve/Reject.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given I open Admin Queue → Held, when I select an item, then I see comment text, post context, Response Agreement, and triggered reasons.
  * Given I approve, when saved, then the comment becomes Approved and is visible per visibility rules.
  * Given I reject, when saved, then the comment is set to Rejected and (optionally) rewrite guidance is stored/shown.
2. **User story:** As an admin, I can review Flagged items and user reports to take action.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given a report exists, when I open it, then I can see reporter, reason, and linked comment.
  * Given I mark a report “Resolved,” when saved, then it no longer appears in the default open queue.
3. **User story:** As an admin, I can suspend a user to limit abuse/spam.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given I suspend a user, when they attempt to comment, then the system blocks the action and shows a suspension message.
  * Given a user is suspended, when they attempt to log in, then access is denied (configurable) or limited to read-only pages.
---
### Epic 7 — Non-functional hardening (privacy, abuse prevention, observability)
**Goal:** Privacy definitions are enforced, abuse controls are present, and the system is supportable.
1. **User story:** As the system, I enforce privacy rules for private comments (creator + commenter + admins).
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given a private comment, when a different authenticated user views the post, then they cannot access that comment via UI or direct URL.
  * Given an admin views moderation detail, when they open the item, then they can see private comments for enforcement purposes.
2. **User story:** As the system, I provide audit logging for moderation actions.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given any approve/reject/hide/suspend action, when executed, then a ModerationLog entry is created with actor + reason + timestamp.
3. **User story:** As the system, I implement baseline anti-abuse controls.
* **Moved to:** `CompletedBacklog.md`
* **Acceptance criteria**
  * Given repeated rapid submissions, when thresholds are exceeded, then rate limiting is applied.
  * Given suspicious patterns (new accounts spamming), when detected, then content is Held by default.
---
## Minimal wireframe spec per page
### Page 1 — Create Post
**Route:** `/posts/create`
**Access:** Authenticated users only; Creator capability (any logged-in user can create unless you later restrict).
**Primary job:** Choose post type (Text-only or YouTube-backed) → (if YouTube) capture URL + preview → Response Agreement → publish.
**Layout (top to bottom)**
1. **Header**
* Page title: “Create Post”
* Secondary: “Draft saved” indicator (optional MVP)
2. **Post type**
* Radio group: `PostType`
  * Text-only
  * YouTube-backed
* Helper text (shown when YouTube-backed): “Videos are hosted on YouTube. Use an Unlisted video and disable YouTube comments; discussion happens here.”

3. **Post Details**
* Field: `Title` (optional)
* Field: `BodyText` (textarea, optional; character limit). For Text-only posts this is the primary content; for YouTube-backed posts this is supporting context.

4. **Media (conditional)**
* If `PostType == YouTube-backed`:
  * Field: `YouTubeUrl` (text input)
  * Button (optional): “Preview”
  * **Preview panel** (hidden until valid URL): embedded player using extracted VideoId
  * Validation states:
    * Invalid URL format
    * URL valid but no VideoId extracted
* If `PostType == Text-only`:
  * No YouTube URL field (show a small note: “This post has no video.”)

5. **Response Agreement (structured)**
* **What I’m looking for** (multi-select checkboxes)
  * Presence-only
  * Reflective listening
  * Clarifying questions
  * Share your perspective
  * Suggestions/advice allowed (opt-in)
  * Resources allowed (opt-in)
* **Please avoid** (checkboxes / toggles)
  * Diagnosing/labeling
  * Moralizing/shaming
  * Prescriptive language if advice not allowed
  * Minimizing/dismissing
  * Pushing toward resolution if presence-only
* **Sensitivity toggles** (optional)
  * Extra gentle container
  * No mental health labels
  * No relationship advice
  * No medical advice
* Field: `CustomRulesText` (optional textarea)
6. **Comment visibility policy**
* Radio group: `VisibilityPolicy`
  * Private only
  * Public only
  * Commenter chooses
* Inline note: “Public requires commenter consent.”
7. **Moderation strictness**
* Radio group: `ModerationLevel` = Standard / High
8. **Actions**
* Primary: `Publish`
* Secondary: `Cancel` (returns to dashboard/home)
* Optional MVP: `Save Draft` (if you want drafts; otherwise omit)
**Publish outcome states**
* Success: redirect to Post View with toast “Post published”
* Failure: inline error summary (top) + field-level errors
---
### Page 2 — Post View (viewer/commenter experience)
**Route:** `/posts/{postId}`
**Access:** Authenticated users only (per your earlier decision).
**Primary job:** View post content (video or text), read Response Agreement, write a comment (text-only), see public comments if enabled.
**Layout (top to bottom)**
1. **Post content**
* If YouTube-backed: embedded YouTube player (iframe) using stored VideoId
* If Text-only: no video; render the post body (and any supporting text) as the primary content
2. **Post header**
* Creator display name
* Title (or “Untitled”)
* Created date
* Optional: Context text block (if provided)
3. **Response Agreement card (high prominence)**
* “What I’m looking for” bullets (selected items)
* “Please avoid” bullets (selected items)
* Visibility policy label (e.g., “Private feedback only”)
* Moderation strictness label (optional to show publicly; always visible to creator)
4. **Comment composer**
* Textarea: `CommentBody`
* Acknowledgement: checkbox “I will respond within these boundaries” (required before Submit)
* Visibility selector:
  * Shown only if `VisibilityPolicy == CommenterChoice`
  * Otherwise show fixed label (e.g., “This will be private”)
* Optional button: `Check my comment` (if enabled)
* Primary button: `Submit`
* Composer validation:
  * Min/max length
  * “You are suspended” or “Rate limit exceeded” banners as applicable
5. **Submission result panel**
* If **Approved**: confirmation + (if public) renders in list below
* If **Held**: “Pending review” + brief reason summary (high-level)
* If **Rejected**: rewrite guidance + keep text in editor for revision
6. **Public comments section** (only if enabled by creator policy)
* List items show:
  * Commenter name
  * Timestamp
  * Comment body
  * Action: `Report` (creates report)
* Empty state: “No public comments yet.”
**Edge cases**
* If viewer is the creator: show a compact link/button to “View in Inbox” for this post.
* If commenter is blocked by creator: disable composer + show message.
---
### Page 3 — Creator Inbox
**Route:** `/creator/inbox` (and optional `/creator/inbox?postId=...`)
**Access:** Authenticated creator; shows only content for posts owned by the creator.
**Primary job:** Triage and manage comments across posts.
**Layout**
1. **Header**
* Title: “Inbox”
* Post filter dropdown: “All posts” + list of creator’s posts
2. **Status tabs with counts**
* Approved
* Held
* Flagged
* Rejected (optional to show, but useful for learning/calibration)
3. **Comment list (table or cards)**
* Each item shows:
  * Post title (and link)
  * Commenter name
  * Visibility (Private/Public)
  * Status badge (Approved/Held/Flagged/Rejected/Hidden)
  * Created timestamp
  * Short preview of comment text
  * Reason chips (from ModerationReasons; compact)
4. **Detail drawer / panel (on select)**
* Full comment text
* Response Agreement snapshot (compact)
* Moderation reasons (expanded)
* Reporter info if flagged/reported
5. **Actions (context-sensitive)**
* For Held: `Approve`, `Reject`
* For Approved: `Hide` (and `Unhide` if hidden)
* For any: `Report to admin`, `Block user`
* Optional Phase 2: `Highlight`
**States**
* Empty state per tab (e.g., “No held comments right now.”)
* Error state: cannot load comments → retry
---
### Page 4 — Admin Queue
**Route:** `/admin/queue`
**Access:** Admin/Moderator only.
**Primary job:** Resolve held/flagged content and reports; manage suspensions; maintain audit trail.
**Layout**
1. **Header**
* Title: “Admin Queue”
* Optional quick filters: date range, severity, moderation level
2. **Tabs**
* Held comments
* Flagged comments
* Reports
* Users (optional MVP if you want suspensions in-app immediately)
3. **Queue list (left)**
* Each row shows:
  * Item type + status
  * Post reference (title + creator)
  * Trigger reasons (chips)
  * Timestamp
  * Risk indicator (simple: Low/Med/High)
4. **Detail panel (right)**
* Post context:
  * Creator, title, context text
  * Response Agreement snapshot
* Comment detail:
  * Commenter
  * Visibility + consent indicator (if public)
  * Full text
  * ModerationReasons JSON rendered as human-readable list
* Report detail (if applicable):
  * Reporter, reason, notes
5. **Admin actions**
* For held/flagged comments: `Approve`, `Reject`, `Hold` (keep held), `Request rewrite` (optional; can map to Reject with guidance)
* For users: `Suspend`, `Unsuspend`
* For reports: `Resolve`, `Dismiss`
6. **Audit / logging**
* Show recent ModerationLog entries for the selected entity (read-only list)
**States**
* Empty queue state (per tab)
* Concurrency note: if item was resolved by another admin, show “Already resolved” and refresh list
