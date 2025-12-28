\## Backlog starter



\### Epic 1 — Identity, access control, and roles



\*\*Goal:\*\* Only authenticated users can access posts and participate; role-based access for creator vs admin/moderator.



1\. \*\*User story:\*\* As a user, I can manage my account basics (display name, password) so my identity is consistent.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given I change my display name, when I save, then future posts/comments show the updated display name.

&nbsp;    \* Given I change my password, when I log in next time, then the new password works and the old password does not.



---



\### Epic 2 — Create post with YouTube embed + Response Agreement



\*\*Goal:\*\* Creators can publish a post by providing an unlisted YouTube URL plus structured consent settings (“Response Agreement”). 



1\. \*\*User story:\*\* As a creator, I can create a post by pasting a YouTube URL so the app stores the VideoId and renders an embed.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given I paste a valid youtu.be or youtube.com URL, when I submit/leave the field, then the system extracts/stores VideoId and shows an embed preview.

&nbsp;    \* Given I paste an invalid URL, when I submit, then I see a validation error and cannot publish.

&nbsp;    \* Given I publish successfully, when I open the post view, then the embedded player loads using the stored VideoId.



2\. \*\*User story:\*\* As a creator, I can set post metadata (title optional, context prompt optional) to frame the share without forcing narrative.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given I leave Title blank, when I publish, then the post is created and displays a default title (e.g., “Untitled”).

&nbsp;    \* Given I enter Context text, when I publish, then it displays on the post view page.



3\. \*\*User story:\*\* As a creator, I can configure a structured Response Agreement (what I’m looking for + what to avoid) so moderation can enforce it. 



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given the “What I’m looking for” list, when I select one or more options, then they are saved and displayed prominently on the post view page.

&nbsp;    \* Given “Suggestions/advice allowed” is not selected, when a comment contains prescriptive advice with high confidence, then the system can Hold or Reject per moderation rules.

&nbsp;    \* Given I add optional custom rules text, when I publish, then it is displayed alongside structured options.



4\. \*\*User story:\*\* As a creator, I can set visibility policy (Private only / Public only / Commenter chooses) so comment visibility is governed.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given Private only, when a commenter composes a comment, then there is no option to make it public.

&nbsp;    \* Given Public only, when a commenter composes a comment, then the comment will be visible publicly upon approval.

&nbsp;    \* Given Commenter chooses, when a commenter selects Public, then the system records commenter consent for public display and never makes it public without that consent. 



5\. \*\*User story:\*\* As a creator, I can choose moderation strictness (Standard/High) so the container can be “extra gentle.”



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given High, when a comment is borderline on tone thresholds, then the system more often Holds rather than Approves (vs Standard).

&nbsp;    \* Given I view the post, when I open the Response Agreement, then the chosen strictness is visible (at least to the creator; optionally to everyone).



---



\### Epic 3 — Post viewing and comment submission (text-only MVP)



\*\*Goal:\*\* A viewer watches the embed, reads the Response Agreement, and submits text feedback that is routed through moderation. 



1\. \*\*User story:\*\* As a logged-in commenter, I can view a post (video + rules) so I understand the container before responding.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given I open a post, when the page loads, then the Response Agreement card is visible above the comment composer.

&nbsp;    \* Given public comments are enabled, when the page loads, then approved public comments are visible in a list.

&nbsp;    \* Given public comments are not enabled, when the page loads, then no public comments section is shown (or it shows “Public comments are disabled”).



2\. \*\*User story:\*\* As a commenter, I can write and submit a text-only comment that includes a visibility choice if allowed.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given the creator’s policy is Commenter chooses, when I submit, then my selected visibility is stored with the comment.

&nbsp;    \* Given I submit an empty or too-short comment (configurable), when I submit, then I see a validation error and nothing is saved.

&nbsp;    \* Given I submit a valid comment, when moderation returns Approved, then I see confirmation and (if public) the comment appears in the public list.



