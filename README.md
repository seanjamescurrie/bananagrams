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
flowchart
  DailyBrainTeaser --- BrainTeaser
  Solution --- BrainTeaser
  BrainTeaser --- Attempts
  BrainTeaser --- BattleBrainTeaser
  Attempts --- User
  User --- UserBattle
  Battle --- UserBattle
```
<!--
## Entity Relationship Diargram v1

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

## Entity Relationship Diargram v2

```mermaid
erDiagram

  bt_anagram }o--|| brain_teaser : ""  
  bt_anagram }o--|| bt_anagram_type : ""
  app_user_anagram ||--|| app_user : ""
  app_user_anagram ||--|| bt_anagram : ""
  face_off_user_anagram }|--|| face_off : ""
  face_off_user_anagram }|--|| bt_anagram : ""
  face_off_user_anagram }|--|| app_user : ""
  
  brain_teaser {
    int id PK
    string name
    string description
  }
  
  bt_anagram {
    int id PK
    string anagram_word
    date_time date_created
    string image_location
    int length
    string word
    int bt_anagram_type FK
    int brain_teaser_id FK
  }
  
  bt_anagram_type {
    int id PK
    int max_attempts
    string name
    int time_allowed
  }
  
  app_user_anagram {
    int id PK
    int attempts
    date_time date_played
    int max_attempts
    int time_allowed
    int bt_anagram_id FK
    int user_id FK
  }
  
  face_off {
    int id PK
    string name
    date_time date_created
  }
  
  app_user {
    int id PK
    time_stamp date_created
    string email_address
    string first_name
    string last_name
  }
  
  face_off_user_anagram {
    int face_off_id FK
    int bt_anagram_id FK
    int user_id FK
  }
  
  

