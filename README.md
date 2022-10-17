# IndieSpainJam 2022
Summary
## QUICK REMINDERS
- If two people are working in the same feature, and you want to avoid code conflicts, you  can add your name before the branch name (i.e. dani/feature/player_controller)
- Please, use the sneaky_case convention (i.e. ~~feature/playerMovement~~ feature/player_movement)
- Avoid pushing code to the main branch, instead, create a release branch from which to work from
- Do not merge not-working code into the main branch :)
## Branch naming convention
| Branch Purpose              | Name Structure         | Example                           |
|-----------------------------|------------------------|-----------------------------------|
| Most recent working version | main                   | main                              |
| Specific feature            | feature/<feature_name> | feature/player_movement           |
| Specific version            | release/<release_name> | release/mvp                       |
| Bug solving                 | hotfix/<bug_name>      | hotfix/double_jump_when_spamming  |
| Specific build              | build/v<build_number>  | build/v0.8                        |
| Polish task                 | polish/<task>          | polish/refactor_player_controller |
| Test task                   | test/<task>            | text/player_jump                  |