3\. \*\*User story:\*\* As the system, I route each submitted comment into Approved / Held / Rejected with reason codes to support transparency. 



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given a comment is confidently compliant, when submitted, then status is Approved and it is visible per its visibility rules.

&nbsp;    \* Given moderation is uncertain, when submitted, then status is Held and the commenter sees “Pending review.”

&nbsp;    \* Given a comment clearly violates container rules, when submitted, then status is Rejected and the commenter sees rewrite guidance (no comment is posted).



4\. \*\*User story (optional MVP / Phase 2-ready):\*\* As a commenter, I can run an “AI check” before submitting to see alignment/boundary issues. 



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given I click “Check my comment,” when results return, then I see detected issues mapped to the creator’s rules (e.g., advice not allowed).

&nbsp;    \* Given I ignore the check and submit anyway, when submitted, then server-side enforcement still applies.



---



\### Epic 4 — Moderation v1 (automated checks + decisioning + logs)



\*\*Goal:\*\* Implement “Hold + explain” as the safe default; support reason codes and audit trail. 



1\. \*\*User story:\*\* As the system, I run baseline heuristics (spam/rate limits/profanity) before deeper classification.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given a user exceeds rate limits, when submitting, then the submission is blocked with a clear error message.

&nbsp;    \* Given spam heuristics trigger, when submitting, then comment is Held or Rejected (configurable) with “Spam suspected” reason.



2\. \*\*User story:\*\* As the system, I classify likely violations (disrespect/harassment, diagnosing/labeling, unsolicited advice, moralizing/shaming/minimizing).



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given advice is disabled for the post, when a comment contains prescriptive advice with high confidence, then it is Held or Rejected based on configured thresholds.

&nbsp;    \* Given advice is enabled, when advice is detected, then it does not cause rejection by itself (though other violations still apply).

&nbsp;    \* Given the post has “No mental health labels,” when a comment contains diagnostic language, then it is Held or Rejected and the reason references that setting.



3\. \*\*User story:\*\* As the system, I persist moderation reasons and decisions so creators/admin can review and calibrate.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given any moderation decision, when stored, then ModerationReasons JSON includes category + confidence + triggered rule.

&nbsp;    \* Given an admin reviews a comment, when they override a decision, then a ModerationLog entry is created with actor + timestamp + action.



---



\### Epic 5 — Creator inbox (review, triage, and controls)



\*\*Goal:\*\* Creators can see all inbound feedback (private/public/held/flagged) and act: approve, hide, report, block. 



1\. \*\*User story:\*\* As a creator, I can view an inbox grouped by status so I can triage quickly.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given I open Creator Inbox, when the page loads, then I see tabs/counters for Approved, Held, Rejected, Flagged.

&nbsp;    \* Given I filter to a specific post, when selected, then the list updates to only that post’s comments.



2\. \*\*User story:\*\* As a creator, I can approve or hide comments to manage what is visible and what stays private.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given a comment is Held, when I click Approve, then status becomes Approved and it becomes visible per visibility rules.

&nbsp;    \* Given a comment is Approved, when I click Hide, then it is removed from public display (if public) and remains visible in creator inbox with status Hidden (or Approved + Hidden flag).



3\. \*\*User story:\*\* As a creator, I can block/report a user or a specific comment to protect my container.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given I click Block user from a comment, when confirmed, then that user can no longer comment on my posts.

&nbsp;    \* Given I report a comment, when submitted, then a Report record is created and appears in the Admin Queue.



---



\### Epic 6 — Reporting and Admin Queue (held/flagged/reports/users)



\*\*Goal:\*\* Admin/moderator can resolve held/flagged content, handle reports, and suspend users. 



1\. \*\*User story:\*\* As an admin, I can see a queue of Held comments and decide Approve/Reject.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given I open Admin Queue → Held, when I select an item, then I see comment text, post context, Response Agreement, and triggered reasons.