```
-->
## Entity Relationship Diargram v3

```mermaid
erDiagram

  games ||--|{ game_users : ""
  games ||--|{ game_anagrams : ""
  game_users }o--|| users : ""
  game_users ||--|{ game_user_game_anagrams : ""
  game_anagrams ||--|{ game_user_game_anagrams : ""
  game_anagrams ||--|| words : ""
  game_anagrams ||--|{ game_anagram_types : ""
  
  game_anagram_types {
    int id PK
    string name
    int max_attempts
    int time_allowed
  }
  
  games {
    int id PK
    string name
    date_time date_created
  }
  
  words {
    int id PK
    string name
    string description
    string image_location
  }
  
  game_anagrams {
    int id PK
    string anagram_solution
    string anagram_word
    date_time date_created
    int max_attempts
    int order
    int timer
    int game_id FK
    int word_id FK
    int game_anagram_type_id FK
  }
  
  users {
    int id PK
    time_stamp date_created
    string email_address
    string first_name
    string last_name
  }
  
  game_users {
    int id PK
    int game_id FK
    int user_id FK
  }
  
  game_user_game_anagrams {
    int id PK
    int attempts
    date_time date_played
    bool solved
    int game_anagram_id FK
    int game_user_id FK
  }
  
  

```

<!--
## API Specifications v1

### Users

`GET /users` 
###### Returns a list of users

Response: `200 OK`
```json
[
  {
    "id": 1,
    "date_created": "2023-01-11 10:27:21.240752",
    "email_address": "sean.currie@unosquare.com",
    "first_name": "Sean",
    "last_name": "Currie",
    "username": "seancurrie"
  },
  {
    "id": 2,
    "date_created": "2023-01-11 10:28:21.240752",
    "email_address": "sean.currie+david@unosquare.com",
    "first_name": "David",
    "last_name": "Currie",
    "username": "davidcurrie"
  }
]
```

---

`GET /users?user-name={name}` 
###### Returns a list of users based on search word

Response: `200 OK`
```json
[
  {
    "id": 1,
    "date_created": "2023-01-11 10:27:21.240752",
    "email_address": "sean.currie@unosquare.com",
    "first_name": "Sean",
    "last_name": "Currie"
    "username": "seancurrie"
  },
  {
    "id": 2,
    "date_created": "2023-01-11 10:28:21.240752",
    "email_address": "sean.currie+david@unosquare.com",
    "first_name": "David",
    "last_name": "Currie"
    "username": "davidcurrie"
  }
]
```

---

`GET /users/{id}` 
###### Returns a user

Response: `200 OK`
```json
{
    "id": 1,
    "date_created": "2023-01-11 10:27:21.240752",
    "email_address": "sean.currie@unosquare.com",
    "first_name": "Sean",
    "last_name": "Currie"
  }
```

---

`POST /users`
###### Creates a user

Request:
```json
  {
    "email_address": "sean.currie+john@unosquare.com",
    "first_name": "John",
    "last_name": "Currie",
    "password": "password"
  }
```

Response: `201 Created`

---

`PUT /users/{id}`
###### Updates user by id

Request:
```json
  {
    "email_address": "sean.currie+david@unosquare.com",
    "first_name": "David Mark",
    "last_name": "Currie Powder"
  }
```

Response: `200 OK`

---

`DELETE /users/{id}`
###### Deletes a user by id

Response: `200 OK`

---
---

### Anagrams

`GET /anagrams`
###### Return a list of anagrams

Response: `200 OK`
```json
[
  {
    "id": 1,
    "anagram_word": "NANABA",
    "anagram_length": 6,
    "anagram_solution": "BANANA"
    "date_created": "2023-01-11 10:30:21.240752",
    "image_location": "http://tropicalfruitandveg.com/images/bananauk2.jpg"
  },
  {
    "id": 2,
    "anagram_word": "NSPSAOI RIUTF",
    "anagram_length": 12,
    "anagram_solution": "PASSION FRUIT"
    "date_created": "2023-01-11 10:31:21.240752",
    "image_location": "http://tropicalfruitandveg.com/images/passionyel.jpg"
  }
]
```

---

`GET /anagrams/{id}`
###### Return an anagram

Response - `200 OK`
```json
{
    "id": 1,
    "anagram_word": "NANABA",
    "anagram_length": 6,
    "anagram_solution": "BANANA"
    "date_created": "2023-01-11 10:30:21.240752",
    "image_location": "http://tropicalfruitandveg.com/images/bananauk2.jpg"
  }
```

---

`POST /anagrams`
###### Create an anagram

Request:
```json
  {
    "anagram_word": "EHYLEC",
    "anagram_length": 6,
    "anagram_solution": "LYCHEE"
    "date_created": "2023-01-11 10:32:21.240752",
    "image_location": "http://tropicalfruitandveg.com/images/lychee2.jpg"
  }
```

Response: `201 Created`

---

`PUT /anagrams/{id}`
###### Update anagram by id

Request:
```json
  {
    "anagram_word": "CHEELY",
    "anagram_length": 6,
    "anagram_solution": "LYCHEE"
    "date_created": "2023-01-11 10:32:21.240752",
    "image_location": "http://tropicalfruitandveg.com/images/lychee2.jpg"
  }
```

Response: `200 OK`

---

`DELETE /anagrams/{id}`
###### Delete an anagram by id

Response: `200 OK`

---
---

### User Anagrams

`GET /users/{id}/user-anagrams`
###### Returns a list of anagrams attempted by a user

Response: `200 OK`
```json
[
  {
    "id": 1,
    "attempts": "3",
    "date_played": "2023-01-11 10:35:21.240752",
    "max_attempts": 5,
    "solved": true,
    "time_allowed": 0
  },
  {
    "id": 2,
    "attempts": "3",
    "date_played": "2023-01-11 10:36:21.240752",
    "max_attempts": 3,
    "solved": false,
    "time_allowed": 30
  }
]
```

---

`GET /users/{id}/user-anagrams/{id}`
###### Returns an anagram attempted by a user

Response: `200 OK`
```json
{
    "id": 1,
    "attempts": "3",
    "date_played": "2023-01-11 10:35:21.240752",
    "max_attempts": 5,
    "solved": true,
    "time_allowed": 0
  }
```

---

`POST /user/{id}/user-anagrams`
###### Creates a record of a user's attempt at an anagram

Request:
```json
  {
    "attempts": "3",
    "max_attempts": 5,
    "solved": true,
    "time_allowed": 0
  }
```
Response: `201 Created`

---
---

### Face Offs

`GET /users/{id}/face-offs/{id}`
###### Gets a face off by an id

Response: `200 OK`
```json
  {
    "id": 1,
    "title": "Sean Vs David",
    "user_anagrams":
      [
        {
          "user_id": "1",
          "anagram_id": "1"
        },
        {
          "user_id": "2",
          "anagram_id": "1"
        },
        {
          "user_id": "1",
          "anagram_id": "2"
        },
        {
          "user_id": "2",
          "anagram_id": "2"
        },
        {
          "user_id": "1",
          "anagram_id": "3"
        },
        {
          "user_id": "2",
          "anagram_id": "3"
        }
      ]
  }
```
---

`POST /face-offs`
###### Creates a competitive face off challenge

Request:
```json
  {
    "title": "Sean Vs David"
    "user_ids":
    [
      {
        "user_id": 1
      },
      {
        "user_id": 2
      }
    ]
  }
```

Response: `201 Created`

---
---
-->

## API Specifications v2

### Users

`GET /users` 
###### Returns a list of users

Response: `200 OK`
```json
[
  {
    "id": 1,
    "date_created": "2023-01-11 10:27:21.240752",
    "email_address": "sean.currie@unosquare.com",
    "first_name": "Sean",
    "last_name": "Currie",
    "username": "seancurrie"
  },
  {
    "id": 2,
    "date_created": "2023-01-11 10:28:21.240752",
    "email_address": "sean.currie+david@unosquare.com",
    "first_name": "David",
    "last_name": "Currie",
    "username": "davidcurrie"
  }
]
```

---

`GET /users?user-name={name}` 
###### Returns a list of users based on search word

Response: `200 OK`
```json
[
  {
    "id": 1,
    "date_created": "2023-01-11 10:27:21.240752",
    "email_address": "sean.currie@unosquare.com",
    "first_name": "Sean",
    "last_name": "Currie"
    "username": "seancurrie"
  },
  {
    "id": 2,
    "date_created": "2023-01-11 10:28:21.240752",
    "email_address": "sean.currie+david@unosquare.com",
    "first_name": "David",
    "last_name": "Currie"
    "username": "davidcurrie"
  }
]
```

---

`GET /users/{id}` 
###### Returns a user

Response: `200 OK`
```json
{
    "id": 1,
    "date_created": "2023-01-11 10:27:21.240752",
    "email_address": "sean.currie@unosquare.com",
    "first_name": "Sean",
    "last_name": "Currie"
  }
```

---

`POST /users`
###### Creates a user

Request:
```json
  {
    "email_address": "sean.currie+john@unosquare.com",
    "first_name": "John",
    "last_name": "Currie",
    "password": "password"
  }
```

Response: `201 Created`

---

`PUT /users/{id}`
###### Updates user by id

Request:
```json
  {
    "email_address": "sean.currie+david@unosquare.com",
    "first_name": "David Mark",
    "last_name": "Currie Powder"
  }
```

Response: `200 OK`

---

`DELETE /users/{id}`
###### Deletes a user by id

Response: `200 OK`

---
---

### Games

`GET /users/{id}/games`
###### Gets a list of all games for a user

Response: `200 OK`
```json
  {
    [
      {
        "game_id": 1,
        "title": "Sean Vs David",
        "date_created": "2023-01-11 10:32:21.240752",
        "game_type": "Face Off",
        "game_users":
          [
            {
              "game_user_id": 1
            },
            {
              "game_user_id": 2
            }
          ]
        "games_anagrams":
          [
            {
              "game_anagram_id": "1",
              "anagram_word": "ANANAB",
              "anagram_solution": "BANANA",
              "max_attempts": 3,
              "order": 1,
              "timer": 30
            },
            {
              "game_anagram_id": "1",
              "anagram_word": "ANANAB",
              "anagram_solution": "BANANA",
              "max_attempts": 3,
              "order": 1,
              "timer": 30
            },
            {
              "game_anagram_id": "1",
              "anagram_word": "ANANAB",
              "anagram_solution": "BANANA",
              "max_attempts": 3,
              "order": 1,
              "timer": 30
            }
          ]
      },
      {
        "game_id": 2,
        "title": "Daily Anagram",
        "date_created": "2023-01-11 10:32:21.240752",
        "game_type": "Daily",
        "game_users":
          [
            {
              "game_user_id": 1
            }
          ]
        "games_anagrams":
          [
            {
              "game_anagram_id": "1",
              "anagram_word": "ANANAB",
              "anagram_solution": "BANANA",
              "max_attempts": 3,
              "order": 1,
              "timer": 30
            }
          ]
      }
    ]
  }
```

---

`GET /users/{id}/games/{id}`
###### Gets a competitive face off game by an id

Response: `200 OK`
```json
  {
    "id": 1,
    "title": "Sean Vs David",
    "date_created": "2023-01-11 10:32:21.240752",
    "game_type": "Face Off",
    "game_users":
      [
        {
          "game_user_id": "1",
          "first_name": "Sean"
        },
        {
          "game_user_id": "2"
          "first_name": "David"
        }
      ],
    "game_anagrams":
      [
        {
          "game_anagram_id": "1",
          "anagram_word": "ANANAB",
          "anagram_solution": "BANANA",
          "max_attempts": 3,
          "order": 1,
          "timer": 30
        },
        {
          "game_anagram_id": "2",
          "anagram_word": "ANANAB",
          "anagram_solution": "BANANA",
          "max_attempts": 3,
          "order": 1,
          "timer": 30
        },
        {
          "game_anagram_id": "3",
          "anagram_word": "ANANAB",
          "anagram_solution": "BANANA",
          "max_attempts": 3,
          "order": 1,
          "timer": 30
        }
      ]
  }
```

---

`POST /users/{id}/games`
###### Creates a competitive game with another user

Request:
```json
  {
    "title": "Sean Vs David"
    "user_ids":
    [
      {
        "user_id": 1
      },
      {
        "user_id": 2
      }
    ]
  }
```

Response: `201 Created`

---

`POST /user/{id}/games/{id}/game-anagrams/{id}/game-user-game-anagrams`
###### Creates a record of a user's attempt at an anagram

Request:
```json
  {
    "attempts": "3",
    "solved": true
  }
```
Response: `201 Created`

---
---
