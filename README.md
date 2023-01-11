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

## Entity Relationship Diargram v-1

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

## Entity Relationship Diargram v-2

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
    int battle_id FK
    int bt_anagram_id FK
    int user_id FK
  }
  
  

```

## API Specifications

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
    "last_name": "Currie"
  },
  {
    "id": 2,
    "date_created": "2023-01-11 10:28:21.240752",
    "email_address": "sean.currie+david@unosquare.com",
    "first_name": "David",
    "last_name": "Currie"
  }
]
```

---

`GET /users/{user_id}` 
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

`PUT /users/{user_id}`
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

`DELETE /users/{user_id}`
###### Deletes a user by id

Response: `200 OK`

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

`GET /anagrams/{anagram_id}`
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

`PUT /anagrams/{anagram_id}`
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

`DELETE /anagrams/{anagram_id}`
###### Delete an anagram by id

Response: `200 OK`

---

### User Anagrams

`GET /users/{user_id}/anagrams`
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

`GET /users/{user_id}/anagrams/{anagram_id}`
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

`POST /user/{user_id}/anagrams`
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

### Face Off

`GET /face-offs/{face_off_id}`
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
  }
```

Response: `201 Created`

---
---
