# Bananagrams

## What is the application?

An e-learning app designed to improve the pattern recognition and language skills of children.

The app is based around two themes. A brain teaser, the anagram, and tropical fruit. Designed to aid in the brain development of children by presenting users with anagrams of tropical fruit.

Using gamification, the intent is bi-functional in that it will promote language development and encourage an interest in fruit by showcasing the wierd and wonderful world of tropical fruit, sharing images of their peculiar shapes along with descriptions of their enticing, and often strange, flavours.

## Why

Getting children excited about both learning and eating healthy can be challenging. This app is designed to tackle both in an intriguing and rewarding manner through the gamification of the app.

## MVP

- User account creation/login
- Connect to the tropicalfruitandveg.com API
- Auto-generate an anagram a day of a random fruit/veg from the API
- Allow users 5 attempts to unscramble the anagram of the day
- Users can view/share their stats
- Users can view other user's stats
- Users can face off on a set of anagrams (set a custom anagram length for difficulty)

## Stretch Goals

- Allow users to create their own anagrams for friends to solve
- Allow users to comment on user generated anagrams after complete (hide comments until complete)
- Expand to other topics (capital cities, history, books, art etc)

## Domain Model

```mermaid
erDiagram
  DailyBrainTeaser ||--|| BrainTeaser : ""
  BrainTeaserType ||--|| BrainTeaser : ""
  Solution ||--|| BrainTeaser : ""
  BrainTeaser ||--|{ Attempts : ""
  BrainTeaser ||--|{ BattleBrainTeaser : ""
  Attempts }o--|| User : ""
  User ||--|| Stats : ""
  User ||--o{ UserBattle : ""
  BattleBrainTeaser }|--|| Battle : ""
  Battle ||--|{ UserBattle : ""
```

## Entity Relationship Diargram

```mermaid
erDiagram

  DailyBrainTeaser ||--|| BrainTeaser : ""  
  BrainTeaserType ||--|| BrainTeaser : ""
  Solution ||--|| BrainTeaser : ""
  Solution ||--|| ObjectReference : ""
  BrainTeaser ||--|{ Attempts : ""
  BrainTeaser ||--|{ BattleBrainTeaser : ""
  Attempts }o--|| User : ""
  User ||--|| Stats : ""
  User ||--o{ UserBattle : ""
  BattleBrainTeaser }|--|| Battle : ""
  Battle ||--|{ UserBattle : ""
  
  DailyBrainTeaser {
    int id PK
    time_stamp created_on
    int brain_teaser_id FK
  }
  
  BrainTeaserType {
    int id PK
    string name
  }
  
  Solution {
    int id PK
    int object_reference_id FK
    int brain_teaser_id FK
  }
  
  ObjectReference {
    int id PK
    string name
    string description
    string image_url
    int solution_id FK
  }
  
  BrainTeaser {
    int id PK
    string anagram
    int brain_teaser_type_id FK
    int solution_id FK
    array attempts
    array battle_brain_teasers
  }
  
  Attempts {
    int id PK
    int total_attempts
    bool solved
    int brain_teaser_id FK
    int user_id FK
  }
  
  User {
    int id PK
    string first_name
    string last_name
    string email_address
    string password
    time_stamp dob
    time_stamp created_on
    array user_battles
    array attempts
  }
  
  Stats {
    int id PK
    int total_brain_teasers_solved
    int total_challenges_played
    int total_challenges_won
    int total_challenges_lost
    int total_daily_brain_teasers_played
    int longest_daily_brain_teasers_streak
  }
  
  Battle {
    int id PK
    array battle_brain_teasers
    array user_battles
  }
  
  BattleBrainTeaser {
    int id PK
    int battle FK
    int brain_teaser FK
  }
  
  UserBattle {
    int id PK
    int user_id FK
    int battle_id FK
  }
  
  

```