&nbsp;    \* Given I approve, when saved, then the comment becomes Approved and is visible per visibility rules.

&nbsp;    \* Given I reject, when saved, then the comment is set to Rejected and (optionally) rewrite guidance is stored/shown.



2\. \*\*User story:\*\* As an admin, I can review Flagged items and user reports to take action.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given a report exists, when I open it, then I can see reporter, reason, and linked comment.

&nbsp;    \* Given I mark a report “Resolved,” when saved, then it no longer appears in the default open queue.



3\. \*\*User story:\*\* As an admin, I can suspend a user to limit abuse/spam.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given I suspend a user, when they attempt to comment, then the system blocks the action and shows a suspension message.

&nbsp;    \* Given a user is suspended, when they attempt to log in, then access is denied (configurable) or limited to read-only pages.



---



\### Epic 7 — Non-functional hardening (privacy, abuse prevention, observability)



\*\*Goal:\*\* Privacy definitions are enforced, abuse controls are present, and the system is supportable. 



1\. \*\*User story:\*\* As the system, I enforce privacy rules for private comments (creator + commenter + admins).



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given a private comment, when a different authenticated user views the post, then they cannot access that comment via UI or direct URL.

&nbsp;    \* Given an admin views moderation detail, when they open the item, then they can see private comments for enforcement purposes.



2\. \*\*User story:\*\* As the system, I provide audit logging for moderation actions.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given any approve/reject/hide/suspend action, when executed, then a ModerationLog entry is created with actor + reason + timestamp.



3\. \*\*User story:\*\* As the system, I implement baseline anti-abuse controls.



&nbsp;  \* \*\*Acceptance criteria\*\*



&nbsp;    \* Given repeated rapid submissions, when thresholds are exceeded, then rate limiting is applied.

&nbsp;    \* Given suspicious patterns (new accounts spamming), when detected, then content is Held by default.






---



\## Minimal wireframe spec per page



\### Page 1 — Create Post



\*\*Route:\*\* `/posts/create`

\*\*Access:\*\* Authenticated users only; Creator capability (any logged-in user can create unless you later restrict). 

\*\*Primary job:\*\* Capture YouTube URL → preview → Response Agreement → publish.



\*\*Layout (top to bottom)\*\*



1\. \*\*Header\*\*



&nbsp;  \* Page title: “Create Post”

&nbsp;  \* Secondary: “Draft saved” indicator (optional MVP)

2\. \*\*YouTube Video\*\*



&nbsp;  \* Field: `YouTubeUrl` (text input)

&nbsp;  \* Helper text: “Use an Unlisted YouTube video; disable YouTube comments.”

&nbsp;  \* Button (optional): “Preview”

&nbsp;  \* \*\*Preview panel\*\* (hidden until valid URL): embedded player using extracted VideoId

&nbsp;  \* Validation states:



&nbsp;    \* Invalid URL format

&nbsp;    \* URL valid but no VideoId extracted

3\. \*\*Post Details\*\*



&nbsp;  \* Field: `Title` (optional)

&nbsp;  \* Field: `ContextText` (textarea, optional; character limit)

4\. \*\*Response Agreement (structured)\*\*



&nbsp;  \* \*\*What I’m looking for\*\* (multi-select checkboxes)



&nbsp;    \* Presence-only

&nbsp;    \* Reflective listening

&nbsp;    \* Clarifying questions

&nbsp;    \* Share your perspective

&nbsp;    \* Suggestions/advice allowed (opt-in)

&nbsp;    \* Resources allowed (opt-in)

&nbsp;  \* \*\*Please avoid\*\* (checkboxes / toggles)



&nbsp;    \* Diagnosing/labeling

&nbsp;    \* Moralizing/shaming

&nbsp;    \* Prescriptive language if advice not allowed

&nbsp;    \* Minimizing/dismissing

&nbsp;    \* Pushing toward resolution if presence-only

&nbsp;  \* \*\*Sensitivity toggles\*\* (optional)



&nbsp;    \* Extra gentle container

&nbsp;    \* No mental health labels

&nbsp;    \* No relationship advice

&nbsp;    \* No medical advice

&nbsp;  \* Field: `CustomRulesText` (optional textarea)

5\. \*\*Comment visibility policy\*\*



&nbsp;  \* Radio group: `VisibilityPolicy`



&nbsp;    \* Private only

&nbsp;    \* Public only

&nbsp;    \* Commenter chooses

&nbsp;  \* Inline note: “Public requires commenter consent.”

6\. \*\*Moderation strictness\*\*



&nbsp;  \* Radio group: `ModerationLevel` = Standard / High

7\. \*\*Actions\*\*



&nbsp;  \* Primary: `Publish`

&nbsp;  \* Secondary: `Cancel` (returns to dashboard/home)

&nbsp;  \* Optional MVP: `Save Draft` (if you want drafts; otherwise omit)



\*\*Publish outcome states\*\*



\* Success: redirect to Post View with toast “Post published”

\* Failure: inline error summary (top) + field-level errors



---



\### Page 2 — Post View (viewer/commenter experience)



\*\*Route:\*\* `/posts/{postId}`

\*\*Access:\*\* Authenticated users only (per your earlier decision).

\*\*Primary job:\*\* Watch video, read Response Agreement, write a comment (text-only), see public comments if enabled. 



\*\*Layout (top to bottom)\*\*



1\. \*\*Video\*\*



&nbsp;  \* Embedded YouTube player (iframe) using stored VideoId

2\. \*\*Post header\*\*



&nbsp;  \* Creator display name

&nbsp;  \* Title (or “Untitled”)

&nbsp;  \* Created date

&nbsp;  \* Optional: Context text block (if provided)

3\. \*\*Response Agreement card (high prominence)\*\*



&nbsp;  \* “What I’m looking for” bullets (selected items)

&nbsp;  \* “Please avoid” bullets (selected items)

&nbsp;  \* Visibility policy label (e.g., “Private feedback only”)

&nbsp;  \* Moderation strictness label (optional to show publicly; always visible to creator)

4\. \*\*Comment composer\*\*



&nbsp;  \* Textarea: `CommentBody`

&nbsp;  \* Visibility selector:



&nbsp;    \* Shown only if `VisibilityPolicy == CommenterChoice`

&nbsp;    \* Otherwise show fixed label (e.g., “This will be private”)

&nbsp;  \* Optional button: `Check my comment` (if enabled)

&nbsp;  \* Primary button: `Submit`

&nbsp;  \* Composer validation:



&nbsp;    \* Min/max length

&nbsp;    \* “You are suspended” or “Rate limit exceeded” banners as applicable

5\. \*\*Submission result panel\*\*



&nbsp;  \* If \*\*Approved\*\*: confirmation + (if public) renders in list below

&nbsp;  \* If \*\*Held\*\*: “Pending review” + brief reason summary (high-level)

&nbsp;  \* If \*\*Rejected\*\*: rewrite guidance + keep text in editor for revision

6\. \*\*Public comments section\*\* (only if enabled by creator policy)



&nbsp;  \* List items show:



&nbsp;    \* Commenter name

&nbsp;    \* Timestamp

&nbsp;    \* Comment body

&nbsp;    \* Action: `Report` (creates report)

&nbsp;  \* Empty state: “No public comments yet.”



\*\*Edge cases\*\*



\* If viewer is the creator: show a compact link/button to “View in Inbox” for this post.

\* If commenter is blocked by creator: disable composer + show message.



---



\### Page 3 — Creator Inbox



\*\*Route:\*\* `/creator/inbox` (and optional `/creator/inbox?postId=...`)

\*\*Access:\*\* Authenticated creator; shows only content for posts owned by the creator. 

\*\*Primary job:\*\* Triage and manage comments across posts.



\*\*Layout\*\*



1\. \*\*Header\*\*



&nbsp;  \* Title: “Inbox”

&nbsp;  \* Post filter dropdown: “All posts” + list of creator’s posts

2\. \*\*Status tabs with counts\*\*



&nbsp;  \* Approved

&nbsp;  \* Held

&nbsp;  \* Flagged

&nbsp;  \* Rejected (optional to show, but useful for learning/calibration)

3\. \*\*Comment list (table or cards)\*\*



&nbsp;  \* Each item shows:



&nbsp;    \* Post title (and link)

&nbsp;    \* Commenter name

&nbsp;    \* Visibility (Private/Public)

&nbsp;    \* Status badge (Approved/Held/Flagged/Rejected/Hidden)

&nbsp;    \* Created timestamp

&nbsp;    \* Short preview of comment text

&nbsp;    \* Reason chips (from ModerationReasons; compact)

4\. \*\*Detail drawer / panel (on select)\*\*



&nbsp;  \* Full comment text

&nbsp;  \* Response Agreement snapshot (compact)

&nbsp;  \* Moderation reasons (expanded)

&nbsp;  \* Reporter info if flagged/reported

5\. \*\*Actions (context-sensitive)\*\*



&nbsp;  \* For Held: `Approve`, `Reject`

&nbsp;  \* For Approved: `Hide` (and `Unhide` if hidden)

&nbsp;  \* For any: `Report to admin`, `Block user`

&nbsp;  \* Optional Phase 2: `Highlight`



\*\*States\*\*



\* Empty state per tab (e.g., “No held comments right now.”)

\* Error state: cannot load comments → retry



---



\### Page 4 — Admin Queue



\*\*Route:\*\* `/admin/queue`

\*\*Access:\*\* Admin/Moderator only. 

\*\*Primary job:\*\* Resolve held/flagged content and reports; manage suspensions; maintain audit trail.



\*\*Layout\*\*



1\. \*\*Header\*\*



&nbsp;  \* Title: “Admin Queue”

&nbsp;  \* Optional quick filters: date range, severity, moderation level

2\. \*\*Tabs\*\*



&nbsp;  \* Held comments

&nbsp;  \* Flagged comments

&nbsp;  \* Reports

&nbsp;  \* Users (optional MVP if you want suspensions in-app immediately)

3\. \*\*Queue list (left)\*\*



&nbsp;  \* Each row shows:



&nbsp;    \* Item type + status

&nbsp;    \* Post reference (title + creator)

&nbsp;    \* Trigger reasons (chips)

&nbsp;    \* Timestamp

&nbsp;    \* Risk indicator (simple: Low/Med/High)

4\. \*\*Detail panel (right)\*\*



&nbsp;  \* Post context:



&nbsp;    \* Creator, title, context text

&nbsp;    \* Response Agreement snapshot

&nbsp;  \* Comment detail:



&nbsp;    \* Commenter

&nbsp;    \* Visibility + consent indicator (if public)

&nbsp;    \* Full text

&nbsp;    \* ModerationReasons JSON rendered as human-readable list

&nbsp;  \* Report detail (if applicable):



&nbsp;    \* Reporter, reason, notes

5\. \*\*Admin actions\*\*



&nbsp;  \* For held/flagged comments: `Approve`, `Reject`, `Hold` (keep held), `Request rewrite` (optional; can map to Reject with guidance)

&nbsp;  \* For users: `Suspend`, `Unsuspend`

&nbsp;  \* For reports: `Resolve`, `Dismiss`

6\. \*\*Audit / logging\*\*



&nbsp;  \* Show recent ModerationLog entries for the selected entity (read-only list)



\*\*States\*\*



\* Empty queue state (per tab)

\* Concurrency note: if item was resolved by another admin, show “Already resolved” and refresh list
